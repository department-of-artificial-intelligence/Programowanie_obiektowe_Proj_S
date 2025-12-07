using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model;

public class Director : Person, IPlayManager
{
    private static int MaxId = 0;
    private int _yearsOfExperience;
    private decimal _salary;
    public int DirectorId { get; }
    public int YearsOfExperience
    {
        get => _yearsOfExperience;
        set
        {
            if (value < 0) throw new ArgumentException("Doświadczenie nie może być ujemne", nameof(YearsOfExperience));
            _yearsOfExperience = value;
        }
    }
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

    //public Director()
    //{
    //    DirectorId = GetNextId();
    //    MaxId = DirectorId;
    //    YearsOfExperience = 0;
    //    Salary = 0;
    //    Plays = new List<Play>();
    //}

    public Director(string firstName, string lastName, int yearsOfExperience, decimal salary, List<Play>? plays = null) 
        : this(GetNextId(), firstName, lastName, yearsOfExperience, salary, plays) { }

    public Director(int directorId, string firstName, string lastName, int yearsOfExperience, decimal salary, List<Play>? plays = null)
        : base(firstName, lastName)
    {
        if (directorId <= MaxId) throw new ArgumentOutOfRangeException(nameof(directorId), $"ID reżysera {directorId} jest mniejsze lub równe MaxId {MaxId}");
        DirectorId = directorId;
        MaxId = DirectorId;
        YearsOfExperience = yearsOfExperience;
        Salary = salary;
        Plays = plays ?? new List<Play>();
    }

    public bool AddPlay(Play play)
    {
        if (play is null || Plays.Contains(play)) return false;
        play.Director = this;
        Plays.Add(play);
        return true;
    }
    public bool RemovePlay(Play play)
    {
        if (Plays.Count == 0 || play is null) return false;
        play.Director = null;
        return Plays.Remove(play);
    }
    public bool RemovePlay(int playId)
    {
        if (Plays.Count == 0) return false;
        var play = Plays.FirstOrDefault(p => p.PlayId == playId);
        if (play is null) return false;
        play.Director = null;
        return Plays.Remove(play);
    }
    public void RemoveAllPlays()
    {
        foreach (Play play in Plays)
        {
            play.Director = null;
        }
        Plays.Clear();
    }

    public string GetPlaysString()
    {
        return Plays.ListToString("Nie reżyserował żadnych sztuk", '-');
    }

    public override string ToString()
    {
        return base.ToString() + $"/{DirectorId}/{YearsOfExperience}/{Salary}PLN";
    }
}
