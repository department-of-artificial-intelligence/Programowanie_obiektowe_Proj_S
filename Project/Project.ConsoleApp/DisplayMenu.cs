namespace Project.ConsoleApp;

internal static class DisplayMenu
{
    internal static void MainMenu()
    {
        Console.WriteLine();
        Console.WriteLine("===== Main Menu =====");
        Console.WriteLine("[1] - wyświetlanie");
        Console.WriteLine("[2] - tworzenie");
        Console.WriteLine("[3] - zarządzanie");
        Console.WriteLine("[x] - zakończ program");
        Console.WriteLine("---------------------");
    }

    internal static void View1()
    {
        Console.WriteLine();
        Console.WriteLine("============== View Menu 1 ==============");
        Console.WriteLine("[1] - wyświetl listę teatrów");
        Console.WriteLine("[2] - wyświetl listę autorów");
        Console.WriteLine("[3] - wyświetl listę reżyserów");
        Console.WriteLine("[4] - wyświetl listę aktorów");
        Console.WriteLine("[5] - wyświetl listę klientów");
        Console.WriteLine("[6] - wyświetl listę sztuk");
        Console.WriteLine("[7] - wyświetl listę przedstawień bez sali");
        Console.WriteLine("[x] - cofnij");
        Console.WriteLine("-----------------------------------------");
    }

    internal static void View1_1()
    {
        Console.WriteLine();
        Console.WriteLine("====== View Menu 1.1 ======");
        Console.WriteLine("[1] - wyświetl listę sali");
        Console.WriteLine("[x] - cofnij");
        Console.WriteLine("---------------------------");
    }

    internal static void View1_1_1()
    {
        Console.WriteLine();
        Console.WriteLine("======== View Menu 1.1.1 ========");
        Console.WriteLine("[1] - wyświetl listę siedzeń");
        Console.WriteLine("[2] - wizualizuj siedzenia");
        Console.WriteLine("[3] - wyświetl listę przedstawień");
        Console.WriteLine("[x] - cofnij");
        Console.WriteLine("---------------------------------");
    }

    internal static void View1_1_1_3()
    {
        Console.WriteLine();
        Console.WriteLine("======= View Menu 1.1.1.1 =======");
        Console.WriteLine("[1] - wyświetl listę biletów");
        Console.WriteLine("[2] - wizualizuj bilety na sali");
        Console.WriteLine("[x] - cofnij");
        Console.WriteLine("---------------------------------");
    }

    internal static void View1_2()
    {
        Console.WriteLine();
        Console.WriteLine("========= View Menu 1.2 =========");
        Console.WriteLine("[1] - wyświetl listę sztuk autora");
        Console.WriteLine("[x] - cofnij");
        Console.WriteLine("---------------------------------");
    }

    internal static void View1_3()
    {
        Console.WriteLine();
        Console.WriteLine("========== View Menu 1.3 ==========");
        Console.WriteLine("[1] - wyświetl listę sztuk reżysera");
        Console.WriteLine("[x] - cofnij");
        Console.WriteLine("-----------------------------------");
    }

    internal static void View1_4()
    {
        Console.WriteLine();
        Console.WriteLine("========= View Menu 1.4 =========");
        Console.WriteLine("[1] - wyświetl listę sztuk aktora");
        Console.WriteLine("[x] - cofnij");
        Console.WriteLine("---------------------------------");
    }

    internal static void View1_5()
    {
        Console.WriteLine();
        Console.WriteLine("========== View Menu 1.5 ==========");
        Console.WriteLine("[1] - wyświetl listę biletów klienta");
        Console.WriteLine("[x] - cofnij");
        Console.WriteLine("-----------------------------------");
    }

    internal static void Creation2()
    {
        Console.WriteLine();
        Console.WriteLine("======= Creation Menu 2 =======");
        Console.WriteLine("[1] - tworzenie struktury teatru");
        Console.WriteLine("[2] - tworzenie przedstawień");
        Console.WriteLine("[3] - tworzenie osób");
        Console.WriteLine("[x] - cofnij");
        Console.WriteLine("-------------------------------");
    }

    internal static void Creation2_1()
    {
        Console.WriteLine();
        Console.WriteLine("============== Creation Menu 2.1 ==============");
        Console.WriteLine("[1] - stwórz teatr");
        Console.WriteLine("[2] - stwórz salę");
        Console.WriteLine("[3] - stwórz siedzenie");
        Console.WriteLine("[4] - stwórz siedzenia sali o podanych wymiarach");
        Console.WriteLine("[x] - cofnij");
        Console.WriteLine("-----------------------------------------------");
    }

