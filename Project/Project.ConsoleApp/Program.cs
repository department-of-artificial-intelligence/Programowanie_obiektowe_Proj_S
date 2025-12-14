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
var theaterService = new TheaterService(context);
var hallService = new HallService(context);
var authorService = new AuthorService(context);
var directorService = new DirectorService(context);
var actorService = new ActorService(context);
var customerService = new CustomerService(context);
var playService = new PlayService(context);
var performanceService = new PerformanceService(context);

if (theaterNetworkService.GetNetwork() is null)
{
    string networkName = ConsoleHelper.UserInput("Sieć nie istnieje. Podaj nazwę sieci: ");
    context.Add(new TheaterNetwork(networkName));
    context.SaveChanges();
}

Console.WriteLine("-------------------------------------------------------------");
Console.WriteLine($"System zarządzania siecią teatrów: {theaterNetworkService?.GetNetwork()?.NetworkName}");
Console.WriteLine("-------------------------------------------------------------");

while (true)
{
    DisplayMenu.MainMenu();
    string input = ConsoleHelper.UserInput();
    switch (input)
    {
        case "1": // wyświetl
            ViewMenu1();
            break;
        case "2": // stwórz
            CreationMenu2(); 
            break;
        case "x":
            return;
        default:
            Console.WriteLine("Zły wybór");
            break;
    }
}

void ViewMenu1()
{
    while (true)
    {
        DisplayMenu.View1();
        string input = ConsoleHelper.UserInput();
        switch (input)
        {
            case "1": // wyświetl listę teatrów
                TheaterNetwork? network = theaterNetworkService?.GetFullTheaterNetwork();
                if (network is null)
                {
                    Console.WriteLine("Nie znaleziono sieci");
                    break;
                }
                Console.WriteLine("Lista teatrów:");
                Console.WriteLine(network.GetTheatersString());
                if (network.Theaters.Count == 0) break;
                ViewMenu1_1(network);
                break;
            case "2": // wyświetl listę autorów
                List<Author> authors = authorService.GetAllAuthors();
                Console.WriteLine("Lista autorów:");
                Console.WriteLine(authors.ListToString("Brak autorów", '-'));
                if (authors.Count == 0) break;
                ViewMenu1_2(authors);
                break;
            case "3": // wyświetl listę reżyserów
                List<Director> directors = directorService.GetAllDirectors();
                Console.WriteLine("Lista reżyserów:");
                Console.WriteLine(directors.ListToString("Brak reżyserów", '-'));
                if (directors.Count == 0) break;
                ViewMenu1_3(directors);
                break;
            case "4": // wyświetl listę aktorów
                List<Actor> actors = actorService.GetAllActors();
                Console.WriteLine("Lista aktorów:");
                Console.WriteLine(actors.ListToString("Brak aktorów", '-'));
                if (actors.Count == 0) break;
                ViewMenu1_4(actors);
                break;
            case "5": // wyświetl listę klientów
                List<Customer> customers = customerService.GetAllCustomers();
                Console.WriteLine("Lista klientów:");
                Console.WriteLine(customers.ListToString("Brak klientów", '-'));
                if (customers.Count == 0) break;
                ViewMenu1_5(customers);
                break;
            case "6": // wyświetl listę sztuk
                List<Play> plays = playService.GetAllPlays();
                Console.WriteLine("Lista sztuk:");
                Console.WriteLine(plays.ListToString("Brak sztuk", '-'));
                break;
            case "7":
                List<Performance> performances = performanceService.GetAllPerformancesWithoutHall();
                Console.WriteLine("Lista przedstawień bez sali:");
                Console.WriteLine(performances.ListToString("Brak przedstawień", '-'));
                break;
            case "x":
                return;
            default:
                Console.WriteLine("Zły wybór");
                break;
        }
    }
}

void ViewMenu1_1(TheaterNetwork network)
{
    while (true)
    {
        DisplayMenu.View1_1();
        string input = ConsoleHelper.UserInput();
        switch (input)
        {
            case "1": // wyświetl listę sali
                Console.WriteLine(network.GetTheatersString());
                if (network.Theaters.Count == 0) break;
                Theater theater = ConsoleHelper.GetById(network.Theaters, t => t.TheaterId, "Podaj ID teatru: ");
                Console.WriteLine($"Lista sal w teatrze {theater.TheaterName}:");
                Console.WriteLine(theater.GetHallsString());
                if (theater.Halls.Count == 0) break;
                ViewMenu1_1_1(theater);
                break;
            case "x":
                return;
            default:
                Console.WriteLine("Zły wybór");
                break;
        }
    }
}

