using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model;

public class Actor : Person, IPlayManager
{
    private static int MaxId = 0;
    private decimal _salary;
    public int ActorId { get; }
    public decimal Salary 
    { 
        get => _salary;
        set 
        {
            if (value < 0) throw new ArgumentException("Płaca nie może być ujemna", nameof(Salary));
            _salary = value;
        }
    }
    public List<Play> Plays { get; }
    private static int GetNextId() => MaxId + 1;

    //public Actor()
    //{
    //    ActorId = GetNextId();
    //    MaxId = ActorId;
    //    Salary = 0;
    //    Plays = new List<Play>();
    //}

    public Actor(string firstName, string lastName, decimal salary, List<Play>? plays = null)
        : this(GetNextId(), firstName, lastName, salary, plays) { }

    public Actor(int actorId, string firstName, string lastName, decimal salary, List<Play>? plays = null) 
        : base(firstName, lastName)
    {
        if (actorId <= MaxId) throw new ArgumentOutOfRangeException(nameof(actorId), $"ID aktora {actorId} jest mniejsze lub równe MaxId {MaxId}");
        ActorId = actorId;
        MaxId = ActorId;
        Salary = salary;
        Plays = plays ?? new List<Play>();
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

    public string GetPlaysString()
    {
        return Plays.ListToString("Nie gra w żadnych sztukach", '-');
    }

    public override string ToString()
    {
        return base.ToString() + $"/{ActorId}/{Salary}PLN";
    }
}