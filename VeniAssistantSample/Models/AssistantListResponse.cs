using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VeniAssistantSample.Models;

internal record AssistantListResponse
{
    [JsonPropertyName("object")]
    public required string ObjectName { get; set; } = "assistant";
    [JsonPropertyName("data")]
    public required List<AIAssistant> Data { get; set; } 
    [JsonPropertyName("first_id")]
    public required string FirstID { get; set; }
    [JsonPropertyName("last_id")]
    public required string LastID { get; set; }
    [JsonPropertyName("has_more")]
    public required bool HasMore { get; set; }
}
