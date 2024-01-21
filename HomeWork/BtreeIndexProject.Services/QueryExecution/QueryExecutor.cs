using BtreeIndexProject.Abstractions;
using BtreeIndexProject.Abstractions.Indexing;
using BtreeIndexProject.Model;

namespace BtreeIndexProject.Services.QueryExecution
{
	public class QueryExecutor: IQueryExecutor
	{
		private readonly IMetaDataManager _metaDataManager;
		private readonly IIndexWriter _indexWriter;

		public QueryExecutor(IMetaDataManager metaDataManager, IIndexWriter indexWriter)
		{
			_metaDataManager = metaDataManager;
			_indexWriter = indexWriter;
		}
		public async Task<QueryResult> Execute(QueryModelInput model)
		{
			if (string.IsNullOrEmpty(model.Query)) return new QueryResult();
			var queryStatement = model.Query.ToUpperInvariant().Split(' ').Select(q => q.Trim()).ToArray();
			if(queryStatement.Length == 0) return new QueryResult();
			return await GetConcreteExecutor(queryStatement).Execute(model);
		}

		private IQueryExecutor GetConcreteExecutor(string[] queryParts)
		{
			var statement = queryParts[0];
			return statement switch
			{
				"SELECT" => new SelectExecutor(queryParts),
				"UPDATE" => new UpdateQueryExecutor(queryParts),
				"INSERT" => new InsertQueryExecutor(queryParts),
				"CREATE" => new CreateIndexExecutor(_indexWriter),
				_ => new InvalidStatementExecutor(queryParts)
			};
		}
	}
}
