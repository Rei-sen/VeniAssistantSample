﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VeniAssistantSample.Models;

internal record AssistantListRequest
{
    [JsonPropertyName("limit")]
    public int? Limit { get; set; }
    [JsonPropertyName("order")]
    public string? Order { get; set; }
    [JsonPropertyName("after")]
    public string? After { get; set; }

    [JsonPropertyName("before")]
    public string? Before { get; set; }
}
