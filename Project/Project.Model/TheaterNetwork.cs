using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            this.Name = name;
            this.Theaters = theaters;
        }
    }
}
