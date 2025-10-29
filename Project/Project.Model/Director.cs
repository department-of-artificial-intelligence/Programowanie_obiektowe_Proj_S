using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Director : Person
    {
        public int yearsOfExperience;

        public Director() : base(string.Empty, string.Empty) 
        {
            yearsOfExperience = default;
        }
        public Director(string firstName, string lastName, int yearsOfExperience) : base(firstName, lastName)
        {
            this.yearsOfExperience = yearsOfExperience;
        }
    }
}
