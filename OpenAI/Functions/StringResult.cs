namespace OpenAI.Functions;

public record StringResult(string Result) : FunctionResult
{
    public override string Serialize() => Result;
};
