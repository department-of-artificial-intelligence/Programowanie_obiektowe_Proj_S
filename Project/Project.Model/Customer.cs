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
        Tickets = tickets ?? new List<Ticket>();
        foreach (var ticket in Tickets)
        {
            ticket.Customer = this;
        }
    }

    // Metody dodawania i usuwania elementów listy Ticket
    public bool AddTicket(Ticket ticket)
    {
        if (ticket is null || Tickets.Contains(ticket)) return false;
        ticket.Customer = this;
        Tickets.Add(ticket);
        return true;
    }
    public bool RemoveTicket(Ticket ticket)
    {
        if (Tickets.Count == 0 || ticket is null) return false;
        ticket.Customer = null;
        return Tickets.Remove(ticket);
    }
    public bool RemoveTicket(int ticketId)
    {
        if (Tickets.Count == 0) return false;
        var ticket = Tickets.FirstOrDefault(t => t.TicketId == ticketId);
        if (ticket is null) return false;
        ticket.Customer = null;
        return Tickets.Remove(ticket);
    }
    public void RemoveAllTickets()
    {
        foreach (var ticket in Tickets)
        {
            ticket.Customer = null;
        }
        Tickets.Clear();
    }

    //public bool ReserveTicket(Performance performance, Seat seat) //WIP, seat or coordinates?
    //{
    //    Ticket? foundTicket = performance.Tickets.FirstOrDefault(t => t.Seat.SeatLocation() == seat.SeatLocation());
    //    if (foundTicket is null || foundTicket.Status != TicketStatus.Available) return false;
    //    foundTicket.Customer = this;
    //    foundTicket.Status = TicketStatus.Reserved;
    //    return true;
    //}
    //// buy ticket for reserved tickets:BuyTicket(Performance performance, Ticket ticket)
    //public bool BuyTicket(Performance performance, Seat seat) //WIP
    //{
    //    Ticket? foundTicket = performance.Tickets.FirstOrDefault(t => t.Seat.SeatLocation() == seat.SeatLocation());
    //    if (foundTicket is null || foundTicket.Status == TicketStatus.Sold) return false;
    //    if (foundTicket.Status == TicketStatus.Reserved && foundTicket.Customer != this) return false;
    //    foundTicket.Status = TicketStatus.Sold;
    //    return true;
    //}
    //public bool CancelReservation(Performance performance) //WIP
    //{
    //    Ticket? foundTicket = performance.Tickets.FirstOrDefault(t => t.Seat.SeatLocation() == seat.SeatLocation());
    //    if (foundTicket is null || foundTicket.Status != TicketStatus.Reserved) return false;
    //    foundTicket.Status = TicketStatus.Available;
    //    return true;
    //}

    // Metody string
    public override string ToString()
    {
        return base.ToString();
    }
}
