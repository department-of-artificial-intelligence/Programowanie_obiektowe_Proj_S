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

        public int Number { get { return _number; } set { _number = value; } }
        public int Seats { get { return _seats; } set { _seats = value; } }


        public override string ToString()
        {
            return $"Sala nr: {_number}. Liczba miejsc: {_seats} ";
        }
    }
}
