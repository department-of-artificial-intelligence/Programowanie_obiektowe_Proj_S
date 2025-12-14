using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Projekt.DAL;
using Projekt.Model;
using Projekt.ConsoleApp;
using System;

class Program
{
    static void Main(string[] args)
    {
        IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                var connectionString = context.Configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString));
            })
            .Build();
        using (var scope = host.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            Management manager = new Management(context);
            bool dziala = true;
            while (dziala)
            {
                Console.Clear();
                Console.WriteLine("=== MENU GŁÓWNE BAZY POJAZDÓW ===");
                Console.WriteLine("1. Dodaj SAMOCHÓD OSOBOWY");
                Console.WriteLine("2. Dodaj CIĘŻARÓWKĘ");
                Console.WriteLine("3. Dodaj MOTOCYKL");
                Console.WriteLine("4. Pokaż wszystkie pojazdy");
                Console.WriteLine("5. Usuń pojazd po tablicy");
                Console.WriteLine("6. Dodaj wpis serwisowy");
                Console.WriteLine("7. Pokaż historię serwisową");
                Console.WriteLine("0. Wyjdź");
                Console.Write("\nWybierz opcję: ");
                string wybor = Console.ReadLine();
                try
                {
                    switch (wybor)
                    {
                        case "1": DodajSamochodInteraktywnie(manager); break;
                        case "2": DodajCiezarowkeInteraktywnie(manager); break;
                        case "3": DodajMotocyklInteraktywnie(manager); break;
                        case "4":
                            Console.WriteLine("\n--- LISTA POJAZDÓW ---");
                            manager.PokazWszystkie();
                            CzekajNaEnter();
                            break;
                        case "5":
                            Console.Write("\nPodaj tablicę do usunięcia: ");
                            manager.UsunPojazd(Console.ReadLine());
                            CzekajNaEnter();
                            break;
                        case "6":
                            ObslugaSerwisu(manager);
                            break;
                        case "7":
                            Console.Write("\nPodaj tablicę pojazdu: ");
                            manager.PokazSerwisPojazdu(Console.ReadLine());
                            CzekajNaEnter();
                            break;
                        case "0": dziala = false; break;
                        default: Console.WriteLine("Nieznana opcja."); CzekajNaEnter(); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nBŁĄD: {ex.Message}");
                    CzekajNaEnter();
                }
            }
        }
    }
    static void DodajSamochodInteraktywnie(Management manager)
    {
        Console.WriteLine("\n--- DODAWANIE OSOBÓWKI ---");
        Console.Write("Marka: "); string marka = Console.ReadLine();
        Console.Write("Model: "); string model = Console.ReadLine();
        Console.Write("Tablica: "); string tablica = Console.ReadLine();
        Console.Write("Rocznik: "); int.TryParse(Console.ReadLine(), out int rocznik);
        Console.Write("Przebieg: "); int.TryParse(Console.ReadLine(), out int przebieg);
        Console.Write("Silnik (np 2.0): "); double.TryParse(Console.ReadLine(), out double silnik);
        Console.Write("Paliwo: "); string paliwo = Console.ReadLine();
        Console.Write("Liczba drzwi: "); int.TryParse(Console.ReadLine(), out int drzwi);
        Console.Write("Nadwozie (Sedan/Kombi): "); string nadwozie = Console.ReadLine();

        Car auto = new Car
        {
            Marka = marka,
            Model = model,
            Tablica = tablica,
            Rocznik = rocznik,
            Przebieg = przebieg,
            Silnik = silnik,
            Paliwo = paliwo,
            LiczbaDrzwi = drzwi,
            Nadwozie = nadwozie,
            PrzypisanyKierowca = StworzKierowceInteraktywnie()
        };
        manager.DodajPojazd(auto);
        Console.WriteLine("\nDodano Samochód");
        CzekajNaEnter();
    }
    static void DodajCiezarowkeInteraktywnie(Management manager)
    {
        Console.WriteLine("\n--- DODAWANIE CIĘŻARÓWKI ---");
        Console.Write("Marka: "); string marka = Console.ReadLine();
        Console.Write("Model: "); string model = Console.ReadLine();
        Console.Write("Tablica: "); string tablica = Console.ReadLine();
        Console.Write("Rocznik: "); int.TryParse(Console.ReadLine(), out int rocznik);
        Console.Write("Przebieg: "); int.TryParse(Console.ReadLine(), out int przebieg);
        Console.Write("Silnik (np 12.0): "); double.TryParse(Console.ReadLine(), out double silnik);
        Console.Write("Paliwo: "); string paliwo = Console.ReadLine();
        Console.Write("Ładowność (kg): "); double.TryParse(Console.ReadLine(), out double ladownosc);
        Truck truck = new Truck
        {
            Marka = marka,
            Model = model,
            Tablica = tablica,
            Rocznik = rocznik,
            Przebieg = przebieg,
            Silnik = silnik,
            Paliwo = paliwo,
            Ladownosc = ladownosc,
            PrzypisanyKierowca = StworzKierowceInteraktywnie()
        };
        manager.DodajPojazd(truck);
        Console.WriteLine("\nDodano Ciężarówkę");
        CzekajNaEnter();
    }
    static void DodajMotocyklInteraktywnie(Management manager)
    {
        Console.WriteLine("\n--- DODAWANIE MOTOCYKLA ---");
        Console.Write("Marka: "); string marka = Console.ReadLine();
        Console.Write("Model: "); string model = Console.ReadLine();
        Console.Write("Tablica: "); string tablica = Console.ReadLine();
        Console.Write("Rocznik: "); int.TryParse(Console.ReadLine(), out int rocznik);
        Console.Write("Przebieg: "); int.TryParse(Console.ReadLine(), out int przebieg);
        Console.Write("Paliwo: "); string paliwo = Console.ReadLine();
        Console.Write("Pojemność (cm3): "); int.TryParse(Console.ReadLine(), out int pojemnosc);
        Console.Write("Typ motocykla (np. Chopper, Sport, Enduro): ");
        string typ = Console.ReadLine();
        Motorbike motor = new Motorbike
        {
            Marka = marka,
            Model = model,
            Tablica = tablica,
            Rocznik = rocznik,
            Przebieg = przebieg,
            Silnik = 0,
            Paliwo = paliwo,
            PojemnoscSilnikaCm3 = pojemnosc,
            TypRamy = typ,
            PrzypisanyKierowca = StworzKierowceInteraktywnie()
        };
        manager.DodajPojazd(motor);
        Console.WriteLine("\nDodano Motocykl");
        CzekajNaEnter();
    }
    static Driver StworzKierowceInteraktywnie()
    {
        Console.WriteLine("\n> DANE KIEROWCY:");
        Console.Write("Imię: "); string imie = Console.ReadLine();
        Console.Write("Nazwisko: "); string nazwisko = Console.ReadLine();
        Console.Write("Nr Prawa Jazdy: "); string prawko = Console.ReadLine();
        return new Driver { Imie = imie, Nazwisko = nazwisko, NumerPrawaJazdy = prawko };
    }
    static void ObslugaSerwisu(Management manager)
    {
        Console.Write("\nPodaj tablicę pojazdu: "); string tab = Console.ReadLine();
        Console.Write("Opis: "); string opis = Console.ReadLine();
        Console.Write("Koszt: ");
        if (double.TryParse(Console.ReadLine(), out double koszt))
            manager.DodajWpisSerwisowy(tab, opis, koszt);
        else Console.WriteLine("Błędna cena.");
        CzekajNaEnter();
    }
    static void CzekajNaEnter()
    {
        Console.WriteLine("\nWciśnij Enter...");
        Console.ReadLine();
    }
}