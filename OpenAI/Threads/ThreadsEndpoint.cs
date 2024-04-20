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

    public async Task<ThreadResponse> CreateThreadAsync(ThreadCreateRequest request, CancellationToken cancellationToken = default)
    {
        var url = CreateUrl("");
        return await SendRequestAsync<ThreadResponse>(url, HttpMethod.Post, request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<ThreadResponse> RetrieveThreadAsync(string threadID, CancellationToken cancellationToken = default)
    {
        var url = CreateUrl($"/{threadID}");
        return await SendRequestAsync<ThreadResponse>(url, HttpMethod.Get, cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<ListResponse<ThreadResponse>> ListThreadsAsync(ListQuery? query = null, CancellationToken cancellationToken = default)
    {
        var url = CreateUrl("", query);
        return await SendRequestAsync<ListResponse<ThreadResponse>>(url, HttpMethod.Get, cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<ThreadResponse> ModifyThreadAsync(string threadID, ThreadModifyRequest request, CancellationToken cancellationToken = default)
    {
        var url = CreateUrl($"/{threadID}");
        return await SendRequestAsync<ThreadResponse>(url, HttpMethod.Post, request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<DeletionStatus> DeleteThreadAsync(string threadID, CancellationToken cancellationToken = default)
    {
        var url = CreateUrl($"/{threadID}");
        return await SendRequestAsync<DeletionStatus>(url, HttpMethod.Delete, cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<ThreadResponse> CreateMessageAsync(string threadID, MessageCreateRequest request, CancellationToken cancellationToken = default)
    {
        var url = CreateUrl($"/{threadID}/messages");
        return await SendRequestAsync<ThreadResponse>(url, HttpMethod.Post, request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<ListResponse<MessageResponse>> ListMessagesAsync(string threadID, MessageListQuery? query = null, CancellationToken cancellationToken = default)
    {
        var url = CreateUrl($"/{threadID}/messages", query);
        return await SendRequestAsync<ListResponse<MessageResponse>>(url, HttpMethod.Get, cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<MessageResponse> RetrieveMessageAsync(string threadID, string messageID, CancellationToken cancellationToken = default)
    {
        var url = CreateUrl($"/{threadID}/messages/{messageID}");
        return await SendRequestAsync<MessageResponse>(url, HttpMethod.Get, cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<MessageResponse> ModifyMessageAsync(string threadID, string messageID, MessageModifyRequest request, CancellationToken cancellationToken = default)
    {
        var url = CreateUrl($"/{threadID}/messages/{messageID}");
        return await SendRequestAsync<MessageResponse>(url, HttpMethod.Post, request, cancellationToken).ConfigureAwait(false);
    }

    //public async Task<RunResponse> CreateRunAsync(string threadID, RunCreateRequest request, CancellationToken cancellationToken = default)
    //{
    //    var url = CreateUrl($"/{threadID}/runs");
    //    return await SendRequestAsync<RunResponse>(url, HttpMethod.Post, request, cancellationToken).ConfigureAwait(false);
    //}
}
