using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VeniAssistantSample.Models;

internal class RunCreateRequest
{
    [JsonPropertyName("assistant_id")]
    public required string AssistantID { get; set; }
    [JsonPropertyName("model")]
    public string? Model { get; set; }
    [JsonPropertyName("instructions")]
    public string? Instructions { get; set; }
    [JsonPropertyName("additional_instructions")]
    public string? AdditionalInstructions { get; set; }
    [JsonPropertyName("temperature")]
    public double? Temperature { get; set; }
    [JsonPropertyName("top_p")]
    public double? TopP { get; set; }
    [JsonPropertyName("max_prompt_tokens")]
    public int? MaxPromptTokens { get; set; }
    [JsonPropertyName("max_completion_tokens")]
    public int? MaxCompletionTokens { get; set; }
    [JsonPropertyName("truncation_strategy")]
    public string? TruncationStrategy { get; set; }
    [JsonPropertyName("tool_choice")]
    public string? ToolChoice { get; set; }
    [JsonPropertyName("response_format")]
    public string? ResponseFormat { get; set; }
}
