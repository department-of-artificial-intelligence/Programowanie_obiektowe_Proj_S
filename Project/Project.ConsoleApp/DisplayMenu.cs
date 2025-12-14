namespace Project.ConsoleApp;

internal static class DisplayMenu
{
    internal static void MainMenu()
    {
        Console.WriteLine();
        Console.WriteLine("===== Main Menu =====");
        Console.WriteLine("1 - wyświetl");
        Console.WriteLine("2 - stwórz");
        Console.WriteLine("x - zakończ program");
        Console.WriteLine("---------------------");
    }

    internal static void View1()
    {
        Console.WriteLine();
        Console.WriteLine("============== View Menu 1 ==============");
        Console.WriteLine("1 - wyświetl listę teatrów"); // -> wyświetl listę sali -> wywietl listę siedzeń / wyświetl wizualizację siedzeń / (wyświetl listę przedstawień -> wyświetl listę biletów)
        Console.WriteLine("2 - wyświetl listę autorów"); // -> wyświetl listę sztuk autora
        Console.WriteLine("3 - wyświetl listę reżyserów"); // -> wyświetl listę sztuk reżysera
        Console.WriteLine("4 - wyświetl listę aktorów"); // -> wyświetl listę sztuk aktora
        Console.WriteLine("5 - wyświetl listę klientów"); // -> wyświetl listę biletów klienta
        Console.WriteLine("6 - wyświetl listę sztuk");
        Console.WriteLine("7 - wyświetl listę przedstawień bez sali");
        Console.WriteLine("x - cofnij");
        Console.WriteLine("-----------------------------------------");
    }

    internal static void View1_1()
    {
        Console.WriteLine();
        Console.WriteLine("====== View Menu 1.1 ======");
        Console.WriteLine("1 - wyświetl listę sali"); // wybierz salę
        Console.WriteLine("x - cofnij");
        Console.WriteLine("---------------------------");
    }

    internal static void View1_1_1()
    {
        Console.WriteLine();
        Console.WriteLine("======== View Menu 1.1.1 ========");
        Console.WriteLine("1 - wyświetl listę siedzeń");
        Console.WriteLine("2 - wizualizuj siedzenia");
        Console.WriteLine("3 - wyświetl listę przedstawień"); // wybierz przedstawienie
        Console.WriteLine("x - cofnij");
        Console.WriteLine("---------------------------------");
    }

    internal static void View1_1_1_3()
    {
        Console.WriteLine();
        Console.WriteLine("======= View Menu 1.1.1.1 =======");
        Console.WriteLine("1 - wyświetl listę biletów");
        Console.WriteLine("2 - wizualizuj bilety na sali");
        Console.WriteLine("x - cofnij");
        Console.WriteLine("---------------------------------");
    }

    internal static void View1_2()
    {
        Console.WriteLine();
        Console.WriteLine("========= View Menu 1.2 =========");
        Console.WriteLine("1 - wyświetl listę sztuk autora"); 
        Console.WriteLine("x - cofnij");
        Console.WriteLine("---------------------------------");
    }

    internal static void View1_3()
    {
        Console.WriteLine();
        Console.WriteLine("========== View Menu 1.3 ==========");
        Console.WriteLine("1 - wyświetl listę sztuk reżysera");
        Console.WriteLine("x - cofnij");
        Console.WriteLine("-----------------------------------");
    }

    internal static void View1_4()
    {
        Console.WriteLine();
        Console.WriteLine("========= View Menu 1.4 =========");
        Console.WriteLine("1 - wyświetl listę sztuk aktora");
        Console.WriteLine("x - cofnij");
        Console.WriteLine("---------------------------------");
    }

    internal static void View1_5()
    {
        Console.WriteLine();
        Console.WriteLine("========== View Menu 1.5 ==========");
        Console.WriteLine("1 - wyświetl listę biletów klienta");
        Console.WriteLine("x - cofnij");
        Console.WriteLine("-----------------------------------");
    }

    internal static void Creation2()
    {
        Console.WriteLine();
        Console.WriteLine("======= Creation Menu 2 =======");
        Console.WriteLine("1 - tworzenie struktury teatru");
        Console.WriteLine("2 - tworzenie przedstawień");
        Console.WriteLine("3 - tworzenie osób");
        Console.WriteLine("x - cofnij");
        Console.WriteLine("-------------------------------");
    }

    internal static void Creation2_1()
    {
        Console.WriteLine();
        Console.WriteLine("============== Creation Menu 2.1 =============="); // struktura teatru
        Console.WriteLine("1 - stwórz teatr"); // potrzebne: sieć
        Console.WriteLine("2 - stwórz salę"); // potrzebne: sieć->teatr
        Console.WriteLine("3 - stwórz siedzenie"); // potrzebne: sieć->teatr->sala->siedzenia
        Console.WriteLine("4 - stwórz siedzenia sali o podanych wymiarach"); // potrzebne: sieć->teatr->sala->siedzenia
        Console.WriteLine("x - cofnij");
        Console.WriteLine("-----------------------------------------------");
    }

    internal static void Creation2_2()
    {
        Console.WriteLine();
        Console.WriteLine("=========== Creation Menu 2.2 ==========="); // przedstawienia
        Console.WriteLine("1 - stwórz sztukę"); // potrzebne: autor, reżyser
        Console.WriteLine("2 - stwórz przedstawienie"); // potrzebne: przedstawienie
        Console.WriteLine("3 - stwórz bilet"); // potrzebne: sala->siedzenie, przedstawienie
        Console.WriteLine("4 - stwórz bilety dla wszystkich siedzeń"); // potrzebne: sala->siedzenie, przedstawienie
        Console.WriteLine("x - cofnij");
        Console.WriteLine("-----------------------------------------");
    }

    internal static void Creation2_3()
    {
        Console.WriteLine();
        Console.WriteLine("==== Creation Menu 2.3 ===="); // osoby
        Console.WriteLine("1 - stwórz autora");
        Console.WriteLine("2 - stwórz reżysera");
        Console.WriteLine("3 - stwórz aktora");
        Console.WriteLine("4 - stwórz klienta");
        Console.WriteLine("x - cofnij");
        Console.WriteLine("---------------------------");
    }
}