using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model;

public class Address
{
    private string _country;
    private string _city;
    private string _street;
    public string Country
    {
        get => _country;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Kraj nie może być null lub pusty", nameof(Country));
            _country = value;
        }
    }
    public string City
    {
        get => _city;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Miasto nie może być null lub puste", nameof(City));
            _city = value;
        }
    }
    public string Street
    {
        get => _street;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Ulica nie może być null lub pusta", nameof(Street));
            _street = value;
        }
    }

    //public Address()
    //{
    //    Country = string.Empty;
    //    City = string.Empty;
    //    Street = string.Empty;
    //}

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
