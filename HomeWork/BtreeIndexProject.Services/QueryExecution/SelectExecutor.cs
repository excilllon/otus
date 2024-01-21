using System.Text.RegularExpressions;
using BtreeIndexProject.Abstractions;
using BtreeIndexProject.Model;
using BtreeIndexProject.Services.Results;

namespace BtreeIndexProject.Services.QueryExecution
{
	internal class SelectExecutor: IQueryExecutor
	{
		private readonly string[] _query;

		public SelectExecutor(string[] query)
		{
			_query = query;
		}
		public async Task<QueryResult> Execute(QueryModelInput model)
		{
			if (_query[0] != "SELECT") return new InvalidSyntaxResult();
			var selectMatch = Regex.Match(model.Query, "(SELECT\\s*)(\\S[\\s\\,].*)\\s*(FROM)\\s*(\\S*)\\s*(\\S*)?\\s*(.*)");
			if (!selectMatch.Success) return new InvalidSyntaxResult();
			if(selectMatch.Groups.Count < 4) return new InvalidSyntaxResult();
			
			var columns = selectMatch.Groups[2];
			var from = selectMatch.Groups[3];
			var tableName = selectMatch.Groups[4];
			var whereKeyWord = selectMatch.Groups[5];
			var whereExpression = selectMatch.Groups[6];
			return new InvalidSyntaxResult();
		}
	}
}
