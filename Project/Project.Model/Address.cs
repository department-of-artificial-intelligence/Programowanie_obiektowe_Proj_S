using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Address
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        
        public Address() : this(string.Empty, string.Empty, string.Empty, string.Empty) { }
        public Address(string country, string city, string street, string postalCode)
        {
            Country = country;
            City = city;
            Street = street;
            PostalCode = postalCode;
        }
    }
}
