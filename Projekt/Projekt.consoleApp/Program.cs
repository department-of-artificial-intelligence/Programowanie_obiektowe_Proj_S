using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Projekt.DAL;
using Projekt.Model;

/// Inicjalizacja konfiguracji - wczytanie ustawień z pliku appsettings.json.

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

IConfiguration config = builder.Build();

/// Konfiguracja kontenera Dependency Injection (DI).

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
    })
    .Build();

var context = host.Services.GetService<ApplicationDbContext>();
                
context.Database.Migrate();
      
InicjalizujDane(context);
MenuGlowne(context);


static void InicjalizujDane(ApplicationDbContext db)
{

    ///Zabezpieczenie przed dublowaniem danych
    if (db.Branches.Any()) 
        return;

    Console.WriteLine("Inicjalizacja bazy danych (pierwsze uruchomienie)...");

    /// Oddziały
    var oddzialWawa = new Branch { Name = "Warszawa Centrum", Address = "ul. Marszałkowska 1" };
    var oddzialKrk = new Branch { Name = "Kraków Rynek", Address = "ul. Floriańska 2" };
    var oddzialCzew = new Branch { Name = "Częstochowa Centrum", Address = "ul. Warszawska 31" };

    db.Branches.AddRange(oddzialWawa, oddzialKrk, oddzialCzew);
    db.SaveChanges(); 

    /// Samochody
    var cars = new[]
    {
        new Car { Marka = "Toyota", Model = "Yaris", Year = 2022, RegistrationNumber = "WA 12345", DailyRate = 100, Status = CarStatus.Available, CurrentBranchId = oddzialWawa.Id },
        new Car { Marka = "Skoda", Model = "Octavia", Year = 2023, RegistrationNumber = "KR 54321", DailyRate = 150, Status = CarStatus.Available, CurrentBranchId = oddzialKrk.Id },
        new Car { Marka = "Ford", Model = "Mondeo", Year = 2021, RegistrationNumber = "WA 67890", DailyRate = 180, Status = CarStatus.Rented, CurrentBranchId = oddzialWawa.Id },
        new Car { Marka = "BMW", Model = "X5", Year = 2023, RegistrationNumber = "KR 98765", DailyRate = 300, Status = CarStatus.InService, CurrentBranchId = oddzialKrk.Id },
        new Car { Marka = "BMW", Model = "Seria 5", Year = 2025, RegistrationNumber = "SC 345CL", DailyRate = 500, Status = CarStatus.Available, CurrentBranchId = oddzialCzew.Id }
    };
    db.Cars.AddRange(cars);

    /// Klienci
    var cust1 = new Customer { FirstName = "Jan", LastName = "Kowalski", PhoneNumber = "111222333", DateOfBirth = new DateTime(1990, 5, 15) };
    var cust2 = new Customer { FirstName = "Anna", LastName = "Nowak", PhoneNumber = "444555666", DateOfBirth = new DateTime(1985, 10, 2) };
    db.Customers.AddRange(cust1, cust2);

    /// Pracownicy
    db.Employees.Add(new Employee { FirstName = "Piotr", LastName = "Zieliński", BranchId = oddzialWawa.Id });
    db.Employees.Add(new Employee { FirstName = "Ewa", LastName = "Wiśniewska", BranchId = oddzialKrk.Id });
    db.Employees.Add(new Employee { FirstName = "Jan", LastName = "Krawczyk", BranchId = oddzialCzew.Id });

    db.SaveChanges(); 

    var autoDoWyp = db.Cars
        .First(c => c.RegistrationNumber == "WA 67890");

    var rental1 = new Rental
    {
        CustomerId = cust1.Id,
        CarId = autoDoWyp.Id,
        PickupBranchId = oddzialWawa.Id,
        StartDate = DateTime.Now.AddDays(-2),
        EndDate = DateTime.Now.AddDays(3),
        Status = RentalStatus.Active,
        ActualReturnDate = null,
        TotalCost = 0
    };
    db.Rentals.Add(rental1);

    cust1.RentalHistory.Add(rental1);

    db.SaveChanges();
    Console.WriteLine("Dane zostały załadowane do bazy");
}

