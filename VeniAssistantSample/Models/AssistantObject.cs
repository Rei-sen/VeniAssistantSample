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
    public required string ID { get; set; }
    [JsonPropertyName("object")]
    public required string ObjectName { get; set; } = "assistant";
    [JsonPropertyName("created_at")]
    public required ulong CreatedAt { get; set; }
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("description")]
    public required string Description { get; set; }
    [JsonPropertyName("model")]
    public required string Model { get; set; }
    [JsonPropertyName("instructions")]
    public required string Instructions { get; set; }
    [JsonPropertyName("temperature")]
    public required double Temperature { get; set; }
    [JsonPropertyName("top_p")]
    public required double TopP { get; set; }
    [JsonPropertyName("response_format")]
    public required string ResponseFormat { get; set; }

    public static async Task<AssistantObject?> FromResponse(HttpResponseMessage response)
    {
        if (response is null)
            return null;

        using var responseContent = await response.Content.ReadAsStreamAsync();
        if (responseContent is null)
            return null;

        var result = await JsonSerializer.DeserializeAsync<AssistantObject>(responseContent);
        if (result is null)
            return null;

        return result;
    }
}
