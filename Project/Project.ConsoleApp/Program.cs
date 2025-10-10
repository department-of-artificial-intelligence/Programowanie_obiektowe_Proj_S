// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using Project.Model;

PierwszaKlasa pk = new PierwszaKlasa();


Person p1 = new Person() { FirstName = "Jan", LastName = "Kowalski", Age = 40 };
Console.WriteLine(p1);
//Console.WriteLine($"{p1.FirstName} {p1.LastName} {p1.Age}");