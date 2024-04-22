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
    internal Delegate? Delegate;
    [JsonIgnore]
    private static readonly Dictionary<string, Function> _knownMethods = new();

    public static Function FromFunc(Delegate @delegate)
    {
        if (@delegate.Method.ReturnType != typeof(Task<FunctionResult>))
            throw new ArgumentException($"{@delegate.Method.Name} does not return type of `Task<FunctionResult>`");
            
        var function = SchemaGenerator.GenerateSchema(@delegate);
        _knownMethods[@delegate.Method.Name] = function;
        return function;
    }

    public static async Task<string> InvokeAsync(FunctionCall functionCall)
    {
        try
        {
            if (!_knownMethods.TryGetValue(functionCall.Name, out var function))
                throw new Exception($"Function '{functionCall.Name}' not found");

            if (function.Delegate is null)
                throw new Exception($"Function '{functionCall.Name}' is null. Please ensure that the function is properly registered.");

            var arguments = function.ParseArguments(functionCall.Arguments);
            
            var returnTask = function.Delegate.DynamicInvoke(arguments) as Task<FunctionResult>;
            var result = await returnTask!;
            return result switch
            {
                StringResult stringResult => stringResult.Result,
                JsonResult jsonResult => JsonSerializer.Serialize(jsonResult),
            };
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

    private object[] ParseArguments(string arguments)
    {
        if (Delegate is null)
        {
            throw new Exception("Method is null. Please ensure that the function is properly registered.");
        }
        var argumentValues = JsonSerializer.Deserialize<Dictionary<string, object>>(arguments);
        if (argumentValues is null)
        {
            throw new Exception("Failed to parse arguments");
        }

        object[] parsedArguments = new object[Delegate.Method.GetParameters().Length];

        int index = 0;
        foreach (var parameter in Delegate.Method.GetParameters())
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
