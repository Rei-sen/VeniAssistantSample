using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using VeniAssistantSample.Utilities;

namespace VeniAssistantSample.Utilities;

internal class ResponseDeserializer
{
    public static async Task<T?> FromResponse<T>(HttpResponseMessage response)
    {
        return await FromResponse<T>(response, JsonSerializerOptions.Default);
    }

    public static async Task<T?> FromResponse<T>(HttpResponseMessage response, JsonSerializerOptions options)
    {
        if (response is null)
            return default;

        var responseContent = await response.Content.ReadAsStringAsync();
        if (responseContent is null)
            return default;


        JsonObject? jsonObject = JsonSerializer.Deserialize<JsonObject>(responseContent);
        if (jsonObject?["error"] is not null)
        {
            var error = JsonSerializer.Deserialize<OpenAIError>(jsonObject["error"]);
            if (error is not null)
                throw error;
        }

        var result = JsonSerializer.Deserialize<T>(responseContent);
        if (result is null)
            return default;

        return result;
    }
}
