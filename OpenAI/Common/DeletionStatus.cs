using System.Text.Json.Serialization;

namespace OpenAI.Common;

public class DeletionStatus
{
    [JsonPropertyName("id")]
    public required string ID { get; set; }
    [JsonPropertyName("object")]
    public required string ObjectName { get; set; }
    [JsonPropertyName("deleted")]
    public required bool Deleted { get; set; }
}
