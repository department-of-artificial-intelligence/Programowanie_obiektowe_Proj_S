using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Vehicle
    {
        public int Id { get; set; }
        public int RegNum { get; set; }
        public int ProdYear { get; set; }
        public int MileAge { get; set; } //przebieg
        public string Name { get; set; }

    }
}
