using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace OpenAI.Threads;

public enum RunStatus
{
    [EnumMember(Value = "queued")]
    Queued,
    [EnumMember(Value = "in_progress")]
    InProgress,
    [EnumMember(Value = "requires_action")]
    RequresAction,
    [EnumMember(Value = "cancelling")]
    Cancelling,
    [EnumMember(Value = "cancelled")]
    Cancelled,
    [EnumMember(Value = "failed")]
    Failed,
    [EnumMember(Value = "completed")]
    Completed,
    [EnumMember(Value = "expired")]
    Expired
}
