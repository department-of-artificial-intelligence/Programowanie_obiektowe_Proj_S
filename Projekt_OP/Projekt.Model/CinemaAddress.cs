using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class CinemaAddress
    {
        private string City { get; set; }
        private string Street { get; set; }
        private int Number { get; set; }

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
