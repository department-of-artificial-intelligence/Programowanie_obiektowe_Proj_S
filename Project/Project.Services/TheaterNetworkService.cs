using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Model;

namespace Project.Services;

public class TheaterNetworkService
{
    private readonly ApplicationDbContext _context;

    public TheaterNetworkService(ApplicationDbContext context)
    {
        _context = context;
    }

    public TheaterNetwork? GetNetwork()
    {
        return _context.TheaterNetworks
                    .OrderBy(t => t.TheaterNetworkId)
                    .FirstOrDefault();
    }

    public TheaterNetwork? GetNetworkWithTheaters()
    {
        return _context.TheaterNetworks
                    .Include(t => t.Theaters)
                    .OrderBy(t => t.TheaterNetworkId)
                    .FirstOrDefault();
    }

    public TheaterNetwork? GetFromNetworkToSeats()
    {
        return _context.TheaterNetworks
                    .AsSplitQuery()
                    .Include(t => t.Theaters)
                        .ThenInclude(t => t.Halls)
                            .ThenInclude(h => h.Seats)
                    .OrderBy(t => t.TheaterNetworkId)
                    .FirstOrDefault();
    }

    public TheaterNetwork? GetFromNetworkToTicket()
    {
        return _context.TheaterNetworks
                    .AsSplitQuery()
                    .Include(t => t.Theaters)
                        .ThenInclude(t => t.Halls)
                            .ThenInclude(h => h.Seats)
                    .Include(t => t.Theaters)
                        .ThenInclude(t => t.Halls)
                            .ThenInclude(h => h.Performances)
                                .ThenInclude(p => p.Play)
                    .Include(t => t.Theaters)
                        .ThenInclude(t => t.Halls)
                            .ThenInclude(h => h.Performances)
                                .ThenInclude(p => p.Tickets)
                    .OrderBy(t => t.TheaterNetworkId)
                    .FirstOrDefault();
    }

    public TheaterNetwork? GetFullTheaterNetwork() 
    {
        return _context.TheaterNetworks
                    .AsSplitQuery()
                    .Include(t => t.Theaters)
                        .ThenInclude(t => t.Halls)
                            .ThenInclude(h => h.Seats)
                    .Include(t => t.Theaters)
                        .ThenInclude(t => t.Halls)
                            .ThenInclude(h => h.Performances)
                                .ThenInclude(p => p.Play)
                    .Include(t => t.Theaters)
                        .ThenInclude(t => t.Halls)
                            .ThenInclude(h => h.Performances)
                                .ThenInclude(p => p.Tickets)
                                    .ThenInclude(t => t.Customer)
                    .OrderBy(t => t.TheaterNetworkId)
                    .FirstOrDefault();
    }

    public Theater? CreateNewTheater(string theaterName, string country, string city, string street)
    {
        if (theaterName == string.Empty || country == string.Empty || city == string.Empty || street == string.Empty) return null;

        Theater? theater = GetNetwork()?.CreateTheater(theaterName, country, city, street);
        if (theater is null) return null;

        _context.Theaters.Add(theater);
        _context.SaveChanges();

        return theater;
    }
}
