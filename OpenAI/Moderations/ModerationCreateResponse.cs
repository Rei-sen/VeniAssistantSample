using System.Text.Json.Serialization;

namespace OpenAI.Moderations;

public record ModerationCreateResponse
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }
    [JsonPropertyName("model")]
    public required string Model { get; set; }
    [JsonPropertyName("results")]
    public required IReadOnlyList<ModerationResults> Results { get; set; }
}