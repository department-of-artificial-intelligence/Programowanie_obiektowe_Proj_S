// See https://aka.ms/new-console-template for more information

using Project.ConsoleApp;
using Project.Model;
using System.Reflection.Metadata;

/* ToDo:
 * ogólne:
 * faktyczny program.cs
 * testy jednostkowe
 * baza danych
 * 
 * metody:
 * wyświetlenie siedzeń zmienić aby było widać które zajęte? tak samo ale D-dostępne, Z-zarezerwowane, S-sprzedane ?
 * zarządanie biletami
 */

try
{
    Console.WriteLine("--------------------------------------------");
    Console.WriteLine("System zarządzania siecią teatrów");
    Console.WriteLine("--------------------------------------------");

    // ------------------------
    // struktura teatrów
    // ------------------------

    TheaterNetwork siecTeatrow = new TheaterNetwork("Globalna Sieć Teatralna");
    //Console.WriteLine(siecTeatrow);
    //Console.WriteLine();

    siecTeatrow.CreateTheater(1, "Teatr Stary", "Polska", "Kraków", "Jagiellońska 1");
    siecTeatrow.CreateTheater(2, "Teatr Wielki - Opera Narodowa", "Polska", "Warszawa", "Plac Teatralny 1");
    siecTeatrow.CreateTheater(3, "Teatr Muzyczny Capitol", "Polska", "Wrocław", "Piłsudskiego 67");

    //Console.WriteLine(siecTeatrow);
    //Console.WriteLine();

    var theater1 = siecTeatrow.Theaters[0];
    var theater2 = siecTeatrow.Theaters[1];
    var theater3 = siecTeatrow.Theaters[2];

    theater1.CreateHall(1);
    theater1.CreateHall(2);
    theater2.CreateHall(3);
    theater2.CreateHall(4);
    theater2.CreateHall(5);
    theater3.CreateHall(6);
    theater3.CreateHall(7);

    Console.WriteLine(siecTeatrow);
    Console.WriteLine();

    var hall1 = siecTeatrow.Theaters[0].Halls[0];
    var hall2 = siecTeatrow.Theaters[0].Halls[1];

    hall1.CreateSeat(2, 2);
    hall1.CreateSeat(1, 2);
    hall1.CreateSeat(1, 1);
    hall1.CreateSeat(1, 1); // powtórzenie siedzenia

    Console.WriteLine("Siedzenia w Sali 1:");
    Console.WriteLine(hall1.VisualizeSeatsString());

    Console.WriteLine("Siedzenia w Sali 1:");
    Console.WriteLine(hall1.GetSeatsString());

    Console.WriteLine("\nSiedzenia w Sali 2:");
    Console.WriteLine(hall2.VisualizeSeatsString());

    // ------------------------
    // osoby i sztuki
    // ------------------------

    var author1 = new Author(1, "Jan", "Kowalski");
    var author2 = new Author(2, "Anna", "Nowak");

    var director1 = new Director(1, "Michał", "Wiśniewski", 15, 12000m);
    var director2 = new Director(2, "Ewa", "Zielińska", 10, 9000m);

    var actor1 = new Actor(1, "Tomasz", "Adamski", 5000m);
    var actor2 = new Actor(2, "Katarzyna", "Lewandowska", 5500m);
    var actor3 = new Actor(3, "Paweł", "Sikora", 4800m);

    var customer1 = new Customer(1, "Marta", "Kaczmarek");
    var customer2 = new Customer(2, "Robert", "Wiśniewski");

    var play1 = new Play(1, "Hamlet", author1, director1);
    var play2 = new Play(2, "Romeo i Julia", author2, director2);
    var play3 = new Play(3, "Makbet", author1, director2);
    var play4 = new Play(4, "Sen nocy letniej", author2, director1);
    var play5 = new Play(5, "Król Lear", author1, director1);

    play1.AddActor(actor1);
    play1.AddActor(actor2);

    play2.AddActor(actor2);
    play2.AddActor(actor3);

    play3.AddActor(actor1);
    play3.AddActor(actor3);

    play4.AddActor(actor2);

    play5.AddActor(actor1);
    play5.AddActor(actor2);
    play5.AddActor(actor3);

    Console.WriteLine("--------------------------------------------");

    Console.WriteLine("Klienci:");
    Console.WriteLine(customer1);
    Console.WriteLine(customer2);

    Console.WriteLine("--------------------------------------------");

    Console.WriteLine("Sztuki:");
    Console.WriteLine(play1);
    Console.WriteLine(play1.GetActorsString());
    Console.WriteLine(play2);
    Console.WriteLine(play2.GetActorsString());
    Console.WriteLine(play3);
    Console.WriteLine(play3.GetActorsString());
    Console.WriteLine(play4);
    Console.WriteLine(play4.GetActorsString());
    Console.WriteLine(play5);
    Console.WriteLine(play5.GetActorsString());

    Console.WriteLine("--------------------------------------------");

    Console.WriteLine("Aktorzy:");
    Console.WriteLine(actor1);
    Console.WriteLine(actor1.GetPlaysString());
    Console.WriteLine(actor2);
    Console.WriteLine(actor2.GetPlaysString());
    Console.WriteLine(actor3);
    Console.WriteLine(actor2.GetPlaysString());

    Console.WriteLine("--------------------------------------------");

    Console.WriteLine("Reżyserzy:");
    Console.WriteLine(director1);
    Console.WriteLine(director1.GetPlaysString());
    Console.WriteLine(director2);
    Console.WriteLine(director2.GetPlaysString());

    Console.WriteLine("--------------------------------------------");

    Console.WriteLine("Autorzy:");
    Console.WriteLine(author1);
    Console.WriteLine(author1.GetPlaysString());
    Console.WriteLine(author2);
    Console.WriteLine(author2.GetPlaysString());
    Console.WriteLine();

    // ------------------------
    // przedstawienia
    // ------------------------

    var performance1 = new Performance(
        1,
        play1,
        new DateTime(2025, 12, 5, 19, 0, 0),
        new DateTime(2025, 12, 5, 21, 0, 0)
    );

    var performance2 = new Performance(
        2,
        play2,
        new DateTime(2025, 12, 6, 18, 0, 0),
        new DateTime(2025, 12, 6, 20, 0, 0)
    );

    var performance3 = new Performance(
        3,
        play3,
        new DateTime(2025, 12, 7, 19, 30, 0),
        new DateTime(2025, 12, 7, 21, 30, 0)
    );

    hall1.AddPerformance(performance1);
    hall1.AddPerformance(performance2);
    hall2.AddPerformance(performance3);

    Console.WriteLine("Przedstawienia w Sali 1:");
    Console.WriteLine(hall1.GetPerformancesString());

    Console.WriteLine("--------------------------------------------");

    // ------------------------------
    // bilety i rozszerzenie siedzeń
    // ------------------------------

    performance1.CreateTicketForEverySeat(15.90m);
    Console.WriteLine(performance1.GetTicketsString());

    hall1.CreateSeats(4, 6);
    Console.WriteLine("Siedzenia w Sali 1:");
    Console.WriteLine(hall1.GetSeatsString());
    Console.WriteLine("Siedzenia w Sali 1:");
    Console.WriteLine(hall1.VisualizeSeatsString());
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine("Błąd zakresu: " + ex.Message);
}
catch (ArgumentNullException ex)
{
    Console.WriteLine("Błąd argumentu null: " + ex.Message);
}
catch (ArgumentException ex)
{
    Console.WriteLine("Błąd argumentu: " + ex.Message);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine("Błąd operacji: " + ex.Message);
}

