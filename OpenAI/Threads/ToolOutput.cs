using System.Text.Json.Serialization;

namespace OpenAI.Threads;

public class ToolOutput
{
    [JsonPropertyName("tool_call_id")]
    public string? ToolCallId { get; set; }
    [JsonPropertyName("output")]
    public string? Output { get; set; }
}
