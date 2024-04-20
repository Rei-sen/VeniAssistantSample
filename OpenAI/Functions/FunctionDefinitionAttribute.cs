using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI.Functions;

[AttributeUsage(AttributeTargets.Method)]
public class FunctionDefinitionAttribute : Attribute
{
    public string? Name { get; }
    public string? Description { get; }

    public FunctionDefinitionAttribute(string? name = null, string? description = null)
    {
        Name = name;
        Description = description;
    }
}
