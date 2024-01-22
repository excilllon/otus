using System.Text.Json.Serialization;

namespace BtreeIndexProject.Model.ViewModels
{
    public class QueryModelInput
    {
        [JsonPropertyName("query")]
        public string Query { get; set; }
    }
}
