using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenAI.Threads;

public class FunctionCall
{
    [JsonPropertyName("name")] public required string Name { get; init; }
    [JsonPropertyName("arguments")] public required string Arguments { get; init; }

    public object?[] ParseArguments(ParameterInfo[] parameters)
    {
        var jsonDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(Arguments)
                      ?? throw new Exception("Failed to parse arguments");

        return parameters.Select(param =>
            jsonDict.TryGetValue(param.Name!, out var jsonElement)
                ? JsonSerializer.Deserialize(jsonElement.GetRawText(), param.ParameterType)
                : param.HasDefaultValue
                    ? param.DefaultValue
                    : throw new Exception($"Missing value for parameter '{param.Name}'")
        ).ToArray();
    }
}