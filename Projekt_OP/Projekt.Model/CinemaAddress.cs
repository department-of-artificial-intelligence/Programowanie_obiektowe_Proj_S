using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class CinemaAddress
    {
        public string _zipCode;
        public string _street;
        public int _number;
        public string _city;
        public string _country;

        public string ZipCode { get { return _zipCode; } set { _zipCode = value; } }
        public string City { get { return _city; } set { _city = value; } }
        public string Country { get { return _country; } set { _country = value; } }
        public string Street { get { return _street; } set { _street = value; } }
        public int Number { get { return _number; } set { _number = value; } }

        public CinemaAddress() 
        {
            _zipCode = string.Empty;
            _street = string.Empty;
            _number = 0;
            _city = string.Empty;
            _country = string.Empty;

        }
        
        public CinemaAddress(string ZipCode, string Street, int Number, string City, string Country) 
        {
            _zipCode = ZipCode;
            _street = Street;
            _number = Number;
            _city = City;
            _country = Country;
        }

    }
}
