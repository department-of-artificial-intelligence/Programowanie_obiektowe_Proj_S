using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project.Model
{
    public class TheaterNetwork
    {
        public required string Name { get; set; }
        public List<Theater> Theaters { get; private set; }
        
        public TheaterNetwork(string name)
        {
            Name = name;
            Theaters = new List<Theater>();
        }
        
        public bool AddTheater(int theaterId, string name, string country, string city, string street, string postalCode)
        {
            if (theaterId <= 0 || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(country) || string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(street) || string.IsNullOrWhiteSpace(postalCode)) return false;
            Theater theater = new Theater(theaterId, name, country, city, street, postalCode);
            Theaters.Add(theater);
            return true;
        }
        public bool DeleteTheater(int theaterId)
        {
            if (Theaters.Count == 0 || theaterId <= 0) return false;
            var theater = Theaters.FirstOrDefault(t => t.TheaterId == theaterId);
            if (theater is null) return false;
            return Theaters.Remove(theater);
        }
        public void DeleteAllTheaters()
        {
            Theaters.Clear();
        }
        public string GetTheaters() //placeholder
        {
            return string.Join("\n", Theaters);
        }
        public override string ToString() //placeholder
        {
            string s = base.ToString() + "Sieć teatrów: Name\n" + string.Join("", Theaters.Select(t => "- " + t));
            return s;
        }
    }
}