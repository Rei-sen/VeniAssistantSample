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
    public required string ObjectName { get; set; }
    [JsonPropertyName("data")]
    public required List<AssistantObject> Data { get; set; } 
    [JsonPropertyName("first_id")]
    public required string FirstID { get; set; }
    [JsonPropertyName("last_id")]
    public required string LastID { get; set; }
    [JsonPropertyName("has_more")]
    public required bool HasMore { get; set; }

    public static async Task<AssistantListResponse?> FromResponse(HttpResponseMessage response)
    {
        if (response is null)
            return null;
         var responseContent = await response.Content.ReadAsStringAsync();
        if (responseContent is null)
            return null;

        var result = JsonSerializer.Deserialize<AssistantListResponse>(responseContent);

        return result;
    }

}
