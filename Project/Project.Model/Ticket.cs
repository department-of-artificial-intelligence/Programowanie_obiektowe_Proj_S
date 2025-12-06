using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model;

public class Ticket
{
    private static int MaxId = 0;
    public int TicketId { get; set; }
    public decimal Price { get; set; }
    public Performance Performance { get; set; }
    public Seat Seat { get; set; }
    public TicketStatus Status { get; set; }
    public Customer? Customer { get; set; }
    private static int GetNextId() => MaxId + 1;

    public Ticket(Performance performance, Seat seat)
    {
        TicketId = GetNextId();
        MaxId = TicketId;
        Price = 0;
        Performance = performance;
        Seat = seat;
        Status = TicketStatus.Available;
        Customer = null;
    }

    public Ticket(decimal price, Performance performance, Seat seat, TicketStatus status = TicketStatus.Available)
        : this(GetNextId(), price, performance, seat, status) { }

    public Ticket(int ticketId, decimal price, Performance performance, Seat seat, TicketStatus status = TicketStatus.Available)
    {
        if (ticketId <= MaxId) throw new Exception($"ticketId {ticketId} jest mniejsze lub równe MaxId {MaxId}");
        TicketId = ticketId;
        MaxId = TicketId;
        Price = price;
        Performance = performance;
        Seat = seat;
        Status = status;
        Customer = null;
    }

    public override string ToString()
    {
        return $"{TicketId}/{Price}PLN/{Status}/Sztuka:{Performance.Play.Title}/Sala:{Performance.Hall?.HallId}/Siedzenie:{Seat}";
    }
}
