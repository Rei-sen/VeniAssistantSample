using System.Text.Json.Serialization;
using OpenAI.Common;

namespace OpenAI.Threads;

public class ToolCall
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }
    [JsonPropertyName("type")]
    [JsonConverter(typeof(JsonEnumConverter<ToolType>))]
    public required ToolType Type { get; set; }
    [JsonPropertyName("function")]
    public required FunctionCall Function { get; set; }

}