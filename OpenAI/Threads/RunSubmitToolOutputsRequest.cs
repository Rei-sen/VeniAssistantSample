using System.Text.Json.Serialization;

namespace OpenAI.Threads;

public class RunSubmitToolOutputsRequest
{
    [JsonPropertyName("tool_outputs")]
    public required List<ToolOutput> ToolOutputs { get; set; }
    [JsonPropertyName("stream")]
    public bool? Stream { get; set; }
}