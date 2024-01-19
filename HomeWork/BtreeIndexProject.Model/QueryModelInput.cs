using System.Text.Json.Serialization;

namespace BtreeIndexProject.Model
{
	public class QueryModelInput
	{
		[JsonPropertyName("query")]
		public string Query { get; set; }
	}
}
