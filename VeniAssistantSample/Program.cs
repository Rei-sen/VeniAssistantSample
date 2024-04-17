
using Microsoft.Extensions.Configuration;
using VeniAssistantSample;

public class Program
{
    public static async Task Main(string[] args)
    {
        var configBuilder = new ConfigurationBuilder()
            .AddUserSecrets<Program>();
        var config = configBuilder.Build();

        var httpClient = new HttpClient();
        var apiKey = config["OpenAiApiKey"] ?? "";

        var assistant = new AIAssistant(httpClient, apiKey);

        await assistant.CreateAssistant();

        var thread = new AIThread(httpClient, apiKey);
        await thread.Create();

        string input;
        Console.Write("> ");
        while ((input = Console.ReadLine()) != "exit")
        {
            await thread.AddMessageAsync(input);
            string runID = await thread.RunAsync(assistant);
            while (await thread.PollRunResult(runID) != "completed")
                Thread.Sleep(500);

            await thread.GetMessagesAsync();
            Console.WriteLine(thread.Data.Data[0].Content[0].Text.Value);
            
            Console.Write("> ");
        }
    }
}
