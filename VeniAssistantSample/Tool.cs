using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VeniAssistantSample;

public class Tool
{
    [JsonPropertyName("type")]
    public string Type { get; set; }
}
