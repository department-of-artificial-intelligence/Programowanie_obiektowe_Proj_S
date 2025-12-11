using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class Hall
    {
        public int ID { get; private set; }
        public int Seats { get; private set; }
        public List<Film> Films {get; set; } = new List<Film>();
        public int CinemaID { get; set; }
        public Cinema Cinema { get; set; }

        public Hall()
        {
        }

        public Hall(int seats,Cinema cinema)
        {
            Seats = seats;
            Cinema = cinema;
        }

        public override string ToString()
        {
            return $"Sala nr: {ID}. Liczba miejsc: {Seats}";
        }
    }
}
