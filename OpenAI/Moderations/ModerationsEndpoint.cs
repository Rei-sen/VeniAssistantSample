using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI.Moderations;

public class ModerationsEndpoint : BaseEndpoint
{
    protected override string Root => "moderations";
    public ModerationsEndpoint(OpenAIClient client) : base(client)
    {
    }

    public async Task<ModerationCreateResponse> CreateModerationAsync(ModerationCreateRequest request)
    {
        var url = CreateUrl("");
        return await SendRequestAsync<ModerationCreateResponse>(url, HttpMethod.Post, request).ConfigureAwait(false);
    }
}
