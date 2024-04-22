using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAI.Error;

public class Error
{
    [JsonPropertyName("code")]
    public required ErrorCode Code { get; set; }
    [JsonPropertyName("message")]
    public required string Message { get; set; }

}
