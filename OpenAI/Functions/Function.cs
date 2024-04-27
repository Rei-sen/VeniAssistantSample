using System.Text.Json.Nodes;

namespace OpenAI.Functions;

#pragma warning disable CS8618
// todo: change parameters to NOT be a jsonObject so as to use something more generic and abstract away from implementation detail
public record Function(string Name, string? Description, JsonObject Parameters)
{
    public Function() : this(string.Empty, null, new JsonObject())
    {

    }
    public Function(Delegate @delegate) : this(SchemaGenerator.GetNameAndDescription(@delegate), SchemaGenerator.GetParameterSchema(@delegate))
    {

    }

    public Function((string Name, string? Description) value, JsonObject jsonObject) : this(value.Name, value.Description, jsonObject)
    {
    }
}