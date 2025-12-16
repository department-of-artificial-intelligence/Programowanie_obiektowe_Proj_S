using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{

    public class Pharmacy
    {
        public int Id { get; set; } // Klucz glowny
        public string Name { get; set; }
        public Address Address { get; private set;}
        public int AddressId { get; set; } // Klucz obcy
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public List<Drug> Drugs { get; set; } = new List<Drug>();

        public Pharmacy() { }
        public Pharmacy(string nazwa, Address address)
        {
            Name = nazwa;
            Address = address;
        }
        public override string ToString()
        {
            return $"Apteka -- Id: {Id}, Nazwa: {Name}";
        }

    }
}
