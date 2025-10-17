using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Biura.model
{
    public class Excursion
    {
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public float Cost { get; set; }
        public int Persons { get; set; }
        public Excursion(string location, DateTime date, float cost, int persons)
        {
            Location = location;
            Date = date;
            Cost = cost;
            Persons = persons;
        }
        public override string ToString()
        {
            return $"{Location} in {Date} will cost {Cost} per person for {Persons} people totaling {Cost*Persons}";
        }
    }
}
