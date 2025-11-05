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
        
        public string Name { get; set; }
        public List<Theater> Theaters { get; set; }
        
        public TheaterNetwork() : this(string.Empty, new List<Theater>()) { }
        public TheaterNetwork(string name, List<Theater> theaters)
        {
            Name = name;
            Theaters = theaters ?? new List<Theater>();
        }
        
        public bool AddTheater(Theater theater)
        {
            if (theater is null) return false;
            Theaters.Add(theater);
            return true;
        }
        public bool AddTheater(string name, Address address, List<Hall> halls)
        {
            if (string.IsNullOrWhiteSpace(name) || address is null || halls is null) return false;
            Theater theater = new Theater(name, address, halls);
            Theaters.Add(theater);
            return true;
        }
        public bool DeleteTheater(Theater theater)
        {
            if (theater is null || Theaters.Count == 0) return false;
            return Theaters.Remove(theater);
        }
        public bool DeleteTheater(string name, Address address, List<Hall> halls)
        {
            if (string.IsNullOrWhiteSpace(name) || address is null || halls is null || Theaters.Count == 0) return false;

            var theater = Theaters.FirstOrDefault(t =>
                t.Name == name &&
                address.Equals(t.Address) &&
                halls.Equals(t.Halls)
            );

            if (theater is null) return false;

            return Theaters.Remove(theater);
        }
        public void DeleteAllTheaters()
        {
            Theaters.Clear();
        }
        public string GetTheaters() //placeholder?
        {
            return string.Join("\n", Theaters);
        }
        public override string ToString() //placeholder?
        {
            string s = base.ToString() + "Sieć teatrów: Name\n" + string.Join("", Theaters.Select(t => "- " + t));
            return s;
        }
    }
}