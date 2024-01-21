using System.Text.Json;
using BtreeIndexProject.Abstractions;
using BtreeIndexProject.Model.MetaData;
using Lesson13;
using Microsoft.Extensions.Configuration;

namespace BtreeIndexProject.Services
{
	public class MetaDataManager : IMetaDataManager
	{
		private const string MetaFileExtension = ".meta";
		HashMap<string, TableMetaData> _tableMetaData = new();
	    private string _dbFolder;
	    public MetaDataManager(IConfiguration configuration)
	    {
		    _dbFolder = configuration["DatabasePath"];
	    }

	    public async Task ReadMetaData()
	    {
			if(!Directory.Exists(_dbFolder)) return;

			foreach (var dbFile in Directory.EnumerateFiles(_dbFolder).Where(f=>Path.GetExtension(f) == ".csv"))
			{
				using var streamReader = new StreamReader(dbFile);
				var columns = (await streamReader.ReadLineAsync()).Split(';');
				
				var tableName =  Path.GetFileName(dbFile).Replace(".csv", null).ToUpperInvariant();
				var tableMetaFileName = GetTableMetaFileName(tableName);

				await using var streamMetaReader = new FileStream(tableMetaFileName, FileMode.Open);
				_tableMetaData[tableName] = await JsonSerializer.DeserializeAsync<TableMetaData>(streamMetaReader);
				
			}
	    }

	    private string GetTableMetaFileName(string tableName)
	    {
		    return Path.Combine(_dbFolder, tableName + MetaFileExtension);
	    }

	    public async Task AddIndexToMetaData(string tableName, TableIndex newIndex)
	    {
		    tableName = tableName.ToUpperInvariant();
		    if (!_tableMetaData.ContainsKey(tableName)) throw new Exception($"Таблица {_tableMetaData} не найдена");

		    _tableMetaData[tableName].Indicies.Add(newIndex);
		    await PersistTableMetaData(tableName);
	    }

	    private async Task PersistTableMetaData(string tableName)
	    {
		    await using var streamWriter = new FileStream(GetTableMetaFileName(tableName), FileMode.Create);
		    await JsonSerializer.SerializeAsync(streamWriter, _tableMetaData[tableName]);
		    await streamWriter.FlushAsync();
	    }

		/// <summary>
		/// Список колонок по таблице
		/// </summary>
		/// <param name="tableName"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
	    public List<ColumnMetaData> GetTableColumns(string tableName)
	    {
		    if (!_tableMetaData.ContainsKey(tableName.ToUpperInvariant()))
			    throw new Exception($"Таблица {tableName} не найдена");

			return _tableMetaData[tableName.ToUpperInvariant()].Columns;
	    }

		public ColumnMetaData? GetColumnByName(string tableName, string columnName)
		{
			if (!_tableMetaData.ContainsKey(tableName.ToUpperInvariant()))
				throw new Exception($"Таблица {tableName} не найдена");

			var column = _tableMetaData[tableName.ToUpperInvariant()].Columns.FirstOrDefault(x => x.Name == columnName);

			return column;
		}
	}
	

	internal class TableMetaData
    {
		public string Name { get; set; }
		public List<ColumnMetaData> Columns { get; set; } = new();
		public List<TableIndex> Indicies { get; set; } = new();
    }
}
