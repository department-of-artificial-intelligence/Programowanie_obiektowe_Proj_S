namespace Project.Model;

public class Customer : Person, ITicketTransactions
{
    // Właściwości
    public int CustomerId { get; private set; } // PK
    public List<Ticket> Tickets { get; } = new List<Ticket>(); 

    // Konstruktory
    public Customer() { }

    public Customer(string firstName, string lastName)
        : base(firstName, lastName) { }

    // Metody dodawania i usuwania elementów listy Ticket (potrzebne do rezerwacji/kupna)
    private bool AddTicket(Ticket ticket)
    {
        if (ticket is null || Tickets.Contains(ticket)) return false;
        if (ticket.Customer is not null && ticket.Customer != this) throw new InvalidOperationException($"Bilet ma już właściciela: {ticket}");
        ticket.Customer ??= this;
        Tickets.Add(ticket);
        return true;
    }
    private bool RemoveTicket(Ticket ticket)
    {
        if (!Tickets.Contains(ticket)) return false;
        ticket.Status = TicketStatus.Available;
        ticket.Customer = null;
        return Tickets.Remove(ticket);
    }

    // Metody zarządzania rezerwacją/kupnem biletów
    public bool BuyTicket(Ticket ticket)
    {
        if (!ticket.CanBeBought(this)) return false;
        ticket.Status = TicketStatus.Sold;
        return AddTicket(ticket);
    }
    public bool RefundTicket(Ticket ticket)
    {
        if (!ticket.CanBeRefunded(this)) return false;
        return RemoveTicket(ticket);
    }
    public bool ReserveTicket(Ticket ticket)
    {
        if (!ticket.CanBeReserved(this)) return false;
        ticket.Status = TicketStatus.Reserved;
        return AddTicket(ticket);
    }
    public bool CancelReservation(Ticket ticket)
    {
        if (!ticket.CanBeCanceled(this)) return false;
        return RemoveTicket(ticket);
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
    public void RefundAllBought()
    {
        var boughtTickets = Tickets
            .Where(t => t.Status == TicketStatus.Sold)
            .ToList();
        foreach (var ticket in boughtTickets)
        {
            RefundTicket(ticket);
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
