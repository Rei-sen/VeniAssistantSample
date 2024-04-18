using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace VeniAssistantSample;
public class Program
{
    public static async Task Main(string[] args)
    {
        var configBuilder = new ConfigurationBuilder()
        .AddUserSecrets<Program>();
        var config = configBuilder.Build();
        using var httpClient = new HttpClient();
        var apiKey = config["OpenAiApiKey"] ?? "";
        
        var assistant = await AIAssistant.GetVeniKiOrCreateNew(httpClient, apiKey);

        var thread = await AIThread.CreateAsync(httpClient, apiKey);
        Console.WriteLine("Enter 'exit' to quit");
        var run = await thread.CreateRunAsync(httpClient, apiKey, assistant);
        do
        {
            var status = await thread.RetrieveRunAsync(httpClient, apiKey, run.ID);
            if (status.Status == "requires_action")
            {
                Console.WriteLine(JsonSerializer.Serialize(status));
            }
            else if (status.Status == "completed")
            {
                break;
            }
        } while (true);

        var messages = await thread.ListMessagesAsync(httpClient, apiKey).ToListAsync();

        Console.WriteLine(messages[0].Content[0].Text.Value);
        do
        {
            Console.Write("> ");
            var input = Console.ReadLine() ?? "";
            if (input == "exit")
                break;
            await thread.CreateMessageAsync(httpClient, apiKey, input);
            run = await thread.CreateRunAsync(httpClient, apiKey, assistant);
            do
            {
                var status = await thread.RetrieveRunAsync(httpClient, apiKey, run.ID);
                if (status.Status == "requires_action")
                {
                    Console.WriteLine(JsonSerializer.Serialize(status));
                }
                else if (status.Status == "completed")
                {
                    break;
                }
            } while (true);
            messages = await thread.ListMessagesAsync(httpClient, apiKey).ToListAsync();
            Console.WriteLine(messages[0].Content[0].Text.Value);
        } while (true);
        
        //await assistant.CreateAssistant();

        //var thread = new AIThread(httpClient, apiKey);
        //await thread.Create();

        //string input;
        //Console.Write("> ");
        //while ((input = Console.ReadLine()) != "exit")
        //{
        //    await thread.AddMessageAsync(input);
        //    string runID = await thread.RunAsync(assistant);
        //    while (await thread.PollRunResult(runID) != "completed")
        //        Thread.Sleep(500);

        //    await thread.GetMessagesAsync();
        //    Console.WriteLine(thread.Data.Data[0].Content[0].Text.Value);

        //    Console.Write("> ");
        //}
    }
}
