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
    public int CustomerId { get; set; }
    public List<Ticket> Tickets { get; set; }
    private static int GetNextId() => MaxId + 1;

    public Customer()
    {
        CustomerId = GetNextId();
        MaxId = CustomerId;
        Tickets = new List<Ticket>();
    }

    public Customer(string firstName, string lastName)
        : this(GetNextId(), firstName, lastName) { }

    public Customer(int customerId, string firstName, string lastName) 
        : base(firstName, lastName)
    {
        if (customerId <= MaxId) throw new Exception($"customerId {customerId} jest mniejsze lub równe MaxId {MaxId}");
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

    public override string ToString()
    {
        return base.ToString() + $"/{CustomerId}";
    }
}
