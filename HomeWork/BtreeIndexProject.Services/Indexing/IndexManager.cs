using System.Text;
using BtreeIndexProject.Abstractions.Indexing;
using BtreeIndexProject.Abstractions.MetaData;
using BtreeIndexProject.Model.MetaData;
using BtreeIndexProject.Services.BTreeFile;
using Microsoft.Extensions.Configuration;

namespace BtreeIndexProject.Services.Indexing
{
    public class IndexManager : IIndexWriter, IIndexReader
	{
		private const int RowSize = 513;
		private readonly IMetaDataManager _metaDataManager;
		private readonly string? _dbPath;

		public IndexManager(IConfiguration configuration, IMetaDataManager metaDataManager)
		{
			_metaDataManager = metaDataManager;
			_dbPath = configuration["DatabasePath"];
		}

		public async Task CreateIndex(string tableName, string indexName, string columnName)
		{
			var columnForIndex = _metaDataManager.GetColumnByName(tableName, columnName );
			if (columnForIndex == null) throw new Exception($"Колонка {columnName} не найдена в таблице {tableName}");
			if (!Directory.Exists(_dbPath)) throw new Exception($"Путь {_dbPath} не найден");
			if (_metaDataManager.GetIndexByColumnName(tableName, columnName)
			    .Any(idx => idx.Name == indexName.ToUpperInvariant())) throw new Exception("Индекс с таким названием уже существует");

			var fileName = Path.Combine(_dbPath, tableName + ".csv");

			if (!File.Exists(fileName)) throw new Exception($"Файл БД {tableName} не найден");

			await using var streamReader = new FileStream(fileName, FileMode.Open);
			if (columnForIndex.Type == ColumnType.Integer)
			{
				await CreateIntegerIndex(streamReader, columnForIndex.Order, indexName);
			}
			else throw new Exception("Индекс для данного поля не поддерживается");

			await _metaDataManager.AddIndexToMetaData(tableName, new TableIndex()
			{
				ColumnName = columnName,
				Name = indexName
			});
		}

		private async Task CreateIntegerIndex(FileStream streamReader, int columnIndex, string indexName)
		{
			var indexFileName = Path.Combine(_dbPath, indexName);
			using var btree = new BtreeFile(indexFileName);
			await btree.InitTree();
			var byteBuffer = new byte[RowSize];
			long streamOffset = 0;
			int i = 0;
			while (await streamReader.ReadAsync(byteBuffer, 0, RowSize) > 0)
			{
				var row = Encoding.UTF8.GetString(byteBuffer);
				if (row == null) continue;
				var cells = row.Split(';');
				await btree.Insert(int.Parse(cells[columnIndex]), streamOffset);
				streamOffset = streamReader.Position;
			}
		}

		
		public async Task<long?> ReadIndex(string indexName, int value)
		{
			using var btree = await OpenIndex(indexName);
			var rowOffset = await btree.Search(value);
			return rowOffset;
		}

		private async Task<BtreeFile> OpenIndex(string indexName)
		{
			if (!Directory.Exists(_dbPath)) throw new Exception($"Путь {_dbPath} не найден");
			var indexFileName = Path.Combine(_dbPath, indexName);
			if (!File.Exists(indexFileName)) throw new Exception($"Файл индекса {indexName} не найден");

			var btree = new BtreeFile(indexFileName);
			await btree.InitTree();
			return btree;
		}

		public async Task UpdateIndex(string indexName, int value, long offset)
		{
			using var btree = await OpenIndex(indexName);
			await btree.Insert(value, offset);
		}
	}
}
