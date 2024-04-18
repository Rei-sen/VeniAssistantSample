using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VeniAssistantSample;

public class ToolOutput
{
    [JsonPropertyName("tool_call_id")]
    public string? ToolCallId { get; set; }
    [JsonPropertyName("output")]
    public string? Output { get; set; }
}
