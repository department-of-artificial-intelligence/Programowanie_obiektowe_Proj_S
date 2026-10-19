namespace Shelter.ConsoleApp;
using Shelter.Model;

internal class Program
{
    static void Main(string[] args)
    {
        Shelter a = new Shelter();
        a.Id = 1;
        a.Address = "Bababaababababababavbabab";
        Console.WriteLine($"{a.Id}, {a.Address}");
    }
}
