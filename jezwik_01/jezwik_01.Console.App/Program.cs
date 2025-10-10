// See https://aka.ms/new-console-template for more information
using jezwik_01.Model;
using System.IO.Pipes;
using System.Net.Cache;
//1_klasa 

Person p1 = new Person() { FirstName = "Jan" , LastName = "Kowalski" , Age = 40 };



Console.WriteLine($"{p1.FirstName} {p1.LastName} {p1.Age}");
Console.WriteLine(p1);
