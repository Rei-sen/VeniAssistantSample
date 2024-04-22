using System.Text.Json.Serialization;

namespace OpenAI.Threads;

public class SubmitToolOutputs
{
    [JsonPropertyName("tool_calls")]
    public List<ToolCall> ToolCalls { get; set; } = new();
}