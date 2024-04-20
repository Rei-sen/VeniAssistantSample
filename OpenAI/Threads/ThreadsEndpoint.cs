using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OpenAI.Common;

namespace OpenAI.Threads;

public class ThreadsEndpoint : BaseEndpoint
{
    public ThreadsEndpoint(OpenAIClient client) : base(client)
    {
    }

    protected override string Root => "threads";

    public async Task<ThreadResponse?> CreateThreadAsync(ThreadCreateRequest request, CancellationToken cancellationToken = default)
    {
        var url = CreateUrl("");
        return await SendRequestAsync<ThreadResponse>(url, HttpMethod.Post, request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<ThreadResponse?> RetrieveThreadAsync(string threadId, CancellationToken cancellationToken = default)
    {
        var url = CreateUrl($"/{threadId}");
        return await SendRequestAsync<ThreadResponse>(url, HttpMethod.Get, cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<ListResponse<ThreadResponse>?> ListThreadsAsync(ListQuery? query = null, CancellationToken cancellationToken = default)
    {
        var url = CreateUrl("", query);
        return await SendRequestAsync<ListResponse<ThreadResponse>>(url, HttpMethod.Get, cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<ThreadResponse?> ModifyThreadAsync(string threadId, ThreadModifyRequest request, CancellationToken cancellationToken = default)
    {
        var url = CreateUrl($"/{threadId}");
        return await SendRequestAsync<ThreadResponse>(url, HttpMethod.Patch, request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<DeletionStatus?> DeleteThreadAsync(string threadId, CancellationToken cancellationToken = default)
    {
        var url = CreateUrl($"/{threadId}");
        return await SendRequestAsync<DeletionStatus>(url, HttpMethod.Delete, cancellationToken: cancellationToken).ConfigureAwait(false);
    }
}
