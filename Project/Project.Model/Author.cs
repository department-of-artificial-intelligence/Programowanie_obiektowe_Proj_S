namespace Project.Model;

public class Author : Person, IPlayManager
{
    // Właściwości
    public List<Play> Plays { get; } = new List<Play>(); 

    // Konstruktory
    public Author() { }

    public Author(string firstName, string lastName)
        : base(firstName, lastName) { }

    // Metody dodawania i usuwania elementów listy Play
    public bool AddPlay(Play play)
    {
        if (play is null || Plays.Contains(play)) return false;
        if (play.Author is not null && play.Author != this) return false;
        play.Author ??= this;
        Plays.Add(play);
        return true;
    }
    public bool RemovePlay(Play play)
    {
        if (!Plays.Contains(play)) return false;
        play.Author = null;
        return Plays.Remove(play);
    }
    public bool RemoveAllPlays()
    {
        if (Plays.Count == 0) return false;
        foreach (var play in Plays)
        {
            play.Author = null;
        }
        Plays.Clear();
        return true;
    }

    // Metody string
    public string GetPlaysString()
    {
        return Plays.ListToString("Nie napisał żadnych sztuk", '-');
    }

    public override string ToString()
    {
        return base.ToString() + $" (l.sztuk:{Plays.Count})";
    }
}
