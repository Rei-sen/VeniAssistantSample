using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeniAssistantSample.Function;

using System.Text.Json.Serialization;

public class FunctionData
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("parameters")]
    public Parameter Parameters { get; set; }
}
