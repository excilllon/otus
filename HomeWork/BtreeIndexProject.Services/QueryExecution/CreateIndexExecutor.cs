using BtreeIndexProject.Abstractions;
using BtreeIndexProject.Model;
using System.Text.RegularExpressions;
using BtreeIndexProject.Services.Results;
using BtreeIndexProject.Abstractions.Indexing;

namespace BtreeIndexProject.Services.QueryExecution
{
	internal class CreateIndexExecutor : IQueryExecutor
	{
		private readonly IIndexWriter _indexWriter;

		public CreateIndexExecutor(IIndexWriter indexWriter)
		{
			_indexWriter = indexWriter;
		}
		public async Task<QueryResult> Execute(QueryModelInput model)
		{
			var createIndexMatch = Regex.Match(model.Query, "(CREATE\\s*INDEX)\\s*(\\S*)\\s*(ON)\\s*(\\S*)\\s*\\(\\s*(\\S*)\\s*\\)\\s*");

			if (!createIndexMatch.Success) return new InvalidSyntaxResult();
			if (createIndexMatch.Groups.Count != 6) return new InvalidSyntaxResult();
			var from = createIndexMatch.Groups[1];
			var indexName = createIndexMatch.Groups[2].Value;
			var tableName = createIndexMatch.Groups[4].Value;
			var columnName = createIndexMatch.Groups[5].Value;

			try
			{
				await _indexWriter.CreateIndex(tableName, indexName, columnName);
				return new QueryResult()
				{
					UserMessage = "Индекс создан"
				};
			}
			catch (Exception ex)
			{
				return new IndexCreateFailResult(ex.Message);
			}
		}
	}
}
