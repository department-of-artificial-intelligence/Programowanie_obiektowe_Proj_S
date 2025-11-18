using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Projekt.Model
{
    public class CinemaMenager
    {
        public List<Cinema> _cinemas = new List<Cinema>();
        public List<Film> _films = new List<Film>();
        public List<Employee> _employees = new List<Employee>();



        public void AddCinema(Cinema cinema)
        {
            Console.WriteLine("Podaj Nazwe Kina : ");
            string  Name = Console.ReadLine();

            _cinemas.Add(new Cinema { CinemaName = Name });
            Console.WriteLine("Kino Dodane Pomyslnie");
        }

        public void AddFilm(Film film)
        {
            Console.WriteLine("Podaj Tytul Filmu : ");
            string Title = Console.ReadLine();

            _films.Add(new Film { Title = Title });
            Console.WriteLine("Film Dodany Pomyslnie");
        }



    }
}
