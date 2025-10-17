// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using Project.Model;

Praca p = new Praca();

Pracownik empl = new Pracownik() { FirstName = "Jan", LastName = "Kowalski", Id = 23231, Age = 19 };

Console.WriteLine(empl);
//Console.WriteLine($"{p1.FirstName} {p1.LastName} {p1.Age}");