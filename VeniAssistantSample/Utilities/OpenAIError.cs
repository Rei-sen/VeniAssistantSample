using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VeniAssistantSample.Utilities;

public class OpenAIError : Exception
{
    [JsonPropertyName("message")]
    public override  string Message { get; } = "";
    [JsonPropertyName("type")]
    public string? Type { get; set; }
    [JsonPropertyName("param")]
    public string? Param { get; set; }
    [JsonPropertyName("code")]
    public string? Code { get; set; }
}
