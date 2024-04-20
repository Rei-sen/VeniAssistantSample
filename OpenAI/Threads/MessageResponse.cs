using System.Text.Json.Serialization;

namespace OpenAI.Threads;

public class MessageResponse
{
    [JsonPropertyName("id")]
    public required string ID { get; set; }
    [JsonPropertyName("object")]
    public required string ObjectName { get; set; }
    [JsonPropertyName("created_at")]
    public required ulong CreatedAt { get; set; }
    [JsonPropertyName("assistant_id")]
    public required string ThreadID { get; set; }
    [JsonPropertyName("status")]
    public required string Status { get; set; }
    // incomplete details
    [JsonPropertyName("completed_at")]
    public  ulong? CompletedAt { get; set; }
    [JsonPropertyName("incomplete_at")]
    public  ulong? IncompleteAt { get; set; }
    [JsonPropertyName("role")]
    public  string? Role { get; set; }
    [JsonPropertyName("content")]
    public required List<Content> Content { get; set; }
    [JsonPropertyName("assistant_id")]
    public string? AssistantID { get; set; }
    [JsonPropertyName("attachments")]
    public List<Attachment>? Attachments { get; set; }
    [JsonPropertyName("metadata")]
    public Dictionary<string, string> Metadata { get; set; }
}