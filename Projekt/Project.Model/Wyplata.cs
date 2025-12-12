using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Wyplata
	{
	
		public string Title { get; set; }

        public DateTime Date { get; set; }
       
        public decimal Amount { get; set; }

        private const decimal Bonus = 400m;

        private const decimal BestEmployeeBonus = 1000m;
       
       
        public void DoPayment(Pracownik pracownik, int podstawowaKwota, bool isBestEmployee)
        {
            decimal premia = 0m;
       
            int zaliczone = pracownik.PassedProjects();
       
            int niezaliczone = pracownik.NotPassedProjects();
       
            Console.WriteLine($"Wypłata dla pracownika {pracownik.FirstName} {pracownik.LastName} ");
       
            Console.WriteLine($"Zaliczone projekty: {zaliczone} ");
       
            Console.WriteLine($"Niezaliczone projekty: {niezaliczone} ");
       
            if (zaliczone > niezaliczone && zaliczone > 0)
            {
                premia = Bonus;
                Console.WriteLine($"Przyznano premię");
            }
            else
            {
                Console.WriteLine($"Brak premii");
            }


            decimal premiaZaWyniki = 0m;

            if(isBestEmployee)
            {
                premiaZaWyniki = BestEmployeeBonus;
                Console.WriteLine($"Przyznano premię za najlepszego pracownika: {BestEmployeeBonus} PLN");
            }

            decimal lacznaPremia = premia + premiaZaWyniki;
            this.Title = "Wypłata miesięczna";
            this.Date = DateTime.Now;
            this.Amount = podstawowaKwota + premia;
       
            Console.WriteLine("---Podsumowanie wypłaty---");
            Console.WriteLine($"Dziś: {Date:d}");
            Console.WriteLine($"Pracownik: {pracownik.FirstName} {pracownik.LastName}");
            Console.WriteLine($"Kwota podstawowa: {podstawowaKwota}");
            Console.WriteLine($"Premia: {lacznaPremia}");
            Console.WriteLine($"Łącznie do wypłaty: {this.Amount} PLN");
        }

    }  
}


     






