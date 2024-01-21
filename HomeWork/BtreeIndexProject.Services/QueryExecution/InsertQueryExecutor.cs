using BtreeIndexProject.Abstractions;
using BtreeIndexProject.Model;
using BtreeIndexProject.Services.Results;
using System.Text.RegularExpressions;

namespace BtreeIndexProject.Services.QueryExecution
{
	internal class InsertQueryExecutor: IQueryExecutor
	{
		private readonly string[] _query;
		private string _tableName;
		public InsertQueryExecutor(string[] query)
		{
			_query = query;
		}
		public async Task<QueryResult> Execute(QueryModelInput model)
		{
			var query = model.Query?.ToUpperInvariant();
			var selectMatch = Regex.Match(query, "(INSERT\\s*INTO)\\s*([a-zA-Z]*)\\s*(VALUES)\\s*\\(\\s*(\\S*)\\s*\\)");
			if (!selectMatch.Success) return new InvalidSyntaxResult();
			if (selectMatch.Groups.Count < 5) return new InvalidSyntaxResult();

			_tableName = selectMatch.Groups[2].Value;
			var values = selectMatch.Groups[4].Value;

			return new QueryResult();
		}
	}
}
