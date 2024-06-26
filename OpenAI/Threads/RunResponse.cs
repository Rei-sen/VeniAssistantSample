﻿using System.Formats.Asn1;
using System.Text.Json.Serialization;
using OpenAI.Common;
using OpenAI.Error;
using OpenAI.Functions;

namespace OpenAI.Threads;

public class RunResponse
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }
    [JsonPropertyName("object")]
    public required string ObjectName { get; set; }
    [JsonPropertyName("created_at")]
    public required long CreatedAtUnix { get; set; }
    [JsonIgnore]
    public DateTime CreatedAt => DateTimeOffset.FromUnixTimeSeconds(CreatedAtUnix).DateTime;
    [JsonPropertyName("thread_id")]
    public required string ThreadId { get; set; }
    [JsonPropertyName("assistant_id")]
    public required string AssistantId { get; set; }
    [JsonConverter(typeof(JsonEnumConverter<RunStatus>))]
    [JsonPropertyName("status")]
    public required RunStatus Status { get; set; }
    [JsonPropertyName("required_action")]
    public RequiredAction? RequiredAction { get; set; }
    [JsonPropertyName("last_error")]
    public Error.Error? LastError { get; set; }
    [JsonPropertyName("expires_at")]
    public long? ExpiresAtUnix { get; set; }
    [JsonIgnore]
    public DateTime? ExpiresAt => ExpiresAtUnix.HasValue ? DateTimeOffset.FromUnixTimeSeconds(ExpiresAtUnix.Value).DateTime : null;
    [JsonPropertyName("started_at")]
    public long? StartedAtUnix { get; set; }
    [JsonIgnore]
    public DateTime? StartedAt => StartedAtUnix.HasValue ? DateTimeOffset.FromUnixTimeSeconds(StartedAtUnix.Value).DateTime : null;
    [JsonPropertyName("failed_at")]
    public long? FailedAtUnix { get; set; }
    [JsonIgnore]
    public DateTime? FailedAt => FailedAtUnix.HasValue ? DateTimeOffset.FromUnixTimeSeconds(FailedAtUnix.Value).DateTime : null;
    [JsonPropertyName("completed_at")]
    public long? CompletedAtUnix { get; set; }
    [JsonIgnore]
    public DateTime? CompletedAt => CompletedAtUnix.HasValue ? DateTimeOffset.FromUnixTimeSeconds(CompletedAtUnix.Value).DateTime : null;
    [JsonPropertyName("incomplete_details")]
    public IncompleteDetails? IncompleteDetails { get; set; }
    [JsonPropertyName("model")]
    public required string Model { get; set; }
    [JsonPropertyName("instructions")]
    public string? Instructions { get; set; }
    [JsonPropertyName("tools")]
    public required List<Tool> Tools { get; set; }
    [JsonPropertyName("metadata")]
    public Dictionary<string, string> Metadata { get; set; }
    [JsonPropertyName("temperature")]
    public float? Temperature { get; set; }
    [JsonPropertyName("top_p")]
    public float? TopP { get; set; }
    [JsonPropertyName("max_prompt_tokens")]
    public int? MaxPromptTokens { get; set; }
    [JsonPropertyName("max_completion_tokens")]
    public int? MaxCompletionTokens { get; set; }
    [JsonPropertyName("truncation_strategy")]
    public TruncationStrategy TruncationStrategy { get; set; }
    //[JsonPropertyName("tool_choice")]
    //public string ToolChoice { get; set; }
    [JsonPropertyName("response_format")]
    public string? ResponseFormat { get; set; }


    // todo: cancellationToken should be passed down
    public async Task<string> InvokeToolCallAsync(ToolCall toolCall, CancellationToken cancellationToken = default)
    {
        if (toolCall.Type != ToolType.Function)
            throw new InvalidOperationException("Cannot invoke non-function tools.");

        var result = await FunctionCallBroker.InvokeCallAsync(toolCall.Function).ConfigureAwait(false);
        return result.Serialize();
    }

    public async Task<ToolOutput> GetToolOutputAsync(ToolCall toolCall, CancellationToken cancellationToken = default)
    {
        var output = await InvokeToolCallAsync(toolCall).ConfigureAwait(false);
        return new() { ToolCallId = toolCall.Id, Output = output };
    }
    public async Task<IReadOnlyList<ToolOutput>> GetToolOutputsAsync(IEnumerable<ToolCall> toolCalls, CancellationToken cancellationToken = default) =>
         await Task.WhenAll(
            toolCalls.Select(async tc => await GetToolOutputAsync(tc, cancellationToken).ConfigureAwait(false)
            )
            ).ConfigureAwait(false);

}