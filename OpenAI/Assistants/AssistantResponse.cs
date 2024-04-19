using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAI.Assistants;

public class AssistantResponse
{
    [JsonPropertyName("id")]
    public required string ID { get; set; }
    [JsonPropertyName("object")]
    public required string ObjectName { get; set; }
    [JsonPropertyName("created_at")]
    public required ulong CreatedAt { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    [JsonPropertyName("model")]
    public required string Model { get; set; }
    [JsonPropertyName("instructions")]
    public string? Instructions { get; set; }
    //[JsonPropertyName("tools")]
    //public List<FunctionTool> Tools { get; set; } = new();
    [JsonPropertyName("temperature")]
    public double? Temperature { get; set; }
    [JsonPropertyName("top_p")]
    public double? TopP { get; set; }
    [JsonPropertyName("response_format")]
    public string? ResponseFormat { get; set; }
}
