using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model;

public class Actor : Person, IPlayManager
{
    public int ActorId { get; set; }
    public decimal Salary { get; set; }
    public List<Play> Plays { get; set; }

    public Actor()
    {
        ActorId = 0;
        Salary = 0;
        Plays = new List<Play>();
    }

    public Actor(string firstName, string lastName, int actorId, decimal salary, List<Play> plays) 
        : base(firstName, lastName)
    {
        ActorId = actorId;
        Salary = salary;
        Plays = plays;
    }

    public bool AddPlay(Play play)
    {
        if (play is null || Plays.Contains(play)) return false;
        if (!play.Actors.Contains(this))
        {
            play.Actors.Add(this);
        }
        Plays.Add(play);
        return true;
    }
    public bool RemovePlay(Play play)
    {
        if (Plays.Count == 0 || play is null) return false;
        if (play.Actors.Contains(this))
        {
            play.Actors.Remove(this);
        }
        return Plays.Remove(play);
    }
    public bool RemovePlay(int playId)
    {
        if (Plays.Count == 0) return false;
        var play = Plays.FirstOrDefault(p => p.PlayId == playId);
        if (play is null) return false;
        if (play.Actors.Contains(this))
        {
            play.Actors.Remove(this);
        }
        return Plays.Remove(play);
    }
    public void RemoveAllPlays()
    {
        foreach (Play play in Plays)
        {
            if (play.Actors.Contains(this))
            {
                play.Actors.Remove(this);
            }
        }
        Plays.Clear();
    }
}