using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Projekt.Model;


namespace Projekt
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isRunning = true;

            Console.WriteLine(" Witamy w Systemie Zarzadzania Kinami! ");
            Console.WriteLine("---------------------------------------");

            List<Hall> sale1 = new List<Hall>()
            {
                new Hall(1,20),
                new Hall(2,20),
                new Hall(3,20)
            };
            List<Employee> pracownicy1 = new List<Employee>()
            {
                new Employee(1,"Olek","Wyrazik"),
                new Employee(2,"Mateusz","Szczepanik"),
                new Employee(3,"Kacper","Marek"),
                new Employee(4,"Norbert","Cwiklinski")
            };

            List<Cinema> Kina = new List<Cinema>()
            {
                new Cinema(1,"Kino1",new CinemaAddress("Czestochowa","Cukierkowa", 12),sale1,pracownicy1)
            };
            
            CinemaMenager menager = new CinemaMenager(Kina);
            menager.DisplayCinemas();


            Console.Write("Podaj ID: ");
            string? Input = Console.ReadLine();
            if(!int.TryParse(Input, out int ID))
            {
                Console.WriteLine("Nie Podałes Cyfry");
            }
            Cinema? wybraneKino = Kina.FirstOrDefault(x => x.CinemaID == ID);
            Console.WriteLine(wybraneKino);




            


        }
    }
}
    




