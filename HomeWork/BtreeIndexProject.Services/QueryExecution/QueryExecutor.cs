using System.Diagnostics;
using BtreeIndexProject.Abstractions.Indexing;
using BtreeIndexProject.Abstractions.MetaData;
using BtreeIndexProject.Abstractions.QueryExecution;
using BtreeIndexProject.Model.QueryExecution;
using BtreeIndexProject.Model.ViewModels;

namespace BtreeIndexProject.Services.QueryExecution
{
    public class QueryExecutor : IQueryExecutor
	{
		private readonly IMetaDataManager _metaDataManager;
		private readonly IIndexWriter _indexWriter;
		private readonly IIndexReader _indexReader;

		public QueryExecutor(IMetaDataManager metaDataManager, IIndexWriter indexWriter, IIndexReader indexReader)
		{
			_metaDataManager = metaDataManager;
			_indexWriter = indexWriter;
			_indexReader = indexReader;
		}
		public async Task<QueryResult> Execute(QueryModelInput model)
		{
			if (string.IsNullOrEmpty(model.Query)) return new QueryResult();
			var queryStatement = model.Query.ToUpperInvariant().Split(' ').Select(q => q.Trim()).ToArray();
			if (queryStatement.Length == 0) return new QueryResult();
			Stopwatch stopwatch = Stopwatch.StartNew();
			var result = await GetConcreteExecutor(queryStatement).Execute(model);
			stopwatch.Stop();
			result.ExecutionTime = stopwatch.ElapsedMilliseconds.ToString();
			return result;
		}

		private IQueryExecutor GetConcreteExecutor(string[] queryParts)
		{
			var statement = queryParts[0];
			return statement switch
			{
				"SELECT" => new SelectExecutor(_metaDataManager, _indexReader),
				"INSERT" => new InsertQueryExecutor(_metaDataManager, _indexWriter, _indexReader),
				"CREATE" => new CreateIndexExecutor(_indexWriter),
				_ => new InvalidStatementExecutor(queryParts)
			};
		}
	}
}
