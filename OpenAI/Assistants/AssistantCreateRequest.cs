using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAI.Assistants;

public class AssistantCreateRequest
{
    [JsonPropertyName("model")]
    public string? Model { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    [JsonPropertyName("instructions")]
    public string? Instructions { get; set; }
    //[JsonPropertyName("tools")]
    //public List<FunctionTool> Tools { get; set; } = new();
    // tool resources
    [JsonPropertyName("metadata")]
    public Dictionary<string, string>? Metadata { get; set; }
    [JsonPropertyName("temperature")]
    public double? Temperature { get; set; }
    [JsonPropertyName("top_p")]
    public double? TopP { get; set; }
    [JsonPropertyName("response_format")]
    public string? ResponseFormat { get; set; }
}
