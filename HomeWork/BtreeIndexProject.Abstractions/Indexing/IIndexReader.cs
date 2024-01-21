namespace BtreeIndexProject.Abstractions.Indexing;

public interface IIndexReader
{
	/// <summary>
	/// Поиск смещения строки по ключу в файле индекса
	/// </summary>
	/// <param name="indexName"></param>
	/// <param name="value"></param>
	/// <returns></returns>
	/// <exception cref="Exception"></exception>
	Task<long?> ReadIndex(string indexName, int value);
}