using System.Text.Json.Serialization;

namespace VeniAssistantSample.Function;

public class Property
{
    [JsonPropertyName("type")]
    public required string Type { get; set; }
    [JsonPropertyName("description")]
    public required string Description { get; set; }
    [JsonPropertyName("enum")]
    public List<string>? Enum { get; set; }
}
