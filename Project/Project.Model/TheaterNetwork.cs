using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Project.Model;

public class TheaterNetwork
{
    private string _name;
    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Nazwa sieci nie może być null lub pusta", nameof(Name));
            _name = value;
        }
    }
    public List<Theater> Theaters { get; }

    //public TheaterNetwork()
    //{
    //    Name = string.Empty;
    //    Theaters = new List<Theater>();
    //}

    public TheaterNetwork(string name)
    {
        Name = name;
        Theaters = new List<Theater>();
    }

    public bool CreateTheater(string name, string country, string city, string street)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(country) || string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(street)) return false;
        Theater theater = new Theater(name, country, city, street);
        Theaters.Add(theater);
        return true;
    }
    public bool CreateTheater(int theaterId, string name, string country, string city, string street)
    {
        if (theaterId <= 0 || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(country) || string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(street)) return false;
        if (Theaters.Any(t => t.TheaterId == theaterId)) return false;
        Theater theater = new Theater(theaterId, name, country, city, street);
        Theaters.Add(theater);
        return true;
    }
    public bool DeleteTheater(int theaterId)
    {
        if (Theaters.Count == 0) return false;
        var theater = Theaters.FirstOrDefault(t => t.TheaterId == theaterId);
        if (theater is null) return false;
        return Theaters.Remove(theater);
    }
    public void DeleteAllTheaters()
    {
        Theaters.Clear();
    }

    public string GetTheatersString()
    {
        return Theaters.ListToString("Brak teatrów", '*');
    }

    public override string ToString()
    {
        return $"Sieć teatrów: {Name}\n" + GetTheatersString();
    }
}