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
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public int StreetNumber { get; set; }
        public Address(string city, string street, string postalCode, int streetNumber)
        {
            City = city;
            Street = street;
            PostalCode = postalCode;
            StreetNumber = streetNumber;
        }
        public Address() : this(string.Empty, string.Empty, string.Empty, 0) { }
        public override string ToString()
        {
            return $"Adres Miasto: {City}, Ulica: {Street}, Kod-Pocztowy: {PostalCode}, Numer Budynku: {StreetNumber}";
        }
    }
}
