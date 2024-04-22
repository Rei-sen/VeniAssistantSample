using OpenAI.Threads;

namespace OpenAI.Functions;

#pragma warning disable CS8618
// todo DI?
public static class FunctionCallBroker
{
    private static readonly Dictionary<string, Function> _knownFunctions = new();

    public static Function RegisterFunction(Delegate @delegate)
    {
        var function = new Function(@delegate);
        _knownFunctions.Add(function.Descriptor.Name, function);
        return function;
    }

    public static Task<FunctionResult> InvokeCallAsync(FunctionCall functionCall)
    {
        if (!_knownFunctions.TryGetValue(functionCall.Name, out var function))
            throw new MissingMethodException($"Function {functionCall.Name} could not be found.");

        return function.InvokeAsync(functionCall);
    }
    
    public record Function
    {
    
        public Delegate Delegate { get; set; }
        public FunctionDescriptor Descriptor { get; set; }
    
        internal Function(Delegate @delegate)
        {
            if (@delegate.Method.ReturnType != typeof(Task<FunctionResult>))
                throw new ArgumentException($"{@delegate.Method.Name} does not return type of `Task<FunctionResult>`");
        
            var (name, description) = SchemaGenerator.GetNameAndDescription(@delegate);
            var parameters = SchemaGenerator.GetParameterSchema(@delegate);

            this.Delegate = @delegate;
            this.Descriptor = new (name, description, parameters);
        }

        public async Task<FunctionResult> InvokeAsync(FunctionCall functionCall)
        {
            var arguments = functionCall.ParseArguments(this.Delegate.Method.GetParameters());
            var returnTask = this.Delegate.DynamicInvoke(arguments) as Task<FunctionResult>;
            return await returnTask!;
        }
    }
    
}