using OpenAI.Assistants;
using OpenAI.Threads;
namespace OpenAI;

public class OpenAIClient : IDisposable
{
    internal HttpClient _httpClient;
    internal string _apiKey;
    private bool disposedValue;

    public string BaseUrl { get; set; } = "https://api.openai.com/v1/";

    public AssistantsEndpoint Assistants { get; }
    public ThreadsEndpoint Threads { get; }

    public OpenAIClient(string apiKey)
    {
        _httpClient = new();
        _apiKey = apiKey;

        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
        _httpClient.DefaultRequestHeaders.Add("OpenAI-Beta", "assistants=v2");

        Assistants = new(this);
        Threads = new(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
                _httpClient.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~OpenAIClient()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
