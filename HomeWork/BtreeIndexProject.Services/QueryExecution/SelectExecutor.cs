using System.Text;
using System.Text.RegularExpressions;
using BtreeIndexProject.Abstractions.Indexing;
using BtreeIndexProject.Abstractions.MetaData;
using BtreeIndexProject.Abstractions.QueryExecution;
using BtreeIndexProject.Model.QueryExecution;
using BtreeIndexProject.Model.ViewModels;
using BtreeIndexProject.Services.Results;

namespace BtreeIndexProject.Services.QueryExecution
{
    internal class SelectExecutor : IQueryExecutor
	{
		private const int RowSize = 513;
		private readonly IMetaDataManager _metaDataManager;
		private readonly IIndexReader _indexReader;
		private string _tableName;

		public SelectExecutor(IMetaDataManager metaDataManager, IIndexReader indexReader)
		{
			_metaDataManager = metaDataManager;
			_indexReader = indexReader;
		}

		public async Task<QueryResult> Execute(QueryModelInput model)
		{
			var query = model.Query?.ToUpperInvariant();
			var selectMatch = Regex.Match(query, "(SELECT\\s*)(\\S[\\s\\,].*)\\s*(FROM)\\s*(\\S*)\\s*(\\S*)?\\s*(.*)");
			if (!selectMatch.Success) return new InvalidSyntaxResult();
			if (selectMatch.Groups.Count < 4) return new InvalidSyntaxResult();

			var columns = selectMatch.Groups[2];
			var from = selectMatch.Groups[3];
			_tableName = selectMatch.Groups[4].Value;
			var whereKeyWord = selectMatch.Groups[5];
			var whereExpression = selectMatch.Groups[6];

			var whereMatch = Regex.Match(whereExpression.Value, "(\\S*)\\s*=\\s*(\\S*)");

			try
			{
				var tableColumns = _metaDataManager.GetTableColumns(_tableName).OrderBy(c=>c.Order);
				List<string[]> rows = new List<string[]>();
				if (whereMatch.Success && whereMatch.Groups.Count >= 3)
				{
					var columnName = whereMatch.Groups[1].Value;
					var value = whereMatch.Groups[2].Value;
					var index = _metaDataManager.GetIndexByColumnName(_tableName, columnName).FirstOrDefault();
					if (index != null)
					{
						var offset = await _indexReader.ReadIndex(index.Name, int.Parse(value));
						if (offset == null) return new QueryResult();
						rows = await GetRowsByOffsetStrategy(new[] { offset.Value });
					}
					else if (!tableColumns.Any(c => c.Name.ToUpperInvariant() == columnName.ToUpperInvariant()))
					{
						return new InvalidSyntaxResult($"Колонка {columnName} не найдена в таблице {_tableName}");
					}
					else rows = await GetRowsFullsScanStrategy(columnName, value);
				}
				else
				{
					rows = await GetAllRowsStrategy();
				}

				var result = new QueryResult();
				result.Columns = tableColumns.Select(c => c.Name).ToArray();
				result.TableRows = new Dictionary<string, string>[rows.Count];
				for (var rowIndex = 0; rowIndex < rows.Count; rowIndex++)
				{
					var row = rows[rowIndex];
					result.TableRows[rowIndex] = new Dictionary<string, string>();
					for (int i = 0; i < row.Length; i++)
					{
						result.TableRows[rowIndex][result.Columns[i]] = row[i];
					}
				}

				return result;
			}
			catch (Exception ex)
			{
				return new InvalidSyntaxResult(ex.Message);
			}
		}

		public async Task<List<string[]>> GetAllRowsStrategy()
		{
			return await GetRowsFullsScanStrategy(null, null);
		}

		public async Task<List<string[]>> GetRowsFullsScanStrategy(string columnName, string value)
		{
			var rows = new List<string[]>();
			var columnIndex = string.IsNullOrEmpty(columnName)
				? null
				: _metaDataManager.GetColumnByName(_tableName, columnName)?.Order;
			await using var tableReader = new FileStream(_metaDataManager.GetTableFileName(_tableName), FileMode.Open);
			var byteBuffer = new byte[RowSize];
			while (await tableReader.ReadAsync(byteBuffer, 0, RowSize) > 0)
			{
				var row = Encoding.UTF8.GetString(byteBuffer);
				byteBuffer = new byte[RowSize];
				if (row == null) continue;
				var cells = row.Split(';');
				if(columnIndex == null || columnIndex != null && cells[columnIndex.Value].Trim() == value) rows.Add(cells);
			}
			return rows;
		}

		public async Task<List<string[]>> GetRowsByOffsetStrategy(IEnumerable<long> offsets)
		{
			var rows = new List<string[]>();
			await using var tableReader = new FileStream(_metaDataManager.GetTableFileName(_tableName), FileMode.Open);
			foreach (var offset in offsets)
			{
				tableReader.Seek(offset, SeekOrigin.Begin);
				var byteBuffer = new byte[RowSize];
				await tableReader.ReadAsync(byteBuffer, 0, RowSize);
				var row = Encoding.UTF8.GetString(byteBuffer);
				if (row == null) continue;
				var cells = row.Split(';');
				rows.Add(cells);
			}
			return rows;
		}
	}
}
