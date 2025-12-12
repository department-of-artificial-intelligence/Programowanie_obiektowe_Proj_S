namespace Project.Model;

public class Performance
{
    // Pola prywatne
    private PerformanceStatus _status;

    // Właściwości
    public int PerformanceId { get; private set; } // PK
    public Play Play { get; } = default!; 
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public PerformanceStatus Status
    {
        get => _status;
        set
        {
            if (value == PerformanceStatus.Canceled)
            {
                DeleteTickets();
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

    // Metody tworzenia i usuwania elementów listy Ticket
    public bool CreateTicket(decimal price, Seat seat, TicketStatus status = TicketStatus.Available)
    {
        if (price < 0 || seat is null || Hall is null) return false;
        if (!Hall.Seats.Contains(seat)) return false;
        Ticket ticket = new Ticket(price, this, seat, status);
        Tickets.Add(ticket);
        return true;
    }
    public bool DeleteTicket(Ticket ticket)
    {   
        if (!Tickets.Contains(ticket)) return false;
        if (ticket.Customer is null)
        {
            Tickets.Remove(ticket);
            return true;
        }
        var customer = ticket.Customer;
        if (customer.RefundTicket(ticket))
        {
            Tickets.Remove(ticket);
            return true;
        }
        if (customer.CancelReservation(ticket))
        {
            Tickets.Remove(ticket);
            return true;
        }
        if (ticket.Status == TicketStatus.Sold && Status == PerformanceStatus.Finished)
        {
            customer.RemoveTicket(ticket);
            Tickets.Remove(ticket);
            return true;
        }
        return false;
    }
    public void DeleteTickets()
    {
        foreach (var ticket in Tickets.ToList())
        {
            DeleteTicket(ticket);
        }
    }
    public void CreateTicketForEverySeat(decimal price, TicketStatus status = TicketStatus.Available)
    {
        if (Hall is null) return;
        foreach (var seat in Hall.Seats)
        {
            CreateTicket(price, seat, status);
        }
    }

    // Metody string
    public string GetTicketsString()
    {
        return Tickets.ListToString("Brak biletów", '-');
    }

    public string VisualizeTicketsString()
    {
        if (Hall is null) return "Brak przypisanej Sali";
        var allSeats = Hall.Seats;
        if (allSeats.Count == 0) return "Brak siedzeń w Sali";

        int maxRow = allSeats.Max(s => s.RowNumber);
        int maxSeat = allSeats.Max(s => s.SeatNumber);

        string ticketsString = "";
        ticketsString += $"Wizualizacja biletów dla sztuki: {Play.Title} | Sala: {Hall.HallName}\n";
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

                rowString += $"{symbol} ";
            }
            ticketsString += rowString + (r != maxRow ? "\n" : string.Empty);
        }

        return ticketsString;
    }

    public override string ToString()
    {
        string hallName = Hall?.HallName ?? "nieznana";
        return $"\"{Play.Title}\"/{Status}/Sala {hallName}/start:{StartTime}/koniec:{EndTime}";
    }
}
