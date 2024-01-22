using BtreeIndexProject.Model.MetaData;

namespace BtreeIndexProject.Abstractions.MetaData;

/// <summary>
/// Работа с метаданными таблиц
/// </summary>
public interface IMetaDataManager
{
    /// <summary>
    /// Чтение структуры таблиц при запуске приложения
    /// </summary>
    /// <returns></returns>
    Task ReadMetaData();
    /// <summary>
    /// Возвращает список колонок таблицы по названию таблицы
    /// </summary>
    /// <param name="tableName">Название таблицы</param>
    /// <returns></returns>
    List<ColumnMetaData> GetTableColumns(string tableName);
    /// <summary>
    /// Возвращает метаднные колонки по ее названию
    /// </summary>
    /// <param name="tableName">Название таблицы</param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    ColumnMetaData? GetColumnByName(string tableName, string columnName);
    Task AddIndexToMetaData(string tableName, TableIndex newIndex);
    /// <summary>
    /// Возвращает индексы по колонке
    /// </summary>
    /// <param name="tableName">Название таблицы</param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    List<TableIndex> GetIndexByColumnName(string tableName, string columnName);
    /// <summary>
    /// Возвращает названеи файла таблицы
    /// </summary>
    /// <param name="tableName">Название таблицы</param>
    /// <returns></returns>
    string GetTableFileName(string tableName);
    /// <summary>
    /// Возвращает индексы таблицы
    /// </summary>
    /// <param name="tableName">Название таблицы</param>
    /// <returns></returns>
    List<TableIndex> GetTableAllTableIndices(string tableName);
    /// <summary>
    /// Проверяет существует ли таблица
    /// </summary>
    /// <param name="tableName">Название таблицы</param>
    /// <returns></returns>
    bool IsTableExists(string tableName);
}