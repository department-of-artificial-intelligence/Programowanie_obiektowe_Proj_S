// See https://aka.ms/new-console-template for more information

using Project.Model;
using System.Globalization;
Pierwsza pk = new Pierwsza();

Person p1 = new Person() {FirstName = "Jan", LastName="PP", Age=10};

Console.WriteLine($"{p1.FirstName} {p1.LastName} {p1.Age}" );