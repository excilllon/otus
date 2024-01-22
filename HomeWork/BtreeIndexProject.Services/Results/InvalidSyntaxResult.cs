using BtreeIndexProject.Model.QueryExecution;

namespace BtreeIndexProject.Services.Results;

public class InvalidSyntaxResult : QueryResult
{
	public InvalidSyntaxResult(string message)
	{
		ErrorMessage = "Ошибка: " + message;
	}
    public InvalidSyntaxResult()
    {
        ErrorMessage = "Ошибка: Некорректный синтаксис";
    }
}