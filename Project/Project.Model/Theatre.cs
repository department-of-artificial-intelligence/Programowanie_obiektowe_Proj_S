using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Theatre
    {
        public string Name { get; set; }
        public Address Address { get; set; }

        public Theatre() : this(string.Empty, new Address()) { }
        public Theatre(string name, Address address)
        {
            Name = name;
            Address = address;
        }
    }
}
