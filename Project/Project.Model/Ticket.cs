using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public decimal Price { get; set; }
        public TicketStatus Status { get; set; }
        public Customer Customer { get; private set; }
        public Performance Performance { get; private set; }
        public Seat Seat { get; private set; }

        public Ticket(int ticketId, decimal price, TicketStatus status, Customer customer, Performance performance, Seat seat)
        {
            TicketId = ticketId;
            Price = price;
            Status = status;
            Customer = customer;
            Performance = performance;
            Seat = seat;
        }
    }
}
