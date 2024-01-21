using BtreeIndexProject.Abstractions;

namespace BtreeIndexProject.Services.Results;

public class InvalidSyntaxResult : QueryResult
{
    public InvalidSyntaxResult()
    {
        ErrorMessage = "Некорректный синтаксис";
    }
}