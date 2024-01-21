namespace BtreeIndexProject.Abstractions.Indexing
{
    public interface IIndexWriter
	{
		Task CreateIndex(string tableName, string indexName, string column);
	}
}
