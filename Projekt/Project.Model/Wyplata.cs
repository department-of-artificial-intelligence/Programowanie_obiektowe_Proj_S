using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Wyplata()
	{
	
		public required int Tittle { get; set; }

        public required string Date { get; set; }
       
        public required int Amount { get; set; }
       
       
        public void DoPayment(Pracownik pracownik, int podstawowaKwota)
        {
            int premia = 0;
       
            int zaliczone = pracownik.PassedProjects();
       
            int niezaliczone = pracownik.NotPassedProjects();
       
            Console.WriteLine($"Wypłata dla pracownika {pracownik.FirstName} {pracownik.LastName} ");
       
            Console.WriteLine($"Zaliczone projekty: {zaliczone} ");
       
            Console.WriteLine($"Niezaliczone projekty: {niezaliczone} ");
       
            if (zaliczone > niezaliczone && zaliczone > 0)
            {
                premia = 400;
                Console.WriteLine($"Przyznano premię");
            }
            else
            {
                Console.WriteLine($"Brak premii");
            }
       
            this.Tittle = "Wypłata miesięczna";
            this.Date = 8.10.2025;
            this.Amount = podstawowaKwota + premia;
       
            Console.WriteLine("---Podsumowanie wypłaty---");
            Console.WriteLine($"Dziś: {Date}");
            Console.WriteLine($"Pracownik: {pracownik.FirstName} {pracownik.LastName}");
            Console.WriteLine($"Kwota podstawowa: {podstawowaKwota}");
            Console.WriteLine($"Premia: {premia}");
            Console.WriteLine($"Łącznie do wypłaty: {this.Amount} PLN");
        }

    }  
}


     






