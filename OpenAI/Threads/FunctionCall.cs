using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenAI.Threads;

public class FunctionCall
{
    [JsonPropertyName("name")] public required string Name { get; init; }
    [JsonPropertyName("arguments")] public required string Arguments { get; init; }

    public object[] ParseArguments(ParameterInfo[] parameters)
    {
        var argValues = JsonSerializer.Deserialize<Dictionary<string, object>>(Arguments)
                        ?? throw new Exception("Failed to parse arguments");

        return parameters.Select((param, i) =>
            argValues.TryGetValue(param.Name, out var argValue)
                ? argValue
                : param.HasDefaultValue
                    ? param.DefaultValue
                    : throw new Exception($"Missing value for parameter '{param.Name}'")).ToArray();
    }
}