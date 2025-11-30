using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model;

public class Ticket
{
    public int TicketId { get; set; }
    public decimal Price { get; set; }
    public Performance Performance { get; set; }
    public Seat Seat { get; set; }
    public TicketStatus Status { get; set; }
    public Customer? Customer { get; set; }

    public Ticket(int ticketId, decimal price, Performance performance, Seat seat, TicketStatus status = TicketStatus.Available)
    {
        TicketId = ticketId;
        Price = price;
        Performance = performance;
        Seat = seat;
        Status = status;
        Customer = null;
    }
}
