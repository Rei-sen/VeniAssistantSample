using System.Text.Json.Serialization;

public record ErrorDetails
{
    [JsonPropertyName("message")]
    public required string Message { get; set; }
    [JsonPropertyName("type")]
    public required string Type { get; set; }
    [JsonPropertyName("param")]
    public required string Param { get; set; }
    [JsonPropertyName("code")]
    public required string Code { get; set;  }
}

public record ErrorOjbect
{
    [JsonPropertyName("error")]
    public required ErrorDetails ErrorDetails { get; set; }
}