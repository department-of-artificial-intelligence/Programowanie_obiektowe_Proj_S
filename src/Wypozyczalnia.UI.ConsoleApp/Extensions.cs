using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wypozyczalnia.Model;

namespace Wypozyczalnia.UI.ConsoleApp;

public static class Extensions {
    public static void Print(this IPrintable printer) {
        Console.WriteLine(printer);
    }
    public static void Compute<T>(this List<T> list, int a, int b) {
        Console.WriteLine($"Lista ma {list.Count} elementow, a + b = {a + b}");
    }
}

