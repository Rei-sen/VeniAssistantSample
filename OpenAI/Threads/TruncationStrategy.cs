using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAI.Threads;

public class TruncationStrategy
{
    [JsonPropertyName("type")]
    public required string Type { get; set; }
    [JsonPropertyName("last_messages")]
    public int? LastMessages { get; set; }
}
