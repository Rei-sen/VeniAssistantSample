using System.Reflection;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace OpenAI.Functions;

public class Function
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    [JsonPropertyName("parameters")]
    public required JsonObject Parameters { get; set; }

    [JsonIgnore]
    internal MethodInfo? _method;
    [JsonIgnore]
    private static readonly Dictionary<string, Function> _knownMethods = new();

    public static Function FromFunc(MethodInfo method)
    {
        var function =  SchemaGenerator.GenerateSchema(method);
        _knownMethods[method.Name] = function;
        return function;
    }


}