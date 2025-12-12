using Project.ConsoleApp;
using Project.Model;

/* ToDo:
 * ogólne:
 * faktyczny program.cs
 * testy jednostkowe
 * baza danych
 */

Console.WriteLine("--------------------------------------------");
Console.WriteLine("System zarządzania siecią teatrów");
Console.WriteLine("--------------------------------------------");

try
{
    // ------------------------
    // struktura teatrów
    // ------------------------

    TheaterNetwork siecTeatrow = new TheaterNetwork("Globalna Sieć Teatralna");

    siecTeatrow.CreateTheater("Teatr Stary", "Polska", "Kraków", "Jagiellońska 1");
    siecTeatrow.CreateTheater("Teatr Wielki - Opera Narodowa", "Polska", "Warszawa", "Plac Teatralny 1");
    siecTeatrow.CreateTheater("Teatr Muzyczny Capitol", "Polska", "Wrocław", "Piłsudskiego 67");

    var theater1 = siecTeatrow.Theaters[0];
    var theater2 = siecTeatrow.Theaters[1];
    var theater3 = siecTeatrow.Theaters[2];

    theater1.CreateHall("Duża Scena");
    theater1.CreateHall("Scena Kameralna");
    theater2.CreateHall("Główna");
    theater3.CreateHall("Musicalowa");

    Console.WriteLine(siecTeatrow);
    Console.WriteLine();

    var hall1 = siecTeatrow.Theaters[0].Halls[0]; // Duża Scena

    hall1.CreateSeat(2, 2);
    hall1.CreateSeat(1, 2);
    hall1.CreateSeat(1, 1);
    hall1.CreateSeat(1, 1);

    Console.WriteLine("Siedzenia w Sali 1 (po utworzeniu):");
    Console.WriteLine(hall1.VisualizeSeatsString());
    Console.WriteLine();

    // ------------------------
    // osoby i sztuki
    // ------------------------

    var author1 = new Author("Jan", "Kowalski");
    var author2 = new Author("Anna", "Nowak");

    var director1 = new Director("Michał", "Wiśniewski", 15, 12000m);
    var director2 = new Director("Ewa", "Zielińska", 10, 9000m);

    var actor1 = new Actor("Tomasz", "Adamski", 5000m);
    var actor2 = new Actor("Katarzyna", "Lewandowska", 5500m);
    var actor3 = new Actor("Paweł", "Sikora", 4800m);

    var customer1 = new Customer("Marta", "Kaczmarek");
    var customer2 = new Customer("Robert", "Wiśniewski");

    var play1 = new Play("Hamlet", author1, director1);
    var play2 = new Play("Romeo i Julia", author2, director2);

    actor1.AddPlay(play1);
    actor2.AddPlay(play1);

    Console.WriteLine("--------------------------------------------");
    Console.WriteLine("Sztuki:");
    Console.WriteLine(play1);
    Console.WriteLine(play1.GetActorsString());
    Console.WriteLine("--------------------------------------------");

    // ------------------------
    // przedstawienia
    // ------------------------

    var performance1 = new Performance(
        play1,
        new DateTime(2025, 12, 5, 19, 0, 0),
        new DateTime(2025, 12, 5, 21, 0, 0)
    );

    hall1.AddPerformance(performance1);

    Console.WriteLine("Przedstawienia w Sali 1:");
    Console.WriteLine(hall1.GetPerformancesString());
    Console.WriteLine("--------------------------------------------");

    // ------------------------------
    // bilety i wizualizacja statusu
    // ------------------------------

    performance1.CreateTicketForEverySeat(15.90m);

    Console.WriteLine("Wizualizacja biletów przed transakcjami:");
    Console.WriteLine(performance1.VisualizeTicketsString());

    var biletKupiony = performance1.Tickets
        .FirstOrDefault(t => t.Seat.RowNumber == 1 && t.Seat.SeatNumber == 1);

    var biletZarezerwowany = performance1.Tickets
        .FirstOrDefault(t => t.Seat.RowNumber == 2 && t.Seat.SeatNumber == 2);

    if (biletKupiony != null && biletZarezerwowany != null)
    {
        customer1.BuyTicket(biletKupiony);

        customer2.ReserveTicket(biletZarezerwowany);

        Console.WriteLine($"\n--- Transakcje ---");
        Console.WriteLine($"Kupiono bilet (1, 1) przez {customer1.FirstName}");
        Console.WriteLine($"Zarezerwowano bilet (2, 2) przez {customer2.FirstName}");
        Console.WriteLine("--------------------------------------------");

        Console.WriteLine("Wizualizacja biletów dla Performance 1 (Hamlet) PO transakcjach:");
        Console.WriteLine(performance1.VisualizeTicketsString());
    }
    else
    {
        Console.WriteLine("Brak niektórych biletów, nie przeprowadzono transakcji testowych.");
    }

    customer1.RefundAllBought();
    customer2.CancelAllReserved();

    Console.WriteLine("--------------------------------------------");
    Console.WriteLine("Wizualizacja biletów po anulowaniu i zwrocie (wszystkie powinny być 'D'):");
    Console.WriteLine(performance1.VisualizeTicketsString());

    // ------------------------------
    // rozszerzenie siedzeń
    // ------------------------------

    hall1.CreateSeats(4, 6);
    Console.WriteLine("\nSiedzenia w Sali 1 (po rozszerzeniu - nowo dodane miejsca):");
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
catch (Exception ex)
{
    Console.WriteLine("Wystąpił nieoczekiwany błąd: " + ex.Message);
}

////test
///*
//string userInput = string.Empty;
//Console.WriteLine("==================================");
//Console.WriteLine("System zarządzania siecią teatrów");
//Console.WriteLine("==================================");

//Console.Write("Podaj nazwę sieci: ");
//userInput = Console.ReadLine();
//TheaterNetwork theaterNetwork = new TheaterNetwork(userInput);

//List<Director> directors = new List<Director>();
//List<Actor> actors = new List<Actor>();
//List<Author> authors = new List<Author>();
//List<Customer> customers = new List<Customer>();
//List<Play> plays = new List<Play>();
//List<Performance> performances = new List<Performance>();

//while (true)
//{
//    try
//    {
//        Menu.StartOptions();
//        Console.Write("> ");
//        userInput = Console.ReadLine();
//        if (userInput == "x") break;
//        switch (userInput)
//        {
//            case "1":
//                while (true)
//                {
//                    Menu.CreationOptions();
//                    Console.Write("> ");
//                    userInput = Console.ReadLine();
//                    if (userInput == "x") break;
//                    switch (userInput) 
//                    {
//                        case "1":
//                            while (true)
//                            {
//                                Menu.TheaterCreationOptions();
//                                Console.Write("> ");
//                                userInput = Console.ReadLine();
//                                if (userInput == "x") break;
//                                switch (userInput)
//                                {
//                                    case "1":
//                                        Console.Write("Podaj nazwę teatru: ");
//                                        string name;
//                                        name = Console.ReadLine();
//                                        Console.Write("Podaj kraj: ");
//                                        string country;
//                                        country = Console.ReadLine();
//                                        Console.Write("Podaj miasto: ");
//                                        string city;
//                                        city = Console.ReadLine();
//                                        Console.Write("Podaj ulicę: ");
//                                        string street;
//                                        street = Console.ReadLine();
//                                        bool isCreated = theaterNetwork.CreateTheater(name, country, city, street);
//                                        Console.WriteLine(isCreated ? $"Poprawnie utworzono teatr" : "Wystąpił błąd podczas tworzenia");
//                                        break;
//                                    case "2":
//                                        break;
//                                    case "3":
//                                        break;
//                                }
//                            }
//                        break;
//                        case "2":
//                            break;
//                        case "3":
//                            break;
//                        case "4":
//                            break;
//                        case "5":
//                            break;
//                    }
//                }
//                userInput = string.Empty;
//                break;
//            case "2":
//                Console.WriteLine("not implemented yet");
//                userInput = string.Empty;
//                break;
//            case "3":
//                Console.WriteLine("not implemented yet");
//                userInput = string.Empty;
//                break;
//        }
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine("================Error Message================");
//        Console.WriteLine(ex.Message);
//    }
//}
//*/