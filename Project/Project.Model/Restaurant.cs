using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Restaurant
    {
        public required string Name {  get; set; }
        public required string Address { get; set; }
        public List<MenuItem> Menu { get; set; } = new();
        public List<Employee> Employees { get; set; } = new();

        public Restaurant(string name, string address) {
            Name = name;
            Address = address;
            Menu = new List<MenuItem>();
            Employees = new List<Employee>();
        }
    }
}
