using System;
using Project.Model;

public class Program
{
    public static void Main()
    {
        var worker = new Worker("Mykhailo", "Lytvyn", 6500, "Cook");

        Console.WriteLine(worker.GetInfo());
    }
}