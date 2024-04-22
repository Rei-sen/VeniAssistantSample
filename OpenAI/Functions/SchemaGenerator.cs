using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using OpenAI.Common;

namespace OpenAI.Functions;

internal class SchemaGenerator
{
    public static Function GenerateSchema(Delegate @delegate)
    {
        var method = @delegate.Method;
        var functionAttribute = method.GetCustomAttribute<FunctionDefinitionAttribute>();
        var functionName = functionAttribute?.Name ?? method.Name;
        var functionDescription = functionAttribute?.Description;

        var properties = new JsonObject();
        var propertiesInfos = method.GetParameters();
        var requiredParameters = new JsonArray();

        foreach (var parameterInfo in propertiesInfos)
        {
            var parameterAttribute = parameterInfo.GetCustomAttribute<FunctionParameterAttribute>();
            var parameterName = parameterInfo.Name;

            if (parameterName is null)
            {
                throw new InvalidOperationException("Parameter name is required");
            }

            var parameterType = parameterInfo.ParameterType;

            var parameterObject = new JsonObject();
            parameterObject["type"] = GetJsonType(parameterType);
            parameterObject["description"] = parameterAttribute?.Description;
            if (parameterType.IsEnum)
            {
                var enumValues = Enum.GetNames(parameterType);
                var enumNode = new JsonArray();
                foreach (var enumValue in enumValues)
                {
                    enumNode.Add(enumValue);
                }
                parameterObject["enum"] = enumNode;
            }

            properties[parameterName] = parameterObject;

            if (!parameterInfo.HasDefaultValue)
            {
                requiredParameters.Add(parameterName);
            }
        }

        var parameters = new JsonObject();
        parameters["type"] = "object";
        parameters["properties"] = properties;
        parameters["required"] = requiredParameters;

        var function = new Function
        {
            Name = functionName,
            Description = functionDescription,
            Parameters = parameters,
            Delegate = @delegate
        };

        return function;
    }

    private static JsonNode GetJsonType(Type type)
    {
        if (type == typeof(string))
        {
            return "string";
        }
        else if (type == typeof(int))
        {
            return "integer";
        }
        else if (type == typeof(double))
        {
            return "number";
        }
        else if (type == typeof(bool))
        {
            return "boolean";
        }
        else if (type.IsEnum)
        {
            return "string";
        }
        //else if (type.IsClass || type.IsValueType)
        //{
        //    var properties = new JsonObject();
        //    var propertyInfos = type.GetProperties();
        //    foreach (var propertyInfo in propertyInfos)
        //    {
        //        var propertyName = propertyInfo.Name;
        //        var propertyType = propertyInfo.PropertyType;

        //        var propertyObject = new JsonObject();
        //        propertyObject["type"] = GetJsonType(propertyType);

        //        properties[propertyName] = propertyObject;
        //    }
        //    return new JsonObject
        //    {
        //        ["type"] = "object",
        //        ["properties"] = properties,
        //        ["required"] = new JsonArray(propertyInfos.Select(p => p.Name))
        //    };
        //}
        else
        {
            return "unknown";
            throw new InvalidOperationException($"Unknown type {type}");
        }
    }
}
