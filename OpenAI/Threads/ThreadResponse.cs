using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAI.Threads;

public class ThreadResponse
{
    [JsonPropertyName("id")]
    public required string ID { get; set; }
    [JsonPropertyName("object")]
    public required string ObjectName { get; set; }
    [JsonPropertyName("created_at")]
    public required ulong CreatedAt { get; set; }
    // tool_resources

    [JsonPropertyName("metadata")]
    public Dictionary<string, string> Metadata { get; set; }
}
