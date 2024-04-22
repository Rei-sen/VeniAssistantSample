using System.Text.Json.Serialization;

namespace OpenAI.Threads;

public class FunctionCall
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("arguments")]
    public string Arguments { get; set; }
}