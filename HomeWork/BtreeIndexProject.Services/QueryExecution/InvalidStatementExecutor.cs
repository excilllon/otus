using BtreeIndexProject.Abstractions.QueryExecution;
using BtreeIndexProject.Model.QueryExecution;
using BtreeIndexProject.Model.ViewModels;
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
