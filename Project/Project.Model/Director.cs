using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Director : Person
    {
        public int YearsOfExperience;
        public decimal Salary { get; set; }
        public List<Play> Plays { get; set; }

        public Director() : base(string.Empty, string.Empty) 
        {
            YearsOfExperience = default;
            Salary = default;
            Plays = new List<Play>();
        }
        public Director(string firstName, string lastName, int yearsOfExperience, decimal salary, List<Play> plays) : base(firstName, lastName)
        {
            YearsOfExperience = yearsOfExperience;
            Salary = salary;
            Plays = plays;
        }
    }
}
