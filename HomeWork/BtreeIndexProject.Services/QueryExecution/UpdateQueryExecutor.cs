using BtreeIndexProject.Abstractions;
using BtreeIndexProject.Model;

namespace BtreeIndexProject.Services.QueryExecution
{
	internal class UpdateQueryExecutor:IQueryExecutor
	{
		public UpdateQueryExecutor(string[] query)
		{
			
		}
		public Task<QueryResult> Execute(QueryModelInput model)
		{
			throw new NotImplementedException();
		}
	}
}
