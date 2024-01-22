namespace BtreeIndexProject.Services.BTreeFile
{
	/// <summary>
	/// Реализация B-Tree дерева для хранения индекса в виде файла
	/// </summary>
	internal class BtreeFile : IDisposable
	{
		/// <summary>
		/// корневой узел
		/// </summary>
		private BTreeFileNode? root;
		/// <summary>
		/// Файл для хранения дереве
		/// </summary>
		private readonly string _fileName;
		
		private FileStream _fileStream;
		/// <summary>
		/// Размер блока для хранения одного узла
		/// </summary>
		private readonly int _blockSize = 4096;
		/// <summary>
		/// Минимальная степень дерева
		/// </summary>
		private readonly int _minDegree = 128;
		/// <summary>
		/// В первых 4 байтах файла хранится номер блока с корневым узлом
		/// </summary>
		private const int HeaderOffset = 4;
		/// <summary>
		/// 
		/// </summary>
		private int _currentBlockNumber;

		public BtreeFile(string fileName)
		{
			_fileName = fileName;
		}

		/// <summary>
		/// Инициализирует дерево. Открывает файл если он есть и читает номер блока с корнем и десериализует сам корень
		/// Если файл не создн, то создает его
		/// </summary>
		/// <returns></returns>
		public async Task InitTree()
		{
			_fileStream = new FileStream(_fileName, FileMode.OpenOrCreate);
			_fileStream.Seek(0, SeekOrigin.Begin);
			// Первые 4 байта - номер блока корневого узла
			var buffer = new byte[4];
			var bytesRead = await _fileStream.ReadAsync(buffer, 0, 4);
			// Файл индекса не пустой, читаем корневой узел
			if (bytesRead == 4)
			{
				var rootBlock = BitConverter.ToInt32(buffer, 0);
				root = await ReadNodeFromBlock(rootBlock);
				_currentBlockNumber = (int)(_fileStream.Length / _blockSize);
			}
		}

		/// <summary>
		/// Поиск по ключу
		/// </summary>
		/// <param name="searchKey"></param>
		/// <param name="node"></param>
		/// <returns></returns>
		private async Task<long?> Search(int searchKey, BTreeFileNode node)
		{
			// Ищем перый ключ, который не меньше searchKey
			int i = 0;
			while (i < node.CurrentKeysCount && searchKey.CompareTo(node.Keys[i].key) > 0)
				i++;

			if (node.Keys[i].key.Equals(searchKey)) return node.Keys[i].offset;

			// Если достигли листов и не нашли ключ, значит такого ключе нет в дереве
			if (node.IsLeaf) return null;

			// Ищем в дочерних узлах
			return await Search(searchKey, await ReadNodeFromBlock(node.Childs[i]));
		}

		/// <summary>
		/// Поиск смещения строки по ключу
		/// </summary>
		/// <param name="k">Значение ключа</param>
		/// <returns>Смещение строки, где этот ключ есть</returns>
		public async Task<long?> Search(int k)
		{
			return await Search(k, root);
		}

		/// <summary>
		/// Вставка ключа (индексирумого значения) и офсета записи с этим значениям в файле таблицы
		/// </summary>
		/// <param name="key">Ключ</param>
		/// <param name="offset">Сдвиг</param>
		/// <returns></returns>
		public async Task Insert(int key, long offset)
		{
			if (root == null)
			{
				root = new BTreeFileNode(_minDegree, true)
				{
					Keys =
					{
						[0] = (key, offset)
					},
					CurrentKeysCount = 1,
					BlockNumber = 0
				};
				await UpdateRootBlockNumber(0);
				await WriteNode(root);
			}
			else
			{
				// Если корень заполен, создаем новый
				if (root.CurrentKeysCount == 2 * _minDegree - 1)
				{
					var s = new BTreeFileNode(_minDegree, false)
					{
						Childs =
						{
							// Старый корень становится дочерним узлом нового
							[0] = root.BlockNumber
						}
					};

					// Разделяем старый корень и переносим 1 ключ в новый корень
					await SplitChildOfParent(0, s, root);

					// Добавляем новый ключ в одному из двух дочерних узлов
					int i = 0;
					if (s.Keys[0].key.CompareTo(key) < 0)
						i++;
					await InsertIntoFreeNode(key, offset, await ReadNodeFromBlock(s.Childs[i]));

					root = s;
					await UpdateRootBlockNumber(s.BlockNumber);
				}
				else await InsertIntoFreeNode(key, offset, root);
			}
		}

		/// <summary>
		/// ДОбавление ключа в еще незаполненную ноду
		/// </summary>
		/// <param name="insertKey"></param>
		/// <param name="offset"></param>
		/// <param name="node"></param>
		/// <returns></returns>
		public async Task InsertIntoFreeNode(int insertKey, long offset, BTreeFileNode node)
		{
			// Берем последний занятый индекс в массиве
			int i = node.CurrentKeysCount - 1;

			if (node.IsLeaf)
			{
				// Ищем куда вставить новый ключ и сдвигаем все ключи с большим значением вперед
				while (i >= 0 && node.Keys[i].key.CompareTo(insertKey) > 0)
				{
					node.Keys[i + 1] = node.Keys[i];
					i--;
				}
				node.Keys[i + 1].key = insertKey;
				node.Keys[i + 1].offset = offset;
				node.CurrentKeysCount += 1;
				await WriteNode(node);
			}
			else
			{
				// Ищем дочерний узел (номер его блока) куда нужно добавить ключ
				while (i >= 0 && node.Keys[i].key.CompareTo(insertKey) > 0)
					i--;

				var nodeToInsertKey = await ReadNodeFromBlock(node.Childs[i + 1]);
				var isChildIsFull = nodeToInsertKey.CurrentKeysCount == 2 * _minDegree - 1;
				if (isChildIsFull)
				{
					// Если потомок заполнен, разделим его на два узла и привяжем полученный узел к родителю
					await SplitChildOfParent(i + 1, node, nodeToInsertKey);
					
					if (node.Keys[i + 1].key.CompareTo(insertKey) < 0)
						i++;
				}
				await InsertIntoFreeNode(insertKey, offset, await ReadNodeFromBlock(node.Childs[i + 1]));
			}
		}

		/// <summary>
		/// Разделение дочернего узла указанного родителя
		/// </summary>
		/// <param name="i"></param>
		/// <param name="parentNode">Родительский узел</param>
		/// <param name="childNodeToSplit">Дочерний узел родительского parentNode для разделения</param>
		/// <returns></returns>
		public async Task SplitChildOfParent(int i, BTreeFileNode parentNode, BTreeFileNode childNodeToSplit)
		{
			// Создаем новый узел, куда скопируем часть ключей childNodeToSplit
			var newSplitFromChild = new BTreeFileNode(_minDegree, childNodeToSplit.IsLeaf)
			{
				CurrentKeysCount = _minDegree - 1
			};

			// Копируем последние (t-1) ключей childNodeToSplit в новый узел newSplitFromChild
			for (int j = 0; j < _minDegree - 1; j++)
				newSplitFromChild.Keys[j] = childNodeToSplit.Keys[j + _minDegree];

			// Копируем последние t потомков из childNodeToSplit в newSplitFromChild
			if (childNodeToSplit.IsLeaf == false)
			{
				for (int j = 0; j < _minDegree; j++)
					newSplitFromChild.Childs[j] = childNodeToSplit.Childs[j + _minDegree];
			}

			await WriteNode(newSplitFromChild);

			childNodeToSplit.CurrentKeysCount = _minDegree - 1;
			await WriteNode(childNodeToSplit);

			// Сдвигаем место для нового дочернего узла (newSplitFromChild)
			for (int j = parentNode.CurrentKeysCount; j >= i + 1; j--)
				parentNode.Childs[j + 1] = parentNode.Childs[j];

			parentNode.Childs[i + 1] = newSplitFromChild.BlockNumber;

			// Новый ключ будет вставлен в parentNode, ищем для него место и сдвигаем большие ключи
			for (int j = parentNode.CurrentKeysCount - 1; j >= i; j--)
				parentNode.Keys[j + 1] = parentNode.Keys[j];

			// Копируем ключ из середины childNodeToSplit
			parentNode.Keys[i] = childNodeToSplit.Keys[_minDegree - 1];
			parentNode.CurrentKeysCount++;

			await WriteNode(parentNode);
		}
		/// <summary>
		/// Запись узла в блок файла
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		private async Task WriteNode(BTreeFileNode node)
		{
			if (node.BlockNumber == -1)
			{
				_fileStream.Seek(0, SeekOrigin.End);
				node.BlockNumber = ++_currentBlockNumber;
			}
			else
			{
				var blockPosition = node.BlockNumber * _blockSize + HeaderOffset;
				_fileStream.Seek(blockPosition, SeekOrigin.Begin);
			}
			var blockStartOffset = _fileStream.Position;
			// CurrentKeysCount
			await _fileStream.WriteAsync(BitConverter.GetBytes(node.CurrentKeysCount));
			// Признак листа
			await _fileStream.WriteAsync(BitConverter.GetBytes(node.IsLeaf));
			foreach (var childBlock in node.Childs)
			{
				await _fileStream.WriteAsync(BitConverter.GetBytes(childBlock));
			}
			foreach (var keyBlock in node.Keys)
			{
				await _fileStream.WriteAsync(BitConverter.GetBytes(keyBlock.key));
				await _fileStream.WriteAsync(BitConverter.GetBytes(keyBlock.offset));
			}
			// В оставшееся место блока записываем нули
			var blockFreeSpace = (blockStartOffset + _blockSize) - _fileStream.Position;
			var emptyBytes = new byte[blockFreeSpace];
			await _fileStream.WriteAsync(emptyBytes);
		}

		/// <summary>
		/// Обновление номера блока с корнем
		/// </summary>
		/// <param name="rootBlockNumber"></param>
		/// <returns></returns>
		private async Task UpdateRootBlockNumber(int rootBlockNumber)
		{
			_fileStream.Seek(0, SeekOrigin.Begin);
			await _fileStream.WriteAsync(BitConverter.GetBytes(rootBlockNumber));
		}

		/// <summary>
		/// Чтение узла из указанного блока
		/// </summary>
		/// <param name="blockNumber">Номер блока</param>
		/// <returns></returns>
		private async Task<BTreeFileNode> ReadNodeFromBlock(int blockNumber)
		{
			long blockPosition = blockNumber * _blockSize + HeaderOffset;
			_fileStream.Seek(blockPosition, SeekOrigin.Begin);
			byte[] buffer = new byte[_blockSize];
			await _fileStream.ReadAsync(buffer, 0, _blockSize);
			int byteIndex = 0;
			var keyCount = BitConverter.ToInt32(buffer, byteIndex);
			byteIndex += sizeof(int);
			var isLeaf = BitConverter.ToBoolean(buffer, byteIndex);
			byteIndex += sizeof(bool);

			var childs = new int[2 * _minDegree];
			int j = 0;
			var childsOffset = byteIndex;
			var childsBlockSize = (2 * _minDegree) * sizeof(int);
			// читаем номера дочерних блоков
			for (; byteIndex < childsBlockSize + childsOffset; byteIndex += sizeof(int))
			{
				childs[j++] = BitConverter.ToInt32(buffer, byteIndex);
			}
			// Читам ключ-смещение
			var keys = new (int key, long offset)[2 * _minDegree - 1];
			j = 0;
			var keysOffset = byteIndex;
			var keysBlockSize = (2 * _minDegree - 1) * (sizeof(int) + sizeof(long));
			for (; byteIndex < keysOffset + keysBlockSize;)
			{
				keys[j].key = BitConverter.ToInt32(buffer, byteIndex);
				byteIndex += sizeof(int);
				keys[j++].offset = BitConverter.ToInt64(buffer, byteIndex);
				byteIndex += sizeof(long);
			}

			var res = new BTreeFileNode(_minDegree, isLeaf)
			{
				BlockNumber = blockNumber,
				Childs = childs,
				Keys = keys,
				CurrentKeysCount = keyCount
			};
			return res;
		}

		public void Dispose()
		{
			_fileStream.Dispose();
		}
	}
}
