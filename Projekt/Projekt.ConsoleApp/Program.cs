using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.DAL;
using Project.Model;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

var _host = Host.CreateDefaultBuilder()
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
    })
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<ApplicationDbcontext>(options => options.UseSqlite("Data Source=Baza.db"));
    })
    .Build();


var db = _host.Services.GetRequiredService<ApplicationDbcontext>();
db.Database.EnsureDeleted();
db.Database.EnsureCreated();

bool dziala = true;

while (dziala)
{
    Console.Clear();
    Console.WriteLine("**********************************************");
    Console.WriteLine("||    SYSTEM ZARZĄDZANIA PRACOWNIKAMI       ||");
    Console.WriteLine("**********************************************");
    Console.WriteLine("|| 1. Pokaż Szefa i Pracowników             ||");
    Console.WriteLine("|| 2. Zatrudnij nowego pracownika           ||");
    Console.WriteLine("|| 3. Zrób wypłaty pracownikom              ||");
    Console.WriteLine("|| 4. Znajdź najlepszego pracownika         ||");
    Console.WriteLine("|| 5. Zwolnij pracownika                    ||");
    Console.WriteLine("|| 0. Wyjście z programu                    ||");
    Console.WriteLine("**********************************************");
    Console.Write("Wybierz opcję: ");

    switch (Console.ReadLine())
    {
        case "1":
            Funkcja1_PokazSzefaIListe(db);
            break;
        case "2":
            Funkcja2_DodajPracownika(db);
            break;
        case "3":
            Funkcja3_ZrobPrzelewy(db);
            break;
        case "4":
            Funkcja4_ZnajdzNajlepszego(db);
            break;
        case "5":
            Funkcja5_UsunPracownika(db);
            break;
        case "0":
            dziala = false;
            break;
        default:
            Komunikat("Nie ma takiej opcji!");
            break;
    }
}

static void Funkcja1_PokazSzefaIListe(ApplicationDbcontext db)
{
    Console.Clear();

    var szef = new Pracodawca
    {
        FirstName = "Adam",
        LastName = "Nowak (CEO)",
        Email = "ceo@firma.pl",
        Telefon = "111-222-333",
        Adres = new Adres { Miasto = "Warszawa" }
    };

    Console.WriteLine("--- DANE SZEFA ---");
    Console.WriteLine(szef.LoadContactInfo());
    Console.WriteLine("----------------------------------------------");

    var pracownicy = db.Pracownicy.Include(p => p.ListaProjektow).ToList();

    Console.WriteLine($"\n--- LISTA PRACOWNIKÓW (Razem: {pracownicy.Count}) ---");
    if (!pracownicy.Any()) Console.WriteLine("Brak pracowników w bazie.");

    foreach (var p in pracownicy)
    {
        Console.WriteLine($"[ID: {p.Id}] {p.FirstName} {p.LastName} | Stanowisko: {p.StanowiskoPracy}");
        Console.WriteLine($"       Projekty: {p.ListaProjektow.Count} (Zaliczone: {p.PassedProjects()})");
    }

    Komunikat("");
}

