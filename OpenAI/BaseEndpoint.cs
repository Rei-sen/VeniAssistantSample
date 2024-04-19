using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI;

public abstract class BaseEndpoint(OpenAIClient client)
{
    protected readonly OpenAIClient _client = client;
    protected abstract string Root { get; }

    protected string CreateUrl(string endPoint, object? getParameters = null)
    {
        string url = $"{_client.BaseUrl}/{Root}{endPoint}";

        if (getParameters is not null)
        {
            var queryString = SerializeObjectToQueryString(getParameters);
            url += $"?{queryString}";
        }

        return url;
    }

    private static string SerializeObjectToQueryString(object obj)
    {
        var properties = obj.GetType().GetProperties();
        var query = new StringBuilder();

        foreach (var property in properties)
        {
            var propertyName = property.Name;
            var propertyValue = property.GetValue(obj)?.ToString();
            if (propertyValue is null)
                continue;

            var encodedName = Uri.EscapeDataString(propertyName);
            var encodedValue = Uri.EscapeDataString(propertyValue);

            query.Append($"{encodedName}={encodedValue}&");
        }

        if (query.Length > 0)
        {
            query.Remove(query.Length - 1, 1);
        }

        return query.ToString();
    }
}
