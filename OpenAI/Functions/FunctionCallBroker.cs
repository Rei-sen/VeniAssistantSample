using OpenAI.Threads;

namespace OpenAI.Functions;

#pragma warning disable CS8618
// todo DI?
public static class FunctionCallBroker
{
    private static readonly Dictionary<string, Delegate> _knownFunctions = new();

    public static Function RegisterFunction(Delegate @delegate)
    {
        var function = new Function(@delegate);
        if (_knownFunctions.ContainsKey(function.Name))
        {
            _knownFunctions[function.Name] = @delegate;
        }
        else
        {
            _knownFunctions.Add(function.Name, @delegate);
        }
        return function;
    }

    public static async Task<FunctionResult> InvokeCallAsync(FunctionCall functionCall)
    {
        if (!_knownFunctions.TryGetValue(functionCall.Name, out var function))
            throw new MissingMethodException($"Function {functionCall.Name} could not be found.");

        var arguments = functionCall.ParseArguments(function.Method.GetParameters());
        var returnTask = function.DynamicInvoke(arguments) as Task<FunctionResult>;

        return await returnTask!;
    }
}