void ViewMenu1_1_1(Theater theater)
{
    while (true)
    {
        DisplayMenu.View1_1_1();
        string input = ConsoleHelper.UserInput();
        Hall hall;
        switch (input)
        {
            case "1": // wyświetl listę siedzeń
                Console.WriteLine(theater.GetHallsString());
                if (theater.Halls.Count == 0) break;
                hall = ConsoleHelper.GetById(theater.Halls, h => h.HallId, "Podaj ID sali: ");
                Console.WriteLine($"Lista siedzeń w sali {hall.HallName}:");
                Console.WriteLine(hall.GetSeatsString());
                break;
            case "2": // wizualizuj siedzenia
                Console.WriteLine(theater.GetHallsString());
                if (theater.Halls.Count == 0) break;
                hall = ConsoleHelper.GetById(theater.Halls, h => h.HallId, "Podaj ID sali: ");
                Console.WriteLine($"Wizualizacja siedzeń w sali {hall.HallName}:");
                Console.WriteLine(hall.VisualizeSeatsString());
                break;
            case "3": // wyświetl listę przedstawień
                Console.WriteLine(theater.GetHallsString());
                if (theater.Halls.Count == 0) break;
                hall = ConsoleHelper.GetById(theater.Halls, h => h.HallId, "Podaj ID sali: ");
                Console.WriteLine($"Lista przedstawień w sali {hall.HallName}:");
                Console.WriteLine(hall.GetPerformancesString());
                ViewMenu1_1_1_3(hall);
                break;
            case "x":
                return;
            default:
                Console.WriteLine("Zły wybór");
                break;
        }
    }
}

