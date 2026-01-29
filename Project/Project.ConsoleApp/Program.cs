using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.DAL;
using Project.Model;
using Project.Logic;

using var context = new ApplicationDBContext();

context.Database.EnsureCreated(); //sqlite

var service = new ConcertService(context);


//Venue v1 = new Venue(){Name = "Klub Wytwórnia", City = "Łódź", Capacity = 1000};
//Venue v2 = new Venue(){Name = "Tauron Arena", City = "Kraków", Capacity = 20000};
//Venue v3 = new Venue(){Name = "Klub Studio", City = "Kraków", Capacity = 1500};
//Venue v4 = new Venue(){Name = "Stadion Narodowy", City = "Warszawa", Capacity = 80000};

//context.Venues.AddRange(v1,v2,v3,v4);

//Artist a1 = new Artist() { Name = "Afroman", Genre = "Hip-Hop", Country = "USA" };
//Artist a2 = new Artist() { Name = "TOOL", Genre = "Progressive Rock", Country = "USA" };
//Artist a3 = new Artist() { Name = "Meshuggah", Genre = "Metal", Country = "Sweden" };
//Artist a4 = new Artist() { Name = "AC/DC", Genre = "Rock", Country = "Australia" };

//context.Artists.AddRange(a1, a2, a3, a4);

//Concert c1 = new Concert(){Artist = a1,Venue = v1,Date = new DateTime(2026,03,15)};
//Concert c2 = new Concert(){Artist = a2,Venue = v2,Date = new DateTime(2026,05,10)};
//Concert c3 = new Concert(){Artist = a3,Venue = v3,Date = new DateTime(2026,06,27)};
//Concert c4 = new Concert(){Artist = a4,Venue = v4,Date = new DateTime(2026,08,21)};

//context.Concerts.AddRange(c1, c2, c3, c4);
//context.SaveChanges();

/////////////

//var concerts = context.Concerts
//    .Include(c => c.Artist)
//    .Include(c => c.Venue);

//Console.WriteLine("Wydarzenia: ");
//foreach (var concert in concerts)
//{
//    Console.WriteLine($"{concert} - Wyprzedany: {concert.IsSoldOut()}");
//}

while (true) // menu
{
    Console.Clear();
    Console.WriteLine("Menu:");
    Console.WriteLine("1 - Lista Artystów");
    Console.WriteLine("2 - Lista Lokali");
    Console.WriteLine("3 - Lista Koncertów");
    Console.WriteLine("4 - Dodaj Artystę");
    Console.WriteLine("5 - Dodaj Lokal");
    Console.WriteLine("6 - Dodaj Koncert");
    Console.WriteLine("7 - Usuń Artystę");
    Console.WriteLine("0 - Wyjście");
    Console.WriteLine("Naciśnij przycisk: ");
    
    
    string wybor = Console.ReadLine();

    if (wybor == "0")
    {
        Console.Clear();
        break;
    }
    switch (wybor)
    {
        case "1": //lista artystow
            Console.Clear();
            Console.WriteLine("Artyści: ");
            service.ShowAllArtists();
            break;
        case "2": //lista lokali
            Console.Clear();
            Console.WriteLine("Lokale: ");
            service.ShowAllVenues();
            break;

        case "3": //lista koncertow
            Console.Clear();
            Console.Write("Koncerty:\n");
            service.ShowAllConcerts(); 
            break;
        
        case "4": //dodanie artysty
            Console.Clear();
            Console.WriteLine("Podaj nazwę artysty: ");
            string nazwa = Console.ReadLine();
            Console.WriteLine("Podaj gatunek: ");
            string gatunek = Console.ReadLine();
            Console.WriteLine("Podaj kraj pochodzenia: ");
            string kraj = Console.ReadLine();

            Artist nowyArtysta = new Artist()
            {
                Name = nazwa,
                Genre = gatunek,
                Country = kraj
            };

            context.Artists.Add(nowyArtysta);
            context.SaveChanges();
            Console.WriteLine("Dodano artystę do bazy");
            break;

        case "5": // dodanie lokalu
            Console.Clear();
            Console.WriteLine("Podaj nazwę lokalu: ");
            string nazwalokalu = Console.ReadLine();
            Console.WriteLine("Podaj miasto: ");
            string miasto = Console.ReadLine();
            Console.WriteLine("Podaj pojemność: ");

            if(int.TryParse(Console.ReadLine(),out int pojemnosc))
            {
                Venue nowyLokal = new Venue()
                {
                    Name = nazwalokalu,
                    City = miasto,
                    Capacity = pojemnosc
                };
                context.Venues.Add(nowyLokal);
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Pojemność musi być liczbą całkowitą");
            }
            break;
        case "6": // dodanie koncertu
            Console.Clear();
            Console.WriteLine("Podaj nazwę artysty: ");
            string nazwaArtysty = Console.ReadLine();
            var artysta = context.Artists.FirstOrDefault(a =>  a.Name == nazwaArtysty);
            if (artysta == null)
            {
                Console.Write("Nie znaleziono artysty");
                break;
            }

            Console.WriteLine("Podaj nazwę lokalu: ");
            string nazwaLokalu = Console.ReadLine();
            var lokal = context.Venues.FirstOrDefault(v => v.Name == nazwaLokalu);
            if (lokal == null)
            {
                Console.Write("Nie znaleziono lokalu");
                break;
            }

            Console.WriteLine("Podaj Datę (RRRR-MM-DD): ");
            if (DateTime.TryParse(Console.ReadLine(),out DateTime data)){
                Concert concert = new Concert()
                {
                    Artist = artysta,
                    Venue = lokal,
                    Date = data
                };
                context.Concerts.Add(concert);
                context.SaveChanges();
                break;

            }
            else {
                Console.WriteLine("Podano błędną datę");
                break;
            }

        case "7": // Usunięcie artysty
            Console.WriteLine("Podaj nazwę artysty którego chcesz usunąć: ");
            nazwaArtysty = Console.ReadLine();
            artysta = context.Artists.FirstOrDefault(a => a.Name == nazwaArtysty);
            if (artysta == null)
            {
                Console.WriteLine("Nie znaleziono artysty");
            }
            else
            {
                context.Artists.Remove(artysta);
                context.SaveChanges();
                Console.WriteLine("Usunięto artystę");
            }
            break;

        default:
            Console.WriteLine("Niepoprawny wybór. Spróbuj ponownie: ");
            break;

    }

    Console.WriteLine("\nNaciśnij dowolny klawisz aby wrócić");
    Console.ReadKey();

}