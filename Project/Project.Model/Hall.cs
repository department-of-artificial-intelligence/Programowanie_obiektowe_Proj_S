namespace Project.Model;

public class Hall
{
    // Pola prywatne
    private string _hallName = string.Empty;

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
    public List<Seat> Seats { get; } = new List<Seat>(); // Navigation property
    public List<Performance> Performances { get; } = new List<Performance>(); // Navigation property
    public Theater Theater { get; } = default!; // Navigation property

    // Konstruktory
    private Hall() { }

    internal Hall(Theater theater, List<Performance>? performances = null)
    {
        Theater = theater;
        Performances = performances ?? new List<Performance>();
        foreach (var performance in Performances)
        {
            performance.Hall = this;
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
        Seat seat = new Seat(rowNumber, seatNumber, this);
        Seats.Add(seat);
        return true;
    }
    public void CreateSeats(int a, int b)
    {
        if (a <= 0 || b <= 0) return;
        for (int i = 1; i <= a; i++)
        {
            for (int j = 1; j <= b; j++)
            {
                if (!Seats.Any(s => s.RowNumber == i && s.SeatNumber == j)) CreateSeat(i, j);
            }
                
        }
    }
    public bool DeleteSeat(int rowNumber, int seatNumber)
    {
        if (Seats.Count == 0) return false;
        var seat = Seats.FirstOrDefault(t => t.RowNumber == rowNumber && t.SeatNumber == seatNumber);
        if (seat is null) return false;
        return Seats.Remove(seat);
    }
    public void DeleteAllSeats()
    {
        Seats.Clear();
    }

    // Metody dodawania i usuwania elementów listy Performance
    public bool AddPerformance(Performance performance)
    {
        if (performance is null || Performances.Contains(performance)) return false;
        performance.Hall = this;
        Performances.Add(performance);
        return true;
    }
    public bool RemovePerformance(Performance performance)
    {
        if (Performances.Count == 0 || performance is null) return false;
        performance.Hall = null;
        return Performances.Remove(performance);
    }
    public bool RemovePerformance(int performanceId)
    {
        if (Performances.Count == 0) return false;
        var performance = Performances.FirstOrDefault(p => p.PerformanceId == performanceId);
        if (performance is null) return false;
        performance.Hall = null;
        return Performances.Remove(performance);
    }
    public void RemoveAllPerformances()
    {
        foreach (var performance in Performances)
        {
            performance.Hall = null;
        }
        Performances.Clear();
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
    public string GetSeatsString() // zwraca tylko istniejące siedzenia
    {
        if (Seats.Count == 0) return "Brak siedzeń";
        List<Seat> orderedSeats = OrderSeats();
        int maxRows = MaxRows();
        string seatsString = string.Empty;

        for (int i = 1; i <= maxRows; i++)
        {
            seatsString += string.Join(" ", orderedSeats
                .Where(s => s.RowNumber == i)
                .Select(s => $"{s.SeatLocation()}"));
            seatsString += i != maxRows ? "\n" : string.Empty;
        }

        return seatsString;
    }

    public string VisualizeSeatsString() // zwraca też puste miejsca pomiędzy jako (X, X)
    {
        if (Seats.Count == 0) return "Brak siedzeń";
        List<Seat> orderedSeats = OrderSeats();
        int maxRows = MaxRows();
        string seatsString = string.Empty;
        Seat? seat;

        for (int i = 1; i <= maxRows; i++)
        {
            for (int j = 1; j <= MaxSeatsInRow(i); j++)
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