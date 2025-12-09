namespace Project.Model;

public class Actor : Person, IPlayManager
{
    // Pola prywatne
    private decimal _salary;

    // Właściwości
    public int ActorId { get; private set; } // PK
    public decimal Salary 
    { 
        get => _salary;
        set 
        {
            if (value < 0) throw new ArgumentException("Płaca nie może być ujemna", nameof(Salary));
            _salary = value;
        }
    }
    public List<Play> Plays { get; } = new List<Play>(); // Navigation property

    // Konstruktory
    private Actor() { }

    public Actor(string firstName, string lastName, decimal salary, List<Play>? plays = null) 
        : base(firstName, lastName)
    {
        Salary = salary;
        Plays = plays ?? new List<Play>();
        foreach (var play in Plays)
        {
            if (!play.Actors.Contains(this))
            {
                play.Actors.Add(this);
            }
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

    // Metody string
    public string GetPlaysString()
    {
        return Plays.ListToString("Nie gra w żadnych sztukach", '-');
    }

    public override string ToString()
    {
        return base.ToString() + $"/{Salary}PLN";
    }
}