using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Exercise
    {
        public required string Name { get; set; }
        public required string Series { get; set; }
        public required string Rep { get; set; }
        public required string Treining_load { get; set; }
        public required string Break { get; set; }

    }
}
