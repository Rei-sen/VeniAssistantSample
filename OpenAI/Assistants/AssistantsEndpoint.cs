using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OpenAI.Assistants;

public class AssistantsEndpoint : BaseEndpoint
{
    protected override string Root => "assistants";

    public AssistantsEndpoint(OpenAIClient client) : base(client)
    {
    }

    public async Task<AssistantResponse?> CreateAssistantAsync(AssistantCreateRequest request, CancellationToken cancellationToken = default)
    {
        var url = CreateUrl("");
        var jsonContent = JsonSerializer.Serialize(request);
        var response = await _client._httpClient.PostAsync(url, JsonContent.Create(jsonContent), cancellationToken).ConfigureAwait(false);
        using var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        return await JsonSerializer.DeserializeAsync<AssistantResponse>(responseStream, cancellationToken: cancellationToken).ConfigureAwait(false);
    }

}
