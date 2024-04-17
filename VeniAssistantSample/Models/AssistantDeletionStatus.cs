using System.Text.Json;
using System.Text.Json.Serialization;

namespace VeniAssistantSample.Models;

internal record AssistantDeletionStatus
{
    [JsonPropertyName("id")]
    public required string ID { get; set; }
    [JsonPropertyName("object")]
    public required string ObjectName { get; set; }
    [JsonPropertyName("deleted")]
    public required bool Deleted { get; set; }

    public static async Task<AssistantDeletionStatus?> FromResponse(HttpResponseMessage response)
    {
        if (response is null)
            return null;

        using var responseContent = await response.Content.ReadAsStreamAsync();
        if (responseContent is null)
            return null;

        var result = await JsonSerializer.DeserializeAsync<AssistantDeletionStatus>(responseContent);

        return result;
    }
}
