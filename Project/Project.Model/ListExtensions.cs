using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model;

public static class ListExtensions
{
    public static string ListToString<T> (this List<T> list, string emptyMessage = "list is empty", char? bullet = '-') where T : class
    {
        if (list.Count == 0) return $"{emptyMessage}";
        return string.Join("\n", list.Select(x => $"{bullet} {x}"));
    }
}
