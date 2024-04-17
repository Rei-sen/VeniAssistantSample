using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace VeniAssistantSample;

internal class AIThread
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public string ID {  get; private set; }
    public ThreadData? Data { get; private set; }

    public AIThread(HttpClient httpClient, string apiKey)
    {
        _httpClient = httpClient;
        _apiKey = apiKey;
    }

    public class ThreadData
    {
        public string Object { get; set; }
        public List<Message> Data { get; set; }
        public string FirstId { get; set; }
        public string LastId { get; set; }
        public bool HasMore { get; set; }
    }

    public async Task Create()
    {
        var request = new RequestBuilder()
            .WithMethod(HttpMethod.Post)
            .WithApiKey(_apiKey)
            .WithURL("threads")
            .WithContent("")
            .Build();

        var response = await _httpClient.SendAsync(request);
        string responseContent = await response.Content.ReadAsStringAsync();
        
        var jsonDocument = JObject.Parse(responseContent);
        ID = (string?)jsonDocument.GetValue("id") ?? "";

        response.EnsureSuccessStatusCode();
    }

    public async Task AddMessageAsync(string message)
    {
        string requestBody = @"
        {
            ""role"": ""user"",
            ""content"": """ + message + @"""
        }";

        var request = new RequestBuilder()
            .WithMethod(HttpMethod.Post)
            .WithApiKey(_apiKey)
            .WithURL($"threads/{ID}/messages")
            .WithContent(requestBody)
            .Build();

        var response = await _httpClient.SendAsync(request);
        string responseContent = await response.Content.ReadAsStringAsync();
    }


    public async Task<string> RunAsync(AIAssistant assistant)
    {
        string requestBody = @"
        {
            ""assistant_id"": """ + assistant.ID +  @""",
            ""instructions"": """"
        }";

        var request = new RequestBuilder()
            .WithMethod(HttpMethod.Post)
            .WithApiKey(_apiKey)
            .WithURL($"threads/{ID}/runs")
            .WithContent(requestBody)
            .Build();

        var response = await _httpClient.SendAsync(request);
        var responseContent = await response.Content.ReadAsStringAsync();
        var jsonDocument = JObject.Parse(responseContent);
        string runID = (string?)jsonDocument.GetValue("id") ?? "";

        return runID;
    }

    public async Task<string> PollRunResult(string runID)
    {
        var request = new RequestBuilder()
            .WithMethod(HttpMethod.Get)
            .WithApiKey(_apiKey)
            .WithURL($"threads/{ID}/runs/{runID}")
            .WithContent("")
            .Build();

        var response = await _httpClient.SendAsync(request);
        var responseContent = await response.Content.ReadAsStringAsync();
        var jsonDocument = JObject.Parse(responseContent);
        string status = (string?)jsonDocument.GetValue("status") ?? "";

        return status;
    }

    public async Task GetMessagesAsync()
    {
        var request = new RequestBuilder()
            .WithMethod(HttpMethod.Get)
            .WithApiKey(_apiKey)
            .WithURL($"threads/{ID}/messages")
            .WithContent("")
            .Build();

        var response = await _httpClient.SendAsync(request);
        var responseContent = await response.Content.ReadAsStringAsync();
        AssigntThreadData(responseContent);
    }

    private void AssigntThreadData(string jsonString)
    {
        var jsonDocument = JObject.Parse(jsonString);
        Data = new ThreadData
        {
            Object = (string?)jsonDocument.GetValue("object") ?? "",
            Data = jsonDocument["data"].ToObject<List<Message>>() ?? new List<Message>(),
            FirstId = (string?)jsonDocument.GetValue("first_id") ?? "",
            LastId = (string?)jsonDocument.GetValue("last_id") ?? "",
            HasMore = (bool?)jsonDocument.GetValue("has_more") ?? false
        };
    }
}
