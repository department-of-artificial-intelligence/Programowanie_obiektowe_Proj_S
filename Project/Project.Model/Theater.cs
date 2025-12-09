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
    public TheaterNetwork TheaterNetwork { get; private set; } = default!; // Navigation property
    public List<Hall> Halls { get; } = new List<Hall>(); // Navigation property

    // Konstruktory
    private Theater() { }

    internal Theater(string theaterName, string country, string city, string street, TheaterNetwork theaterNetwork)
    {
        TheaterName = theaterName;
        Address = new Address(country, city, street);
        TheaterNetwork = theaterNetwork;
    }

    // Metoda zmiany adresu
    public void ChangeAddress(string country, string city, string street)
    {
        Address = new Address(country, city, street);
    }

    // Metody tworzenia i usuwania elementów listy Hall
    public bool CreateHall(List<Performance>? performances = null)
    {
        Hall hall = new Hall(this, performances);
        Halls.Add(hall);
        return true;
    }
    public bool DeleteHall(int hallId)
    {
        if (Halls.Count == 0 || hallId <= 0) return false;
        var hall = Halls.FirstOrDefault(t => t.HallId == hallId);
        if (hall is null) return false;
        return Halls.Remove(hall);
    }
    public void DeleteAllHalls()
    {
        Halls.Clear();
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
