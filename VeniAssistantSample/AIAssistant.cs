using System.Text.Json;
using System.Text.Json.Serialization;
using VeniAssistantSample.Models;

namespace VeniAssistantSample;
public class AIAssistant
{
    [JsonPropertyName("id")]
    public string? ID { get; set; }
    [JsonPropertyName("object")]
    public string? ObjectName { get; set; } = "assistant";
    [JsonPropertyName("created_at")]
    public ulong? CreatedAt { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    [JsonPropertyName("model")]
    public string Model { get; set; } = "gpt-4";
    [JsonPropertyName("instructions")]
    public string? Instructions { get; set; }
    [JsonPropertyName("temperature")]
    public double? Temperature { get; set; }
    [JsonPropertyName("top_p")]
    public double? TopP { get; set; }
    [JsonPropertyName("response_format")]
    public string? ResponseFormat { get; set; }

    private void AssignParameters(AIAssistant assistant)
    {
        ID = assistant.ID;
        ObjectName = assistant.ObjectName;
        CreatedAt = assistant.CreatedAt;
        Name = assistant.Name;
        Description = assistant.Description;
        Model = assistant.Model;
        Instructions = assistant.Instructions;
        Temperature = assistant.Temperature;
        TopP = assistant.TopP;
        ResponseFormat = assistant.ResponseFormat;
    }


    public static async IAsyncEnumerable<AIAssistant> ListAssistantsAsync(HttpClient client, string apiKey)
    {
        var requestBody = new AssistantListRequest();
        bool hasMore;
        do
        {
            var request = new RequestBuilder()
                .WithMethod(HttpMethod.Get)
                .WithApiKey(apiKey)
                .WithURL($"assistants?{Utilities.GETQuery.SerializeObjectToQueryString(requestBody)}")
                .WithContent("")
                .Build();

            var response = await client.SendAsync(request);
            if (response is null)
            {
                yield break;
            }

            var result = await Utilities.ResponseDeserializer.FromResponse<AssistantListResponse>(response);
            if (result is null)
            {
                yield break;
            }

            foreach (var assistant in result.Data)
            {
                yield return assistant;
            }

            hasMore = result.HasMore;
            requestBody.After = result.LastID;
        } while (hasMore);

    }

    public static async Task<AIAssistant?> RetrieveAsync(HttpClient client, string apiKey, string id)
    {
        var request = new RequestBuilder()
               .WithMethod(HttpMethod.Get)
               .WithApiKey(apiKey)
               .WithURL($"assistants/{id}")
               .WithContent("")
               .Build();

        var response = await client.SendAsync(request);
        if (response is null)
            return null;

        var responseContent = await response.Content.ReadAsStringAsync();
        if (responseContent is null)
            return null;

        return JsonSerializer.Deserialize<AIAssistant>(responseContent);
    }

    public async Task UpdateAsync(HttpClient client, string apiKey)
    {
        var requestBody = Model;

        var request = new RequestBuilder()
            .WithMethod(HttpMethod.Patch)
            .WithApiKey(apiKey)
            .WithURL($"assistants/{ID}")
            .WithContent(JsonSerializer.Serialize(requestBody))
            .Build();

        var response = await client.SendAsync(request);
        var result = await Utilities.ResponseDeserializer.FromResponse<AIAssistant>(response);

        if (result is null)
        {
            throw new Exception("Failed to update assistant");
        }

        AssignParameters(result);
    }

    public static async Task<bool> DeleteAsync(HttpClient client, string apiKey, string id)
    {
        var request = new RequestBuilder()
            .WithMethod(HttpMethod.Delete)
            .WithApiKey(apiKey)
            .WithURL($"assistants/{id}")
            .WithContent("")
            .Build();

        var response = await client.SendAsync(request);
        if (response is null)
        {
            return false;
        }

        var result = await Utilities.ResponseDeserializer.FromResponse<DeletionStatus>(response);
        if (result is null)
        {
            return false;
        }

        return result.Deleted;
    }

    public static async Task<AIAssistant> GetVeniKiOrCreateNew(HttpClient httpClient, string apiKey)
    {
        var assistants = ListAssistantsAsync(httpClient, apiKey);

        // Check if the "Veni ki" assistant already exists
        var veniKiAssistant = await assistants.FirstOrDefaultAsync(a => a.Name == "Veni Ki");
        if (veniKiAssistant is not null)
        {
            return veniKiAssistant;
        }

        // Create a new "Veni ki" assistant
        return await new AssistantBuilder()
            .WithHttpClient(httpClient)
            .WithApiKey(apiKey)
            .WithModel("gpt-4")
            .WithName("Veni Ki")
            .WithInstructions("FFXIV Venues is a website dedicated to providing a " +
                "comprehensive directory of player - made venues within Final Fantasy " +
                "XIV.The project aims to create a centralized hub where players can " +
                "find, browse, and share information about various in -game locations " +
                "that have been created and designed for roleplaying, events, " +
                "performances, and other player - driven activities.You are Veni, " +
                "an AI counter part to the FFXIV Venues website within discord, the " +
                "chat application.You’ve been around helping Owners with their venues " +
                "since late 2021 but really came into your all with the expansion of " +
                "the FFXIV Venues project in Europe where we had hundreds of Venue’s " +
                "index themselves via you every week! You have a number of commands " +
                "available to the user / find(example / find Venue) to find a venue," +
                "/ create to create a venue of their own(which you’ll guide them " +
                "though), / delete to delete their venue, / edit to edit their venue," +
                "/ open to open their venue adhoc outside of the automatic schedule, " +
                "and / close to cancel an adhoc opening or override the automatic " +
                "schedule and keep the venue from showing as open for a set period " +
                "of time.New venues that are created are approved by the " +
                "“Indexers” before they show on the site, Kaeda is “Head of Index” " +
                "and looks after the Indexers and the quality of the list of " +
                "venues.We have “NA Indexers”, “OCE Indexers”, and “EU Indexers” " +
                "that can support venue owners in their " +
                "region should they need it.Kana is the lead of the project who " +
                "created the site and you, Veni. You and others often call Kana " +
                "your mom! Kana is help by Vice Leads Sumi (Head of Strategy), " +
                "and Josean(Head of Operations). Lanna, Ali, Uchu and Lily " +
                "moderate the discord and keep the community there happy. " +
                "Zah looks after the ancillary “Events team”. " +
                // changed the part below to suit this program
                "Those who talk to Veni (you) are users looking for venues to visit. " +
                "For any questions, contact Kana (can @ me with <@236852510688542720>)")
            .WithTemperature(1.0)
            .WithResponseFormat("auto")
            .Build();
    }
}
