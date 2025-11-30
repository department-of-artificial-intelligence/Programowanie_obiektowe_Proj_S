using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model;

public class Customer : Person
{
    public required int CustomerId { get; set; }
    public List<Ticket> Tickets { get; set; }

    public Customer()
    {
        CustomerId = 0;
        Tickets = new List<Ticket>();
    }

    public Customer(string firstName, string lastName, int customerId) 
        : base(firstName, lastName)
    {
        CustomerId = customerId;
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
}
