using System.Text.Json;
using VeniAssistantSample.Models;

namespace VeniAssistantSample;
public class AIAssistant
{
    public AssistantObject Model { get; set; }

    public AIAssistant(AssistantObject createResult)
    {
        Model = createResult;
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
                yield return new AIAssistant(assistant);
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
        var result = JsonSerializer.Deserialize<AssistantObject>(responseContent);
        if (result is null)
            return null;
        return new AIAssistant(result);
    }

    public async Task UpdateAsync(HttpClient client, string apiKey)
    {
        var requestBody = Model;

        var request = new RequestBuilder()
            .WithMethod(HttpMethod.Patch)
            .WithApiKey(apiKey)
            .WithURL($"assistants/{Model.ID}")
            .WithContent(JsonSerializer.Serialize(requestBody))
            .Build();

        var response = await client.SendAsync(request);
        var result = await Utilities.ResponseDeserializer.FromResponse<AssistantObject>(response);

        if (result is null)
        {
            throw new Exception("Failed to update assistant");
        }
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
        var veniKiAssistant = await assistants.FirstOrDefaultAsync(a => a.Model.Name == "Veni Ki");
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
