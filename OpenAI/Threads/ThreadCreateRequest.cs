using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAI.Threads;

public class ThreadCreateRequest
{
    [JsonPropertyName("messages")]
    public List<MessageCreateRequest> Messages { get; set; } = new();
    // tool resources
    [JsonPropertyName("metadata")]
    public Dictionary<string, string>? Metadata { get; set; }
}
