using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ConsoleApp;

internal static class Menu
{
    internal static void StartOptions()
    {
        Console.WriteLine();
        Console.WriteLine("[1] - tworzenie");
        // CreationOptions
        Console.WriteLine("[2] - zarządzanie");
        // ManagingOptions
        Console.WriteLine("[3] - wyświetlanie");
        Console.WriteLine("[x] - zakończ program");
    }
    internal static void CreationOptions()
    {
        Console.WriteLine();
        Console.WriteLine("[1] - tworzenie teatrów");
        // theater, hall, seat
        Console.WriteLine("[2] - tworzenie osób");
        // director, actor, author, customer
        Console.WriteLine("[3] - tworzenie sztuk");
        // play
        Console.WriteLine("[4] - tworzenie przedstawień");
        // performance
        Console.WriteLine("[5] - tworzenie biletów");
        // ticket
        Console.WriteLine("[x] - cofnij");
    }
    internal static void TheaterCreationOptions()
    {
        Console.WriteLine();
        Console.WriteLine("[1] - stwórz teatr");
        Console.WriteLine("[2] - stwórz salę");
        Console.WriteLine("[3] - stwórz siedzenia");
        Console.WriteLine("[x] - cofnij");
    }

}
