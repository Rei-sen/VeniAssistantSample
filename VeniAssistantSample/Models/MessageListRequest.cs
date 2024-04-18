using System.Text.Json.Serialization;

namespace VeniAssistantSample.Models;

internal class MessageListRequest
{
    [JsonPropertyName("limit")]
    public uint? Limit { get; set; }
    [JsonPropertyName("order")]
    public string? Order { get; set; }
    [JsonPropertyName("after")]
    public string? After { get; set; }
    [JsonPropertyName("before")]
    public string? Before { get; set; }
    [JsonPropertyName("run_id")]
    public string? RunID { get; set; }
}
