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

        public Exercise(string name, string series, string rep, string trainingLoad, string breakTime)
        {
            Name = name;
            Series = series;
            Rep = rep;
            Treining_load = trainingLoad;
            Break = breakTime;
        }
        public Exercise()
        {
            Name = string.Empty;
            Series = string.Empty;
            Rep = string.Empty;
            Treining_load = string.Empty;
            Break = string.Empty;
        }
    }
}
