using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model;

public class Customer : Person
{
    private static int MaxId = 0;
    public int CustomerId { get; }
    public List<Ticket> Tickets { get; }
    private static int GetNextId() => MaxId + 1;

    //public Customer()
    //{
    //    CustomerId = GetNextId();
    //    MaxId = CustomerId;
    //    Tickets = new List<Ticket>();
    //}

    public Customer(string firstName, string lastName)
        : this(GetNextId(), firstName, lastName) { }

    public Customer(int customerId, string firstName, string lastName) 
        : base(firstName, lastName)
    {
        if (customerId <= MaxId) throw new ArgumentOutOfRangeException(nameof(customerId), $"ID klienta {customerId} jest mniejsze lub równe MaxId {MaxId}");
        CustomerId = customerId;
        MaxId = CustomerId;
        Tickets = new List<Ticket>();
    }

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

    public bool ReserveTicket(Performance performance, Seat seat) //WIP, seat or coordinates?
    {
        Ticket? foundTicket = performance.Tickets.FirstOrDefault(t => t.Seat.SeatLocation() == seat.SeatLocation());
        if (foundTicket is null || foundTicket.Status != TicketStatus.Available) return false;
        foundTicket.Customer = this;
        foundTicket.Status = TicketStatus.Reserved;
        return true;
    }
    // buy ticket for reserved tickets:BuyTicket(Performance performance, Ticket ticket)
    public bool BuyTicket(Performance performance, Seat seat) //WIP
    {
        Ticket? foundTicket = performance.Tickets.FirstOrDefault(t => t.Seat.SeatLocation() == seat.SeatLocation());
        if (foundTicket is null || foundTicket.Status == TicketStatus.Sold) return false;
        if (foundTicket.Status == TicketStatus.Reserved && foundTicket.Customer != this) return false;
        foundTicket.Status = TicketStatus.Sold;
        return true;
    }
    //public bool CancelReservation(Performance performance) //WIP
    //{
    //    Ticket? foundTicket = performance.Tickets.FirstOrDefault(t => t.Seat.SeatLocation() == seat.SeatLocation());
    //    if (foundTicket is null || foundTicket.Status != TicketStatus.Reserved) return false;
    //    foundTicket.Status = TicketStatus.Available;
    //    return true;
    //}

    public override string ToString()
    {
        return base.ToString() + $"/{CustomerId}";
    }
}
