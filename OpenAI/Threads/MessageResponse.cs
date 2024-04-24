using System.Text.Json.Serialization;

namespace OpenAI.Threads;

public class MessageResponse
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }
    [JsonPropertyName("object")]
    public required string ObjectName { get; set; }
    [JsonPropertyName("created_at")]
    public required ulong CreatedAt { get; set; }
    [JsonPropertyName("thread_id")]
    public required string ThreadId { get; set; }
    [JsonPropertyName("status")]
    public string? Status { get; set; }
    // incomplete details
    [JsonPropertyName("completed_at")]
    public long? CompletedAtUnix { get; set; }
    [JsonIgnore]
    public DateTime? CompletedAt => CompletedAtUnix.HasValue ? DateTimeOffset.FromUnixTimeSeconds(CompletedAtUnix.Value).DateTime : null;
    [JsonPropertyName("incomplete_at")]
    public long? IncompleteAtUnix { get; set; }
    [JsonIgnore]
    public DateTime? IncompleteAt => IncompleteAtUnix.HasValue ? DateTimeOffset.FromUnixTimeSeconds(IncompleteAtUnix.Value).DateTime : null;
    [JsonPropertyName("role")]
    public string? Role { get; set; }
    [JsonPropertyName("content")]
    public required List<Content> Content { get; set; }
    [JsonPropertyName("assistant_id")]
    public string? AssistantId { get; set; }
    [JsonPropertyName("run_id")]
    public string? RunId { get; set; }
    [JsonPropertyName("attachments")]
    public List<Attachment>? Attachments { get; set; }
    [JsonPropertyName("metadata")]
    public Dictionary<string, string> Metadata { get; set; }
}