

using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using OpenAI;
using OpenAI.Moderations;

var configBuilder = new ConfigurationBuilder()
           .AddUserSecrets<Program>();
var config = configBuilder.Build();
using var httpClient = new HttpClient();
var apiKey = config["OpenAiApiKey"] ?? "";

var openAIClient = new OpenAIClient(apiKey);

while (true)
{
    Console.Write("> ");
    var input = Console.ReadLine();
    if (input is null)
    {
        break;
    }

    var response = await openAIClient.Moderations.CreateModerationAsync(new ModerationCreateRequest
    {
        Inputs = input
    });
    Console.WriteLine(JsonSerializer.Serialize(response.Results[0]));
}