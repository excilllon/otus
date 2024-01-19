using BtreeIndexProject.Abstractions;
using BtreeIndexProject.Model;

namespace BtreeIndexProject.Services.QueryExecution
{
	public class QueryExecutor: IQueryExecutor
	{
		public async Task<QueryResult> Execute(QueryModelInput model)
		{
			if (string.IsNullOrEmpty(model.Query)) return new QueryResult();
			var queryStatement = model.Query.ToUpperInvariant().Split(' ').Select(q => q.Trim()).ToArray();
			if(queryStatement.Length == 0) return new QueryResult();

			//if(queryStatement[0] == "")

			return new QueryResult();
		}

		//private 
	}
}
