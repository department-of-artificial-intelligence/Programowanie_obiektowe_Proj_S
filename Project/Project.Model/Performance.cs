using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model;

public class Performance
{
    private static int MaxId = 0;
    public int PerformanceId { get; set; }
    public Play Play { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public PerformanceStatus Status { get; set; }
    public List<Ticket> Tickets { get; set; }
    public Hall? Hall { get; set; }
    private static int GetNextId() => MaxId + 1;

    public Performance(Play play)
    {
        PerformanceId = GetNextId();
        Play = play;
        StartTime = DateTime.MinValue;
        EndTime = DateTime.MinValue;
        Tickets = new List<Ticket>();
        Status = PerformanceStatus.Scheduled;
        Hall = null;
    }

    public Performance(Play play, DateTime startTime, DateTime endTime, List<Ticket>? tickets = null, PerformanceStatus status = PerformanceStatus.Scheduled)
        : this(GetNextId(), play, startTime, endTime, tickets, status) { }

    public Performance(int performanceId, Play play, DateTime startTime, DateTime endTime, List<Ticket>? tickets = null, PerformanceStatus status = PerformanceStatus.Scheduled)
    {
        if (performanceId <= MaxId) throw new Exception($"performanceId {performanceId} jest mniejsze lub równe MaxId {MaxId}");
        PerformanceId = performanceId;
        MaxId = PerformanceId;
        Play = play;
        StartTime = startTime;
        EndTime = endTime;
        Tickets = tickets ?? new List<Ticket>();
        Status = status;
        Hall = null;
    }

    public bool CreateTicket(decimal price, Seat seat, TicketStatus status = TicketStatus.Available)
    {
        if (price <= 0 || seat is null) return false;
        Ticket ticket = new Ticket(price, this, seat, status);
        Tickets.Add(ticket);
        return true;
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
    public void CreateTicketForEverySeat(decimal price, TicketStatus status = TicketStatus.Available)
    {
        if (Hall is null) return;
        foreach (Seat seat in Hall.OrderSeats())
        {
            CreateTicket(price, seat, status); // ToDo: statyczne pole maxId i od maxId albo działa tylko gdy nie ma biletów
        }
    }

    public string GetTicketsString()
    {
        return Tickets.ListToString("Brak biletów", '-');
    }

    public override string ToString()
    {
        string hallId = Hall?.HallId.ToString() ?? "nieznana";
        return $"{PerformanceId}/\"{Play.Title}\"/{Status}/Sala {hallId}/start:{StartTime}/koniec:{EndTime}";
    }
}