using System.Text.Json.Nodes;

namespace OpenAI.Functions;

#pragma warning disable CS8618
// todo: change parameters to NOT be a jsonObject so as to use something more generic and abstract away from implementation detail
public record FunctionDescriptor(string Name, string? Description, JsonObject Parameters);