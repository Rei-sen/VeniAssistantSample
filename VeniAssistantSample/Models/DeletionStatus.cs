using System.Text.Json;
using System.Text.Json.Serialization;

namespace VeniAssistantSample.Models;

internal record DeletionStatus
{
    [JsonPropertyName("id")]
    public required string ID { get; set; }
    [JsonPropertyName("object")]
    public required string ObjectName { get; set; }
    [JsonPropertyName("deleted")]
    public required bool Deleted { get; set; }
}
