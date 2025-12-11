namespace Project.Model;

public class Performance
{
    // Właściwości
    public int PerformanceId { get; private set; } // PK
    public Play Play { get; } = default!; // Navigation property
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public PerformanceStatus Status { get; set; }
    public List<Ticket> Tickets { get; } = new List<Ticket>(); // Navigation property
    internal Hall? Hall { get; set; } // Navigation property

    // Konstruktory
    private Performance() { }

    public Performance(Play play, DateTime startTime, DateTime endTime, PerformanceStatus status = PerformanceStatus.Scheduled)
    {
        if (play is null) throw new ArgumentNullException(nameof(play), "Sztuka nie może być null");
        Play = play;
        SetTimes(startTime, endTime);
        Status = status;
        Hall = null;
    }

    // Metoda zmiany czasów
    public void SetTimes(DateTime start, DateTime end)
    {
        if (end <= start) throw new ArgumentException($"Czas zakończenia {end} musi być późniejszy niż rozpoczęcia {start}");
        StartTime = start;
        EndTime = end;
    }

    // Metody tworzenia i usuwania elementów listy Ticket
    public bool CreateTicket(decimal price, Seat seat, TicketStatus status = TicketStatus.Available)
    {
        if (price < 0 || seat is null || Hall is null) return false;
        if (!Hall.Seats.Contains(seat)) return false;
        Ticket ticket = new Ticket(price, this, seat, status);
        Tickets.Add(ticket);
        return true;
    }
    public bool DeleteTicket(int ticketId)
    {
        var ticket = Tickets.FirstOrDefault(t => t.TicketId == ticketId);
        if (ticket is null) return false;
        return Tickets.Remove(ticket);
    }
    public void DeleteTickets()
    {
        Tickets.Clear();
    }
    public void CreateTicketForEverySeat(decimal price, TicketStatus status = TicketStatus.Available)
    {
        if (Hall is null) return;
        foreach (var seat in Hall.OrderSeats())
        {
            CreateTicket(price, seat, status);
        }
    }

    // Metody string
    public string GetTicketsString()
    {
        return Tickets.ListToString("Brak biletów", '-');
    }

    public override string ToString()
    {
        string hallName = Hall?.HallName ?? "nieznana";
        return $"\"{Play.Title}\"/{Status}/Sala {hallName}/start:{StartTime}/koniec:{EndTime}";
    }
}