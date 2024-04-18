using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeniAssistantSample.Utilities;

internal class GETQuery
{

    public static string SerializeObjectToQueryString(object obj)
    {
        var properties = obj.GetType().GetProperties();
        var query = new StringBuilder();

        foreach (var property in properties)
        {
            
            var propertyName = property.Name;
            var propertyValue = property.GetValue(obj)?.ToString();
            if (propertyValue is null)
                continue;

            var encodedName = Uri.EscapeDataString(propertyName);
            var encodedValue = Uri.EscapeDataString(propertyValue);

            query.Append($"{encodedName}={encodedValue}&");
        }
        
        if (query.Length > 0)
        {
            query.Remove(query.Length - 1, 1);
        }

        return query.ToString();
    }
}
