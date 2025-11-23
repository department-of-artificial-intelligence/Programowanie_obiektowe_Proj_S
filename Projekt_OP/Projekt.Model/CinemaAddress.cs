using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class CinemaAddress
    {
        private string _city;
        private string _street;
        private int _number;
        public string City { get { return _city; } set { _city = value; } }
        public string Street { get { return _street; } set { _street = value; } }
        public int Number { get { return _number; } set { _number = value; } }

        public CinemaAddress() 
        {
            _street = string.Empty;
            _number = 0;
            _city = string.Empty;

        }
        
        public CinemaAddress(string City,string Street, int Number) 
        {
            _street = Street;
            _number = Number;
            _city = City;
        }

    }
}
