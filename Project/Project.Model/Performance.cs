namespace Project.Model;

public class Performance
{
    // Pola prywatne
    private PerformanceStatus _status;

    // Właściwości
    public int PerformanceId { get; private set; } // PK
    public Play Play { get; private set; } = default!; 
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public PerformanceStatus Status
    {
        get => _status;
        set
        {
            if (value == PerformanceStatus.Canceled)
            {
                foreach (var ticket in Tickets)
                {
                    if (ticket.CanBeCanceled(ticket.Customer)) ticket.Customer?.CancelReservation(ticket);
                    if (ticket.CanBeRefunded(ticket.Customer)) ticket.Customer?.RefundTicket(ticket);
                }
            }
            _status = value;
        }
    }
    public List<Ticket> Tickets { get; } = new List<Ticket>(); 
    public Hall? Hall { get; set; }

    // Konstruktory
    public Performance() { }

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
        if (Status == PerformanceStatus.Canceled || Status == PerformanceStatus.Finished) throw new ArgumentException($"Przedstawienie ma status: {Status}");
        if (end <= start) throw new ArgumentException($"Czas zakończenia {end} musi być późniejszy niż rozpoczęcia {start}");
        StartTime = start;
        EndTime = end;
    }

    // Metody tworzenia biletów
    public Ticket? CreateTicket(decimal price, Seat seat)
    {
        if (price < 0 || seat is null || Hall is null) return null;
        if (!Hall.Seats.Contains(seat)) return null;
        Ticket ticket = new Ticket(price, this, seat);
        Tickets.Add(ticket);
        return ticket;
    }
    public List<Ticket> CreateTicketForEverySeat(decimal price)
    {
        List<Ticket> createdSeats = new List<Ticket>();
        if (Hall is null) return createdSeats;
        foreach (var seat in Hall.Seats)
        {
            Ticket? ticket = CreateTicket(price, seat);
            if (ticket is not null) createdSeats.Add(ticket);
        }
        return createdSeats;
    }

    public bool AddHall(Hall hall)
    {
        if (hall is null || Hall is not null) return false;
        return hall.AddPerformance(this);
    }

    public List<Ticket> OrderTickets()
    {
        return Tickets
            .OrderBy(t => t.Seat.RowNumber)
            .ThenBy(t => t.Seat.SeatNumber)
            .ToList();
    }

    public Ticket? GetTicketBySeatLocation(int row, int seat)
    {
        return Tickets.FirstOrDefault(t => t.Seat.RowNumber == row && t.Seat.SeatNumber == seat);
    }

    // Metody string
    public string GetTicketsString()
    {
        return OrderTickets().ListToString("Brak biletów", '-');
    }

    public string VisualizeTicketsString()
    {
        if (Hall is null) return "Brak przypisanej Sali";
        var allSeats = Hall.Seats;
        if (allSeats.Count == 0) return "Brak siedzeń w Sali";

        int maxRow = allSeats.Max(s => s.RowNumber);
        int maxSeat = allSeats.Max(s => s.SeatNumber);

        string ticketsString = "";
        ticketsString += "Legenda: D - Dostępny, Z - Zarezerwowany, S - Sprzedany, B - Brak biletu, X - Brak Siedzenia\n";

        for (int r = 1; r <= maxRow; r++)
        {
            string rowString = $"Rząd " + (r<10 ? $"0{r}" : r) + ": ";

            for (int s = 1; s <= maxSeat; s++)
            {
                char symbol;

                var foundSeat = allSeats.FirstOrDefault(seat => seat.RowNumber == r && seat.SeatNumber == s);

                if (foundSeat is null)
                {
                    symbol = 'X';
                }
                else
                {
                    var ticket = Tickets.FirstOrDefault(t => t.Seat == foundSeat);

                    if (ticket is not null)
                    {
                        switch (ticket.Status)
                        {
                            case TicketStatus.Available:
                                symbol = 'D';
                                break;
                            case TicketStatus.Reserved:
                                symbol = 'Z';
                                break;
                            case TicketStatus.Sold:
                                symbol = 'S';
                                break;
                            default:
                                symbol = '?';
                                break;
                        }
                    }
                    else
                    {
                        symbol = 'B';
                    }
                }

                rowString += $"{s}{symbol} ";
            }
            ticketsString += rowString + (r != maxRow ? "\n" : string.Empty);
        }

        return ticketsString;
    }

    public override string ToString()
    {
        string hallName = Hall?.HallName ?? "nieznana";
        return $"{PerformanceId}/\"{Play.Title}\"/{Status}/Sala {hallName}/start:{StartTime}/koniec:{EndTime}";
    }
}
