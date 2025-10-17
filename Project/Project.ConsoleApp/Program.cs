// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using Project.Model;

Person p1 = new Person() { FirstName = "Jan", LastName = "Kowalski", Age = 40 };

Venue v1 = new Venue() {Name = "Stadion Narodowy", City = "Warszawa", FloorCapacity=10000, SeatsCapacity=30000};
Venue v2 = new Venue() { Name = "Tauron Arena", City = "Kraków", FloorCapacity = 5000, SeatsCapacity = 15000 };
Venue v3 = new Venue() { Name = "Klub Studio", City = "Kraków", FloorCapacity = 750, SeatsCapacity = 120 };
Venue v4 = new Venue() { Name = "Klub Wytwórnia", City = "Łódź", FloorCapacity = 500, SeatsCapacity = 50 };

Artist a1 = new Artist() { Name = "Kendrick Lamar", Genre = "Hip-Hop", Country = "USA" };
Artist a2 = new Artist() { Name = "TOOL", Genre = "Progressive Rock", Country = "USA" };
Artist a3 = new Artist() { Name = "Meshuggah", Genre = "Metal", Country = "Sweden" };

Concert c1 = new Concert() { Artist = a1, Venue = v1 };
Concert c2 = new Concert() { Artist = a2, Venue = v2 };
Concert c3 = new Concert() { Artist = a3, Venue = v3 };

Festival f1 = new Festival() {Name ="Festiwal", Artists = [ a1, a2, a3], Venue = v1 };

Console.WriteLine(v1);
Console.WriteLine(a2);
Console.WriteLine(c3);
Console.WriteLine(f1);
//Console.WriteLine($"{p1.FirstName} {p1.LastName} {p1.Age}");
