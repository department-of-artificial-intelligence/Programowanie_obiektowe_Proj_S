// See https://aka.ms/new-console-template for more information

using Projekt.Model;

public class Program
{
    public static void Main()
    {
        Vehicle auto1 = new Car("Skoda", "Octavia", 208, 1.6, 2012, "benzyna", "sc9999", 5, "kombi");
        Vehicle ciezarowka1 = new Truck("Scania", "R500", 500, 13.0, 2019, "diesel", "wz9999", 30, 3);
        Console.WriteLine(auto1);
        Console.WriteLine(ciezarowka1);
    }
}

