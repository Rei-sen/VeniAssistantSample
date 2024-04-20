using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI.Common;

public enum ContentType
{
    [EnumMember(Value = "image_file")]
    ImageFile,
    [EnumMember(Value = "text")]
    Text,
}
