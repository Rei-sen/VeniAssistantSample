namespace OpenAI;

public class OpenAIClient
{
    internal HttpClient _httpClient;
    internal string _apiKey; 

    public string BaseUrl { get; set; } = "https://api.openai.com/v1/";

    public OpenAIClient(HttpClient httpClient, string apiKey)
    {
        _httpClient = httpClient;
        _apiKey = apiKey;
    }
}
