namespace Project.Model;

public class Author : Person, IPlayManager
{
    // Właściwości
    public List<Play> Plays { get; } = new List<Play>(); 

    // Konstruktory
    public Author() { }

    public Author(string firstName, string lastName, List<Play>? plays = null)
        : base(firstName, lastName)
    {
        if (plays is null) return;
        foreach (var play in plays)
        {
            AddPlay(play);
        }
    }

    // Metody dodawania i usuwania elementów listy Play
    public bool AddPlay(Play play)
    {
        if (play is null || Plays.Contains(play)) return false;
        if (play.Author is not null && play.Author != this) throw new InvalidOperationException($"Sztuka \"{play.Title}\" ma już innego autora: {play.Author.FirstName} {play.Author.LastName}");
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
    public void RemoveAllPlays()
    {
        foreach (var play in Plays)
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
        return base.ToString() + $" ({Plays.Count} sztuk)";
    }
}
