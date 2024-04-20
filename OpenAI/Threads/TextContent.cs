using System.Text.Json.Serialization;

namespace OpenAI.Threads;

public class TextContent
{
    [JsonPropertyName("value")]
    public required string Value { get; set; }
    // annotations
}