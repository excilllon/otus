using System.Text.Json;
using BtreeIndexProject.Abstractions;
using Lesson13;

namespace BtreeIndexProject.Services
{
	public class MetaDataReader : IMetaDataReader
	{
	    HashMap<string, TableMetaData> _tableMetaData = new();

	    public async Task ReadMetaData(string dbFolder)
	    {
			if(!Directory.Exists(dbFolder)) return;

			foreach (var dbFile in Directory.EnumerateFiles(dbFolder).Where(f=>Path.GetExtension(f) == ".csv"))
			{
				using var streamReader = new StreamReader(dbFile);
				var columns = (await streamReader.ReadLineAsync()).Split(';');
				
				var tableName =  Path.GetFileName(dbFile).Replace(".csv", null).ToUpperInvariant();
				_tableMetaData[tableName] = new TableMetaData()
				{
					Name = tableName,
					Columns = columns.Select(c => new ColumnMetaData()
					{
						Name = c,
						Type = ColumnType.String
					}).ToList()
				};
				var stringSeriz = JsonSerializer.Serialize(_tableMetaData[tableName]);

				//while (!streamReader.EndOfStream)
				//{
				//	var row = await streamReader.ReadLineAsync();
				//	if (row == null) continue;
				//	var cells = row.Split(';');

				//}
				
			}
	    }


    }

    internal class TableMetaData
    {
		public string Name { get; set; }
		public List<ColumnMetaData> Columns { get; set; } = new();
		public List<TableIndex> Indicies { get; set; } = new();
    }

    internal class TableIndex
    {
		public string Name { get; set; }
		public string ColumnName { get; set; }
    }

    internal class ColumnMetaData
	{
		public string Name { get; set; }
		public ColumnType Type { get; set; }
		public bool IsPrimaryKey { get; set; }
	}

    internal enum ColumnType
	{
		String,
		Integer
	}
}
