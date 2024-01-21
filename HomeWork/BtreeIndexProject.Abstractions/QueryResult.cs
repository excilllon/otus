namespace BtreeIndexProject.Abstractions;

public class QueryResult
{
	public string? ErrorMessage { get; set; }
	public string? UserMessage { get; set; }
	public Dictionary<string, string>[] TableRows { get; set; } = Array.Empty<Dictionary<string, string>>();
	public string[] Columns { get; set; } = new string[0];
	public string ExecutionTime { get; set; }

}