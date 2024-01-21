namespace BtreeIndexProject.Abstractions;

public class QueryResult
{
	public string? ErrorMessage { get; set; }
	public string? UserMessage { get; set; }

	public Dictionary<string, object> TableRows { get; set; } = new Dictionary<string, object>();
}