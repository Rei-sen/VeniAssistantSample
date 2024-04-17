
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace VeniAssistantSample;
public class AIAssistant
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public string ID { get; set; }

    public AIAssistant(HttpClient client, string apiKey)
    {
        _httpClient = client;
        _apiKey = apiKey;
    }

    public async Task CreateAssistant()
    {
        string requestBody = @"{
            ""instructions"": ""You are a personal math tutor. Write and run code to answer math questions."",
            ""name"": ""Math Tutor"",
            ""tools"": [{""type"": ""code_interpreter""}],
            ""model"": ""gpt-4""
        }";

        var request = new RequestBuilder()
           .WithMethod(HttpMethod.Post)
           .WithApiKey(_apiKey)
           .WithURL("assistants")
           .WithContent(requestBody)
           .Build();

        HttpResponseMessage response = await _httpClient.SendAsync(request);
        string responseContent = await response.Content.ReadAsStringAsync();
        var jsonDocument = JObject.Parse(responseContent);
        ID = (string?)jsonDocument.GetValue("id") ?? "";
    }
}
