using BtreeIndexProject.Model.MetaData;
using System.Xml.Linq;

namespace BtreeIndexProject.Abstractions;

public interface IMetaDataManager
{
	/// <summary>
	/// Чтение структуры таблиц при запуске приложения
	/// </summary>
	/// <returns></returns>
	Task ReadMetaData();
	List<ColumnMetaData> GetTableColumns(string tableName);
	/// <summary>
	/// Возвращает метаднные колонки по ее названию
	/// </summary>
	/// <param name="tableName"></param>
	/// <param name="columnName"></param>
	/// <returns></returns>
	ColumnMetaData? GetColumnByName(string tableName, string columnName);
	Task AddIndexToMetaData(string tableName, TableIndex newIndex);
	/// <summary>
	/// Возвращает индексы по колонке
	/// </summary>
	/// <param name="tableName"></param>
	/// <param name="columnName"></param>
	/// <returns></returns>
	List<TableIndex> GetIndexByColumnName(string tableName, string columnName);

	string GetTableFileName(string tableName);

	List<TableIndex> GetTableAllTableIndices(string tableName);
}