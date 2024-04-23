namespace OpenAI.Functions;

public abstract record FunctionResult()
{
    public abstract string Serialize();
};