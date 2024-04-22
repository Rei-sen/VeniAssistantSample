using System.Reflection;
using System.Text.Json.Nodes;

namespace OpenAI.Functions;

public class SchemaGenerator
{
    private static readonly IDictionary<Type, string> TypeDict = new Dictionary<Type, string>
    {
        { typeof(string), "string" },
        { typeof(int), "integer" },
        { typeof(double), "number" },
        { typeof(bool), "boolean" }
    };

    public static (string Name, string? Description) GetNameAndDescription(Delegate del)
    {
        var method = del.Method;
        var attr = method.GetCustomAttribute<FunctionDefinitionAttribute>();
        return (attr?.Name ?? method.Name, attr?.Description);
    }
    
    public static JsonObject GetParameterSchema(Delegate del)
    {
        var paramsJsonObj = new JsonObject();
        var requiredParamsJsonArray = new JsonArray();

        foreach (var paramInfo in del.Method.GetParameters())
        {
            var paramName = paramInfo.Name ?? throw new InvalidOperationException("Parameter name is required");
            paramsJsonObj[paramName] = GenerateParamJsonObj(paramInfo);
            if (!paramInfo.HasDefaultValue) requiredParamsJsonArray.Add(paramName);
        }

        return new () 
        {
            { "type", "object" },
            { "properties", paramsJsonObj },
            { "required", requiredParamsJsonArray }
        };
    }


    private static JsonObject GenerateParamJsonObj(ParameterInfo paramInfo)
    {
        var attr = paramInfo.GetCustomAttribute<FunctionParameterAttribute>();
        var paramType = paramInfo.ParameterType;
        var typeStr = TypeDict.ContainsKey(paramType) ? TypeDict[paramType] : (paramType.IsEnum ? "string" : "unknown");

        var jsonObj = new JsonObject
        {
            { "type", typeStr },
            { "description", attr?.Description }
        };

        if (paramType.IsEnum)
        {
            var jsonArray = new JsonArray();
            foreach (var name in Enum.GetNames(paramType))
                jsonArray.Add(name);
            jsonObj["enum"] = jsonArray;
        }

        return jsonObj;
    }
}