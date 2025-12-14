using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Model;

namespace Project.Services;

public class PerformanceService
{
    private readonly ApplicationDbContext _context;

    public PerformanceService(ApplicationDbContext context)
    {
        _context = context;
    }

    public Performance? GetPerformanceById(int id)
    {
        return _context.Performances
            .AsSplitQuery()
            .Include(p => p.Tickets)
            .Include(p => p.Hall)
            .FirstOrDefault(p => p.PerformanceId == id);
    }
    public Hall? GetHallById(int id)
    {
        return _context.Halls
            .Include(h => h.Seats)
            .FirstOrDefault(h => h.HallId == id);
    }

    public List<Performance> GetAllPerformances()
    {
        return _context.Performances
            .AsSplitQuery()
            .Include(p => p.Play)
            .Include(p => p.Hall)
            .ToList();
    }

    public List<Performance> GetAllPerformancesWithoutHall()
    {
        return _context.Performances
            .AsSplitQuery()
            .Include(p => p.Play)
            .Where(p => p.Hall == null)
            .ToList();
    }

    public bool AddNewPerformance(Play play, DateTime startTime, DateTime endTime)
    {
        if (endTime <= startTime || play is null) return false;
        
        Performance performance = new Performance(play, startTime, endTime);

        _context.Performances.Add(performance);
        _context.SaveChanges();

        return true;
    }

    public Ticket? CreateNewTicket(decimal price, Seat seat, int performanceId)
    {
        if (seat is null || price < 0 || performanceId < 1) return null;

        Ticket? ticket = GetPerformanceById(performanceId)?.CreateTicket(price, seat);
        if (ticket is null) return null;

        _context.Tickets.Add(ticket);
        _context.SaveChanges();

        return ticket;
    }

    public bool CreateNewTickets(decimal price, int performanceId)
    {
        if (price < 0 || performanceId < 1) return false;

        List<Ticket>? createdTickets = GetPerformanceById(performanceId)?.CreateTicketForEverySeat(price);

        if (createdTickets is null || createdTickets.Count == 0) return false;

        _context.Tickets.AddRange(createdTickets);
        _context.SaveChanges();

        return true;
    }
}
