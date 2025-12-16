using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Projekt.Model.UI
{
    public class ConsoleView
    {
        public void DisplayWelcomeMessage()
        {
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("==Witam  W systemie Zarzadzania Kinami!==");
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
                Console.WriteLine("==========================================");
                Console.WriteLine(c.ToString());
            }
        }

        public void DisplayCinemaByID(Cinema cinema) 
        {
            if (cinema == null)
            {
                Console.WriteLine("Kino o podanym ID nie istnieje ");
            }

            Console.WriteLine("==========================================");
            Console.WriteLine($"Kino: {cinema.CinemaName} | ID: {cinema.CinemaID}");
            Console.WriteLine($"Adres: {cinema.Address.City}, ul.{cinema.Address.Street} {cinema.Address.Number}");
            Console.WriteLine("==========================================");

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
                Console.WriteLine("==========================================");
                Console.WriteLine(e.ToString());
            }
        }

        public void DisplayEmployeeByID(Employee employee)
        {
            if (employee == null)
            {
                Console.WriteLine("Pracownik o podanym ID nie istnieje ");
            }

            Console.WriteLine("==========================================");
            Console.WriteLine($"Pracownik: {employee.Name} {employee.LastName} | ID: {employee.ID}") ;
            Console.WriteLine("==========================================");

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
                Console.WriteLine("==========================================");
                Console.WriteLine(f.ToString());
            }

        }

        public void DisplayFilmByID(Film film) 
        {
            if (film == null)
            {
                Console.WriteLine("Film o podanym ID nie istnieje ");
            }
            Console.WriteLine("==========================================");
            Console.WriteLine($"Film: {film.Title}| ID:{film.ID} | CzasTrwania:{film.TimeMin} min | Gatunek {film.Genre}");
            Console.WriteLine("==========================================");
        }

        public int GetIDInput(string info) 
        {
                Console.Write(info);
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int id))
                {
                    return id;
                }
                else
                {
                    throw new FormatException("Nieprawidlowy format ID. Prosze wprowadzic liczbe calkowita.");
                }
        }

        public string GetInput(string info) 
        {
            Console.Write(info);

            return Console.ReadLine()!;


        }
    }
}
