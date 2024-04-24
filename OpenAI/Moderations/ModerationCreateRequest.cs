using System.Text.Json.Serialization;

namespace OpenAI.Moderations;

public record ModerationCreateRequest
{
    [JsonPropertyName("input")]
    public required string Inputs { get; init; }
    [JsonPropertyName("model")]
    public string? Model { get; init; }
}