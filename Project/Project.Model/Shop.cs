using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Shop
    {
        public required string Address {  get; set; }

        public required string City { get; set; }

        public required string Region { get; set; }

        public required string PostalCode { get; set; }
        
        public required string Country { get; set; }

        public required int Phone { get; set; }


        public Shop() : this(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0) { }

        public Shop(string _addr, string _city, string _region, string _postalcode, string _country, int _phone)
        {
            Address = _addr;
            City = _city;
            Region = _region;
            PostalCode = _postalcode;
            Country = _country;
            Phone = _phone;
        }


    }
    
}
