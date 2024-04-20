using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI.Functions;

[AttributeUsage(AttributeTargets.Parameter)]
public class FunctionParameterAttribute : Attribute
{
    public string Description { get; set; }

    public FunctionParameterAttribute(string description)
    {
        Description = description;
    }
}
