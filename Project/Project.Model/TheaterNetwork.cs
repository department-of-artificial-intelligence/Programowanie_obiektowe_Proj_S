namespace Project.Model;

public class TheaterNetwork
{
    // Pola prywatne
    private string _networkName = string.Empty;

    // Właściwości
    public int TheaterNetworkId { get; private set; } // PK
    public string NetworkName
    {
        get => _networkName;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Nazwa sieci nie może być null lub pusta", nameof(NetworkName));
            _networkName = value;
        }
    }
    public List<Theater> Theaters { get; } = new List<Theater>(); // Navigation property

    // Konstruktory
    private TheaterNetwork() { }

    public TheaterNetwork(string name)
    {
        NetworkName = name;
    }

    // Metody tworzenia i usuwania elementów listy Theater
    public bool CreateTheater(string theaterName, string country, string city, string street)
    {
        if (string.IsNullOrWhiteSpace(theaterName) || string.IsNullOrWhiteSpace(country) || string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(street)) return false;
        Theater theater = new Theater(theaterName, country, city, street, this);
        Theaters.Add(theater);
        return true;
    }
    public bool DeleteTheater(int theaterId)
    {
        if (Theaters.Count == 0) return false;
        var theater = Theaters.FirstOrDefault(t => t.TheaterId == theaterId);
        if (theater is null) return false;
        return Theaters.Remove(theater);
    }
    public void DeleteAllTheaters()
    {
        Theaters.Clear();
    }

    // Metody string
    public string GetTheatersString()
    {
        return Theaters.ListToString("Brak teatrów", '*');
    }

    public override string ToString()
    {
        return $"Sieć teatrów: {NetworkName}\n" + GetTheatersString();
    }
}