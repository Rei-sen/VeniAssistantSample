using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAI.Common;

public class ListResponse<T>
{

    [JsonPropertyName("object")]
    public required string ObjectName { get; set; }
    [JsonPropertyName("data")]
    public required List<T> Data { get; set; }
    [JsonPropertyName("first_id")]
    public string? FirstId { get; set; }
    [JsonPropertyName("last_id")]
    public string? LastId { get; set; }
    [JsonPropertyName("has_more")]
    public bool HasMore { get; set; }
}
