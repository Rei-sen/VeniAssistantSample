using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using VeniAssistantSample.Models;

namespace VeniAssistantSample;

internal class AssistantBuilder
{
    private string? _apiKey;
    private HttpClient? _httpClient;
    private AIAssistant _requestBody = new();

    public AssistantBuilder WithApiKey(string apiKey)
    {
        _apiKey = apiKey;
        return this;
    }

    public AssistantBuilder WithHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        return this;
    }

    public AssistantBuilder WithModel(string model)
    {
        _requestBody.Model = model;
        return this;
    }

    public AssistantBuilder WithName(string name)
    {
        _requestBody.Name = name;
        return this;
    }

    public AssistantBuilder WithDescription(string description)
    {
        _requestBody.Description = description;
        return this;
    }

    public AssistantBuilder WithInstructions(string instructions)
    {
        _requestBody.Instructions = instructions;
        return this;
    }

    public AssistantBuilder WithTemperature(double temperature)
    {
        _requestBody.Temperature = temperature;
        return this;
    }

    public AssistantBuilder WithTopP(double top_p)
    {
        _requestBody.TopP = top_p;
        return this;
    }

    public AssistantBuilder WithResponseFormat(string responseFormat)
    {
        _requestBody.ResponseFormat = responseFormat;
        return this;
    }

    public async Task<AIAssistant> Build()
    {
        var requestBody = JsonSerializer.Serialize(_requestBody) ?? "";

        if (_apiKey is null)
        {
            throw new Exception("API key is required");
        }
        if (_httpClient is null)
        {
            throw new Exception("HTTP client is required");
        }

        var request = new RequestBuilder()
           .WithMethod(HttpMethod.Post)
           .WithApiKey(_apiKey)
           .WithURL("assistants")
           .WithContent(requestBody)
           .Build();

        HttpResponseMessage response = await _httpClient.SendAsync(request);
        await using Stream stream = await response.Content.ReadAsStreamAsync();
        var result = await JsonSerializer.DeserializeAsync<AIAssistant>(stream);

        if (result is null)
        {
            throw new Exception("Failed to create assistant");
        }

        return result;
    }
}
