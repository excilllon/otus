using BtreeIndexProject.Abstractions;
using BtreeIndexProject.Model;
using BtreeIndexProject.Services.Results;

namespace BtreeIndexProject.Services.QueryExecution
{
	internal class InvalidStatementExecutor: IQueryExecutor
	{
		private readonly string[] _query;

		public InvalidStatementExecutor(string[] query)
		{
			_query = query;
		}
		public async Task<QueryResult> Execute(QueryModelInput model)
		{
			return new InvalidSyntaxResult();
		}
	}
}
