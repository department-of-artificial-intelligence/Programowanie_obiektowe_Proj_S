using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.ConsoleApp;
using Project.DAL;
using Project.Model;
using Project.Services;

IHost _host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
{
    var cns = context.Configuration.GetConnectionString("DefaultConnection");
    services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(cns));
}).Build();

var context = _host.Services.GetService<ApplicationDbContext>();

if (context is null)
{
    Console.WriteLine("Brak połączenia z bazą");
    return;
}

// wypełnienie bazy danymi
//Initialize(); return;

var theaterNetworkService = new TheaterNetworkService(context);
var authorService = new AuthorService(context);
var directorService = new DirectorService(context);
var actorService = new ActorService(context);
var customerService = new CustomerService(context);

Console.WriteLine("--------------------------------------------");
Console.WriteLine("System zarządzania siecią teatrów");
Console.WriteLine("--------------------------------------------");

while (true)
{
    DisplayMenu.MainMenu();
    string input = ConsoleHelper.UserInput();
    switch (input)
    {
        case "1": // wyświetl
            DisplayMenu1();
            break;
        case "x":
            return;
        default:
            Console.WriteLine("Zły wybór");
            break;
    }
}

void DisplayMenu1()
{
    while (true)
    {
        DisplayMenu.Display1();
        string input = ConsoleHelper.UserInput();
        switch (input)
        {
            case "1": // wyświetl listę teatrów
                TheaterNetwork? network = theaterNetworkService.GetFullTheaterNetwork();
                if (network is null)
                {
                    Console.WriteLine("Nie znaleziono sieci");
                    break;
                }
                Console.WriteLine("Lista teatrów:");
                Console.WriteLine(network.GetTheatersString());
                if (network.Theaters.Count == 0) break;
                DisplayMenu1_1(network);
                break;
            case "2": // wyświetl listę autorów
                List<Author> authors = authorService.GetAllAuthors();
                Console.WriteLine("Lista autorów:");
                Console.WriteLine(authors.ListToString("Brak autorów", '-'));
                if (authors.Count == 0) break;
                DisplayMenu1_2(authors);
                break;
            case "3": // wyświetl listę reżyserów
                List<Director> directors = directorService.GetAllDirectors();
                Console.WriteLine("Lista reżyserów:");
                Console.WriteLine(directors.ListToString("Brak reżyserów", '-'));
                if (directors.Count == 0) break;
                DisplayMenu1_3(directors);
                break;
            case "4": // wyświetl listę aktorów
                List<Actor> actors = actorService.GetAllActors();
                Console.WriteLine("Lista aktorów:");
                Console.WriteLine(actors.ListToString("Brak aktorów", '-'));
                if (actors.Count == 0) break;
                DisplayMenu1_4(actors);
                break;
            case "5": // wyświetl listę klientów
                List<Customer> customers = customerService.GetAllCustomers();
                Console.WriteLine("Lista klientów:");
                Console.WriteLine(customers.ListToString("Brak klientów", '-'));
                if (customers.Count == 0) break;
                DisplayMenu1_5(customers);
                break;
            case "x":
                return;
            default:
                Console.WriteLine("Zły wybór");
                break;
        }
    }
}

void DisplayMenu1_1(TheaterNetwork network)
{
    while (true)
    {
        DisplayMenu.Display1_1();
        string input = ConsoleHelper.UserInput();
        switch (input)
        {
            case "1": // wyświetl listę sali
                Console.WriteLine(network.GetTheatersString());
                Theater theater = ConsoleHelper.GetById(network.Theaters, t => t.TheaterId, "Podaj ID teatru: ");
                Console.WriteLine($"Lista sal w teatrze {theater.TheaterName}:");
                Console.WriteLine(theater.GetHallsString());
                if (theater.Halls.Count == 0) break;
                DisplayMenu1_1_1(theater);
                break;
            case "x":
                return;
            default:
                Console.WriteLine("Zły wybór");
                break;
        }
    }
}

void DisplayMenu1_1_1(Theater theater)
{
    while (true)
    {
        DisplayMenu.Display1_1_1();
        string input = ConsoleHelper.UserInput();
        Hall hall;
        switch (input)
        {
            case "1": // wyświetl listę siedzeń
                Console.WriteLine(theater.GetHallsString());
                hall = ConsoleHelper.GetById(theater.Halls, h => h.HallId, "Podaj ID sali: ");
                Console.WriteLine($"Lista siedzeń w sali {hall.HallName}:");
                Console.WriteLine(hall.GetSeatsString());
                break;
            case "2": // wizualizuj siedzenia
                Console.WriteLine(theater.GetHallsString());
                hall = ConsoleHelper.GetById(theater.Halls, h => h.HallId, "Podaj ID sali: ");
                Console.WriteLine($"Wizualizacja siedzeń w sali {hall.HallName}:");
                Console.WriteLine(hall.VisualizeSeatsString());
                break;
            case "3": // wyświetl listę przedstawień
                Console.WriteLine(theater.GetHallsString());
                hall = ConsoleHelper.GetById(theater.Halls, h => h.HallId, "Podaj ID sali: ");
                Console.WriteLine($"Lista przedstawień w sali {hall.HallName}:");
                Console.WriteLine(hall.GetPerformancesString());
                DisplayMenu1_1_1_3(hall);
                break;
            case "x":
                return;
            default:
                Console.WriteLine("Zły wybór");
                break;
        }
    }
}

