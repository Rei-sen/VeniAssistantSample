﻿using System.Text.Json.Serialization;

namespace OpenAI.Threads;

public class MessageCreateRequest
{
    [JsonPropertyName("role")]
    public required string Role { get; set; }
    [JsonPropertyName("content")]
    public required string Content { get; set; }
    //attachments
    [JsonPropertyName("metadata")]
    public Dictionary<string, string>? Metadata { get; set; }
}