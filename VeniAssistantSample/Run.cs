using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VeniAssistantSample.Models;

public class Run
{
    public class Tool
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class IncompleteDetailsInfo
    {
        [JsonPropertyName("reason")]
        public string Reason { get; set; }
    }

    public class UsageInfo
    {
        [JsonPropertyName("prompt_tokens")]
        public int PromptTokens { get; set; }

        [JsonPropertyName("completion_tokens")]
        public int CompletionTokens { get; set; }

        [JsonPropertyName("total_tokens")]
        public int TotalTokens { get; set; }
    }

    public class TruncationStrategyObject
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("last_messages")]
        public long? LastMessages { get; set; }
    }

    [JsonPropertyName("id")]
    public required string ID { get; set; }

    [JsonPropertyName("object")]
    public required string Object { get; set; }

    [JsonPropertyName("created_at")]
    public required ulong CreatedAt { get; set; }

    [JsonPropertyName("assistant_id")]
    public required string AssistantID { get; set; }

    [JsonPropertyName("thread_id")]
    public required string ThreadID { get; set; }

    [JsonPropertyName("status")]
    public required string Status { get; set; }

    [JsonPropertyName("started_at")]
    public ulong? StartedAt { get; set; }

    [JsonPropertyName("expires_at")]
    public ulong? ExpiresAt { get; set; }

    [JsonPropertyName("cancelled_at")]
    public ulong? CancelledAt { get; set; }

    [JsonPropertyName("failed_at")]
    public ulong? FailedAt { get; set; }

    [JsonPropertyName("completed_at")]
    public ulong? CompletedAt { get; set; }

    [JsonPropertyName("last_error")]
    public required string LastError { get; set; }

    [JsonPropertyName("model")]
    public required string Model { get; set; }

    [JsonPropertyName("instructions")]
    public required string Instructions { get; set; }

    [JsonPropertyName("tools")]
    public required List<Tool> Tools { get; set; }

    [JsonPropertyName("metadata")]
    public required Dictionary<string, object> Metadata { get; set; }

    [JsonPropertyName("incomplete_details")]
    public required IncompleteDetailsInfo? IncompleteDetails { get; set; }

    [JsonPropertyName("usage")]
    public UsageInfo? Usage { get; set; }

    [JsonPropertyName("temperature")]
    public required double? Temperature { get; set; }

    [JsonPropertyName("top_p")]
    public required double? TopP { get; set; }

    [JsonPropertyName("max_prompt_tokens")]
    public required uint? MaxPromptTokens { get; set; }

    [JsonPropertyName("max_completion_tokens")]
    public required uint? MaxCompletionTokens { get; set; }

    [JsonPropertyName("truncation_strategy")]
    public required TruncationStrategyObject TruncationStrategy { get; set; }

    [JsonPropertyName("response_format")]
    public required string ResponseFormat { get; set; }

    [JsonPropertyName("tool_choice")]
    public required string ToolChoice { get; set; }
}


