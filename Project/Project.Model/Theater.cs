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

    // Metody tworzenia i usuwania elementów listy Hall
    public Hall? CreateHall(string hallName, List<Performance>? performances = null)
    {
        Hall hall = new Hall(hallName, performances);
        Halls.Add(hall);
        return hall;
    }

    // Metody string
    public string GetHallsString()
    {
        return Halls.ListToString("Brak sal teatralnych", '-');
    }

    public override string ToString()
    {
        return $"{TheaterId}/{TheaterName}/Adres: {Address}";
    }
}
