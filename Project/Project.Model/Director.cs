namespace Project.Model;

public class Director : Person, IPlayManager
{
    // Pola prywatne
    private int _yearsOfExperience;
    private decimal _salary;

    // Właściwości
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
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(Salary), "Płaca nie może być ujemna");
            _salary = value;
        }
    }
    public List<Play> Plays { get; } = new List<Play>(); 

    // Konstruktory
    public Director() { }

    public Director(string firstName, string lastName, int yearsOfExperience, decimal salary)
        : base(firstName, lastName)
    {
        YearsOfExperience = yearsOfExperience;
        Salary = salary;
    }

    // Metody dodawania i usuwania elementów listy Play
    public bool AddPlay(Play play)
    {
        if (play is null || Plays.Contains(play)) return false;
        if (play.Director is not null && play.Director != this) return false;
        play.Director ??= this;
        Plays.Add(play);
        return true;
    }
    public bool RemovePlay(Play play)
    {
        if (!Plays.Contains(play)) return false;
        play.Director = null;
        return Plays.Remove(play);
    }
    public bool RemoveAllPlays()
    {
        if (Plays.Count == 0) return false;
        foreach (var play in Plays)
        {
            play.Director = null;
        }
        Plays.Clear();
        return true;
    }

    // Metody string
    public string GetPlaysString()
    {
        return Plays.ListToString("Nie reżyserował żadnych sztuk", '-');
    }

    public override string ToString()
    {
        return base.ToString() + $"/{YearsOfExperience}/{Salary}PLN (l.sztuk:{Plays.Count})";
    }
}
