using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeniAssistantSample;

internal class RequestBuilder
{
    private static readonly string _baseUrl = "https://api.openai.com/v1";
    private HttpRequestMessage _httpRequestMessage = new HttpRequestMessage();

    public RequestBuilder()
    {
        _httpRequestMessage.Headers.Add("OpenAI-Beta", "assistants=v1");
    }
    public RequestBuilder WithMethod(HttpMethod method)
    {
        _httpRequestMessage.Method = method;
        return this;
    }

    public RequestBuilder WithURL(string url)
    {
        _httpRequestMessage.RequestUri = new($"{_baseUrl}/{url}");
        return this;
    }

    public RequestBuilder WithApiKey(string apiKey)
    {
        _httpRequestMessage.Headers.Add("Authorization", $"Bearer {apiKey}");
        return this;
    }
    public RequestBuilder WithContent(string content)
    {
        _httpRequestMessage.Content = new StringContent(content, Encoding.UTF8, "application/json");
        return this;
    }

    public HttpRequestMessage Build()
    {
        return _httpRequestMessage;
    }
}
