using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VeniAssistantSample.Models;

internal class MessageListResponse
{
    [JsonPropertyName("object")]
    public required string ObjectName { get; set; } = "list";
    [JsonPropertyName("data")]
    public required List<Message> Data { get; set; }
    [JsonPropertyName("first_id")]
    public required string FirstID { get; set; }
    [JsonPropertyName("last_id")]
    public required string LastID { get; set; }
    [JsonPropertyName("has_more")]
    public required bool HasMore { get; set; }
}
