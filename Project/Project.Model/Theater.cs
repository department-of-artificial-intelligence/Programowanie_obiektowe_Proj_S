namespace Project.Model;

public class Theater
{
    // Pola prywatne
    private string _theaterName = string.Empty;

    // Właściwości
    public int TheaterId { get; private set; } // PK
    public string TheaterName
    {
        get => _theaterName;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Nazwa teatru nie może być null lub pusta", nameof(TheaterName));
            _theaterName = value;
        }
    }
    public Address Address { get; private set; } = default!;
    public List<Hall> Halls { get; } = new List<Hall>();

    // Konstruktory
    private Theater() { }

    internal Theater(string theaterName, string country, string city, string street)
    {
        TheaterName = theaterName;
        Address = new Address(country, city, street);
    }

    // Metoda zmiany adresu
    public void ChangeAddress(string country, string city, string street)
    {
        Address = new Address(country, city, street);
    }

    // Metody tworzenia i usuwania elementów listy Hall
    public bool CreateHall(string hallName, List<Performance>? performances = null)
    {
        Hall hall = new Hall(hallName, performances);
        Halls.Add(hall);
        return true;
    }
    public bool DeleteHall(Hall hall)
    {
        if(!Halls.Contains(hall)) return false;
        foreach (var performance in hall.Performances)
        {
            performance.Hall = null;
        }
        return Halls.Remove(hall);
    }
    public void DeleteAllHalls()
    {
        foreach (var hall in Halls.ToList())
        {
            DeleteHall(hall);
        }
    }

    // Metody string
    public string GetHallsString()
    {
        return Halls.ListToString("Brak sal teatralnych", '-');
    }

    public override string ToString()
    {
        return $"{TheaterName}/Adres: {Address}\nSale teatralne:\n" + GetHallsString();
    }
}
