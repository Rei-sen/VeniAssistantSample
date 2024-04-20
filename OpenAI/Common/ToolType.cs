using System.Runtime.Serialization;

namespace OpenAI.Common;

public enum ToolType
{
    [EnumMember(Value = "code_interpreter")]
    CodeInterpreter,
    [EnumMember(Value = "file_search")]
    FileSearch,
    [EnumMember(Value = "function")]
    Function
}