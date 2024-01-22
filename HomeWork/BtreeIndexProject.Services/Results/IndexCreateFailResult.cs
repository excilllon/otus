using BtreeIndexProject.Model.QueryExecution;

namespace BtreeIndexProject.Services.Results
{
	public class IndexCreateFailResult: QueryResult
	{
		public IndexCreateFailResult(string message)
		{
			ErrorMessage = "Ошибка при создании индекса: " +  message;
		}
	}
}
