namespace Project.Model;

public class Director : Person, IPlayManager
{
    // Pola prywatne
    private int _yearsOfExperience;
    private decimal _salary;

    // Właściwości
    public int DirectorId { get; private set; } //PK
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
    public List<Play> Plays { get; } = new List<Play>(); // Navigation property

    // Konstruktory
    private Director() { }

    public Director(string firstName, string lastName, int yearsOfExperience, decimal salary, List<Play>? plays = null)
        : base(firstName, lastName)
    {
        YearsOfExperience = yearsOfExperience;
        Salary = salary;
        Plays = plays ?? new List<Play>();
        foreach (var play in Plays)
        {
            play.Director = this;
        }
    }

    // Metody dodawania i usuwania elementów listy Play
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

    // Metody string
    public string GetPlaysString()
    {
        return Plays.ListToString("Nie reżyserował żadnych sztuk", '-');
    }

    public override string ToString()
    {
        return base.ToString() + $"/{YearsOfExperience}/{Salary}PLN";
    }
}