//test
/*
string userInput = string.Empty;
Console.WriteLine("==================================");
Console.WriteLine("System zarządzania siecią teatrów");
Console.WriteLine("==================================");

Console.Write("Podaj nazwę sieci: ");
userInput = Console.ReadLine();
TheaterNetwork theaterNetwork = new TheaterNetwork(userInput);

List<Director> directors = new List<Director>();
List<Actor> actors = new List<Actor>();
List<Author> authors = new List<Author>();
List<Customer> customers = new List<Customer>();
List<Play> plays = new List<Play>();
List<Performance> performances = new List<Performance>();

while (true)
{
    try
    {
        Menu.StartOptions();
        Console.Write("> ");
        userInput = Console.ReadLine();
        if (userInput == "x") break;
        switch (userInput)
        {
            case "1":
                while (true)
                {
                    Menu.CreationOptions();
                    Console.Write("> ");
                    userInput = Console.ReadLine();
                    if (userInput == "x") break;
                    switch (userInput) 
                    {
                        case "1":
                            while (true)
                            {
                                Menu.TheaterCreationOptions();
                                Console.Write("> ");
                                userInput = Console.ReadLine();
                                if (userInput == "x") break;
                                switch (userInput)
                                {
                                    case "1":
                                        Console.Write("Podaj nazwę teatru: ");
                                        string name;
                                        name = Console.ReadLine();
                                        Console.Write("Podaj kraj: ");
                                        string country;
                                        country = Console.ReadLine();
                                        Console.Write("Podaj miasto: ");
                                        string city;
                                        city = Console.ReadLine();
                                        Console.Write("Podaj ulicę: ");
                                        string street;
                                        street = Console.ReadLine();
                                        bool isCreated = theaterNetwork.CreateTheater(name, country, city, street);
                                        Console.WriteLine(isCreated ? $"Poprawnie utworzono teatr" : "Wystąpił błąd podczas tworzenia");
                                        break;
                                    case "2":
                                        break;
                                    case "3":
                                        break;
                                }
                            }
                        break;
                        case "2":
                            break;
                        case "3":
                            break;
                        case "4":
                            break;
                        case "5":
                            break;
                    }
                }
                userInput = string.Empty;
                break;
            case "2":
                Console.WriteLine("not implemented yet");
                userInput = string.Empty;
                break;
            case "3":
                Console.WriteLine("not implemented yet");
                userInput = string.Empty;
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("================Error Message================");
        Console.WriteLine(ex.Message);
    }
}
*/