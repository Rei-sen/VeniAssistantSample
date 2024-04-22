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

    public async Task<ThreadResponse> RetrieveThreadAsync(string threadId, CancellationToken cancellationToken = default)
    {
        var url = CreateUrl($"/{threadId}");
        return await SendRequestAsync<ThreadResponse>(url, HttpMethod.Get, cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<ListResponse<ThreadResponse>> ListThreadsAsync(ListQuery? query = null, CancellationToken cancellationToken = default)
    {
        var url = CreateUrl("", query);
        return await SendRequestAsync<ListResponse<ThreadResponse>>(url, HttpMethod.Get, cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<ThreadResponse> ModifyThreadAsync(string threadId, ThreadModifyRequest request, CancellationToken cancellationToken = default)
    {
        var url = CreateUrl($"/{threadId}");
        return await SendRequestAsync<ThreadResponse>(url, HttpMethod.Post, request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<DeletionStatus> DeleteThreadAsync(string threadId, CancellationToken cancellationToken = default)
    {
        var url = CreateUrl($"/{threadId}");
        return await SendRequestAsync<DeletionStatus>(url, HttpMethod.Delete, cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<ThreadResponse> CreateMessageAsync(string threadId, MessageCreateRequest request, CancellationToken cancellationToken = default)
    {
        var url = CreateUrl($"/{threadId}/messages");
        return await SendRequestAsync<ThreadResponse>(url, HttpMethod.Post, request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<ListResponse<MessageResponse>> ListMessagesAsync(string threadId, MessageListQuery? query = null, CancellationToken cancellationToken = default)
    {
        var url = CreateUrl($"/{threadId}/messages", query);
        return await SendRequestAsync<ListResponse<MessageResponse>>(url, HttpMethod.Get, cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<MessageResponse> RetrieveMessageAsync(string threadId, string messageId, CancellationToken cancellationToken = default)
    {
        var url = CreateUrl($"/{threadId}/messages/{messageId}");
        return await SendRequestAsync<MessageResponse>(url, HttpMethod.Get, cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<MessageResponse> ModifyMessageAsync(string threadId, string messageId, MessageModifyRequest request, CancellationToken cancellationToken = default)
    {
        var url = CreateUrl($"/{threadId}/messages/{messageId}");
        return await SendRequestAsync<MessageResponse>(url, HttpMethod.Post, request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<RunResponse> CreateRunAsync(string threadId, RunCreateRequest request, CancellationToken cancellationToken = default)
    {
        var url = CreateUrl($"/{threadId}/runs");
        return await SendRequestAsync<RunResponse>(url, HttpMethod.Post, request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<RunResponse> CreateThreadAndRunAsync(ThreadCreateAndRunRequest request, CancellationToken cancellationToken = default)
    {
        var url = CreateUrl("/runs");
        return await SendRequestAsync<RunResponse>(url, HttpMethod.Post, request, cancellationToken).ConfigureAwait(false);
    }

    public  async Task<ListResponse<RunResponse>> ListRunsAsync(string threadId, ListQuery? query = null, CancellationToken cancellationToken = default)
    {
        var url = CreateUrl($"/{threadId}/runs", query);
        return await SendRequestAsync<ListResponse<RunResponse>>(url, HttpMethod.Get, cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<RunResponse> RetrieveRunAsync(string threadId, string runId, CancellationToken cancellationToken = default)
    {
        var url = CreateUrl($"/{threadId}/runs/{runId}");
        return await SendRequestAsync<RunResponse>(url, HttpMethod.Get, cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<RunResponse> ModifyRunAsync(string threadId, string runId, RunModifyRequest request, CancellationToken cancellationToken = default)
    {
        var url = CreateUrl($"/{threadId}/runs/{runId}");
        return await SendRequestAsync<RunResponse>(url, HttpMethod.Post, request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<RunResponse> SubmitToolOutputsAsync(string threadId, string runId, RunSubmitToolOutputsRequest request, CancellationToken cancellationToken = default)
    {
        var url = CreateUrl($"/{threadId}/runs/{runId}/submit_tool_outputs");
        return await SendRequestAsync<RunResponse>(url, HttpMethod.Post, request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<RunResponse> CancelRunAsync(string threadId, string runId, CancellationToken cancellationToken = default)
    {
        var url = CreateUrl($"/{threadId}/runs/{runId}/cancel");
        return await SendRequestAsync<RunResponse>(url, HttpMethod.Post, cancellationToken: cancellationToken).ConfigureAwait(false);
    }
}
