using System.Text.Json.Serialization;
using OpenAI.Common;

namespace OpenAI.Threads;

public class ThreadCreateAndRunRequest
{
    [JsonPropertyName("assistant_id")]
    public required string AssistantId { get; set; }
    [JsonPropertyName("thread")]
    public ThreadCreateRequest? Thread { get; set; }
    [JsonPropertyName("model")]
    public string? Model { get; set; }
    [JsonPropertyName("tools")]
    public List<Tool>? Tools { get; set; }
    [JsonPropertyName("metadata")]
    public Dictionary<string, string>? Metadata { get; set; }
    [JsonPropertyName("temperature")]
    public float? Temperature { get; set; }
    [JsonPropertyName("top_p")]
    public float? TopP { get; set; }
    [JsonPropertyName("stream")]
    public bool? Stream { get; set; }
    [JsonPropertyName("max_prompt_tokens")]
    public int? MaxPromptTokens { get; set; }
    [JsonPropertyName("max_completion_tokens")]
    public int? MaxCompletionTokens { get; set; }
    [JsonPropertyName("truncation_strategy")]
    public TruncationStrategy? TruncationStrategy { get; set; }
    // tool choice
    [JsonPropertyName("response_format")]
    public string? ResponseFormat { get; set; }
}