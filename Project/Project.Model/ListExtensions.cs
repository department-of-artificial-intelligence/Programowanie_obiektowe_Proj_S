using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model;

public static class ListExtensions
{
    public static string ListToString<T> (this IList<T> list, string? emptyMessage = null, char? bullet = null)
    {
        if (list.Count == 0) 
            return emptyMessage is null ? "list is empty" : $"{emptyMessage}";
        return string.Join("\n", list.Select(x => bullet is null ? $"{x}" : $"{bullet} {x}"));
    }
}
