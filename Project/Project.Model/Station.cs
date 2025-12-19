using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Model
{
    public class Station
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int Capacity { get; set; }

        public virtual ICollection<Bicycle> Bicycles { get; set; }

        public Station()
        {
            Bicycles = new List<Bicycle>();
        }

        public Station(string name, string city, string address, int capacity)
        {
            Name = name;
            City = city;
            Address = address;
            Capacity = capacity; 
            Bicycles = new List<Bicycle>();
        }

        public override string ToString()
        {
            
            return $"[{City}, {Address}] Station '{Name}' ({Bicycles?.Count ?? 0}/{Capacity} slots)";
        }
    }
}