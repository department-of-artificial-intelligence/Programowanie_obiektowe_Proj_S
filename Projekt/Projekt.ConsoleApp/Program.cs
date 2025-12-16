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
    static void Main()
    {
        IHost _host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
        {
            var cns = context.Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(cns));
        }).Build();
        //popbranie kontekstu bazy
        var context = _host.Services.GetService<ApplicationDbContext>();
        //context.Database.EnsureDeleted();
        //migrajcje
        context.Database.Migrate();
        Management manager = new Management(context);
        bool dziala = true;
        while (dziala)
        {
            Console.Clear();
            Console.WriteLine("MENU GŁÓWNE");
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
                    case "1": DodajSamochod(manager); break;
                    case "2": DodajCiezarowke(manager); break;
                    case "3": DodajMotocykl(manager); break;
                    case "4":
                    Console.WriteLine("\n--- LISTA POJAZDÓW ---");
                    manager.PokazWszystkie();
                    Czekaj();
                    break;
                    case "5":
                    Console.Write("\nPodaj tablicę do usunięcia: ");
                    manager.UsunPojazd(Console.ReadLine());
                    Czekaj();
                    break;
                    case "6":
                    ObslugaSerwisu(manager);
                    break;
                    case "7":
                    Console.Write("\nPodaj tablicę pojazdu: ");
                    manager.PokazSerwisPojazdu(Console.ReadLine());
                    Czekaj();
                    break;
                    case "0": dziala = false; break;
                    default: Console.WriteLine("Nieznana opcja."); Czekaj(); break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nBŁĄD: {ex.Message}");
                Czekaj();
            }
        }        
    }
    static void DodajSamochod(IManagement manager)
    {
        Console.WriteLine("\nDODAWANIE OSOBÓWKI");
        Console.Write("Marka: "); string? marka = Console.ReadLine();
        Console.Write("Model: "); string? model = Console.ReadLine();
        Console.Write("Tablica: "); string? tablica = Console.ReadLine();
        Console.Write("Rocznik: "); int.TryParse(Console.ReadLine(), out int rocznik);
        Console.Write("Przebieg: "); int.TryParse(Console.ReadLine(), out int przebieg);
        Console.Write("Silnik (np 2.0): "); double.TryParse(Console.ReadLine(), out double silnik);
        Console.Write("Paliwo: "); string? paliwo = Console.ReadLine();
        Console.Write("Liczba drzwi: "); int.TryParse(Console.ReadLine(), out int drzwi);
        Console.Write("Nadwozie (Sedan/Kombi): "); string? nadwozie = Console.ReadLine();
        Car auto = new Car()
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
            PrzypisanyKierowca = StworzKierowce()
        };
        manager.DodajPojazd(auto);
        Console.WriteLine("\nDodano Samochód!");
        Czekaj();
    }
    static void DodajCiezarowke(IManagement manager)
    {
        Console.WriteLine("\nDODAWANIE CIĘŻARÓWKI");
        Console.Write("Marka: "); string? marka = Console.ReadLine();
        Console.Write("Model: "); string? model = Console.ReadLine();
        Console.Write("Tablica: "); string? tablica = Console.ReadLine();
        Console.Write("Rocznik: "); int.TryParse(Console.ReadLine(), out int rocznik);
        Console.Write("Przebieg: "); int.TryParse(Console.ReadLine(), out int przebieg);
        Console.Write("Silnik: "); double.TryParse(Console.ReadLine(), out double silnik);
        Console.Write("Paliwo: "); string? paliwo = Console.ReadLine();
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
            PrzypisanyKierowca = StworzKierowce()
        };
        manager.DodajPojazd(truck);
        Console.WriteLine("\nDodano Ciężarówkę!");
        Czekaj();
    }
    static void DodajMotocykl(IManagement manager)
    {
        Console.WriteLine("\nDODAWANIE MOTOCYKLA");
        Console.Write("Marka: "); string? marka = Console.ReadLine();
        Console.Write("Model: "); string? model = Console.ReadLine();
        Console.Write("Tablica: "); string? tablica = Console.ReadLine();
        Console.Write("Rocznik: "); int.TryParse(Console.ReadLine(), out int rocznik);
        Console.Write("Przebieg: "); int.TryParse(Console.ReadLine(), out int przebieg);
        Console.Write("Paliwo: "); string? paliwo = Console.ReadLine();
        Console.Write("Pojemność (cm3): "); int.TryParse(Console.ReadLine(), out int pojemnosc);
        Console.Write("Typ motocykla (np. Chopper): ");
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
            PrzypisanyKierowca = StworzKierowce()
        };
        manager.DodajPojazd(motor);
        Console.WriteLine("\nDodano Motocykl!");
        Czekaj();
    }
    static Driver StworzKierowce()
    {
        Console.WriteLine("\n> DANE KIEROWCY:");
        Console.Write("Imię: "); string? imie = Console.ReadLine();
        Console.Write("Nazwisko: "); string? nazwisko = Console.ReadLine();
        Console.Write("Nr Prawa Jazdy: "); string? prawko = Console.ReadLine();
        return new Driver { Imie = imie, Nazwisko = nazwisko, NumerPrawaJazdy = prawko };
    }

    static void ObslugaSerwisu(IManagement manager)
    {
        Console.Write("\nPodaj tablicę pojazdu: "); string? tab = Console.ReadLine();
        Console.Write("Opis: "); string? opis = Console.ReadLine();
        Console.Write("Koszt: ");
        if (double.TryParse(Console.ReadLine(), out double koszt))
            manager.DodajWpisSerwisowy(tab, opis, koszt);
        else Console.WriteLine("Błędna cena.");
        Czekaj();
    }

    static void Czekaj()
    {
        Console.WriteLine("\nWciśnij Enter...");
        Console.ReadLine();
    }
}