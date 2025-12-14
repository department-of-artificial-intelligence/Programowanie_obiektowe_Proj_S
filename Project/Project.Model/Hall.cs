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

    // Metody tworzenia i usuwania elementów listy Seat
    public Seat? CreateSeat(int rowNumber, int seatNumber)
    {
        if (rowNumber <= 0 || seatNumber <= 0) return null;
        if (Seats.Any(s => s.RowNumber == rowNumber && s.SeatNumber == seatNumber)) return null;
        Seat seat = new Seat(rowNumber, seatNumber);
        Seats.Add(seat);
        return seat;
    }
    public List<Seat> CreateSeats(int a, int b)
    {
        List<Seat> createdSeats = new List<Seat>();
        for (int i = 1; i <= a; i++)
        {
            for (int j = 1; j <= b; j++)
            {
                Seat? seat = CreateSeat(i, j);
                if (seat is not null) createdSeats.Add(seat);
            }
        }
        return createdSeats;
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

    public Seat? GetSeatByLocation(int row, int seat) 
    {
        return Seats.FirstOrDefault(s => s.RowNumber == row && s.SeatNumber == seat);
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
        performance.Hall = null;
        return Performances.Remove(performance);
    }
    public void RemoveAllPerformances()
    {
        foreach (var performance in Performances.ToList())
        {
            RemovePerformance(performance);
        }
    }

    public string GetSeatsString()
    {
        return Seats.ListToString("Brak siedzeń");
    }

    public string VisualizeSeatsString()
    {
        if (Seats.Count == 0) return "Brak siedzeń w Sali";

        int maxRow = Seats.Max(s => s.RowNumber);
        int maxSeat = Seats.Max(s => s.SeatNumber);

        string seatsString = "";

        for (int r = 1; r <= maxRow; r++)
        {
            for (int s = 1; s <= maxSeat; s++)
            {
                var foundSeat = Seats.FirstOrDefault(seat => seat.RowNumber == r && seat.SeatNumber == s);
                seatsString += (foundSeat is not null ? foundSeat.SeatLocation() : "(X, X)") + " ";
            };
            seatsString += r != maxRow ? "\n" : string.Empty;
        }

        return seatsString;
    }

    public string GetPerformancesString()
    {
        return Performances.ListToString("Brak przedstawień");
    }

    public override string ToString()
    {
        return $"{HallId}/Sala {HallName}";
    }
}
