using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model;

public class Ticket
{
    private static int MaxId = 0;
    private decimal _price;
    public int TicketId { get; }
    public decimal Price
    {
        get => _price;
        set
        {
            if (value < 0) throw new ArgumentException("Cena biletu nie może być ujemna", nameof(Price));
            _price = value;
        }
    }
    public Performance Performance { get; }
    public Seat Seat { get; }
    public TicketStatus Status { get; set; }
    public Customer? Customer { get; set; }
    private static int GetNextId() => MaxId + 1;

    //public Ticket(Performance performance, Seat seat)
    //{
    //    TicketId = GetNextId();
    //    MaxId = TicketId;
    //    Price = 0;
    //    Performance = performance;
    //    Seat = seat;
    //    Status = TicketStatus.Available;
    //    Customer = null;
    //}

    public Ticket(decimal price, Performance performance, Seat seat, TicketStatus status = TicketStatus.Available)
        : this(GetNextId(), price, performance, seat, status) { }

    public Ticket(int ticketId, decimal price, Performance performance, Seat seat, TicketStatus status = TicketStatus.Available)
    {
        if (ticketId <= MaxId) throw new ArgumentOutOfRangeException(nameof(ticketId), $"ID biletu {ticketId} jest mniejsze lub równe MaxId {MaxId}");
        if (performance is null) throw new ArgumentNullException(nameof(performance), "Przedstawienie nie może być null");
        if (seat is null) throw new ArgumentNullException(nameof(seat), "Siedzenie nie może być null");
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
