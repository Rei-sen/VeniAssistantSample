using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace VeniAssistantSample.Utilities;

internal class ResponseDeserializer
{
    public static async Task<T?> FromResponse<T>(HttpResponseMessage response)
    {
        if (response is null)
            return default;

         var responseContent = await response.Content.ReadAsStringAsync();
        if (responseContent is null)
            return default;

        var result = JsonSerializer.Deserialize<T>(responseContent);
        if (result is null)
            return default;

        return result;
    }
}
