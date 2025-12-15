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

performanceService.UpdateStatuses();

Console.WriteLine("-------------------------------------------------------------");
Console.WriteLine($"System zarządzania siecią teatrów: {theaterNetworkService?.GetNetwork()?.NetworkName}");
Console.WriteLine("-------------------------------------------------------------");

while (true)
{
    DisplayMenu.MainMenu();
    string input = ConsoleHelper.UserInput();
    switch (input)
    {
        case "1": // wyświetlanie
            ViewMenu1();
            break;
        case "2": // tworzenie
            CreationMenu2(); 
            break;
        case "3": // zarządzanie
            ManagementMenu3();
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
                List<Author> authors = authorService.GetAuthorsWithPlays();
                Console.WriteLine("Lista autorów:");
                Console.WriteLine(authors.ListToString("Brak autorów", '-'));
                if (authors.Count == 0) break;
                ViewMenu1_2(authors);
                break;
            case "3": // wyświetl listę reżyserów
                List<Director> directors = directorService.GetDirectorsWithPlays();
                Console.WriteLine("Lista reżyserów:");
                Console.WriteLine(directors.ListToString("Brak reżyserów", '-'));
                if (directors.Count == 0) break;
                ViewMenu1_3(directors);
                break;
            case "4": // wyświetl listę aktorów
                List<Actor> actors = actorService.GetActorsWithPlays();
                Console.WriteLine("Lista aktorów:");
                Console.WriteLine(actors.ListToString("Brak aktorów", '-'));
                if (actors.Count == 0) break;
                ViewMenu1_4(actors);
                break;
            case "5": // wyświetl listę klientów
                List<Customer> customers = customerService.GetCustomers();
                Console.WriteLine("Lista klientów:");
                Console.WriteLine(customers.ListToString("Brak klientów", '-'));
                if (customers.Count == 0) break;
                ViewMenu1_5(customers);
                break;
            case "6": // wyświetl listę sztuk
                List<Play> plays = playService.GetPlays();
                Console.WriteLine("Lista sztuk:");
                Console.WriteLine(plays.ListToString("Brak sztuk", '-'));
                break;
            case "7": // wyświetl listę przedstawień bez sali
                performanceService.UpdateStatuses();
                List<Performance> performances = performanceService.GetPerformancesWithoutHall();
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
                performanceService.UpdateStatuses();
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
        performanceService.UpdateStatuses();
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
                seat = hallService.CreateNewSeat(rowNumber, seatNumber, hall);

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

                if (hallService.CreateNewSeats(rows, seatsPerRow, hall))
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
                    List<Author> authors = authorService.GetAuthorsWithPlays();
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
                    List<Director> directors = directorService.GetDirectorsWithPlays();
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
                List<Play> plays = playService.GetPlays();
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
                performanceService.UpdateStatuses();
                Console.WriteLine(hall.GetPerformancesString());
                if (hall.Performances.Count == 0) break;

                performance = ConsoleHelper.GetById(hall.Performances, p => p.PerformanceId, "Podaj ID przedstawienia: ");
                if (performance.Tickets.Count >= hall.Seats.Count)
                {
                    Console.WriteLine("Wszystkie siedzenia na to przedstawienie mają bilet");
                    break;
                }
                if (performance.Status != PerformanceStatus.Scheduled)
                {
                    Console.WriteLine("Przedstawienie jest odwołane, jest w trakcie lub skończyło się");
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
                
                ticket = performanceService.CreateNewTicket(price, seat, performance);

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
                performanceService.UpdateStatuses();
                Console.WriteLine(hall.GetPerformancesString());
                if (hall.Performances.Count == 0) break;

                performance = ConsoleHelper.GetById(hall.Performances, p => p.PerformanceId, "Podaj ID przedstawienia: ");
                if (performance.Tickets.Count >= hall.Seats.Count)
                {
                    Console.WriteLine("Wszystkie siedzenia na to przedstawienie mają bilet");
                    break;
                }
                if (performance.Status != PerformanceStatus.Scheduled)
                {
                    Console.WriteLine("Przedstawienie jest odwołane, jest w trakcie lub skończyło się");
                    break;
                }
                Console.WriteLine(performance.VisualizeTicketsString());

                price = ConsoleHelper.UserInputDecimal("Podaj cenę dla wszystkich biletów: ");

                if (performanceService.CreateNewTickets(price, performance))
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

void ManagementMenu3()
{
    while (true)
    {
        DisplayMenu.Management3();
        string input = ConsoleHelper.UserInput();
        switch (input)
        {
            case "1": // zarządaj klientami
                List<Customer> customers = customerService.GetCustomers();
                Console.WriteLine("Lista klientów:");
                Console.WriteLine(customers.ListToString("Brak klientów", '-'));
                if (customers.Count == 0) break;
                Customer customer = ConsoleHelper.GetById(customers, a => a.Id, "Podaj ID klienta: ");
                Console.WriteLine("Lista biletów klienta:");
                Console.WriteLine(customer.GetTicketsString());
                ManagementMenu3_1(customer);
                break;
            case "2": // zarządaj autorami
                List<Author> authors = authorService.GetAuthorsWithPlays();
                Console.WriteLine("Lista autorów:");
                Console.WriteLine(authors.ListToString("Brak autorów", '-'));
                if (authors.Count == 0) break;
                Author author = ConsoleHelper.GetById(authors, a => a.Id, "Podaj ID autora: ");
                ManagementMenu3_2(author);
                break;
            case "3": // zarządzaj reżyserami
                List<Director> directors = directorService.GetDirectorsWithPlays();
                Console.WriteLine("Lista reżyserów:");
                Console.WriteLine(directors.ListToString("Brak reżyserów", '-'));
                if (directors.Count == 0) break;
                Director director = ConsoleHelper.GetById(directors, a => a.Id, "Podaj ID reżysera: ");
                ManagementMenu3_3(director);
                break;
            case "4": // zarządzaj aktorami
                List<Actor> actors = actorService.GetActorsWithPlays();
                Console.WriteLine("Lista aktorów:");
                Console.WriteLine(actors.ListToString("Brak aktorów", '-'));
                if (actors.Count == 0) break;
                Actor actor = ConsoleHelper.GetById(actors, a => a.Id, "Podaj ID aktora: ");
                ManagementMenu3_4(actor);
                break;
            case "5": // zarządzaj zaplanowanymi przedstawieniami
                Performance performance;
                performanceService.UpdateStatuses();
                List<Performance> performances = performanceService.GetPerformancesWithStatus(PerformanceStatus.Scheduled);
                Console.WriteLine(performances.ListToString("Brak sztuk", '-'));
                if (performances.Count == 0) break;
                performance = ConsoleHelper.GetById(performances, p => p.PerformanceId, "Podaj ID przedstawienia: ");
                ManagementMenu3_5(performance);
                break;
            case "x":
                return;
            default:
                Console.WriteLine("Zły wybór");
                break;
        }
    }
}

void ManagementMenu3_1(Customer customer)
{
    while (true)
    {
        DisplayMenu.Management3_1();
        string input = ConsoleHelper.UserInput();
        int rowNumber;
        int seatNumber;
        TheaterNetwork? network;
        Theater? theater;
        Hall? hall;
        Performance? performance;
        Ticket? ticket;
        switch (input)
        {
            case "0":
                Console.WriteLine(customer.GetTicketsString());
                break;
            case "1": // zarezerwuj bilet
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
                performanceService.UpdateStatuses();
                Console.WriteLine(hall.GetPerformancesString());
                if (hall.Performances.Count == 0) break;

                performance = ConsoleHelper.GetById(hall.Performances, p => p.PerformanceId, "Podaj ID przedstawienia: ");
                if (performance.Tickets.All(t => t.Status != TicketStatus.Available))
                {
                    Console.WriteLine("Wszystkie bilety są niedostępne");
                    break;
                }
                if (performance.Status != PerformanceStatus.Scheduled)
                {
                    Console.WriteLine("Przedstawienie jest odwołane, jest w trakcie lub skończyło się");
                    break;
                }
                Console.WriteLine(performance.VisualizeTicketsString());
                if (performance.Tickets.Count == 0) break;

                while (true)
                {
                    rowNumber = ConsoleHelper.UserInputInt("Podaj numer rzędu siedzenia: ");
                    seatNumber = ConsoleHelper.UserInputInt("Podaj numer siedzenia: ");
                    ticket = performance.GetTicketBySeatLocation(rowNumber, seatNumber);
                    if (ticket is null || ticket.Status != TicketStatus.Available)
                    {
                        Console.WriteLine("Bilet nie istnieje lub jest niedostępny");
                    }
                    else
                    {
                        break;
                    }
                }

                if (customerService.ReserveTicket(customer, ticket))
                {
                    Console.WriteLine("Poprawnie zarezerwowano bilet");
                }
                else
                {
                    Console.WriteLine("Anulowanie rezerwacji nie powiodło się");
                }
                break;
            case "2": // anuluj rezerwację
                Console.WriteLine(customer.GetTicketsWithStatusString(TicketStatus.Reserved));
                if (!customer.Tickets.Any(t => t.Status == TicketStatus.Reserved)) break;
                ticket = ConsoleHelper.GetById(customer.Tickets.Where(t => t.Status == TicketStatus.Reserved), c => c.TicketId, "Podaj ID biletu: ");

                if (customerService.CancelReservation(customer, ticket))
                {
                    Console.WriteLine("Poprawnie anulowano rezerwację biletu");
                }
                else
                {
                    Console.WriteLine("Anulowanie rezerwacji nie powiodło się");
                }
                break;
            case "3": // anuluj wszystkie rezerwacje
                if (customerService.CancelAllReserved(customer))
                {
                    Console.WriteLine("Poprawnie anulowano wszystkie zarezerwowane bilety");
                }
                else
                {
                    Console.WriteLine("Anulowanie wszystkich zarezerwowanych biletów nie powiodło się");
                }
                break;
            case "4": // kup dostępny bilet
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
                performanceService.UpdateStatuses();
                Console.WriteLine(hall.GetPerformancesString());
                if (hall.Performances.Count == 0) break;

                performance = ConsoleHelper.GetById(hall.Performances, p => p.PerformanceId, "Podaj ID przedstawienia: ");
                if (performance.Tickets.All(t => t.Status != TicketStatus.Available))
                {
                    Console.WriteLine("Wszystkie bilety są niedostępne");
                    break;
                }
                if (performance.Status != PerformanceStatus.Scheduled)
                {
                    Console.WriteLine("Przedstawienie jest odwołane, jest w trakcie lub skończyło się");
                    break;
                }
                Console.WriteLine(performance.VisualizeTicketsString());
                if (performance.Tickets.Count == 0) break;

                while (true)
                {
                    rowNumber = ConsoleHelper.UserInputInt("Podaj numer rzędu siedzenia: ");
                    seatNumber = ConsoleHelper.UserInputInt("Podaj numer siedzenia: ");
                    ticket = performance.GetTicketBySeatLocation(rowNumber, seatNumber);
                    if (ticket is null || ticket.Status != TicketStatus.Available)
                    {
                        Console.WriteLine("Bilet nie istnieje lub jest niedostępny");
                    }
                    else
                    {
                        break;
                    }
                }

                if (customerService.BuyTicket(customer, ticket))
                {
                    Console.WriteLine("Poprawnie kupiono bilet");
                }
                else
                {
                    Console.WriteLine("Kupno biletu nie powiodło się");
                }
                break;
            case "5": // kup zarezerwowany bilet
                Console.WriteLine(customer.GetTicketsWithStatusString(TicketStatus.Reserved));
                if (!customer.Tickets.Any(t => t.Status == TicketStatus.Reserved)) break;
                ticket = ConsoleHelper.GetById(customer.Tickets.Where(t => t.Status == TicketStatus.Reserved), c => c.TicketId, "Podaj ID biletu: ");

                if (customerService.BuyTicket(customer, ticket))
                {
                    Console.WriteLine("Poprawnie kupiono bilet");
                }
                else
                {
                    Console.WriteLine("Kupno biletu nie powiodło się");
                }
                break;
            case "6": // kup wszystkie zarezerwowane
                if (customerService.BuyAllReserved(customer))
                {
                    Console.WriteLine("Poprawnie kupiono wszystkie zarezerwowane bilety");
                }
                else
                {
                    Console.WriteLine("Kupno wszystkich zarezerwowanych biletów nie powiodło się");
                }
                break;
            case "7": // zwróć bilet
                Console.WriteLine(customer.GetTicketsWithStatusString(TicketStatus.Sold));
                if (!customer.Tickets.Any(t => t.Status == TicketStatus.Sold)) break;
                ticket = ConsoleHelper.GetById(customer.Tickets.Where(t => t.Status == TicketStatus.Sold), c => c.TicketId, "Podaj ID biletu: ");

                if (customerService.RefundTicket(customer, ticket))
                {
                    Console.WriteLine("Poprawnie zwrócono bilet");
                }
                else
                {
                    Console.WriteLine("Zwrot biletu nie powiódł się");
                }
                break;
            case "8": // zwróć wszystkie kupione
                if (customerService.RefundAllBought(customer))
                {
                    Console.WriteLine("Poprawnie zwrócono wszystkie kupione bilety");
                } 
                else
                {
                    Console.WriteLine("Zwrot wszystkich kupionych biletów nie powiódł się");
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

void ManagementMenu3_2(Author author)
{
    while (true)
    {
        DisplayMenu.Management3_2();
        string input = ConsoleHelper.UserInput();
        Play play;
        List<Play> plays = playService.GetPlays();
        switch (input)
        {
            case "0":
                Console.WriteLine(author.GetPlaysString());
                break;
            case "1": // dodaj sztukę do listy autora

                Console.WriteLine(plays.Where(t => t.Author == null).ToList().ListToString("Brak sztuk", '-'));
                if (plays.Where(t => t.Author == null).ToList().Count == 0) break;
                play = ConsoleHelper.GetById(plays.Where(t => t.Author == null), p => p.PlayId, "Podaj ID sztuki: ");
                if (authorService.AddPlay(author, play))
                {
                    Console.WriteLine("Poprawnie przypisano sztukę do autora");
                }
                else
                {
                    Console.WriteLine("Przypisanie sztuki do autora nie powiodło się");
                }
                break;
            case "2": // usuń sztukę z listy autora
                Console.WriteLine(author.GetPlaysString());
                play = ConsoleHelper.GetById(author.Plays, p => p.PlayId, "Podaj ID sztuki: ");
                if (authorService.RemovePlay(author, play))
                {
                    Console.WriteLine("Poprawnie usunięto sztukę z listy autora");
                }
                else
                {
                    Console.WriteLine("Usunięcie sztuki z listy autora nie powiodło się");
                }
                break;
            case "3": // usuń wszystkie sztuki z listy autora
                if (authorService.RemoveAllPlays(author))
                {
                    Console.WriteLine("Poprawnie usunięto wszystkie sztuki z listy autora");
                }
                else
                {
                    Console.WriteLine("Usunięcie wszystkich sztuk z listy autora nie powiodło się");
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

void ManagementMenu3_3(Director director)
{
    while (true)
    {
        DisplayMenu.Management3_3();
        string input = ConsoleHelper.UserInput();
        Play play;
        List<Play> plays = playService.GetPlays();
        switch (input)
        {
            case "0":
                Console.WriteLine(director.GetPlaysString());
                break;
            case "1": // dodaj sztukę do listy reżysera
                Console.WriteLine(plays.Where(t => t.Director == null).ToList().ListToString("Brak sztuk", '-'));
                if (plays.Where(t => t.Director == null).ToList().Count == 0) break;
                play = ConsoleHelper.GetById(plays.Where(t => t.Director == null), p => p.PlayId, "Podaj ID sztuki: ");
                if (directorService.AddPlay(director, play))
                {
                    Console.WriteLine("Poprawnie przypisano sztukę do reżysera");
                }
                else
                {
                    Console.WriteLine("Przypisanie sztuki do reżysera nie powiodło się");
                }
                break;
            case "2": // usuń sztukę z listy reżysera
                Console.WriteLine(director.GetPlaysString());
                play = ConsoleHelper.GetById(director.Plays, p => p.PlayId, "Podaj ID sztuki: ");
                if (directorService.RemovePlay(director, play))
                {
                    Console.WriteLine("Poprawnie usunięto sztukę z listy reżysera");
                }
                else
                {
                    Console.WriteLine("Usunięcie sztuki z listy reżysera nie powiodło się");
                }
                break;
            case "3": // usuń wszystkie sztuki z listy reżysera
                if (directorService.RemoveAllPlays(director))
                {
                    Console.WriteLine("Poprawnie usunięto wszystkie sztuki z listy reżysera");
                }
                else
                {
                    Console.WriteLine("Usunięcie wszystkich sztuk z listy reżysera nie powiodło się");
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

void ManagementMenu3_4(Actor actor)
{
    while (true)
    {
        DisplayMenu.Management3_4();
        string input = ConsoleHelper.UserInput();
        Play play;
        List<Play> plays = playService.GetPlays();
        switch (input)
        {
            case "0":
                Console.WriteLine(actor.GetPlaysString());
                break;
            case "1": // dodaj sztukę do listy aktora
                Console.WriteLine(plays.Where(t => !t.Actors.Contains(actor)).ToList().ListToString("Brak sztuk", '-'));
                if (plays.Where(t => !t.Actors.Contains(actor)).ToList().Count == 0) break;
                play = ConsoleHelper.GetById(plays.Where(t => !t.Actors.Contains(actor)), p => p.PlayId, "Podaj ID sztuki: ");
                if (actorService.AddPlay(actor, play))
                {
                    Console.WriteLine("Poprawnie przypisano sztukę do aktora");
                }
                else
                {
                    Console.WriteLine("Przypisanie sztuki do aktora nie powiodło się");
                }
                break;
            case "2": // usuń sztukę z listy aktora
                Console.WriteLine(actor.GetPlaysString());
                if (actor.Plays.Count == 0) break;
                play = ConsoleHelper.GetById(actor.Plays, p => p.PlayId, "Podaj ID sztuki: ");
                if (actorService.RemovePlay(actor, play))
                {
                    Console.WriteLine("Poprawnie usunięto sztukę z listy aktora");
                }
                else
                {
                    Console.WriteLine("Usunięcie sztuki z listy aktora nie powiodło się");
                }
                break;
            case "3": // usuń wszystkie sztuki z listy aktora
                if (actorService.RemoveAllPlays(actor))
                {
                    Console.WriteLine("Poprawnie usunięto wszystkie sztuki z listy aktora");
                }
                else
                {
                    Console.WriteLine("Usunięcie wszystkich sztuk z listy aktora nie powiodło się");
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

void ManagementMenu3_5(Performance performance)
{
    while (true)
    {
        DisplayMenu.Management3_5();
        string input = ConsoleHelper.UserInput();
        switch (input)
        {
            case "1": // dodaj salę
                if (performance.Hall is not null)
                {
                    Console.WriteLine("Przedstawienie ma już przypisaną salę");
                    break;
                }
                TheaterNetwork? network = theaterNetworkService?.GetFromNetworkToPlay();
                if (network is null)
                {
                    Console.WriteLine("Nie znaleziono sieci");
                    break;
                }
                Console.WriteLine("Lista teatrów:");
                Console.WriteLine(network.GetTheatersString());
                if (network.Theaters.Count == 0) break;

                Theater theater = ConsoleHelper.GetById(network.Theaters, t => t.TheaterId, "Podaj ID teatru: ");
                Console.WriteLine($"Lista sal w teatrze {theater.TheaterName}:");
                Console.WriteLine(theater.GetHallsString());
                if (theater.Halls.Count == 0) break;

                Hall hall = ConsoleHelper.GetById(theater.Halls, h => h.HallId, "Podaj ID sali: ");

                try
                {
                    if (performanceService.AddHall(hall, performance))
                    {
                        Console.WriteLine("Poprawnie przypisano przedstawienie do sali");
                    }
                    else
                    {
                        Console.WriteLine("Przypisanie przedstawienia do sali nie powiodło się");
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
                
                break;
            case "2": // odwołaj przedstawienie
                performance.Status = PerformanceStatus.Canceled;
                Console.WriteLine("Przedstawienie zostało odwołane i bilety zostały zwrócone");
                return;
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