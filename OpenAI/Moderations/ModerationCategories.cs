using System.Text.Json.Serialization;

namespace OpenAI.Moderations;

public class ModerationCategories
{
    [JsonPropertyName("sexual")]
    public required bool Sexual { get; set; }

    [JsonPropertyName("hate")]
    public required bool Hate { get; set; }

    [JsonPropertyName("harassment")]
    public required bool Harassment { get; set; }

    [JsonPropertyName("self-harm")]
    public required bool SelfHarm { get; set; }

    [JsonPropertyName("sexual/minors")]
    public required bool SexualMinors { get; set; }

    [JsonPropertyName("hate/threatening")]
    public required bool HateThreatening { get; set; }

    [JsonPropertyName("violence/graphic")]
    public required bool ViolenceGraphic { get; set; }

    [JsonPropertyName("self-harm/intent")]
    public required bool SelfHarmIntent { get; set; }

    [JsonPropertyName("self-harm/instructions")]
    public required bool SelfHarmInstructions { get; set; }

    [JsonPropertyName("harassment/threatening")]
    public required bool HarassmentThreatening { get; set; }

    [JsonPropertyName("violence")]
    public required bool Violence { get; set; }
}
