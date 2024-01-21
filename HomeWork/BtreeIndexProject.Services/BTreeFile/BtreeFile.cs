namespace BtreeIndexProject.Services.BTreeFile
{
	internal class BtreeFile : IDisposable
	{
		private BTreeFileNode? root;
		private int _rootBlock;
		private readonly string _fileName;
		private readonly int _minDegree;
		private FileStream _fileStream;
		private readonly int _blockSize = 2048;
		private const int HeaderOffset = 4;
		private int _currentBlockNumber;

		public BtreeFile(string fileName, int minDegree)
		{
			_fileName = fileName;
			_minDegree = minDegree;
		}

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
				_rootBlock = BitConverter.ToInt32(buffer, 0);
				root = await ReadNodeFromBlock(_rootBlock);
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
			// Find the first key greater than or equal to k
			int i = 0;
			while (i < node.CurrentKeysCount && searchKey.CompareTo(node.Keys[i].key) > 0)
				i++;

			if (node.Keys[i].key.Equals(searchKey)) return node.Keys[i].offset;

			// Если достигли листов и не нашли ключ, значит его нет
			if (node.IsLeaf) return null;

			// Ищем в дочерних узлах
			return await Search(searchKey, await ReadNodeFromBlock(node.Childs[i]));
		}

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
				// If root is full, then tree grows in height
				if (root.CurrentKeysCount == 2 * _minDegree - 1)
				{
					var s = new BTreeFileNode(_minDegree, false)
					{
						Childs =
						{
							// Make old root as child of new root
							[0] = root.BlockNumber
						}
					};

					// Split the old root and move 1 key to the new root
					await SplitChild(0, s, root);

					// New root has two children now.  Decide which of the
					// two children is going to have new key
					int i = 0;
					if (s.Keys[0].key.CompareTo(key) < 0)
						i++;
					await InsertNonFull(key, offset, await ReadNodeFromBlock(s.Childs[i]));

					// Change root
					root = s;
					await UpdateRootBlockNumber(s.BlockNumber);
				}
				else await InsertNonFull(key, offset, root);
			}
		}

		// A utility function to insert a new key in this node
		// The assumption is, the node must be non-full when this
		// function is called
		public async Task InsertNonFull(int insertKey, long offset, BTreeFileNode node)
		{
			// Initialize index as index of rightmost element
			int i = node.CurrentKeysCount - 1;

			// If this is a leaf node
			if (node.IsLeaf)
			{
				// The following loop does two things
				// a) Finds the location of new key to be inserted
				// b) Moves all greater keys to one place ahead
				while (i >= 0 && node.Keys[i].key.CompareTo(insertKey) > 0)
				{
					node.Keys[i + 1] = node.Keys[i];
					i--;
				}

				// Insert the new key at found location
				node.Keys[i + 1].key = insertKey;
				node.Keys[i + 1].offset = offset;
				node.CurrentKeysCount += 1;
				await WriteNode(node);
			}
			else // If this node is not leaf
			{
				// Find the child which is going to have the new key
				while (i >= 0 && node.Keys[i].key.CompareTo(insertKey) > 0)
					i--;

				// See if the found child is full
				var nodeToInsertKey = await ReadNodeFromBlock(node.Childs[i + 1]);
				if (nodeToInsertKey.CurrentKeysCount == 2 * node.MinimumDegree - 1)
				{
					// If the child is full, then split it
					await SplitChild(i + 1, node, nodeToInsertKey);

					// After split, the middle key of C[i] goes up and
					// C[i] is splitted into two.  See which of the two
					// is going to have the new key
					if (node.Keys[i + 1].key.CompareTo(insertKey) < 0)
						i++;
				}
				await InsertNonFull(insertKey, offset, nodeToInsertKey);
			}
		}

		// A utility function to split the child y of this node
		// Note that y must be full when this function is called
		public async Task SplitChild(int i, BTreeFileNode nodeToSplit, BTreeFileNode y)
		{
			// Create a new node which is going to store (t-1) keys
			// of y
			var z = new BTreeFileNode(y.MinimumDegree, y.IsLeaf)
			{
				CurrentKeysCount = nodeToSplit.MinimumDegree - 1
			};

			// Copy the last (t-1) keys of y to z
			for (int j = 0; j < nodeToSplit.MinimumDegree - 1; j++)
				z.Keys[j] = y.Keys[j + nodeToSplit.MinimumDegree];

			// Copy the last t children of y to z
			if (y.IsLeaf == false)
			{
				for (int j = 0; j < nodeToSplit.MinimumDegree; j++)
					z.Childs[j] = y.Childs[j + nodeToSplit.MinimumDegree];
			}

			await WriteNode(z);

			// Reduce the number of keys in y
			y.CurrentKeysCount = nodeToSplit.MinimumDegree - 1;

			// Since this node is going to have a new child,
			// create space of new child
			for (int j = nodeToSplit.CurrentKeysCount; j >= i + 1; j--)
				nodeToSplit.Childs[j + 1] = nodeToSplit.Childs[j];

			// Link the new child to this node
			nodeToSplit.Childs[i + 1] = z.BlockNumber;

			// A key of y will move to this node. Find the location of
			// new key and move all greater keys one space ahead
			for (int j = nodeToSplit.CurrentKeysCount - 1; j >= i; j--)
				nodeToSplit.Keys[j + 1] = nodeToSplit.Keys[j];

			// Copy the middle key of y to this node
			nodeToSplit.Keys[i] = y.Keys[nodeToSplit.MinimumDegree - 1];

			// Increment count of keys in this node
			nodeToSplit.CurrentKeysCount++;

			await WriteNode(nodeToSplit);
		}

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
			// Минимальная степень
			await _fileStream.WriteAsync(BitConverter.GetBytes(_minDegree));
			// CurrentKeysCount
			await _fileStream.WriteAsync(BitConverter.GetBytes(node.CurrentKeysCount));
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

		private async Task UpdateRootBlockNumber(int rootBlockNumber)
		{
			_fileStream.Seek(0, SeekOrigin.Begin);
			await _fileStream.WriteAsync(BitConverter.GetBytes(rootBlockNumber));
		}

		private async Task<BTreeFileNode> ReadNodeFromBlock(int blockNumber)
		{
			long blockPosition = blockNumber * _blockSize + HeaderOffset;
			_fileStream.Seek(blockPosition, SeekOrigin.Begin);
			byte[] buffer = new byte[_blockSize];
			await _fileStream.ReadAsync(buffer, 0, _blockSize);
			int byteIndex = 0; 
			var minDegree = BitConverter.ToInt32(buffer, byteIndex);
			byteIndex += sizeof(int);
			var keyCount = BitConverter.ToInt32(buffer, byteIndex);
			byteIndex += sizeof(int);
			var isLeaf = BitConverter.ToBoolean(buffer, byteIndex);
			byteIndex += sizeof(bool);
			
			var childs = new int[2 * minDegree];
			int j = 0;
			var childsOffset = byteIndex;
			var childsBlockSize = (2 * minDegree) * sizeof(int);
			// читаем номера дочерних блоков
			for (;byteIndex < childsBlockSize + childsOffset; byteIndex += sizeof(int))
			{
				childs[j++] = BitConverter.ToInt32(buffer, byteIndex);
			}
			// Читам ключ-смещение
			var keys = new (int key, long offset)[2 * minDegree - 1];
			j = 0;
			var keysOffset = byteIndex;
			var keysBlockSize = (2 * minDegree - 1)*(sizeof(int)+sizeof(long));
			for (;byteIndex < keysOffset + keysBlockSize;)
			{
				keys[j].key = BitConverter.ToInt32(buffer, byteIndex);
				byteIndex += sizeof(int);
				keys[j++].offset = BitConverter.ToInt64(buffer, byteIndex);
				byteIndex += sizeof(long);
			}

			var res = new BTreeFileNode(minDegree, isLeaf)
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