    internal static void Creation2_2()
    {
        Console.WriteLine();
        Console.WriteLine("=========== Creation Menu 2.2 ===========");
        Console.WriteLine("[1] - stwórz sztukę");
        Console.WriteLine("[2] - stwórz przedstawienie");
        Console.WriteLine("[3] - stwórz bilet");
        Console.WriteLine("[4] - stwórz bilety dla wszystkich siedzeń");
        Console.WriteLine("[x] - cofnij");
        Console.WriteLine("-----------------------------------------");
    }

    internal static void Creation2_3()
    {
        Console.WriteLine();
        Console.WriteLine("==== Creation Menu 2.3 ====");
        Console.WriteLine("[1] - stwórz autora");
        Console.WriteLine("[2] - stwórz reżysera");
        Console.WriteLine("[3] - stwórz aktora");
        Console.WriteLine("[4] - stwórz klienta");
        Console.WriteLine("[x] - cofnij");
        Console.WriteLine("---------------------------");
    }

    internal static void Management3()
    {
        Console.WriteLine();
        Console.WriteLine("===== Management Menu 3 =====");
        Console.WriteLine("[1] - zarządzaj klientem");
        Console.WriteLine("[2] - zarządzaj autorem");
        Console.WriteLine("[3] - zarządzaj reżyserem");
        Console.WriteLine("[4] - zarządzaj aktorem");
        Console.WriteLine("[5] - zarządzaj przedstawieniem");
        Console.WriteLine("[x] - cofnij");
        Console.WriteLine("-----------------------------");
    }

    internal static void Management3_1()
    {
        Console.WriteLine();
        Console.WriteLine("======= Management Menu 3.1 =======");
        Console.WriteLine("[0] - pokaż listę posiadanych biletów");
        Console.WriteLine("[1] - zarezerwuj bilet");
        Console.WriteLine("[2] - anuluj rezerwację");
        Console.WriteLine("[3] - anuluj wszystkie rezerwacje");
        Console.WriteLine("[4] - kup dostępny bilet");
        Console.WriteLine("[5] - kup zarezerwowany bilet");
        Console.WriteLine("[6] - kup wszystkie zarezerwowane");
        Console.WriteLine("[7] - zwróć bilet");
        Console.WriteLine("[8] - zwróć wszystkie kupione");
        Console.WriteLine("[x] - cofnij");
        Console.WriteLine("---------------------------------");
    }

    internal static void Management3_2()
    {
        Console.WriteLine();
        Console.WriteLine("========== Management Menu 3.2 ==========");
        Console.WriteLine("[0] - pokaż listę sztuk autora");
        Console.WriteLine("[1] - dodaj sztukę do listy autora");
        Console.WriteLine("[2] - usuń sztukę z listy autora");
        Console.WriteLine("[3] - usuń wszystkie sztuki z listy autora");
        Console.WriteLine("[x] - cofnij");
        Console.WriteLine("-----------------------------------------");
    }

    internal static void Management3_3()
    {
        Console.WriteLine();
        Console.WriteLine("=========== Management Menu 3.3 ===========");
        Console.WriteLine("[0] - pokaż listę sztuk reżysera");
        Console.WriteLine("[1] - dodaj sztukę do listy reżysera");
        Console.WriteLine("[2] - usuń sztukę z listy reżysera");
        Console.WriteLine("[3] - usuń wszystkie sztuki z listy reżysera");
        Console.WriteLine("[x] - cofnij");
        Console.WriteLine("-------------------------------------------");
    }

    internal static void Management3_4()
    {
        Console.WriteLine();
        Console.WriteLine("========== Management Menu 3.4 ==========");
        Console.WriteLine("[0] - pokaż listę sztuk aktora");
        Console.WriteLine("[1] - dodaj sztukę do listy aktora");
        Console.WriteLine("[2] - usuń sztukę z listy aktora");
        Console.WriteLine("[3] - usuń wszystkie sztuki z listy aktora");
        Console.WriteLine("[x] - cofnij");
        Console.WriteLine("-----------------------------------------");
    }

    internal static void Management3_5()
    {
        Console.WriteLine();
        Console.WriteLine("======== Management Menu 3.5 ========");
        Console.WriteLine("[1] - dodaj przedstawienie do sali");
        Console.WriteLine("[2] - odwołaj przedstawienie");
        Console.WriteLine("[x] - cofnij");
        Console.WriteLine("-------------------------------------");
    }
}
