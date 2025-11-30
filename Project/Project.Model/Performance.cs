using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model;

public class Performance
{
    public int PerformanceId { get; set; }
    public Play Play { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public PerformanceStatus Status { get; set; }
    public List<Ticket> Tickets { get; set; }
    public Hall? Hall { get; set; }

    public Performance(int performanceId, Play play, DateTime startTime, DateTime endTime, List<Ticket>? tickets = null, PerformanceStatus status = PerformanceStatus.Scheduled)
    {
        PerformanceId = performanceId;
        Play = play;
        StartTime = startTime;
        EndTime = endTime;
        Tickets = tickets ?? new List<Ticket>();
        Status = status;
        Hall = null;
    }

    public bool CreateTicket(int ticketId, decimal price, Seat seat, TicketStatus status = TicketStatus.Available)
    {
        if (ticketId <= 0 || price <= 0 || seat is null) return false;
        Ticket ticket = new Ticket(ticketId, price, this, seat, status);
        Tickets.Add(ticket);
        return true;
    }
    public bool DeleteTicket(int ticketId)
    {
        if (Tickets.Count == 0) return false;
        var ticket = Tickets.FirstOrDefault(t => t.TicketId == ticketId);
        if (ticket is null) return false;
        return Tickets.Remove(ticket);
    }
    public void DeleteTickets()
    {
        Tickets.Clear();
    }
}
