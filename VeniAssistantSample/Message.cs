
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace VeniAssistantSample;


public record Message
{
    public record MessageContent
    {
        public record TextContent
        {
            [JsonPropertyName("value")]
            public required string Value { get; set; }
            [JsonPropertyName("annotations")]
            public required List<object> Annotations { get; set; }
        }
        [JsonPropertyName("type")]
        public required string Type { get; set; }
        [JsonPropertyName("text")]
        public required TextContent Text { get; set; }
    }

    [JsonPropertyName("id")]
    public required string Id { get; set; }
    [JsonPropertyName("object")]
    public required string Object { get; set; }
    [JsonPropertyName("created_at")]
    public required ulong CreatedAt { get; set; }
    [JsonPropertyName("assistant_id")]
    public required string AssistantId { get; set; }
    [JsonPropertyName("thread_id")]
    public required string ThreadId { get; set; }
    [JsonPropertyName("run_id")]
    public required string? RunId { get; set; }

    [JsonPropertyName("role")]
    public required string Role { get; set; }
    [JsonPropertyName("content")]
    public required List<MessageContent> Content { get; set; }
    [JsonPropertyName("attachments")]
    public required List<object> Attachments { get; set; }
    [JsonPropertyName("metadata")]
    public required object Metadata { get; set; }

}
