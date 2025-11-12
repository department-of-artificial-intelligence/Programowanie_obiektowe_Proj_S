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

        public bool AddPlay(Play play)
        {
            if (play is null || Plays.Contains(play)) return false;
            Plays.Add(play);
            if (!play.Actors.Contains(this)) play.Actors.Add(this);
            return true;
        }
        public bool RemovePlay(Play play)
        {
            if (Plays.Count == 0 || play is null) return false;
            if (play.Actors.Contains(this)) play.Actors.Remove(this);
            return Plays.Remove(play);
        }
        public void RemoveAllPlays()
        {
            foreach (Play play in Plays) if (play.Actors.Contains(this)) play.RemoveActor(this);
            Plays.Clear();
        }
    }
}
