using System.Text.Json.Serialization;

namespace OpenAI.Threads;

public class RequiredAction
{
    [JsonPropertyName("type")]
    public required string Type { get; set; }
    [JsonPropertyName("submit_tool_outputs")]
    public required SubmitToolOutputs SubmitToolOutputs { get; set; }
}