void ViewMenu1_1_1_3(Hall hall)
{
    while (true)
    {
        DisplayMenu.View1_1_1_3();
        string input = ConsoleHelper.UserInput();
        Performance performance;
        switch (input)
        {
            case "1": // wyświetl listę biletów
                Console.WriteLine(hall.GetPerformancesString());
                if (hall.Performances.Count == 0) break;
                performance = ConsoleHelper.GetById(hall.Performances, p => p.PerformanceId, "Podaj ID przedstawienia: ");
                Console.WriteLine($"Lista biletów na przedstawienie:");
                Console.WriteLine(performance.GetTicketsString());
                break;
            case "2": // wizualizuj bilety na sali
                Console.WriteLine(hall.GetPerformancesString());
                if (hall.Performances.Count == 0) break;
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

void ViewMenu1_2(List<Author> authors)
{
    while (true)
    {
        DisplayMenu.View1_2();
        string input = ConsoleHelper.UserInput();
        switch (input)
        {
            case "1": // wyświetl listę sztuk
                Console.WriteLine(authors.ListToString("Brak autorów", '-'));
                if (authors.Count == 0) break;
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

void ViewMenu1_3(List<Director> directors)
{
    while (true)
    {
        DisplayMenu.View1_3();
        string input = ConsoleHelper.UserInput();
        switch (input)
        {
            case "1": // wyświetl listę sztuk
                Console.WriteLine(directors.ListToString("Brak reżyserów", '-'));
                if (directors.Count == 0) break;
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

void ViewMenu1_4(List<Actor> actors)
{
    while (true)
    {
        DisplayMenu.View1_4();
        string input = ConsoleHelper.UserInput();
        switch (input)
        {
            case "1": // wyświetl listę sztuk
                Console.WriteLine(actors.ListToString("Brak aktorów", '-'));
                if (actors.Count == 0) break;
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

void ViewMenu1_5(List<Customer> customers)
{
    while (true)
    {
        DisplayMenu.View1_5();
        string input = ConsoleHelper.UserInput();
        switch (input)
        {
            case "1": // wyświetl listę biletów
                Console.WriteLine(customers.ListToString("Brak klientów", '-'));
                if (customers.Count == 0) break;
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

void CreationMenu2()
{
    while (true)
    {
        DisplayMenu.Creation2();
        string input = ConsoleHelper.UserInput();
        switch (input)
        {
            case "1": // tworzenie struktury teatru
                CreationMenu2_1();
                break;
            case "2": // tworzenie przedstawień
                CreationMenu2_2();
                break;
            case "3": // tworzenie osób
                CreationMenu2_3();
                break;
            case "x":
                return;
            default:
                Console.WriteLine("Zły wybór");
                break;
        }
    }
}

void CreationMenu2_1()
{
    while (true)
    {
        DisplayMenu.Creation2_1();
        string input = ConsoleHelper.UserInput();
        string theaterName;
        string country;
        string city;
        string street;
        string hallName;
        int rowNumber;
        int seatNumber;
        int rows;
        int seatsPerRow;
        TheaterNetwork? network;
        Theater? theater;
        Hall? hall;
        Seat? seat;
        switch (input)
        {
            case "1": // stwórz teatr
                theaterName = ConsoleHelper.UserInput("Podaj nazwę teatru: ");
                country = ConsoleHelper.UserInput("Podaj kraj: ");
                city = ConsoleHelper.UserInput("Podaj miasto: ");
                street = ConsoleHelper.UserInput("Podaj ulicę: ");
                theater = theaterNetworkService?.CreateNewTheater(theaterName, country, city, street);

                if (theater is not null)
                {
                    Console.WriteLine("Poprawnie stworzono nowy teatr");
                }
                else
                {
                    Console.WriteLine("Stworzenie nowego teatru nie powiodło się");
                }

                break;
            case "2": // stwórz salę
                network = theaterNetworkService?.GetNetworkWithTheaters();
                if (network is null)
                {
                    Console.WriteLine("Nie znaleziono sieci");
                    break;
                }
                Console.WriteLine(network.GetTheatersString());
                if (network.Theaters.Count == 0) break;

                theater = ConsoleHelper.GetById(network.Theaters, t => t.TheaterId, "Podaj ID teatru: ");
                hallName = ConsoleHelper.UserInput("Podaj nazwę sali: ");
                hall = theaterService.CreateNewHall(hallName, theater.TheaterId);

                if (hall is not null)
                {
                    Console.WriteLine("Poprawnie stworzono nową salę");
                }
                else
                {
                    Console.WriteLine("Stworzenie nowej sali nie powiodło się");
                }

                break;
            case "3": // stwórz siedzenie
                network = theaterNetworkService?.GetFromNetworkToSeats();
                if (network is null)
                {
                    Console.WriteLine("Nie znaleziono sieci");
                    break;
                }
                Console.WriteLine(network.GetTheatersString());
                if (network.Theaters.Count == 0) break;
                
                theater = ConsoleHelper.GetById(network.Theaters, t => t.TheaterId, "Podaj ID teatru: ");
                Console.WriteLine(theater.GetHallsString());
                if (theater.Halls.Count == 0) break;

                hall = ConsoleHelper.GetById(theater.Halls, h => h.HallId, "Podaj ID sali: ");
                Console.WriteLine(hall.VisualizeSeatsString());

                rowNumber = ConsoleHelper.UserInputInt("Podaj numer rzędu nowego siedzenia: ");
                seatNumber = ConsoleHelper.UserInputInt("Podaj numer nowego siedzenia: ");
                seat = hallService.CreateNewSeat(rowNumber, seatNumber, hall.HallId);

                if (seat is not null)
                {
                    Console.WriteLine("Poprawnie stworzono nowe siedzenie");
                }
                else
                {
                    Console.WriteLine("Stworzenie nowego siedzenia nie powiodło się");
                }

                break;
            case "4": // stwórz siedzenia sali o podanych wymiarach
                network = theaterNetworkService?.GetFromNetworkToSeats();
                if (network is null)
                {
                    Console.WriteLine("Nie znaleziono sieci");
                    break;
                }
                Console.WriteLine(network.GetTheatersString());
                if (network.Theaters.Count == 0) break;

                theater = ConsoleHelper.GetById(network.Theaters, t => t.TheaterId, "Podaj ID teatru: ");
                Console.WriteLine(theater.GetHallsString());
                if (theater.Halls.Count == 0) break;

                hall = ConsoleHelper.GetById(theater.Halls, h => h.HallId, "Podaj ID sali: ");
                Console.WriteLine(hall.VisualizeSeatsString());

                rows = ConsoleHelper.UserInputInt("Podaj ilość rzędów: ");
                seatsPerRow = ConsoleHelper.UserInputInt("Podaj ilość siedzeń na rząd: ");

                if (hallService.CreateNewSeats(rows, seatsPerRow, hall.HallId))
                {
                    Console.WriteLine("Poprawnie stworzono nowe siedzenia");
                }
                else
                {
                    Console.WriteLine("Stworzenie nowych siedzeń nie powiodło się");
                }

                break;
            case "x":
                return;
            default:
                Console.WriteLine("Zły wybór");
                break;
        }
    }
}

void CreationMenu2_2()
{
    while (true)
    {
        DisplayMenu.Creation2_2();
        string input = ConsoleHelper.UserInput();
        string title;
        decimal price;
        int rowNumber;
        int seatNumber;
        Author? author;
        Director? director;
        Play play;
        DateTime startTime;
        DateTime endTime;
        TheaterNetwork? network;
        Theater? theater;
        Hall? hall;
        Seat? seat;
        Performance? performance;
        Ticket? ticket;
        switch (input)
        {
            case "1": // stwórz sztukę
                title = ConsoleHelper.UserInput("Podaj tytuł: ");
                if (ConsoleHelper.UserInputBool("Czy chcesz dodać autora?"))
                {
                    List<Author> authors = authorService.GetAllAuthors();
                    Console.WriteLine(authors.ListToString("Brak autorów", '-'));
                    if (authors.Count == 0) break;
                    author = ConsoleHelper.GetById(authors, a => a.Id, "Podaj ID autora: ");
                }
                else
                {
                    author = null;
                }
                if (ConsoleHelper.UserInputBool("Czy chcesz dodać reżysera?"))
                {
                    List<Director> directors = directorService.GetAllDirectors();
                    Console.WriteLine(directors.ListToString("Brak reżyserów", '-'));
                    if (directors.Count == 0) break;
                    director = ConsoleHelper.GetById(directors, d => d.Id, "Podaj ID reżysera: ");
                }
                else
                {
                    director = null;
                }
                if (playService.AddNewPlay(title, author, director))
                {
                    Console.WriteLine("Poprawnie stworzono nową sztukę");
                }
                else
                {
                    Console.WriteLine("Stworzenie nowej sztuki nie powiodło się");
                }
                break;
            case "2": // stwórz przedstawienie
                List<Play> plays = playService.GetAllPlays();
                Console.WriteLine(plays.ListToString("Nie można stworzyć przedstawienia przez brak sztuk", '-'));
                if (plays.Count == 0) break;
                play = ConsoleHelper.GetById(plays, p => p.PlayId, "Podaj ID sztuki: ");
                do
                {
                    do
                    {
                        startTime = ConsoleHelper.UserInputDateTime("Podaj datę i godzinę rozpoczęcia");
                        endTime = ConsoleHelper.UserInputDateTime("Podaj datę i godzinę zakończenia");
                        if (startTime < DateTime.Now) Console.WriteLine("Można dodać tylko przyszłe przedstawienia");
                    } while (startTime < DateTime.Now);
                    if (endTime <= startTime) Console.WriteLine("Czas zakończenia musi być późniejszy niż czas rozpoczęcia");
                } while (endTime <= startTime);
                if (performanceService.AddNewPerformance(play, startTime, endTime))
                {
                    Console.WriteLine("Poprawnie stworzono nową sztukę");
                }
                else
                {
                    Console.WriteLine("Stworzenie nowej sztuki nie powiodło się");
                }
                break;
            case "3": // stwórz bilet
                network = theaterNetworkService?.GetFromNetworkToTicket();
                if (network is null)
                {
                    Console.WriteLine("Nie znaleziono sieci");
                    break;
                }
                Console.WriteLine(network.GetTheatersString());
                if (network.Theaters.Count == 0) break;

                theater = ConsoleHelper.GetById(network.Theaters, t => t.TheaterId, "Podaj ID teatru: ");
                Console.WriteLine(theater.GetHallsString());
                if (theater.Halls.Count == 0) break;

                hall = ConsoleHelper.GetById(theater.Halls, h => h.HallId, "Podaj ID sali: ");
                if (hall.Seats.Count == 0)
                {
                    Console.WriteLine("Sala nie ma żadnych siedzeń");
                    break;
                }
                Console.WriteLine(hall.GetPerformancesString());
                if (hall.Performances.Count == 0) break;

                performance = ConsoleHelper.GetById(hall.Performances, p => p.PerformanceId, "Podaj ID przedstawienia: ");
                if (performance.Tickets.Count == hall.Seats.Count)
                {
                    Console.WriteLine("Wszystkie siedzenia na to przedstawienie mają bilet");
                    break;
                }
                Console.WriteLine(performance.VisualizeTicketsString());

                while (true)
                {
                    rowNumber = ConsoleHelper.UserInputInt("Podaj numer rzędu siedzenia: ");
                    seatNumber = ConsoleHelper.UserInputInt("Podaj numer siedzenia: ");
                    seat = hall.GetSeatByLocation(rowNumber, seatNumber);
                    if (seat is null || performance.Tickets.Any(t => t.Seat == seat))
                    {
                        Console.WriteLine("Siedzenie nie istnieje lub posiada już bilet");
                    }
                    else
                    {
                        break;
                    }
                }
                
                price = ConsoleHelper.UserInputDecimal("Podaj cenę biletu: ");
                
                ticket = performanceService.CreateNewTicket(price, seat, performance.PerformanceId);

                if (seat is not null)
                {
                    Console.WriteLine("Poprawnie stworzono nowe siedzenie");
                }
                else
                {
                    Console.WriteLine("Stworzenie nowego siedzenia nie powiodło się");
                }

                break;
            case "4": // stwórz bilety dla wszystkich siedzeń
                network = theaterNetworkService?.GetFromNetworkToTicket();
                if (network is null)
                {
                    Console.WriteLine("Nie znaleziono sieci");
                    break;
                }
                Console.WriteLine(network.GetTheatersString());
                if (network.Theaters.Count == 0) break;

                theater = ConsoleHelper.GetById(network.Theaters, t => t.TheaterId, "Podaj ID teatru: ");
                Console.WriteLine(theater.GetHallsString());
                if (theater.Halls.Count == 0) break;

                hall = ConsoleHelper.GetById(theater.Halls, h => h.HallId, "Podaj ID sali: ");
                if (hall.Seats.Count == 0)
                {
                    Console.WriteLine("Sala nie ma żadnych siedzeń");
                    break;
                }
                Console.WriteLine(hall.GetPerformancesString());
                if (hall.Performances.Count == 0) break;

                performance = ConsoleHelper.GetById(hall.Performances, p => p.PerformanceId, "Podaj ID przedstawienia: ");
                if (performance.Tickets.Count == hall.Seats.Count)
                {
                    Console.WriteLine("Wszystkie siedzenia na to przedstawienie mają bilet");
                    break;
                }
                Console.WriteLine(performance.VisualizeTicketsString());

                price = ConsoleHelper.UserInputDecimal("Podaj cenę dla wszystkich biletów: ");

                if (performanceService.CreateNewTickets(price, performance.PerformanceId))
                {
                    Console.WriteLine("Poprawnie stworzono nowe bilety");
                }
                else
                {
                    Console.WriteLine("Stworzenie nowych biletów nie powiodło się");
                }

                break;
            case "x":
                return;
            default:
                Console.WriteLine("Zły wybór");
                break;
        }
    }
}

void CreationMenu2_3()
{
    while (true)
    {
        DisplayMenu.Creation2_3();
        string input = ConsoleHelper.UserInput();
        string firstName;
        string lastName;
        int yearsOfExperience;
        decimal salary;
        switch (input)
        {
            case "1": // stwórz autora
                firstName = ConsoleHelper.UserInput("Podaj imię: ");
                lastName = ConsoleHelper.UserInput("Podaj nazwisko: ");
                if (authorService.AddNewAuthor(firstName, lastName))
                {
                    Console.WriteLine("Poprawnie stworzono nowego autora");
                }
                else
                {
                    Console.WriteLine("Stworzenie nowego autora nie powiodło się");
                }
                break;
            case "2": // stwórz reżysera
                firstName = ConsoleHelper.UserInput("Podaj imię: ");
                lastName = ConsoleHelper.UserInput("Podaj nazwisko: ");
                yearsOfExperience = ConsoleHelper.UserInputInt("Podaj ilość lat doświadczenia: ");
                salary = ConsoleHelper.UserInputDecimal("Podaj płacę: ");
                if (directorService.AddNewDirector(firstName, lastName, yearsOfExperience, salary))
                {
                    Console.WriteLine("Poprawnie stworzono nowego reżysera");
                }
                else
                {
                    Console.WriteLine("Stworzenie nowego reżysera nie powiodło się");
                }
                break;
            case "3": // stwórz aktora
                firstName = ConsoleHelper.UserInput("Podaj imię: ");
                lastName = ConsoleHelper.UserInput("Podaj nazwisko: ");
                salary = ConsoleHelper.UserInputDecimal("Podaj płacę: ");
                if (actorService.AddNewActor(firstName, lastName, salary))
                {
                    Console.WriteLine("Poprawnie stworzono nowego aktora");
                }
                else
                {
                    Console.WriteLine("Stworzenie nowego aktora nie powiodło się");
                }
                break;
            case "4": // stwórz klienta
                firstName = ConsoleHelper.UserInput("Podaj imię: ");
                lastName = ConsoleHelper.UserInput("Podaj nazwisko: ");
                if (customerService.AddNewCustomer(firstName, lastName))
                {
                    Console.WriteLine("Poprawnie stworzono nowego klienta");
                }
                else
                {
                    Console.WriteLine("Stworzenie nowego klienta nie powiodło się");
                }
                break;
            case "x":
                return;
            default:
                Console.WriteLine("Zły wybór");
                break;
        }
    }
}

/*
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
/**/