static void Funkcja2_DodajPracownika(ApplicationDbcontext db)
{
    Console.Clear();
    Console.WriteLine("--- ZATRUDNIANIE PRACOWNIKA ---");

    try
    {
        var dzial = db.Set<Dzial>().FirstOrDefault(d => d.NazwaDzialu == "IT");
        if (dzial == null)
        {
            dzial = new Dzial { NazwaDzialu = "IT", ListaPracownikow = new List<Pracownik>() };
            db.Add(dzial);
            db.SaveChanges();
        }

        Console.Write("Imię: ");
        string imie = Console.ReadLine();
        Console.Write("Nazwisko: ");
        string nazwisko = Console.ReadLine();
        Console.Write("Wiek: ");
        int wiek = int.Parse(Console.ReadLine());

        var nowyAdres = new Adres
        {
            Miasto = "Biurowe",
            Ulica = "Główna",
            KodPocztowy = "00-000"
        };

        db.Adresy.Add(nowyAdres);
        db.SaveChanges();


        var nowy = new Pracownik
        {
            FirstName = imie,
            LastName = nazwisko,
            Age = wiek,
            Email = $"{imie}@firma.pl",
            Telefon = "Brak",
            StanowiskoPracy = Stanowisko.Programista,
            Adres = nowyAdres,
            ListaProjektow = new List<Projekt>()
        };

        if (dzial.ListaPracownikow == null)
        {
            dzial.ListaPracownikow = new List<Pracownik>();
        }
        dzial.ListaPracownikow.Add(nowy);

        Console.Write("Czy dodać mu projekt startowy? (t/n): ");
        if (Console.ReadLine() == "t")
        {
            var losowa = new Random();

            nowy.ListaProjektow.Add(new Projekt
            {
                Name = "Projekt Startowy",
                Ocena = losowa.Next(50, 101),
                Status = "Zakończony",
                Wlasciciel = "Firma"
            });
        }

        db.Pracownicy.Add(nowy);
        db.SaveChanges();

        Console.WriteLine("Gratulacje! Dodano pracownika.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("----------Błąd!----------");
        Console.WriteLine($"Błąd: {ex.Message}");
        if (ex.InnerException != null)
        {
            Console.WriteLine($"Szczegóły bazy: {ex.InnerException.Message}");
        }
    }
    Komunikat("");
}

static void Funkcja3_ZrobPrzelewy(ApplicationDbcontext db)
{
    Console.Clear();
    Console.WriteLine("--- LISTA PŁAC (PRZELEWY) ---");

    var pracownicy = db.Pracownicy.Include(p => p.ListaProjektow).ToList();

    var dzialPomocniczy = new Dzial { ListaPracownikow = pracownicy };
    var najlepszy = dzialPomocniczy.FindBestEmployeeByProjectGrade();

    var ksiegowosc = new Wyplata();
    int podstawa = 4000;

    foreach (var p in pracownicy)
    {
        bool CzyNajlepszy = (p == najlepszy);
        ksiegowosc.DoPayment(p, podstawa, CzyNajlepszy);
        Console.WriteLine("----------------------------------");
    }
    Komunikat("");
}

static void Funkcja4_ZnajdzNajlepszego(ApplicationDbcontext db)
{
    Console.Clear();
    Console.WriteLine("--- NAJLEPSZY PRACOWNIK ---");

    var pracownicy = db.Pracownicy.Include(p => p.ListaProjektow).ToList();

    var dzial = new Dzial { NazwaDzialu = "IT", ListaPracownikow = pracownicy };

    var najlepszy = dzial.FindBestEmployeeByProjectGrade();

    if (najlepszy != null)
    {
        Console.WriteLine($"Najlepszy pracownik to: {najlepszy.FirstName} {najlepszy.LastName}");
        Console.WriteLine($"Liczba zaliczonych projektów: {najlepszy.PassedProjects()}");
        double srednia = najlepszy.ListaProjektow.Any() ? najlepszy.ListaProjektow.Average(x => x.Ocena) : 0;
        Console.WriteLine($"Średnia ocen projektow: {srednia:F1}");
    }
    else
    {
        Console.WriteLine("Brak pracowników w bazie.");
    }
    Komunikat("");
}

static void Funkcja5_UsunPracownika(ApplicationDbcontext db)
{
    Console.Clear();
    Console.WriteLine("--- ZWALNIANIE PRACOWNIKA ---");

    var lista = db.Pracownicy.ToList();
    foreach (var p in lista)
    {
        Console.WriteLine($"[ID: {p.Id}] {p.FirstName} {p.LastName}");
    }


    Console.Write("Podaj ID pracownika do usunięcia: ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        var pracownik = db.Pracownicy.Find(id);
        if (pracownik != null)
        {
            db.Pracownicy.Remove(pracownik);
            db.SaveChanges();
            Console.WriteLine($"Usunięto pracownika: {pracownik.FirstName} {pracownik.LastName}");
        }
        else
        {
            Console.WriteLine("Nie znaleziono pracownika o podanym ID.");
        }
    }
    else
    {
        Console.WriteLine("Podano nieprawidłowe ID.");
    }
    Komunikat("");
}

static void Komunikat(string tekst)
{
    Console.WriteLine(tekst);
    Console.WriteLine("\nNaciśnij ENTER, aby kontynuować...");
    Console.ReadLine();
}