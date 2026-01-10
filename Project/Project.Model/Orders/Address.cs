using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.Orders
{
    public class Address
    {
        
        public int AddressId { get; private set; }

        public required string Street { get; set; }
        public required string City { get; set; }
        public required string ZipCode { get; set; }
        public required string Country { get; set; }

        
        public Address() { }

        [SetsRequiredMembers]
        public Address(string city, string street, string zipCode, string country)
        {
            Street = street;
            City = city;
            ZipCode = zipCode;
            Country = country;
        }

        public override string ToString()
        {
            return $"{Street}, {ZipCode} {City}, {Country}";
        }
    }
}
