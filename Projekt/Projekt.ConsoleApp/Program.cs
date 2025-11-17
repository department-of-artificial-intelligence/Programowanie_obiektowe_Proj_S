using Projekt.Model;

public class Program
{
    static Management manager = new Management();
    public static void Main(string[] args)
    {
        ZaladujDaneTestowe();
        bool dziala = true;
        while (dziala)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=============================================");
            Console.WriteLine("   SYSTEM ZARZĄDZANIA FLOTĄ (C# Project)     ");
            Console.WriteLine("=============================================");
            Console.ResetColor();
            Console.WriteLine(" 1. Pokaż wszystkie pojazdy");
            Console.WriteLine(" 2. Znajdź pojazd po rejestracji");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine(" 3. Dodaj nowy SAMOCHÓD OSOBOWY");
            Console.WriteLine(" 4. Dodaj nową CIĘŻARÓWKĘ");
            Console.WriteLine(" 5. Dodaj nowy MOTOCYKL");
            Console.WriteLine(" 6. Usuń pojazd");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine(" 7. Przypisz KIEROWCĘ do pojazdu");
            Console.WriteLine(" 8. Dodaj WPIS SERWISOWY");
            Console.WriteLine(" 9. Pokaż historię serwisu pojazdu");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine(" 0. WYJŚCIE");
            Console.WriteLine("=============================================");
            Console.Write(" Twój wybór: ");

            string wybor = Console.ReadLine();

            switch (wybor)
            {
                case "1":
                    manager.PokazWszystkie();
                    Czekaj();
                    break;
                case "2":
                    WyszukajPojazdUI();
                    break;
                case "3":
                    DodajSamochodUI();
                    break;
                case "4":
                    DodajCiezarowkeUI();
                    break;
                case "5":
                    DodajMotocyklUI();
                    break;
                case "6":
                    UsunPojazdUI();
                    break;
                case "7":
                    PrzypiszKierowceUI();
                    break;
                case "8":
                    DodajSerwisUI();
                    break;
                case "9":
                    PokazSerwisUI();
                    break;
                case "0":
                    dziala = false;
                    Console.WriteLine("Zamykanie...");
                    break;
                default:
                    KomunikatBlad("Nieznana opcja!");
                    break;
            }
        }
    }
    static void WyszukajPojazdUI()
    {
        Console.WriteLine("\n--- WYSZUKIWANIE ---");
        string tablica = PobierzTekst("Podaj numer rejestracyjny");
        var pojazd = manager.ZnajdzPojazdPoRejestracji(tablica);

        if (pojazd != null)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("ZNALEZIONO:");
            Console.WriteLine(pojazd);
            Console.ResetColor();
        }
        Czekaj();
    }

    static void DodajSamochodUI()
    {
        Console.WriteLine("\n--- DODAWANIE SAMOCHODU OSOBOWEGO ---");
        string marka = PobierzTekst("Marka");
        string model = PobierzTekst("Model");
        int rocznik = PobierzInt("Rocznik");
        string tablica = PobierzTekst("Rejestracja");
        int przebieg = PobierzInt("Przebieg (km)");
        double silnik = PobierzDouble("Pojemność silnika (np. 2.0)");
        string paliwo = PobierzTekst("Rodzaj paliwa (Benzyna/Diesel/Elektryk)");
        int drzwi = PobierzInt("Liczba drzwi");
        string nadwozie = PobierzTekst("Typ nadwozia (Sedan/Kombi)");
        Car noweAuto = new Car(marka, model, przebieg, silnik, rocznik, paliwo, tablica, drzwi, nadwozie);
        if (manager.DodajPojazd(noweAuto))
        {
            KomunikatSukces("Pojazd dodany pomyślnie!");
        }
        Czekaj();
    }
    static void DodajCiezarowkeUI()
    {
        Console.WriteLine("\n--- DODAWANIE CIĘŻARÓWKI ---");
        string marka = PobierzTekst("Marka");
        string model = PobierzTekst("Model");
        int rocznik = PobierzInt("Rocznik");
        string tablica = PobierzTekst("Rejestracja");
        int przebieg = PobierzInt("Przebieg (km)");
        double silnik = PobierzDouble("Pojemność silnika");
        string paliwo = PobierzTekst("Paliwo");

        double ladownosc = PobierzDouble("Ładowność (tony)");
        int osie = PobierzInt("Liczba osi");

        Truck truck = new Truck(marka, model, przebieg, silnik, rocznik, paliwo, tablica, ladownosc, osie);

        if (manager.DodajPojazd(truck))
        {
            KomunikatSukces("Ciężarówka dodana!");
        }
        Czekaj();
    }
    static void DodajMotocyklUI()
    {
        Console.WriteLine("\n--- DODAWANIE MOTOCYKLA ---");
        string marka = PobierzTekst("Marka");
        string model = PobierzTekst("Model");
        int rocznik = PobierzInt("Rocznik");
        string tablica = PobierzTekst("Rejestracja");
        int przebieg = PobierzInt("Przebieg (km)");
        double silnik = PobierzDouble("Silnik (L)");
        string paliwo = PobierzTekst("Paliwo");

        int cm3 = PobierzInt("Pojemność (cm3)");
        string rama = PobierzTekst("Typ ramy (Sport/Turystyk)");
        Motorbike moto = new Motorbike(marka, model, przebieg, silnik, rocznik, paliwo, tablica, cm3, rama);
        if (manager.DodajPojazd(moto))
        {
            KomunikatSukces("Motocykl dodany!");
        }
        Czekaj();
    }
    static void UsunPojazdUI()
    {
        Console.WriteLine("\n--- USUWANIE POJAZDU ---");
        string tablica = PobierzTekst("Podaj rejestrację do usunięcia");
        if (manager.UsunPojazd(tablica))
        {
            KomunikatSukces("Usunięto!");
        }
        Czekaj();
    }
    static void PrzypiszKierowceUI()
    {
        Console.WriteLine("\n--- PRZYPISYWANIE KIEROWCY ---");
        string tablica = PobierzTekst("Podaj rejestrację pojazdu");
        var pojazd = manager.ZnajdzPojazdPoRejestracji(tablica);
        if (pojazd == null)
        {
            Czekaj();
            return;
        }
        Console.WriteLine("Podaj dane kierowcy:");
        string imie = PobierzTekst("Imię");
        string nazwisko = PobierzTekst("Nazwisko");
        string dowod = PobierzTekst("Nr dokumentu");
        Driver nowyKierowca = new Driver(imie, nazwisko, dowod);
        if (manager.PrzypiszKierowceDoPojazdu(tablica, nowyKierowca))
        {
            KomunikatSukces("Kierowca przypisany!");
        }
        Czekaj();
    }
    static void DodajSerwisUI()
    {
        Console.WriteLine("\n--- DODAWANIE SERWISU ---");
        string tablica = PobierzTekst("Podaj rejestrację pojazdu");
        string opis = PobierzTekst("Opis naprawy/usterki");
        double koszt = PobierzDouble("Koszt naprawy (PLN)");
        if (manager.DodajWpisSerwisowy(tablica, opis, koszt))
        {
            KomunikatSukces("Wpis dodany do historii!");
        }
        else
        {
            KomunikatBlad("Nie udało się dodać wpisu (sprawdź rejestrację).");
        }
        Czekaj();
    }
    static void PokazSerwisUI()
    {
        Console.WriteLine("\n--- HISTORIA SERWISOWA ---");
        string tablica = PobierzTekst("Podaj rejestrację pojazdu");
        manager.PokazSerwisPojazdu(tablica);
        Czekaj();
    }
    static string PobierzTekst(string pytanie)
    {
        Console.Write($"{pytanie}: ");
        string tekst = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(tekst))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Puste pole! Spróbuj ponownie: ");
            Console.ResetColor();
            tekst = Console.ReadLine();
        }
        return tekst;
    }
    static int PobierzInt(string pytanie)
    {
        Console.Write($"{pytanie}: ");
        int wynik;
        while (!int.TryParse(Console.ReadLine(), out wynik))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("To nie jest liczba! Wpisz ponownie: ");
            Console.ResetColor();
        }
        return wynik;
    }
    static double PobierzDouble(string pytanie)
    {
        Console.Write($"{pytanie}: ");
        double wynik;
        while (!double.TryParse(Console.ReadLine(), out wynik))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("To nie jest poprawna liczba! Wpisz ponownie: ");
            Console.ResetColor();
        }
        return wynik;
    }
    static void Czekaj()
    {
        Console.WriteLine("\nNaciśnij ENTER, aby kontynuować...");
        Console.ReadLine();
    }
    static void KomunikatSukces(string wiadomosc)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"SUKCES: {wiadomosc}");
        Console.ResetColor();
    }
    static void KomunikatBlad(string wiadomosc)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"BŁĄD: {wiadomosc}");
        Console.ResetColor();
    }
    static void ZaladujDaneTestowe()
    {
        manager.DodajPojazd(new Car("Toyota", "Corolla", 150000, 1.6, 2019, "Benzyna", "WA 12345", 4, "Sedan"));
        manager.DodajPojazd(new Truck("Volvo", "FH", 450000, 12.8, 2021, "Diesel", "WGM TRUCK", 24.0, 3));
        manager.DodajPojazd(new Motorbike("Yamaha", "MT-07", 12000, 0.7, 2023, "Benzyna", "KR MOTO", 689, "Naked"));
    }
}

