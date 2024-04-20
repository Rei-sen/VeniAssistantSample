using System.Text.Json.Serialization;

namespace OpenAI.Threads;

public class ThreadModifyRequest
{
    //tools
    [JsonPropertyName("metadata")]
    public Dictionary<string, string>? Metadata { get; set; }
}