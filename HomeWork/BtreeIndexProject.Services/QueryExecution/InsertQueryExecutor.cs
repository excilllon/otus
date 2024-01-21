using BtreeIndexProject.Abstractions;
using BtreeIndexProject.Model;

namespace BtreeIndexProject.Services.QueryExecution
{
	internal class InsertQueryExecutor: IQueryExecutor
	{
		private readonly string[] _query;

		public InsertQueryExecutor(string[] query)
		{
			_query = query;
		}
		public Task<QueryResult> Execute(QueryModelInput model)
		{
			throw new NotImplementedException();
		}
	}
}
