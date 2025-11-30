using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project.Model;

public class TheaterNetwork
{
    public string Name { get; set; }
    public List<Theater> Theaters { get; set; }

    public TheaterNetwork()
    {
        Name = string.Empty;
        Theaters = new List<Theater>();
    }

    public TheaterNetwork(string name)
    {
        Name = name;
        Theaters = new List<Theater>();
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
    public override string ToString()
    {
        string s = $"Sieć teatrów: {Name}";
        s += Theaters.Capacity == 0 ? "\nBrak teatrów" : string.Join("", Theaters.Select(t => "\n- " + t));
        return s;
    }
}