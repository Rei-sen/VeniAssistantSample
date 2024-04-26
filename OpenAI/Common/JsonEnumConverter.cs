using System;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenAI.Threads;

public class JsonEnumConverter<TEnum> : JsonConverter<TEnum> where TEnum : Enum
{
    private static readonly Dictionary<TEnum, string> EnumValueMap = new();
    private static readonly Dictionary<string, TEnum> StringValueMap = new();

    static JsonEnumConverter()
    {
        foreach (TEnum enumValue in Enum.GetValues(typeof(TEnum)))
        {
            MemberInfo memberInfo = typeof(TEnum).GetMember(enumValue.ToString()).FirstOrDefault();
            if (memberInfo != null)
            {
                EnumMemberAttribute enumMemberAttribute = memberInfo.GetCustomAttribute<EnumMemberAttribute>();
                if (enumMemberAttribute != null)
                {
                    EnumValueMap[enumValue] = enumMemberAttribute.Value;
                    StringValueMap[enumMemberAttribute.Value] = enumValue;
                }
            }
        }
    }

    public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            string enumValue = reader.GetString();
            if (StringValueMap.TryGetValue(enumValue, out TEnum value))
            {
                return value;
            }
        }
        throw new JsonException($"Unable to convert value '{reader.GetString()}' to {typeof(TEnum).Name}.");
    }

    public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
    {
        if (EnumValueMap.TryGetValue(value, out string enumValue))
        {
            writer.WriteStringValue(enumValue);
        }
        else
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
