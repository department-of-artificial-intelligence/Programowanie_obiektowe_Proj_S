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
    public List<Theater> Theaters { get; } = new List<Theater>(); 

    // Konstruktory
    private TheaterNetwork() { }

    public TheaterNetwork(string name)
    {
        NetworkName = name;
    }

    // Metody tworzenia i usuwania elementów listy Theater
    public Theater? CreateTheater(string theaterName, string country, string city, string street)
    {
        if (string.IsNullOrWhiteSpace(theaterName) || string.IsNullOrWhiteSpace(country) || string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(street)) return null;
        Theater theater = new Theater(theaterName, country, city, street);
        Theaters.Add(theater);
        return theater;
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
