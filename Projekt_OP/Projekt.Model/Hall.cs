using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class Hall
    {
        public int Number { get; private set; }
        public int Seats { get; private set; }
        public List<Film> _films;
        public IReadOnlyList<Film> Films => _films.AsReadOnly();
        
        
        public Hall(int number, int seats,List<Film> films)
        {
            Number = number;
            Seats = seats;
            _films = films ?? new List<Film>();
        }

        public override string ToString()
        {
            return $"Sala nr: {Number}. Liczba miejsc: {Seats}";
        }
    }
}
