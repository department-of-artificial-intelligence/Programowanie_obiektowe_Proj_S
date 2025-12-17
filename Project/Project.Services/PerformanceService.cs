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

    public List<Performance> GetPerformances()
    {
        return _context.Performances
            .ToList();
    }

    public List<Performance> GetPerformancesWithStatus(PerformanceStatus status)
    {
        return _context.Performances
            .AsSplitQuery()
            .Include(p => p.Play)
            .Include(p => p.Tickets)
                .ThenInclude(t => t.Customer)
            .Where(p => p.Status == status)
            .ToList();
    }

    public List<Performance> GetPerformancesWithoutHall()
    {
        return _context.Performances
            .Include(p => p.Play)
            .Where(p => p.Hall == null)
            .ToList();
    }

    public Performance? GetPerformanceById(int id)
    {
        return _context.Performances
            .AsSplitQuery()
            .Include(p => p.Tickets)
            .Include(p => p.Hall)
            .FirstOrDefault(p => p.PerformanceId == id);
    }
    
    public void UpdateStatuses()
    {
        List<Performance> performances = _context.Performances
            .Where(p => (p.Status == PerformanceStatus.Scheduled) || (p.Status == PerformanceStatus.InProgress))
            .ToList();
        var now = DateTime.Now;
        foreach (var performance in performances)
        {
            if (performance.EndTime <= now)
            {
                performance.Status = PerformanceStatus.Finished;
            }
            else if (performance.StartTime <= now)
            {
                performance.Status = PerformanceStatus.InProgress;
            }
        }
        _context.SaveChanges();
    }

    public bool AddNewPerformance(Play play, DateTime startTime, DateTime endTime)
    {
        if (endTime <= startTime || play is null) return false;
        
        Performance performance = new Performance(play, startTime, endTime);

        _context.Performances.Add(performance);
        _context.SaveChanges();

        return true;
    }

    public Ticket? CreateNewTicket(decimal price, Seat seat, Performance performance)
    {
        if (price < 0 || seat is null || performance is null) return null;

        Ticket? ticket = performance.CreateTicket(price, seat);
        if (ticket is null) return null;

        _context.Tickets.Add(ticket);
        _context.SaveChanges();

        return ticket;
    }

    public bool CreateNewTickets(decimal price, Performance performance)
    {
        if (price < 0 || performance is null) return false;

        List<Ticket>? createdTickets = performance.CreateTicketForEverySeat(price);

        if (createdTickets is null || createdTickets.Count == 0) return false;

        _context.Tickets.AddRange(createdTickets);
        _context.SaveChanges();

        return true;
    }

    public bool AddHall(Hall hall, Performance performance)
    {
        if (hall is null || performance is null) return false;
        if (performance.Status != PerformanceStatus.Scheduled) return false;

        var overlappingPerformances = hall.Performances
            .Where(p => p.Status == PerformanceStatus.Scheduled)
            .Where(p =>
                (performance.StartTime >= p.StartTime && performance.StartTime <= p.EndTime) || // zaczyna się w trakcie
                (performance.EndTime >= p.StartTime && performance.EndTime <= p.EndTime) || // kończy się w trakcie
                (performance.StartTime <= p.StartTime && performance.EndTime >= p.EndTime)) // zaczyna się przed i kończy po
            .ToList();

        if (overlappingPerformances.Any()) // jeśli cokolwiek jest w liście to nowe przedstawienie pokrywa się z istniejącym
        {
            throw new Exception("Nie można dodać przedstawienia. W tym czasie jest już jakieś przedstawienie.");
        }

        if (performance.AddHall(hall))
        {
            _context.SaveChanges();
            return true;
        }

        return false;
    }
}
