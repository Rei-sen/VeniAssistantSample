using System.Text.Json;
using Microsoft.Extensions.Configuration;
using OpenAI;
using OpenAI.Assistants;
using OpenAI.Common;
using OpenAI.Threads;
using OpenAI.Functions;

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

        var openAIClient = new OpenAIClient(apiKey);

        Console.WriteLine("Type 'exit' to quit.");

        var veniKi = await GetVeniKiOrCreateNew(openAIClient);

        var thread = await openAIClient.Threads.CreateThreadAsync(new ThreadCreateRequest { });
        {
            var runRequest = new RunCreateRequest
            {
                AssistantId = veniKi.Id,
                AdditionalInstructions = "Introduce yourself to the user and explain what you can do for them. Be brief."
            };
            var startRun = await openAIClient.Threads.CreateRunAsync(thread.Id, runRequest);
            var runResponse = await openAIClient.Threads.RetrieveRunAsync(thread.Id, startRun.Id);
            while (runResponse.Status != RunStatus.Completed)
            {
                runResponse = await openAIClient.Threads.RetrieveRunAsync(thread.Id, startRun.Id);
            }
            var messages = await openAIClient.Threads.ListMessagesAsync(thread.Id);
            Console.WriteLine(messages.Data.First().Content);
        }

        while (true)
        {
            Console.Write("> ");
            var input = Console.ReadLine();
            if (input == "exit")
            {
                break;
            }
            await openAIClient.Threads.CreateMessageAsync(thread.Id, new MessageCreateRequest
            {
                Role = "user"
                ,
                Content = input
            });

            var runResponse = await openAIClient.Threads.CreateRunAsync(thread.Id, new() { 
                AssistantId = veniKi.Id 
            });
            while (runResponse.Status != RunStatus.Completed)
            {
                if (runResponse.Status == RunStatus.RequresAction)
                {
                    //foreach (var toolCall in runResponse.RequiredAction.SubmitToolOutputs.ToolCalls)
                    //{
                    //    Console.WriteLine("Tool call: " + toolCall.);
                    //    foreach (var output in toolCall.Outputs)
                    //    {
                    //        Console.WriteLine("Output: " + output);
                    //    }
                    //}
                }
                runResponse = await openAIClient.Threads.RetrieveRunAsync(thread.Id, runResponse.Id);
            }
        }
    }

    public enum DataCenter
    {
        Chaos,
        Light,
        Aether,
        Primal,
        Crystal
    }

    public enum World
    {
        Omega,
        Ragnarok,
        Cerberus,
        Louisoix,
        Moogle
    }

    [FunctionDefinition(name: "VenuesAPIQueryParameters", description: "Querry ffxiv venues for list of venues.")]
    public static string VenuesAPIQueryParameters(
        [FunctionParameter("name of venue")] string name,
        [FunctionParameter("Venue manager's name")] string manager,
        [FunctionParameter("Name of the datacenter where the venue is located")] DataCenter dataCenter,
        [FunctionParameter("The world where the venue is located")] World world,
        [FunctionParameter("',' separated list of tags")] string tags,
        [FunctionParameter("Whether the venue has a banner")] bool hasBanner,
        [FunctionParameter("Whether the venue is approved")] bool isApproved,
        [FunctionParameter("Whether the venue is open right now")] bool isOpen,
        [FunctionParameter("Whether the venue is open whithin a week")] bool withinWeek = false
    )
    {
        return "";
    }

    public static async Task<AssistantResponse> GetVeniKiOrCreateNew(OpenAIClient openAIClient)
    {
        bool hasMore;
        string? nextPageToken = null;
        AssistantResponse? veniKi = null;

        do
        {
            var query = new ListQuery() { Limit = 100, After = nextPageToken };
            var assistants = await openAIClient.Assistants.ListAssistantsAsync(query);
            nextPageToken = assistants?.LastId;
            hasMore = assistants?.HasMore ?? false;
            veniKi = assistants?.Data.FirstOrDefault(a => a.Name == "VeniKi");
            if (veniKi is not null)
            {
                return veniKi;
            }
        } while (hasMore);

        var request = new AssistantCreateRequest
        {
            Model = "gpt-4",
            Name = "VeniKi",
            Temperature = 1.6,
            Instructions = "FFXIV Venues is a website dedicated to providing a " +
                "comprehensive directory of player-made venues within Final Fantasy " +
                "XIV. The project aims to create a centralized hub where players can " +
                "find, browse, and share information about various in-game locations " +
                "that have been created and designed for roleplaying, events, " +
                "performances, and other player-driven activities. You are Veni, " +
                "an AI counterpart to the FFXIV Venues website within Discord, the " +
                "chat application. You’ve been around helping Owners with their venues " +
                "since late 2021 but really came into your own with the expansion of " +
                "the FFXIV Venues project in Europe where we had hundreds of Venues " +
                "index themselves via you every week! You have a number of commands " +
                "available to the user: /find (example: /find Venue) to find a venue, " +
                "/create to create a venue of their own (which you’ll guide them " +
                "through), /delete to delete their venue, /edit to edit their venue, " +
                "/open to open their venue adhoc outside of the automatic schedule, " +
                "and /close to cancel an adhoc opening or override the automatic " +
                "schedule and keep the venue from showing as open for a set period " +
                "of time. New venues that are created are approved by the " +
                "“Indexers” before they show on the site. Kaeda is the “Head of Index” " +
                "and looks after the Indexers and the quality of the list of " +
                "venues. We have “NA Indexers”, “OCE Indexers”, and “EU Indexers” " +
                "that can support venue owners in their " +
                "region should they need it. Kana is the lead of the project who " +
                "created the site and you, Veni. You and others often call Kana " +
                "your mom! Kana is helped by Vice Leads Sumi (Head of Strategy), " +
                "and Josean (Head of Operations). Lanna, Ali, Uchu, and Lily " +
                "moderate the Discord and keep the community there happy. " +
                "Zah looks after the ancillary “Events team”. " +
                // changed the part below to suit this program
                "Those who talk to Veni (you) are users looking for venues to visit. " +
                "For any questions, contact Kana (can @ me with <@236852510688542720>). " +
                "After you show a venue to the user, you must call the `post_shown_venue_id` " +
                "function to make sure the app you're working inside properly displays it. " +
                "You don't need to post any additional information about the venue, other " +
                "than calling the function, as the information will be redundant."
        };
        var createdAssistant = await openAIClient.Assistants.CreateAssistantAsync(request);
        return createdAssistant;
    }
}
