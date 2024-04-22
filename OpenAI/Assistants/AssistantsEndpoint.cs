using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OpenAI.Common;

namespace OpenAI.Assistants;

public class AssistantsEndpoint : BaseEndpoint
{
    protected override string Root => "assistants";

    public AssistantsEndpoint(OpenAIClient client) : base(client)
    {
    }

    public async Task<AssistantResponse> CreateAssistantAsync(AssistantCreateRequest request, CancellationToken cancellationToken = default)
    {
        if (request.Model is null)
            throw new ArgumentNullException(nameof(request.Model));

        var url = CreateUrl("");
        return await SendRequestAsync<AssistantResponse>(url, HttpMethod.Post, request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<ListResponse<AssistantResponse>> ListAssistantsAsync(ListQuery? query = null, CancellationToken cancellationToken = default)
    {
        var url = CreateUrl("", query);
        return await SendRequestAsync<ListResponse<AssistantResponse>>(url, HttpMethod.Get, cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<AssistantResponse> RetrieveAssistantAsync(string assistantId, CancellationToken cancellationToken = default)
    {
        var url = CreateUrl($"/{assistantId}");
        return await SendRequestAsync<AssistantResponse>(url, HttpMethod.Get, cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<AssistantResponse> ModifyAssistantAsync(string assistantId, AssistantCreateRequest request, CancellationToken cancellationToken = default)
    {
        var url = CreateUrl($"/{assistantId}");
        return await SendRequestAsync<AssistantResponse>(url, HttpMethod.Post, request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<DeletionStatus> DeleteAssistantAsync(string assistantId, CancellationToken cancellationToken = default)
    {
        var url = CreateUrl($"/{assistantId}");
        return await SendRequestAsync<DeletionStatus>(url, HttpMethod.Delete, cancellationToken: cancellationToken).ConfigureAwait(false);
    }
}
