using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model;

public class Author : Person, IPlayManager
{
    private static int MaxId = 0;
    public int AuthorId { get; }
    public List<Play> Plays { get; }
    private static int GetNextId() => MaxId + 1;

    //public Author()
    //{
    //    AuthorId = GetNextId();
    //    MaxId = AuthorId;
    //    Plays = new List<Play>();
    //}

    public Author(string firstName, string lastName, List<Play>? plays = null) 
        : this(GetNextId(), firstName, lastName, plays) { }

    public Author(int authorId, string firstName, string lastName, List<Play>? plays = null)
        : base(firstName, lastName)
    {
        if (authorId <= MaxId) throw new ArgumentOutOfRangeException(nameof(authorId), $"ID autora {authorId} jest mniejsze lub równe MaxId {MaxId}");
        AuthorId = authorId;
        MaxId = AuthorId;
        Plays = plays ?? new List<Play>();
    }

    public bool AddPlay(Play play)
    {
        if (play is null || Plays.Contains(play)) return false;
        play.Author = this;
        Plays.Add(play);
        return true;
    }
    public bool RemovePlay(Play play)
    {
        if (Plays.Count == 0 || play is null) return false;
        play.Author = null;
        return Plays.Remove(play);
    }
    public bool RemovePlay(int playId)
    {
        if (Plays.Count == 0) return false;
        var play = Plays.FirstOrDefault(p => p.PlayId == playId);
        if (play is null) return false;
        play.Author = null;
        return Plays.Remove(play);
    }
    public void RemoveAllPlays()
    {
        foreach (Play play in Plays)
        {
            play.Author = null;
        }
        Plays.Clear();
    }

    public string GetPlaysString()
    {
        return Plays.ListToString("Nie napisał żadnych sztuk", '-');
    }

    public override string ToString()
    {
        return base.ToString() + $"/{AuthorId}";
    }
}
