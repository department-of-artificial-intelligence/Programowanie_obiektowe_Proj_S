using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biura.model
{

    public class Biuro
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public Person Owner { get; set; }

        public Biuro(string name, string location, Person owner)
        {
            Name = name;
            Location = location;
            Owner = owner;
        }

        public override string ToString()
        {
            return $"biuro wycieczkowe {Name} ul {Location} wlasciciel {Owner.ToString()}";
        }
    }
}
