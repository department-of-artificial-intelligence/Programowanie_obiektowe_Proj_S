namespace Project.Model;

public class Customer : Person
{
    // Właściwości
    public int CustomerId { get; private set; } // PK
    public List<Ticket> Tickets { get; } = new List<Ticket>(); // Navigation property

    // Konstruktory
    public Customer() { }

    public Customer(string firstName, string lastName, List<Ticket>? tickets = null)
        : base(firstName, lastName)
    {
        if (tickets is null) return;
        foreach (var ticket in tickets)
        {
            AddTicket(ticket);
        }
    }

    // Metody dodawania i usuwania elementów listy Ticket
    public bool AddTicket(Ticket ticket)
    {
        if (ticket is null || Tickets.Contains(ticket)) return false;
        if (ticket.Customer is not null && ticket.Customer != this) throw new InvalidOperationException($"Bilet ma już właściciela: {ticket}");
        ticket.Customer ??= this;
        Tickets.Add(ticket);
        return true;
    }
    public bool RemoveTicket(Ticket ticket)
    {
        if (ticket is null) return false;
        if (ticket.Status == TicketStatus.Reserved) ticket.Status = TicketStatus.Available;
        ticket.Customer = null;
        return Tickets.Remove(ticket);
    }
    public bool RemoveTicket(int ticketId)
    {
        var ticket = Tickets.FirstOrDefault(t => t.TicketId == ticketId);
        if (ticket is null) return false;
        ticket.Status = TicketStatus.Available;
        ticket.Customer = null;
        return Tickets.Remove(ticket);
    }
    public void RemoveAllTickets()
    {
        foreach (var ticket in Tickets.ToList())
        {
            RemoveTicket(ticket);
        }
    }

    // Metody zarządzania rezerwacją biletów
    // Sprawdzanie możliwości
    public bool CanBeReserved(Ticket ticket)
    {
        if (ticket is null || ticket.Status != TicketStatus.Available) return false;
        return true;
    }
    public bool CanBeCanceled(Ticket ticket)
    {
        if (ticket is null || ticket.Status != TicketStatus.Reserved || ticket.Customer != this) return false;
        return true;
    }
    public bool CanBeBought(Ticket ticket)
    {
        if (ticket is null || ticket.Status == TicketStatus.Sold) return false;
        if (ticket.Status == TicketStatus.Reserved && ticket.Customer != this) return false;
        return true;
    }

    // Zadziała tylko gdy jest dostępny
    public bool ReserveTicket(Performance performance, Seat seat)
    {
        if (performance is null || seat is null) return false;
        Ticket? ticket = performance.Tickets.FirstOrDefault(t => t.Seat == seat);

        if (ticket is null) return false;
        if (!CanBeReserved(ticket)) return false;

        ticket.Status = TicketStatus.Reserved;
        AddTicket(ticket);
        return true;
    }

    // Zadziała gdy jest dostępny lub zarezerwowany przez tego klienta
    public bool BuyTicket(Performance performance, Seat seat)
    {
        if (performance is null || seat is null) return false;
        Ticket? ticket = performance.Tickets.FirstOrDefault(t => t.Seat == seat);

        if (ticket is null) return false;
        if (!CanBeBought(ticket)) return false;

        ticket.Status = TicketStatus.Sold;
        AddTicket(ticket);
        return true;
    }

    // Zadziała tylko gdy jest zarezerwowany
    public bool CancelReservation(Performance performance, Seat seat)
    {
        if (performance is null || seat is null) return false;
        Ticket? ticket = performance.Tickets.FirstOrDefault(t => t.Seat == seat);

        if (ticket is null) return false;
        if (!CanBeCanceled(ticket)) return false;

        RemoveTicket(ticket);
        return true;
    }

    public bool BuyTicket(Ticket ticket)
    {
        if (!CanBeBought(ticket)) return false;
        ticket.Status = TicketStatus.Sold;
        AddTicket(ticket);
        return true;
    }
    public bool CancelReservation(Ticket ticket)
    {
        if (!CanBeCanceled(ticket)) return false;
        RemoveTicket(ticket);
        return true;
    }
    public void BuyAllReserved()
    {
        var reservedTickets = Tickets
            .Where(t => t.Status == TicketStatus.Reserved)
            .ToList();
        foreach (var ticket in reservedTickets)
        {
            ticket.Status = TicketStatus.Sold;
        }
    }
    public void CancelAllReserved()
    {
        var reservedTickets = Tickets
            .Where(t => t.Status == TicketStatus.Reserved)
            .ToList();
        foreach (var ticket in reservedTickets)
        {
            CancelReservation(ticket);
        }
    }

    // Metody string
    public string GetTicketsString()
    {
        return Tickets.ListToString("Brak biletów", '-');
    }

    public override string ToString()
    {
        return base.ToString();
    }
}