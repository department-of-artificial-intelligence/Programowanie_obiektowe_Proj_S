using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Theater
    {
        public string Name { get; set; }
        public Address Address { get; set; }
        public List<Hall> Halls { get; set; }

        public Theater() : this(string.Empty, new Address(), new List<Hall>()) { }
        public Theater(string name, Address address, List<Hall> halls)
        {
            Name = name;
            Address = address;
            Halls = halls;
        }
    }
}
