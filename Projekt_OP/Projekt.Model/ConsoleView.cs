using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class ConsoleView
    {
        public void DisplayWelcomeMessage()
        {
            Console.WriteLine("Witam  W systemie Zarzadzania Kinami!");
            Console.WriteLine("---------------------------------------");
        }


        public void DisplayCinemas(IReadOnlyList<Cinema> cinemas)
        {
            Console.WriteLine("\n---Lista Kin--- ");
            if (cinemas.Count == 0)
            {
                Console.WriteLine("Brak Kin w Systemie!");
            }

            foreach (var c in cinemas)
            {
                Console.WriteLine(c.ToString());
            }
        }

        public void DisplayEmployees(IReadOnlyList<Employee> employees)
        {
            Console.WriteLine("\n---Lista Pracownikow---");
            if (employees.Count == 0)
            {
                Console.WriteLine("Brak Pracownikow!");
            }
            foreach (var e in employees)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void DisplayFilms(IReadOnlyList<Film> films)
        {
            Console.WriteLine("\n---Lista Filmow---");
            if (films.Count == 0)
            {
                Console.WriteLine("Brak filmow!");
            }

            foreach (var f in films)
            {
                Console.WriteLine(f.ToString());
            }

        }
    }
}
