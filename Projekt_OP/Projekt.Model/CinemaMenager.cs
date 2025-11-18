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
            _cinemas.Add(cinema);
        }

        public void AddFilm(Film film)
        {
            _films.Add(film);
        }

        public void DisplayCinemas()
        {
            Console.WriteLine("\n ---Lista Kin--- ");
            if (_cinemas.Count == 0)
            { 
                Console.WriteLine("Brak Kin w Systemie!");
            }

            foreach (var c in _cinemas)
            {
                Console.WriteLine(c.ToString());
            }
        }

        public void DisplayFilms()
        {
            Console.WriteLine("\n ---Lista Filmow--- ");
            if (_films.Count == 0)
            {
                Console.WriteLine("Brak Filomow w Systemie!");
            }
            foreach (var f in _films)
            {
                Console.WriteLine(f.ToString());
            }
        }
    }
}
