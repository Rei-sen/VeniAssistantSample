using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
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

    protected async Task<T> SendRequestAsync<T>(string url, HttpMethod method, object? requestBody = null, CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(method, url);
        if (requestBody is not null)
        {
            request.Content = JsonContent.Create(requestBody);
        }

        var response = await _client._httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            using var errorJson = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            var error = await JsonSerializer.DeserializeAsync<ErrorObject>(errorJson);
            if (error is null)
                throw new JsonException("Failed to deserialize error response");
            throw new ErrorResponseException(error!.ErrorDetails);
        }

        using var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        var result = await JsonSerializer.DeserializeAsync<T>(responseStream, cancellationToken: cancellationToken).ConfigureAwait(false);
        if (result is null)
            throw new JsonException("Failed to deserialize response");

        return result;
    }
}
