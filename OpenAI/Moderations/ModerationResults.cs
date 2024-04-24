using System.Text.Json.Serialization;

namespace OpenAI.Moderations;

public class ModerationResults
{
    [JsonPropertyName("flagged")]
    public bool Flagged { get; set; }
    [JsonPropertyName("categories")]
    public ModerationCategories Categories { get; set; }
    [JsonPropertyName("category_scores")]
    public ModerationScores CategoryScores { get; set; }
}