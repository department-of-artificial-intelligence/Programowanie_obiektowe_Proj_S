using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class CinemaAddress
    {
        public string City { get; private set; }
        public string Street { get; private set; }
        public int Number { get; private set; }

        public CinemaAddress() 
        {
            Street = string.Empty;
            Number = 0;
            City = string.Empty;

        }
        
        public CinemaAddress(string city,string street, int number) 
        {
            Street = street;
            Number = number;
            City = city;
        }

    }
}
