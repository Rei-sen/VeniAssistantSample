using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using OpenAI.Threads;

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
    private object? _instance = null;
    [JsonIgnore]
    internal MethodInfo? _method;
    [JsonIgnore]
    private static readonly Dictionary<string, Function> _knownMethods = new();

    public static Function FromFunc(string methodName, object? instance = null)
    {
        MethodInfo? method = instance!.GetType().GetMethod(methodName);
        if (method is null)
        {
            throw new Exception($"Method '{methodName}' not found on instance of type '{instance.GetType().Name}'");
        }
        var function = SchemaGenerator.GenerateSchema(method);

        function._instance = instance;
        _knownMethods[method.Name] = function;
        return function;
    }

    public static async Task<string> InvokeAsync(FunctionCall functionCall)
    {
        try
        {
            if (!_knownMethods.TryGetValue(functionCall.Name, out var function))
            {
                throw new Exception($"Function '{functionCall.Name}' not found");
            }

            if (function._method is null)
            {
                throw new Exception($"Function '{functionCall.Name}' is null. Please ensure that the function is properly registered.");
            }

            object[] arguments = function.ParseArguments(functionCall.Arguments);
            
            var returnValue = await (dynamic) function._method.Invoke(function._instance, arguments);
            return returnValue switch
            {
                StringResult => returnValue.Value,
                JsonResult => JsonSerializer.Serialize(returnValue.Value),
            };
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

    private object[] ParseArguments(string arguments)
    {
        if (_method is null)
        {
            throw new Exception("Method is null. Please ensure that the function is properly registered.");
        }
        var argumentValues = JsonSerializer.Deserialize<Dictionary<string, object>>(arguments);
        if (argumentValues is null)
        {
            throw new Exception("Failed to parse arguments");
        }

        object[] parsedArguments = new object[_method.GetParameters().Length];

        int index = 0;
        foreach (var parameter in _method.GetParameters())
        {
            string parameterName = parameter.Name;

            if (argumentValues.TryGetValue(parameterName, out object? argumentValue))
            {
                parsedArguments[index] = argumentValue;
            }
            else if (parameter.HasDefaultValue)
            {
                parsedArguments[index] = parameter.DefaultValue;
            }
            else
            {
                throw new Exception($"Missing value for parameter '{parameterName}'");
            }

            index++;
        }

        return parsedArguments;
    }
}