static void MenuGlowne(ApplicationDbContext db)
{
    bool dziala = true;
    while (dziala)
    {
        Console.Clear();
        Console.WriteLine("======= SYSTEM ZARZĄDZANIA WYPOŻYCZALNIĄ =======");
        Console.WriteLine("1. Wypożycz samochód");
        Console.WriteLine("2. Zwróć samochód");
        Console.WriteLine("---------------------------------------------");
        Console.WriteLine("3. Pokaż wszystkie samochody");
        Console.WriteLine("4. Pokaż tylko dostępne samochody");
        Console.WriteLine("5. Pokaż oddziały");
        Console.WriteLine("6. Pokaż klientów");
        Console.WriteLine("7. Pokaż historię wypożyczeń");
        Console.WriteLine("8. Dodaj nowego klienta");
        Console.WriteLine("---------------------------------------------");
        Console.WriteLine("0.  Zakończ program");
        Console.Write("\nWybierz opcję: ");

        string wybor = Console.ReadLine();

        switch (wybor)
        {
            case "1": WypozyczSamochod(db);
                break;
            case "2": ZwrocSamochod(db); 
                break;
            case "3": PokazWszystkieSamochody(db); 
                break;
            case "4": PokazDostepneSamochody(db);
                break;
            case "5": PokazOddzialy(db);
                break;
            case "6": PokazKlientow(db); 
                break;
            case "7": PokazHistorie(db); 
                break;
            case "8": DodajKlienta(db); 
                break;
            case "0": dziala = false; 
                break;
            default: Powiadomienie("Nieznana opcja."); 
                break;
        }
    }
}

static void WypozyczSamochod(ApplicationDbContext db)
{
    Console.Clear();
    Console.WriteLine("---- Nowe wypożyczenie ----");

    Console.WriteLine("Z którego oddziału chcesz odebrac auto?");
    PokazOddzialy(db, false);
    Console.Write("Podaj ID oddziału: ");

    if (!int.TryParse(Console.ReadLine(), out int idOddzialu)) 
        return;

    var oddzial = db.Branches
        .Include(b => b.Cars)
        .FirstOrDefault(b => b.Id == idOddzialu);
    if (oddzial == null) 
    { Powiadomienie("Brak oddziału."); 
        return; 
    }

    Console.WriteLine($"\nDostępne samochody w {oddzial.Name}:");
    var dostepneAuta = db.Cars
        .Where(c => c.CurrentBranchId == idOddzialu && c.Status == CarStatus.Available)
        .ToList();

    if (!dostepneAuta.Any()) 
    { 
        Powiadomienie("Brak aut.");
        return; 
    }

    foreach (var auto in dostepneAuta) 
        Console.WriteLine(auto.ToString());

    Console.Write("Podaj ID samochodu: ");
    if (!int.TryParse(Console.ReadLine(), out int idAuta))
        return;

    var samochod = dostepneAuta.FirstOrDefault(c => c.Id == idAuta);
    if (samochod == null) 
    { 
        Powiadomienie("Błędne ID auta.");
        return;
    }

    Console.WriteLine("\nKlienci w systemie:");
    PokazKlientow(db, false);
    Console.Write("Podaj ID klienta: ");
    if (!int.TryParse(Console.ReadLine(), out int idKlienta)) 
        return;

    var klient = db.Customers
        .FirstOrDefault(k => k.Id == idKlienta);
    if (klient == null) 
    { 
        Powiadomienie("Brak klienta.");
        return;
    }

    Console.Write("Ile dni? ");
    if (!int.TryParse(Console.ReadLine(), out int dni))
        return;

    var noweWypozyczenie = new Rental
    {
        CustomerId = klient.Id,
        CarId = samochod.Id,
        PickupBranchId = oddzial.Id,
        StartDate = DateTime.Now,
        EndDate = DateTime.Now.AddDays(dni),
        TotalCost = samochod.DailyRate * dni,
        Status = RentalStatus.Active
    };

    samochod.Status = CarStatus.Rented;

    db.Rentals.Add(noweWypozyczenie);
    db.SaveChanges();

    Powiadomienie($"SUKCES! Wypożyczono auto {samochod.Marka}.");
}

static void ZwrocSamochod(ApplicationDbContext db)
{
    Console.Clear();
    Console.WriteLine("--- Zwrot samochodu ---");

    var aktywne = db.Rentals
        .Include(r => r.Car)
        .Include(r => r.Customer)
        .Include(r => r.PickupBranch)
        .Where(r => r.Status == RentalStatus.Active)
        .ToList();

    if (!aktywne.Any()) 
    { 
        Powiadomienie("Brak aktywnych wypożyczeń.");
        return;
    }

    foreach (var r in aktywne) 
        Console.WriteLine(r.ToString());

    Console.Write("Podaj ID wypożyczenia: ");
    if (!int.TryParse(Console.ReadLine(), out int idRent)) 
        return;

    var rental = aktywne.FirstOrDefault(r => r.Id == idRent);
    if (rental == null) 
        return;

    Console.WriteLine("Gdzie zwracasz auto?");
    PokazOddzialy(db, false);
    Console.Write("ID oddziału: ");
    if (!int.TryParse(Console.ReadLine(), out int idOddzial)) 
        return;

    var oddzialZwrotu = db.Branches.Find(idOddzial);
    if (oddzialZwrotu == null) 
        return;

    rental.Status = RentalStatus.Completed;
    rental.ActualReturnDate = DateTime.Now;

    rental.Car.Status = CarStatus.Available;
    rental.Car.CurrentBranchId = oddzialZwrotu.Id;

    db.SaveChanges(); 

    Powiadomienie("Auto zwrócone.");
}

