namespace Project.Model;

public class Hall
{
    // Pola prywatne
    private string _hallName = string.Empty;
    // private Dictionary<(int, int), Seat> SeatMap { get; } = new Dictionary<(int, int), Seat>(); // możliwa zmiana: mapa siedzeń

    // Właściwości
    public int HallId { get; private set; } // PK
    public string HallName
    {
        get => _hallName;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Nazwa sali nie może być null lub pusta", nameof(HallName));
            _hallName = value;
        }
    }
    public List<Seat> Seats { get; } = new List<Seat>(); 
    public List<Performance> Performances { get; } = new List<Performance>(); 

    // Konstruktory
    private Hall() { }

    internal Hall(string hallName, List<Performance>? performances = null)
    {
        HallName = hallName;
        if (performances is null) return;
        foreach (var performance in performances)
        {
            AddPerformance(performance);
        }
    }

    // Metody zwracające maksymalną ilość miejsc
    public int MaxRows() => Seats.Any() ? Seats.Max(s => s.RowNumber) : 0;
    public int MaxSeatsInRow(int row) => Seats.Any() ? Seats.Where(s => s.RowNumber == row).Max(s => s.SeatNumber) : 0;

    // Metody tworzenia i usuwania elementów listy Seat
    public bool CreateSeat(int rowNumber, int seatNumber)
    {
        if (rowNumber <= 0 || seatNumber <= 0) return false;
        if (Seats.Any(s => s.RowNumber == rowNumber && s.SeatNumber == seatNumber)) return false;
        Seat seat = new Seat(rowNumber, seatNumber);
        Seats.Add(seat);
        return true;
    }
    public void CreateSeats(int a, int b)
    {
        for (int i = 1; i <= a; i++)
        {
            for (int j = 1; j <= b; j++)
            {
                CreateSeat(i, j);
            }
                
        }
    }
    public bool DeleteSeat(Seat seat)
    {
        if (!Seats.Contains(seat)) return false;
        return Seats.Remove(seat);
    }
    public void DeleteAllSeats()
    {
        foreach (var seat in Seats.ToList())
        {
            DeleteSeat(seat);
        }
    }

    // Metody dodawania i usuwania elementów listy Performance
    public bool AddPerformance(Performance performance)
    {
        if (performance is null || Performances.Contains(performance)) return false;
        if (performance.Hall is not null && performance.Hall != this) throw new InvalidOperationException($"Przedstawienie ma już salę: {performance}");
        performance.Hall ??= this;
        Performances.Add(performance);
        return true;
    }
    public bool RemovePerformance(Performance performance)
    {
        if (!Performances.Contains(performance)) return false;
        performance.RemoveHallReference();
        return Performances.Remove(performance);
    }
    public void RemoveAllPerformances()
    {
        foreach (var performance in Performances.ToList())
        {
            RemovePerformance(performance);
        }
    }

    // Metoda sortująca siedzenia
    public List<Seat> OrderSeats()
    {
        return Seats
            .OrderBy(s => s.RowNumber)
            .ThenBy(s => s.SeatNumber)
            .ToList();
    }

    // Metody string
    //public string GetSeatsString() // zwraca tylko istniejące siedzenia
    //{
    //    if (Seats.Count == 0) return "Brak siedzeń w Sali";
    //    List<Seat> orderedSeats = OrderSeats();
    //    int maxRows = MaxRows();
    //    string seatsString = string.Empty;

    //    for (int i = 1; i <= maxRows; i++)
    //    {
    //        seatsString += string.Join(" ", orderedSeats
    //            .Where(s => s.RowNumber == i)
    //            .Select(s => $"{s.SeatLocation()}"));

    //        seatsString += i != maxRows ? "\n" : string.Empty;
    //    }

    //    return seatsString;
    //}

    public string VisualizeSeatsString() // zwraca też puste miejsca pomiędzy jako (X, X)
    {
        if (Seats.Count == 0) return "Brak siedzeń w Sali";
        List<Seat> orderedSeats = OrderSeats();
        int maxRows = MaxRows();
        string seatsString = $"Wizualizacja miejsc dla Sali: { HallName }\n";
        Seat? seat;

        for (int i = 1; i <= maxRows; i++)
        {
            int maxSeats = MaxSeatsInRow(i);
            for (int j = 1; j <= maxSeats; j++)
            {
                seat = Seats.FirstOrDefault(s => s.RowNumber == i && s.SeatNumber == j);
                seatsString += (seat is not null ? seat.SeatLocation() : "(X, X)") + " ";
            };
            seatsString += i != maxRows ? "\n" : string.Empty;
        }

        return seatsString;
    }

    public string GetPerformancesString()
    {
        return Performances.ListToString("Brak przedstawień");
    }

    public override string ToString()
    {
        return $"Sala {HallId}";
    }
}
