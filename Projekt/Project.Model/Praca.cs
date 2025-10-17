using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Praca
    {
        public required string Name { get; set; }

        public required string Typeofjob { get; set; }

        public void LoadInfoEmployees()
        {
            Console.WriteLine("Podaj imie pracownika: ");
            FirstName = Console.ReadLine();

            Console.WriteLine("Podaj nazwisko pracownika: ");
            LastName = Console.ReadLine();

            Console.WriteLine("Podaj Id pracownika: ");
            Id = Console.ReadLine();

            Console.WriteLine("Podaj wiek pracownika: ");
            Age = Console.ReadLine();

        }

        public void SearchEmployee()
        {

        }
    }

    
}
