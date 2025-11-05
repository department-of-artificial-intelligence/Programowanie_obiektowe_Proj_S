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
        public string Status { get; set; } // na enum
        public Customer Customer { get; set; }
        public Performance Performance { get; set; }
        public Seat Seat { get; set; }

        public Ticket() : this(default, default, string.Empty, new Customer(), new Performance(), new Seat()) { }
        public Ticket(int ticketId, decimal price, string status, Customer customer, Performance performance, Seat seat)
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
