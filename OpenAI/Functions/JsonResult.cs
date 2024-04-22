using System.Text.Json;

namespace OpenAI.Functions;

public record JsonResult(object Result) : FunctionResult
{
    public override string Serialize() =>
        JsonSerializer.Serialize(Result);
}