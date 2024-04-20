using System.Text.Json.Serialization;
using OpenAI.Functions;

namespace OpenAI.Common;

public class Tool
{
    [JsonPropertyName("type")]
    public required ToolType Type { get; set; }
    [JsonPropertyName("function")]
    public Function? Function { get; set; }
}