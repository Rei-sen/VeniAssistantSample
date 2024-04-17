using System.Text.Json.Serialization;

namespace VeniAssistantSample.Models;

internal record AssistantCreateRequest
{
    [JsonPropertyName("model")]
    public string Model { get; set; } = ""; // required
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    [JsonPropertyName("instructions")]
    public string? Instructions { get; set; }
    [JsonPropertyName("temperature")]
    public double? Temperature { get; set; }
    [JsonPropertyName("top_p")]
    public double? TopP { get; set; }
    [JsonPropertyName("response_format")]
    public string? ResponseFormat { get; set; }
}

