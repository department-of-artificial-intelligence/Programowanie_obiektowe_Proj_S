// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using Biura.model;

Biuro tplanetpl = new Biuro("travel", "Czestochowa", new Person("Robert", "Dymski", 32));
List<Excursion> excursions = new List<Excursion>();
void ListExcursions()
{
    Console.WriteLine($"List contains {excursions.Count} excursions");
    foreach (Excursion ex in excursions)
    {
        Console.WriteLine(ex);
    }
}

Person p1 = new Person("Jan", "Kowalski", 32);
Person p2 = new Person("Kamil", "Królikowski", 20);
Person p3 = new Person("Igor", "Kowalczyk", 23);

DateTime d1 = new DateTime(2020, 12, 23);
Excursion e1 = new Excursion("Afryka", DateTime.Now, 3245.23f, 2);
Excursion e2 = new Excursion("Zabrze", DateTime.Today, 324f, 3);
Excursion e3 = new Excursion("Monachium", d1, 4548f, 4);

Client c = new Client(p1, DateTime.Now);
Client c1 = new Client(p2, DateTime.Now);


excursions.Add(e1);

Console.WriteLine(e1);

Console.WriteLine(c1);
c1.ShowBookedTrip();
c1.TripBooked = e1;
c1.ShowBookedTrip();

Console.WriteLine(tplanetpl);

tplanetpl.ListClients();
ListExcursions();

tplanetpl.AddCLient(c1);
tplanetpl.AddClient(new Client("Karol", "Nowak", 32, DateTime.Now));
tplanetpl.AddClient(new Client(p3, d1));
tplanetpl.AddClient(p2);
tplanetpl.ListClients();
