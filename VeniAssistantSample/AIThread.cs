using System.Text.Json;
using System.Text.Json.Serialization;
using VeniAssistantSample.Models;

namespace VeniAssistantSample;

internal class AIThread
{
    [JsonPropertyName("id")]
    public string? ID { get; set; }
    [JsonPropertyName("object")]
    public string Object { get; set; } = "thread";
    [JsonPropertyName("created_at")]
    public ulong CreatedAt { get; set; }

    private void AssignParameters(AIThread thread)
    {
        ID = thread.ID;
        Object = thread.Object;
        CreatedAt = thread.CreatedAt;
    }

    public static async Task<AIThread> CreateAsync(HttpClient httpClient, string apiKey)
    {
        var request = new RequestBuilder()
            .WithMethod(HttpMethod.Post)
            .WithApiKey(apiKey)
            .WithURL("threads")
            .WithContent("")
            .Build();

        var response = await httpClient.SendAsync(request);
        var threadObject = await Utilities.ResponseDeserializer.FromResponse<AIThread>(response);
        if (threadObject is null)
            throw new Exception("Failed to create a new thread");

        return threadObject;
    }

    public static async Task<AIThread> RetrieveThreadAsync(HttpClient httpClient, string apiKey, string id)
    {
        var request = new RequestBuilder()
            .WithMethod(HttpMethod.Get)
            .WithApiKey(apiKey)
            .WithURL($"threads/{id}")
            .WithContent("")
            .Build();

        var response = await httpClient.SendAsync(request);
        var threadObject = await Utilities.ResponseDeserializer.FromResponse<AIThread>(response);
        if (threadObject is null)
            throw new Exception("Failed to get thread");

        return threadObject;
    }

    public async Task UpdateAsync(HttpClient httpClient, string apiKey)
    {
        var request = new RequestBuilder()
            .WithMethod(HttpMethod.Post)
            .WithApiKey(apiKey)
            .WithURL($"threads/{ID}")
            .WithContent("")
            .Build();

        var response = await httpClient.SendAsync(request);
        var threadObject = await Utilities.ResponseDeserializer.FromResponse<AIThread>(response);
        if (threadObject is null)
            throw new Exception("Failed to update thread");

        AssignParameters(threadObject);
    }

    public async Task<DeletionStatus> DeleteAsync(HttpClient httpClient, string apiKey)
    {
        var request = new RequestBuilder()
            .WithMethod(HttpMethod.Delete)
            .WithApiKey(apiKey)
            .WithURL($"threads/{ID}")
            .WithContent("")
            .Build();

        var response = await httpClient.SendAsync(request);
        var deletionStatus = await Utilities.ResponseDeserializer.FromResponse<DeletionStatus>(response);
        if (deletionStatus is null)
            throw new Exception("Failed to delete thread");

        return deletionStatus;
    }

    public async Task<Message> CreateMessageAsync(HttpClient httpClient, string apiKey, string message, string role = "user")
    {
        var requestBody = new
        {
            role = role,
            content = message
        };

        var request = new RequestBuilder()
            .WithMethod(HttpMethod.Post)
            .WithApiKey(apiKey)
            .WithURL($"threads/{ID}/messages")
            .WithContent(JsonSerializer.Serialize(requestBody))
            .Build();

        var response = await httpClient.SendAsync(request);
        var messageObject = await Utilities.ResponseDeserializer.FromResponse<Message>(response);
        if (messageObject is null)
            throw new Exception("Failed to create a new message");

        return messageObject;
    }

    public async IAsyncEnumerable<Message> ListMessagesAsync(HttpClient httpClient, string apiKey)
    {
        var requestBody = new MessageListRequest();
        bool hasMore;

        do
        {
            var request = new RequestBuilder()
                            .WithMethod(HttpMethod.Get)
                            .WithApiKey(apiKey)
                            .WithURL($"threads/{ID}/messages?{Utilities.GETQuery.SerializeObjectToQueryString(requestBody)}")
                            .WithContent("")
                            .Build();

            var response = await httpClient.SendAsync(request);
            if (request is null)
                throw new Exception("Failed to get messages");

            var messages = await Utilities.ResponseDeserializer.FromResponse<MessageListResponse>(response);
            if (messages is null)
                throw new Exception("Failed to get messages");

            foreach (var message in messages.Data)
            {
                yield return message;
            }
          
            hasMore = messages.HasMore;
           
            if (hasMore)
            {
                requestBody.After = messages.LastID;
            }
        } while (hasMore);
    }

    public async Task<Run> CreateRunAsync(HttpClient httpClient, string apiKey, AIAssistant assistant)
    {
        if (assistant.ID is null)
            throw new Exception("Assistant ID is required");

        var requestBody = new RunCreateRequest
        {
            AssistantID = assistant.ID
        };

        var request = new RequestBuilder()
            .WithMethod(HttpMethod.Post)
            .WithApiKey(apiKey)
            .WithURL($"threads/{ID}/runs")
            .WithContent(JsonSerializer.Serialize(requestBody))
            .Build();

        var response = await httpClient.SendAsync(request);
        var runObject = await Utilities.ResponseDeserializer.FromResponse<Run>(response);
        if (runObject is null)
            throw new Exception("Failed to create a new run");

        return runObject;
    }

    public async Task<Run> RetrieveRunAsync(HttpClient httpClient, string apiKey, string runID)
    {
        var request = new RequestBuilder()
            .WithMethod(HttpMethod.Get)
            .WithApiKey(apiKey)
            .WithURL($"threads/{ID}/runs/{runID}")
            .WithContent("")
            .Build();

        var response = await httpClient.SendAsync(request);
        var runObject = await Utilities.ResponseDeserializer.FromResponse<Run>(response);
        if (runObject is null)
            throw new Exception("Failed to get run");

        return runObject;
    }
}
