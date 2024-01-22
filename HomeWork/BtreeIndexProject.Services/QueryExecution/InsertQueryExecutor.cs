using System.Text;
using BtreeIndexProject.Abstractions.Indexing;
using BtreeIndexProject.Services.Results;
using System.Text.RegularExpressions;
using BtreeIndexProject.Model.MetaData;
using BtreeIndexProject.Abstractions.MetaData;
using BtreeIndexProject.Abstractions.QueryExecution;
using BtreeIndexProject.Model.QueryExecution;
using BtreeIndexProject.Model.ViewModels;

namespace BtreeIndexProject.Services.QueryExecution
{
    /// <summary>
    /// Запрос на вставку данных
    /// </summary>
    internal class InsertQueryExecutor: IQueryExecutor
	{
		private readonly IMetaDataManager _metaDataManager;
		private readonly IIndexWriter _indexWriter;
		private readonly IIndexReader _indexReader;
		private string _tableName;
		public InsertQueryExecutor(IMetaDataManager metaDataManager, IIndexWriter indexWriter, IIndexReader indexReader)
		{
			_metaDataManager = metaDataManager;
			_indexWriter = indexWriter;
			_indexReader = indexReader;
		}
		public async Task<QueryResult> Execute(QueryModelInput model)
		{
			var query = model.Query?.ToUpperInvariant();
			var match = Regex.Match(query, "(INSERT\\s*INTO)\\s*([a-zA-Z]*)\\s*(VALUES)\\s*\\(\\s*(.*)\\s*\\)");
			if (!match.Success) return new InvalidSyntaxResult();
			if (match.Groups.Count < 5) return new InvalidSyntaxResult();

			_tableName = match.Groups[2].Value;
			var valuesStr = match.Groups[4].Value;

			try
			{
				if (!_metaDataManager.IsTableExists(_tableName)) return new InvalidSyntaxResult("Таблица не существует");
				var tableColumns = _metaDataManager.GetTableColumns(_tableName);
				var values = valuesStr.Split(',');
				if (values.Length != tableColumns.Count)
				{
					return new InvalidSyntaxResult("Количество значений не совпадает с количеством колонок в таблице");
				}
				var valuesInColumns = new (string val, ColumnMetaData column)[values.Length];
				foreach (var column in tableColumns)
				{
					valuesInColumns[column.Order] = (val: values[column.Order], column);
				}

				await WriteNewRow(valuesInColumns);

				return new QueryResult(){UserMessage = "Вставка строки выполнена"};
			}
			catch (Exception ex)
			{
				return new InvalidSyntaxResult(ex.Message);
			}
		}

		/// <summary>
		/// Запись строки в файл
		/// </summary>
		/// <param name="values"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		private async Task WriteNewRow((string val, ColumnMetaData column)[] values)
		{
			var idVal = values.FirstOrDefault(v => v.column.IsPrimaryKey);
			var pkValue = int.Parse(idVal.val);
			var existedOffset = await _indexReader.ReadIndex("PK_INDEX", pkValue);
			if (existedOffset != null)
			{
				throw new Exception($"Данный ключ {idVal.column.Name}={idVal.val} уже существует в таблице {_tableName}");
			}
			await using var fileStream = new FileStream(_metaDataManager.GetTableFileName(_tableName), FileMode.Open);
			fileStream.Seek(0, SeekOrigin.End);
			var newRow = string.Join(';', values.Select(v => v.val.Trim())).PadRight(511);
			newRow += Environment.NewLine;
			var buffer = Encoding.UTF8.GetBytes(newRow);
			var offset = fileStream.Position;
			await fileStream.WriteAsync(buffer, 0, buffer.Length);
			await _indexWriter.UpdateIndex("PK_INDEX", pkValue, offset);
		}
	}
}
