using System.Text.Json.Serialization;

namespace OpenAI.Threads;

public class RunModifyRequest
{
    [JsonPropertyName("metadata")]
    public Dictionary<string, string>? Metadata { get; set; }
}