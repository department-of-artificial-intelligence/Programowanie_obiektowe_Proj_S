using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


using Project.DAL;
using Project.Model;
using Project.Logic;

using var context = new ApplicationDBContext();

context.Database.EnsureCreated();

var service = new ConcertService(context);

/*
Venue v1 = new Venue(){Name = "Klub Wytwórnia", City = "Łódź", Capacity = 1000};
Venue v2 = new Venue(){Name = "Tauron Arena", City = "Kraków", Capacity = 20000};
Venue v3 = new Venue(){Name = "Klub Studio", City = "Kraków", Capacity = 1500};
Venue v4 = new Venue(){Name = "Stadion Narodowy", City = "Warszawa", Capacity = 80000};

context.Venues.AddRange(v1,v2,v3,v4);

Artist a1 = new Artist() { Name = "Afroman", Genre = "Hip-Hop", Country = "USA" };
Artist a2 = new Artist() { Name = "TOOL", Genre = "Progressive Rock", Country = "USA" };
Artist a3 = new Artist() { Name = "Meshuggah", Genre = "Metal", Country = "Sweden" };
Artist a4 = new Artist() { Name = "AC/DC", Genre = "Rock", Country = "Australia" };

context.Artists.AddRange(a1, a2, a3, a4);

Concert c1 = new Concert(){Artist = a1,Venue = v1,Date = new DateTime(2026,03,15)};
Concert c2 = new Concert(){Artist = a2,Venue = v2,Date = new DateTime(2026,05,10)};
Concert c3 = new Concert(){Artist = a3,Venue = v3,Date = new DateTime(2026,06,27)};
Concert c4 = new Concert(){Artist = a4,Venue = v4,Date = new DateTime(2026,08,21)};

context.Concerts.AddRange(c1, c2, c3, c4);
context.SaveChanges();
*/

var concerts = context.Concerts
    .Include(c => c.Artist)
    .Include(c => c.Venue);
Console.WriteLine("Wydarzenia: ");
foreach (var concert in concerts)
{
    Console.WriteLine($"{concert} - Wyprzedany: {concert.IsSoldOut()}");
}

context.Temp();