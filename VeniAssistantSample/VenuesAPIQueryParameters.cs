using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VeniAssistantSample;

internal class VenuesAPIQueryParameters
{
    [JsonPropertyName("name")]
    public string? Search { get; set; }
    [JsonPropertyName("Manager")]
    public string? Manager { get; set; }
    [JsonPropertyName("DataCenter")]
    public string? DataCenter { get; set; }
    [JsonPropertyName("World")]
    public string? World { get; set; }
    [JsonPropertyName("Tags")]
    public string? Tags { get; set; }
    [JsonPropertyName("HasBanner")]
    public bool? HasBanner { get; set; }
    [JsonPropertyName("Approved")]
    public bool? Approved { get; set; }
    [JsonPropertyName("Open")]
    public bool? Open { get; set; }
    [JsonPropertyName("WithinWeek")]
    public bool? WithinWeek { get; set; }
}
