using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Customer : Person
    {
        public required int CustomerId { get; set; }
        public List<Ticket> Tickets { get; private set; }

        public Customer(string firstName, string lastName, int customerId) : base(firstName, lastName)
        {
            CustomerId = customerId;
            Tickets = new List<Ticket>();
        }

        public bool AddTicket(Ticket ticket)
        {
            if (ticket == null || Tickets.Contains(ticket)) return false;
            Tickets.Add(ticket);
            return true;
        }
        public bool RemoveTicket(Ticket ticket)
        {
            if (Tickets.Count == 0 || ticket is null) return false;
            return Tickets.Remove(ticket);
        }
        public void RemoveAllTickets()
        {
            Tickets.Clear();
        }
    }
}
