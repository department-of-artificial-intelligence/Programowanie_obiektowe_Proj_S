using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Actor : Person
    {
        public decimal Salary { get; set; }
        public List<Play> Plays { get; set; }

        public Actor(string firstName, string lastName, decimal salary, List<Play> plays) : base(firstName, lastName)
        {
            Salary = salary;
            Plays = plays;
        }
    }
}
