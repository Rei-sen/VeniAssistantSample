using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VeniAssistantSample.Models;

public record AssistantObject
{
    [JsonPropertyName("id")]
    public string? ID { get; set; }
    [JsonPropertyName("object")]
    public string ObjectName { get; set; } = "assistant";
    [JsonPropertyName("created_at")]
    public ulong? CreatedAt { get; set; }
    [JsonPropertyName("name")]
    public  string Name { get; set; }
    [JsonPropertyName("description")]
    public  string Description { get; set; }
    [JsonPropertyName("model")]
    public  string Model { get; set; }
    [JsonPropertyName("instructions")]
    public  string Instructions { get; set; }
    [JsonPropertyName("temperature")]
    public  double Temperature { get; set; }
    [JsonPropertyName("top_p")]
    public  double TopP { get; set; }
    [JsonPropertyName("response_format")]
    public  string ResponseFormat { get; set; }
}
