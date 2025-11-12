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

        public Director(string firstName, string lastName, int yearsOfExperience, decimal salary, List<Play> plays) : base(firstName, lastName)
        {
            YearsOfExperience = yearsOfExperience;
            Salary = salary;
            Plays = plays;
        }

        public bool AddPlay(Play play) //WIP
        {
            if (play is null || Plays.Contains(play)) return false;
            Plays.Add(play);
            return true;
        }
        public bool RemovePlay(Play play) //WIP
        {
            if (Plays.Count == 0 || play is null) return false;
            return Plays.Remove(play);
        }
        public void RemoveAllPlays() //WIP
        {
            Plays.Clear();
        }
    }
}
