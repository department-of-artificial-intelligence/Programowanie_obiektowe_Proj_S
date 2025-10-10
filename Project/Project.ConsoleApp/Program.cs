using Project.Model;
PierwszaKlasa pk = new PierwszaKlasa();

Person p1 = new Project.Model.Person() { FirstName = "Jan", LastName = "Kowalski", Age = 40 };
Console.WriteLine($"{p1.FirstName} {p1.LastName} {p1.Age}");

