namespace VeniAssistantSample.Function;

using System.Text.Json.Serialization;

public class FunctionTool
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = "function";
    [JsonPropertyName("function")]
    public FunctionData FunctionData { get; set; }
}