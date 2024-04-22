using System.Text.Json.Serialization;
using OpenAI.Functions;
using OpenAI.Threads;

namespace OpenAI.Common;

public class Tool
{
    [JsonPropertyName("type")]
    public required ToolType Type { get; set; }
    [JsonPropertyName("function")]
    public FunctionDescriptor? Function { get; set; }
}