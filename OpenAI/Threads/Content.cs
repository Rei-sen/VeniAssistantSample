using System.Net.Mime;
using System.Text.Json.Serialization;

namespace OpenAI.Threads;

public class Content
{
    [JsonPropertyName("type")]
    public required ContentType Type { get; set; }


    [JsonPropertyName("text")]
    public TextContent? Text { get; set; }
}