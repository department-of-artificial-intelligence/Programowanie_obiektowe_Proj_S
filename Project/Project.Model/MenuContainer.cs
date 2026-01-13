using System;

namespace Project.Model
{
    public class MenuContainer
    {

        public void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Witamy w Systemie Sieci Sklepów ElectroHub!!!");
            Console.WriteLine("================ MENU GŁÓWNE ================");
            Console.WriteLine("----- 1. Zaloguj się jako Klient ------------");
            Console.WriteLine("----- 2. Zaloguj się jako Administrator -----");
            Console.WriteLine("----- 3. Zarejestruj się (Nowy Klient) ------"); 
            Console.WriteLine("----- 4. Wybierz lokalizację sklepu ---------");    
            Console.WriteLine("----- 5. O ElectroHub (Kontakt) -------------");       
            Console.WriteLine("------------ 0. Wyjście ---------------------");
            Console.WriteLine("=============================================");
            Console.Write("Wybierz opcję: ");
        }


        public void ShowClientMenu()
        {
            Console.Clear();
            Console.WriteLine("=== PANEL KLIENTA - ElectroHub ===");
            Console.WriteLine("1. Przeglądaj dostępne produkty");
            Console.WriteLine("2. Wyszukaj produkt po nazwie");
            Console.WriteLine("3. Dodaj produkt do koszyka");
            Console.WriteLine("4. Pokaż mój koszyk i podsumowanie");
            Console.WriteLine("5. Zrealizuj zamówienie (Płatność)");
            Console.WriteLine("6. Historia moich zakupów");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("0. Wyloguj i wróć do Menu Głównego");
            Console.Write("Wybierz opcję: ");
        }


        public void ShowStoreSelectionHeader()
        {
            Console.Clear();
            Console.WriteLine("=== WYBÓR PLACÓWKI ELECTROHUB ===");
            Console.WriteLine("Wybierz sklep z poniższej listy, aby kontynuować:");
            Console.WriteLine("-----------------------------------------------");
        }


        public void ShowAdminMenu()
        {
            
            Console.WriteLine("=== PANEL ADMINISTRATORA - ElectroHub ===");
            Console.WriteLine("======== ZARZĄDZANIE ASORTYMENTEM =======");
            Console.WriteLine("1. Dodaj nowy produkt");
            Console.WriteLine("2. Edytuj dane produktu (cena, opis)");
            Console.WriteLine("3. Usuń produkt z oferty");
            Console.WriteLine("4. Aktualizuj stany magazynowe");
            Console.WriteLine("=========== ZARZĄDZANIE KADRĄ ==========="); 
            Console.WriteLine("5. Dodaj nowego pracownika");
            Console.WriteLine("6. Wyświetl listę pracowników");
            Console.WriteLine("7. Zmień uprawnienia/rolę pracownika");
            Console.WriteLine("8. Usuń pracownika z systemu");
            Console.WriteLine("======= ZARZĄDZANIE UŻYTKOWNIKAMI =======");
            Console.WriteLine("9. Wyświetl listę wszystkich klientów");
            Console.WriteLine("10. Zablokuj/Usuń konto użytkownika");
            Console.WriteLine("=========== RAPORTY I FINANSE ===========");
            Console.WriteLine("11. Wyświetl historię wszystkich zamówień");
            Console.WriteLine("12. Pokaż całkowity przychód sklepu");
            Console.WriteLine("=========================================");
            Console.WriteLine("0. Wyloguj i wróć do Menu Głównego");
            Console.Write("Wybierz opcję: ");
        }


        public void ShowAboutUs()
        {
            Console.Clear();
            Console.WriteLine("=================== O SIECI SKLEPÓW ELECTROHUB ===============================");
            Console.WriteLine("ElectroHub to lider sprzedaży nowoczesnej elektroniki w Polsce i całej Europy.");
            Console.WriteLine("Działamy od 2020 roku, dostarczając sprzęt najwyższej jakości");
            Console.WriteLine("zarówno dla pasjonatów technologii, jak i profesjonalistów.");
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine("KONTAKT:");
            Console.WriteLine("Infolinia: +48 123 456 789 (pon.- pt. 8:00 - 16:00)");
            Console.WriteLine("E-mail:    kontakt@electrohub.pl");
            Console.WriteLine("WWW:       www.electrohub.pl");
            Console.WriteLine("Siedziba:  ul. Cyfrowa 10, 00-001 Warszawa");
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine("Naciśnij dowolny klawisz, aby wrócić do Menu Głównego...");
            Console.ReadKey();
        }



    }
}