void DisplayMenu1_1_1_3(Hall hall)
{
    while (true)
    {
        DisplayMenu.Display1_1_1_3();
        string input = ConsoleHelper.UserInput();
        Performance performance;
        switch (input)
        {
            case "1": // wyświetl listę biletów
                Console.WriteLine(hall.GetPerformancesString());
                performance = ConsoleHelper.GetById(hall.Performances, p => p.PerformanceId, "Podaj ID przedstawienia: ");
                Console.WriteLine($"Lista biletów na przedstawienie:");
                Console.WriteLine(performance.GetTicketsString());
                break;
            case "2": // wizualizuj bilety na sali
                Console.WriteLine(hall.GetPerformancesString());
                performance = ConsoleHelper.GetById(hall.Performances, p => p.PerformanceId, "Podaj ID przedstawienia: ");
                Console.WriteLine($"Lista biletów na przedstawienie:");
                Console.WriteLine(performance.VisualizeTicketsString());
                break;
            case "x":
                return;
            default:
                Console.WriteLine("Zły wybór");
                break;
        }
    }
}

void DisplayMenu1_2(List<Author> authors)
{
    while (true)
    {
        DisplayMenu.Display1_2();
        string input = ConsoleHelper.UserInput();
        switch (input)
        {
            case "1": // wyświetl listę sztuk
                Console.WriteLine(authors.ListToString("Brak autorów", '-'));
                Author author = ConsoleHelper.GetById(authors, a => a.Id, "Podaj ID autora: ");
                Console.WriteLine($"Lista sztuk autora:");
                Console.WriteLine(author.GetPlaysString());
                break;
            case "x":
                return;
            default:
                Console.WriteLine("Zły wybór");
                break;
        }
    }
}

void DisplayMenu1_3(List<Director> directors)
{
    while (true)
    {
        DisplayMenu.Display1_3();
        string input = ConsoleHelper.UserInput();
        switch (input)
        {
            case "1": // wyświetl listę sztuk
                Console.WriteLine(directors.ListToString("Brak reżyserów", '-'));
                Director director = ConsoleHelper.GetById(directors, a => a.Id, "Podaj ID reżysera: ");
                Console.WriteLine($"Lista sztuk reżysera:");
                Console.WriteLine(director.GetPlaysString());
                break;
            case "x":
                return;
            default:
                Console.WriteLine("Zły wybór");
                break;
        }
    }
}

void DisplayMenu1_4(List<Actor> actors)
{
    while (true)
    {
        DisplayMenu.Display1_4();
        string input = ConsoleHelper.UserInput();
        switch (input)
        {
            case "1": // wyświetl listę sztuk
                Console.WriteLine(actors.ListToString("Brak aktorów", '-'));
                Actor actor = ConsoleHelper.GetById(actors, a => a.Id, "Podaj ID aktora: ");
                Console.WriteLine($"Lista sztuk aktora:");
                Console.WriteLine(actor.GetPlaysString());
                break;
            case "x":
                return;
            default:
                Console.WriteLine("Zły wybór");
                break;
        }
    }
}

void DisplayMenu1_5(List<Customer> customers)
{
    while (true)
    {
        DisplayMenu.Display1_5();
        string input = ConsoleHelper.UserInput();
        switch (input)
        {
            case "1": // wyświetl listę biletów
                Console.WriteLine(customers.ListToString("Brak klientów", '-'));
                Customer customer = ConsoleHelper.GetById(customers, a => a.Id, "Podaj ID klienta: ");
                Console.WriteLine($"Lista biletów klienta:");
                Console.WriteLine(customer.GetTicketsString());
                break;
            case "x":
                return;
            default:
                Console.WriteLine("Zły wybór");
                break;
        }
    }
}

