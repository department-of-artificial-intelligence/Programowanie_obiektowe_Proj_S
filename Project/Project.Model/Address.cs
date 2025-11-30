using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model;

public class Address
{
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    
    public Address()
    {
        Country = string.Empty;
        City = string.Empty;
        Street = string.Empty;
    }

    public Address(string country, string city, string street)
    {
        Country = country;
        City = city;
        Street = street;
    }

    public override string ToString()
    {
        return $"{Country}, {City}, {Street}";
    }
}
