// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using Biura.model;


List<Client> clients = new List<Client>();
List<Excursion> excursions = new List<Excursion>();
void ListClients()
{
    Console.WriteLine($"List contains {clients.Count} clients");
    foreach (Client client in clients)
    {
        Console.WriteLine(client);
    }
}

void ListExcursions()
{
    Console.WriteLine($"List contains {excursions.Count} excursions");
    foreach (Excursion ex in excursions)
    {
        Console.WriteLine(ex);
    }
}

void AddNewClient(Person p, DateTime cd)
{
    Client c = new Client(p,cd);
    clients.Add(c);
}

Person p1 = new Person("Jan", "Kowalski", 32);
Person p2 = new Person("Kamil", "Królikowski", 20);
Person p3 = new Person("Igor", "Kowalczyk", 23);

Biuro biuro = new Biura.model.Biuro("wyczieczki.pl", "główna", p1);

Excursion e1 = new Excursion("Afryka", DateTime.Now, 3245.23f, 2);

DateTime d1 = new DateTime(2020, 12, 23);

Client c1 = new Client(p2, DateTime.Now);
Client c2 = new Client(p3, d1);


clients.Add(c1);
clients.Add(c2);

excursions.Add(e1);

Console.WriteLine(e1);

Console.WriteLine(c1);
c1.ShowBookedTrip();
c1.TripBooked = e1;
c1.ShowBookedTrip();

Console.WriteLine(biuro);

ListClients();
ListExcursions();

AddNewClient(p1, DateTime.Now);
ListClients();
