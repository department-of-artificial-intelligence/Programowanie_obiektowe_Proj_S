using Project.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Treatment : IIdentifiable
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Cost { get; set; }

        public Treatment() { }

        public Treatment(string name, string? description = null, decimal cost = 0m)
        {
            Name = name;
            Description = description;
            Cost = cost;
        }
        public override string ToString()
        {
            return $"{Name} – {Cost} zł";
        }


    }
}
