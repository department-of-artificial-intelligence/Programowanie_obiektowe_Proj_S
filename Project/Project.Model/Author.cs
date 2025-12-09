namespace Project.Model;

public class Author : Person, IPlayManager
{
    // Właściwości
    public int AuthorId { get; private set; } // PK
    public List<Play> Plays { get; } = new List<Play>(); // Navigation property

    // Konstruktory
    private Author() { }

    public Author(string firstName, string lastName, List<Play>? plays = null)
        : base(firstName, lastName)
    {
        Plays = plays ?? new List<Play>();
        foreach (var play in Plays)
        {
            play.Author = this;
        }
    }

    // Metody dodawania i usuwania elementów listy Play
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

    // Metody string
    public string GetPlaysString()
    {
        return Plays.ListToString("Nie napisał żadnych sztuk", '-');
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