/// Metoda wyświetlająca wszystkie samochody.

static void PokazWszystkieSamochody(ApplicationDbContext db, bool czekaj = true)
{
    Console.Clear();
    var auta = db.Cars
        .Include(c => c.CurrentBranch)
        .ToList();
    foreach (var a in auta) 
        Console.WriteLine(a.ToString());
    if (czekaj) 
        CzekajNaEnter();
}

///Metoda filtrująca samochody które sa dostepne po statusie 'Available'.

static void PokazDostepneSamochody(ApplicationDbContext db, bool czekaj = true)
{
    Console.Clear();
    var auta = db.Cars
        .Include(c => c.CurrentBranch)
        .Where(c => c.Status == CarStatus.Available)
        .ToList();
    foreach (var a in auta) 
        Console.WriteLine(a.ToString());
    if (czekaj)
        CzekajNaEnter();
}

/// Wyświetlanie listy oddziałów wraz ze statystykami w(liczba aut i pracowników).

static void PokazOddzialy(ApplicationDbContext db, bool czekaj = true)
{
    Console.Clear();
    var oddzialy = db.Branches
        .Include(b => b.Cars)
        .Include(b => b.Employees)
        .ToList();

    foreach (var o in oddzialy)
    {
        Console.WriteLine($"[{o.Id}] {o.Name} - Aut: {o.Cars.Count}, Prac: {o.Employees.Count}");
    }
    if (czekaj) 
        CzekajNaEnter();
}

///Wyswitlanie listy klientów w bazie.

static void PokazKlientow(ApplicationDbContext db, bool czekaj = true)
{
    Console.Clear();
    foreach (var k in db.Customers.ToList()) Console.WriteLine(k.ToString());
    if (czekaj) 
        CzekajNaEnter();
}

///Historia wszystkich operacji posortowana od najnowszych.

static void PokazHistorie(ApplicationDbContext db, bool czekaj = true)
{
    Console.Clear();
    var rentals = db.Rentals
        .Include(r => r.Car)    
        .Include(r => r.Customer)
        .OrderByDescending(r => r.StartDate)
        .ToList();

    foreach (var r in rentals) 
        Console.WriteLine(r.ToString());
    if (czekaj) 
        CzekajNaEnter();
}

///Dodanie nowego klienta do bazy

static void DodajKlienta(ApplicationDbContext db)
{
    Console.Clear();
    Console.WriteLine("=== REJESTRACJA NOWEGO KLIENTA ===");

    Console.Write("Podaj imię: ");
    string imie = Console.ReadLine();
    while (string.IsNullOrWhiteSpace(imie))
    {
        Console.WriteLine("Imię nie może być puste!");
        Console.Write("Podaj imię: ");
        imie = Console.ReadLine();
    }

    Console.Write("Podaj nazwisko: ");
    string nazwisko = Console.ReadLine();
    while (string.IsNullOrWhiteSpace(nazwisko))
    {
        Console.WriteLine("Nazwisko nie może być puste!");
        Console.Write("Podaj nazwisko: ");
        nazwisko = Console.ReadLine();
    }
    Console.Write("Podaj numer telefonu: ");
    string telefon = Console.ReadLine();

    DateTime dataUrodzenia;
    while (true)
    {
        Console.Write("Podaj datę urodzenia (RRRR-MM-DD): ");
        if (DateTime.TryParse(Console.ReadLine(), out dataUrodzenia))
        {
            if (dataUrodzenia > DateTime.Now.AddYears(-18))
            {
                Powiadomienie("Błąd: Klient musi być pełnoletni.");
                return;
            }
            break;
        }
        Console.WriteLine("Błędny format daty. Spróbuj np. 2000-01-01");
    }

    var nowyKlient = new Customer
    {
        FirstName = imie,
        LastName = nazwisko,
        PhoneNumber = telefon,
        DateOfBirth = dataUrodzenia,
        RentalHistory = new List<Rental>()
    };

    try
    {
        db.Customers.Add(nowyKlient);
        db.SaveChanges();

        Console.WriteLine($"\nSUKCES! Dodano klienta: {imie} {nazwisko} [ID: {nowyKlient.Id}]");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Wystąpił błąd podczas zapisu: {ex.Message}");
    }

    CzekajNaEnter();
}

///Metody pomocnicze.

static void CzekajNaEnter() 
{ 
    Console.WriteLine("\nEnter..."); 
    Console.ReadLine(); 
}
static void Powiadomienie(string msg)
{ 
    Console.WriteLine(msg); 
    CzekajNaEnter(); 
}