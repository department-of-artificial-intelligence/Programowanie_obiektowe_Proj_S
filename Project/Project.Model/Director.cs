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

        public bool AddPlay(string title, List<Actor>? actors) //WIP? - jednak agregacja z play aby nie istniała sztuka bez reżysera
        {
            if (string.IsNullOrEmpty(title)) return false;
            Play newPlay = new Play(title, this, actors);
            Plays.Add(newPlay);
            return true;
        }
        public bool RemovePlay(Play play) //WIP - jednak agregacja z play aby nie istniała sztuka bez reżysera
        {
            if (Plays.Count == 0 || play is null) return false;
            if (play.Director == this) play.Director = null;
            return Plays.Remove(play);
        }
        public void RemoveAllPlays() //WIP
        {
            Plays.Clear();
        }
    }
}
