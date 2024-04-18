namespace VeniAssistantSample.Function;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Parameter
{
    [JsonPropertyName("type")] 
    public string Type { get; set; } = "object";
    [JsonPropertyName("properties")]
    public Dictionary<string, Property> Properties { get; set; }
    public List<string> Required { get; set; } = new();
}
