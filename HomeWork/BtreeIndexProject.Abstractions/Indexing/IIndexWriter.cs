namespace BtreeIndexProject.Abstractions.Indexing
{
	/// <summary>
	/// Создание и обновление индекса
	/// </summary>
    public interface IIndexWriter
	{
		/// <summary>
		/// Создание нового индекса по указанной колонке
		/// </summary>
		/// <param name="tableName">Названеи таблицы</param>
		/// <param name="indexName">Название для нового индекса</param>
		/// <param name="column">Колонк, по которой создается индекс</param>
		/// <returns></returns>
		Task CreateIndex(string tableName, string indexName, string column);
		/// <summary>
		/// Добавление значения в индекс
		/// </summary>
		/// <param name="indexName">Название обновляемого индекса</param>
		/// <param name="value">Новое значение</param>
		/// <param name="offset">Адрес строки в файле таблицы</param>
		/// <returns></returns>
		Task UpdateIndex(string indexName, int value, long offset);
	}
}
