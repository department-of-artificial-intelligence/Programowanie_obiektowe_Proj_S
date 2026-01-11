using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project.Model.Orders
{
    public class Address
    {
        
        public int AddressId { get; private set; }

        public required string Street { get; set; }
        public required string City { get; set; }

        private string _zipCode;
        public required string ZipCode
        {
            get => _zipCode;
            set
            {
                
                string pattern = @"^\d{2}-\d{3}$";

                if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, pattern))
                {
                    throw new ArgumentException($"Nieprawidłowy kod pocztowy: '{value}'. Wymagany format: XX-XXX (np. 00-123).");
                }
                _zipCode = value;
            }
        }


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
