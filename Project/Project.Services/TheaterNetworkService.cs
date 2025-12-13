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
}
