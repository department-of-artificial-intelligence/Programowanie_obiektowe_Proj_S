namespace Project.ConsoleApp;

internal static class DisplayMenu
{
    internal static void MainMenu()
    {
        Console.WriteLine();
        Console.WriteLine("===== Main Menu =====");
        Console.WriteLine("1 - wyświetl");
        Console.WriteLine("x - zakończ program");
        Console.WriteLine("---------------------");
    }

    internal static void Display1()
    {
        Console.WriteLine();
        Console.WriteLine("======= Display Menu1 =======");
        Console.WriteLine("1 - wyświetl listę teatrów"); // -> wyświetl listę sali -> wywietl listę siedzeń || wyświetl wizualizację siedzeń || (wyświetl listę przedstawień -> wyświetl listę biletów)
        Console.WriteLine("2 - wyświetl listę autorów"); // -> wyświetl listę przedstawień autora
        Console.WriteLine("3 - wyświetl listę reżyserów"); // -> wyświetl listę przedstawień reżysera
        Console.WriteLine("4 - wyświetl listę aktorów"); // -> wyświetl listę przedstawień aktora
        Console.WriteLine("5 - wyświetl listę klientów"); // -> wyświetl listę biletów klienta
        Console.WriteLine("x - cofnij");
        Console.WriteLine("-----------------------------");
    }

    internal static void Display1_1()
    {
        Console.WriteLine();
        Console.WriteLine("===== Display Menu1.1 =====");
        Console.WriteLine("1 - wyświetl listę sali"); // wybierz salę
        Console.WriteLine("x - cofnij");
        Console.WriteLine("---------------------------");
    }

    internal static void Display1_1_1()
    {
        Console.WriteLine();
        Console.WriteLine("====== Display Menu1.1.1 ======");
        Console.WriteLine("1 - wyświetl listę siedzeń");
        Console.WriteLine("2 - wizualizuj siedzenia");
        Console.WriteLine("3 - wyświetl listę przedstawień"); // wybierz przedstawienie
        Console.WriteLine("x - cofnij");
        Console.WriteLine("-------------------------------");
    }

    internal static void Display1_1_1_3()
    {
        Console.WriteLine();
        Console.WriteLine("===== Display Menu1.1.1.1 =====");
        Console.WriteLine("1 - wyświetl listę biletów");
        Console.WriteLine("2 - wizualizuj bilety na sali");
        Console.WriteLine("x - cofnij");
        Console.WriteLine("-------------------------------");
    }

    internal static void Display1_2()
    {
        Console.WriteLine();
        Console.WriteLine("======= Display Menu1.2 =======");
        Console.WriteLine("1 - wyświetl listę sztuk autora"); 
        Console.WriteLine("x - cofnij");
        Console.WriteLine("-------------------------------");
    }

    internal static void Display1_3()
    {
        Console.WriteLine();
        Console.WriteLine("======== Display Menu1.3 ========");
        Console.WriteLine("1 - wyświetl listę sztuk reżysera");
        Console.WriteLine("x - cofnij");
        Console.WriteLine("---------------------------------");
    }

    internal static void Display1_4()
    {
        Console.WriteLine();
        Console.WriteLine("======== Display Menu1.4 ========");
        Console.WriteLine("1 - wyświetl listę sztuk aktora");
        Console.WriteLine("x - cofnij");
        Console.WriteLine("---------------------------------");
    }

    internal static void Display1_5()
    {
        Console.WriteLine();
        Console.WriteLine("========= Display Menu1.5 =========");
        Console.WriteLine("1 - wyświetl listę biletów klienta");
        Console.WriteLine("x - cofnij");
        Console.WriteLine("-----------------------------------");
    }
}