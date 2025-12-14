namespace Project.Model;

public class Actor : Person, IPlayManager
{
    // Pola prywatne
    private decimal _salary;

    // Właściwości
    public decimal Salary 
    { 
        get => _salary;
        set 
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(Salary), "Płaca nie może być ujemna");
            _salary = value;
        }
    }
    public List<Play> Plays { get; } = new List<Play>(); 

    // Konstruktory
    public Actor() { }

    public Actor(string firstName, string lastName, decimal salary, List<Play>? plays = null) 
        : base(firstName, lastName)
    {
        Salary = salary;
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
        if (!play.Actors.Contains(this))
        {
            play.Actors.Add(this);
        }
        Plays.Add(play);
        return true;
    }
    public bool RemovePlay(Play play)
    {
        if (!Plays.Contains(play)) return false;
        play.Actors.Remove(this);
        return Plays.Remove(play);
    }
    public bool RemoveAllPlays()
    {
        if (Plays.Count == 0) return false;
        foreach (var play in Plays.ToList())
        {
            play.Actors.Remove(this);
        }
        Plays.Clear();
        return true;
    }

    // Metody string
    public string GetPlaysString()
    {
        return Plays.ListToString("Nie gra w żadnych sztukach", '-');
    }

    public override string ToString()
    {
        return base.ToString() + $"/{Salary}PLN (l.sztuk:{Plays.Count})";
    }
}