void Initialize()
{
    var context = _host.Services.GetService<ApplicationDbContext>();
    if (context != null)
    {
        context.Database.Migrate();
        context.Database.EnsureCreated();

        if (!context.Authors.Any())
        {
            // 1. OSOBY
            Author a1 = new() { FirstName = "William", LastName = "Shakespeare" };
            Author a2 = new() { FirstName = "Adam", LastName = "Mickiewicz" };
            Author a3 = new() { FirstName = "Juliusz", LastName = "Słowacki" };
            Author a4 = new() { FirstName = "Molier", LastName = "Poquelin" };
            context.Authors.AddRange(a1, a2, a3, a4);

            Director d1 = new() { FirstName = "Jan", LastName = "Nowak", YearsOfExperience = 20, Salary = 15000 };
            Director d2 = new() { FirstName = "Anna", LastName = "Wiśniewska", YearsOfExperience = 10, Salary = 10000 };
            Director d3 = new() { FirstName = "Piotr", LastName = "Kamiński", YearsOfExperience = 5, Salary = 7000 };
            context.Directors.AddRange(d1, d2, d3);

            Actor ac1 = new() { FirstName = "Tomasz", LastName = "Kot", Salary = 8000 };
            Actor ac2 = new() { FirstName = "Magdalena", LastName = "Cielecka", Salary = 8500 };
            Actor ac3 = new() { FirstName = "Robert", LastName = "Więckiewicz", Salary = 9000 };
            Actor ac4 = new() { FirstName = "Agnieszka", LastName = "Grochowska", Salary = 7800 };
            Actor ac5 = new() { FirstName = "Maciej", LastName = "Stuhr", Salary = 8200 };
            context.Actors.AddRange(ac1, ac2, ac3, ac4, ac5);

            context.SaveChanges();

            // 2. SZTUKI I RELACJE (Actor–Play)
            Play p1 = new() { Title = "Hamlet", Author = a1, Director = d1 };
            Play p2 = new() { Title = "Makbet", Author = a1, Director = d2 };
            Play p3 = new() { Title = "Dziady", Author = a2, Director = d1 };
            Play p4 = new() { Title = "Kordian", Author = a3, Director = d3 };
            Play p5 = new() { Title = "Skąpiec", Author = a4, Director = d2 };
            context.Plays.AddRange(p1, p2, p3, p4, p5);

            ac1.AddPlay(p1); ac2.AddPlay(p1);
            ac3.AddPlay(p2); ac4.AddPlay(p2);
            ac1.AddPlay(p3); ac5.AddPlay(p3);
            ac2.AddPlay(p4); ac3.AddPlay(p4);
            ac4.AddPlay(p5); ac5.AddPlay(p5);

            context.SaveChanges();

            // 3. INFRASTRUKTURA

            // UTWORZENIE SIECI (ROOT)
            TheaterNetwork network = new("Polskie Teatry Narodowe");
            context.Add(network);
            context.SaveChanges();

            // THEATERS (tworzone przez network.CreateTheater)
            Theater? t1 = network.CreateTheater("Teatr Narodowy", "Polska", "Warszawa", "Plac Teatralny 1");
            Theater? t2 = network.CreateTheater("Teatr Stary", "Polska", "Kraków", "Rynek Główny 32");
            context.SaveChanges();

            // HALLS (tworzone przez t.CreateHall)
            Hall? h1 = t1?.CreateHall("Duża Sala");
            Hall? h2 = t1?.CreateHall("Kameralna");
            Hall? h3 = t2?.CreateHall("Scena Główna");
            context.SaveChanges();

            // SEATS (tworzone przez h.CreateSeats)
            h1?.CreateSeats(6, 10);
            h2?.CreateSeats(4, 6);
            h3?.CreateSeats(8, 12);
            context.SaveChanges();

            // PERFORMANCES (tworzone przez h.AddPerformance)
            var now = DateTime.Now;
            Performance pf1 = new(p1, now.AddDays(1), now.AddDays(1).AddHours(2));
            h1?.AddPerformance(pf1);
            Performance pf2 = new(p2, now.AddDays(2), now.AddDays(2).AddHours(2));
            h1?.AddPerformance(pf2);
            Performance pf3 = new(p3, now.AddDays(3), now.AddDays(3).AddHours(3));
            h3?.AddPerformance(pf3);
            Performance pf4 = new(p4, now.AddDays(4), now.AddDays(4).AddHours(2));
            h2?.AddPerformance(pf4);
            Performance pf5 = new(p5, now.AddDays(5), now.AddDays(5).AddHours(2));
            h3?.AddPerformance(pf5);

            context.SaveChanges();

            // 4. BILETY I TRANSAKCJE

            // TICKETS (tworzone przez pf.CreateTicketForEverySeat)
            pf1.CreateTicketForEverySeat(120);
            pf2.CreateTicketForEverySeat(110);
            pf3.CreateTicketForEverySeat(100);
            pf4.CreateTicketForEverySeat(90);
            pf5.CreateTicketForEverySeat(130);

            Customer c1 = new() { FirstName = "Michał", LastName = "Kaczmarek" };
            Customer c2 = new() { FirstName = "Ewa", LastName = "Kamińska" };
            Customer c3 = new() { FirstName = "Paweł", LastName = "Dąbrowski" };
            context.Customers.AddRange(c1, c2, c3);
            context.SaveChanges();

            // OPERACJE NA BILETACH
            var ticketList = context.Tickets.OrderBy(t => t.TicketId).ToList();

            c1.ReserveTicket(ticketList.First());
            c1.BuyAllReserved();

            c2.BuyTicket(ticketList.Skip(5).First());

            c3.ReserveTicket(ticketList.Skip(10).First());

            context.SaveChanges();
        }
    }
}