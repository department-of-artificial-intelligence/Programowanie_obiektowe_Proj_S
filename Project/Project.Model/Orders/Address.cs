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

        
        private Address() { }

        [SetsRequiredMembers]
        public Address(string street, string city, string zipCode)
        {
            Street = street;
            City = city;
            ZipCode = zipCode;
        }

        public override string ToString()
        {
            return $"{Street}, {ZipCode} {City}, {Country}";
        }
    }
}
