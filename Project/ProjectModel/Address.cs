using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Address
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public int Street_Number { get; set; }

        public Address(string city, string street, string postalCode, int street_number)
        {
            City = city;
            Street = street;
            PostalCode = postalCode;
            Street_Number = street_number;
        }
        public Address() : this(string.Empty, string.Empty, string.Empty, 0) { }

        public void DisplayAddress()
        {
            Console.WriteLine($"Miasto: {City}, Ulica: {Street}, Kod-Pocztowy: {PostalCode}, Numer Budynku: {Street_Number}");
        }
    }
}
