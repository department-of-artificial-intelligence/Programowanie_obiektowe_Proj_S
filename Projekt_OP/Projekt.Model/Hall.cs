using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class Hall
    {
        public int _number;
        public int _seats;
        public Cinema _cinema; 

        public int Number { get { return _number; } set { _number = value; } }
        public int Seats { get { return _seats; } set { _seats = value; } }

        public Cinema Cinema { get { return _cinema; } set { _cinema = value; } }

        public Hall(int number, int seats)
        {
            _number = number;
            _seats = seats;
        }

        public override string ToString()
        {
            return $"Sala nr: {_number}. Liczba miejsc: {_seats} Kino: {_cinema}";
        }
    }
}
