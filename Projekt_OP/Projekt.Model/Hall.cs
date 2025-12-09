using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class Hall
    {
        public int HallID { get; private set; }
        public int Seats { get; private set; }
        public List<Film> _films;
        
        
        public Hall(int number, int seats,List<Film> films)
        {
            HallID = number;
            Seats = seats;
            _films = films ?? new List<Film>();
        }

        public override string ToString()
        {
            return $"Sala nr: {HallID}. Liczba miejsc: {Seats}";
        }
    }
}
