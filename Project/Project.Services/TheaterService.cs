using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Model;

namespace Project.Services;

public class TheaterService
{
    private readonly ApplicationDbContext _context;

    public TheaterService(ApplicationDbContext context)
    {
        _context = context;
    }

    public TheaterNetwork? GetAllTheaters()
    {
        return _context.TheaterNetworks
                    .Include(t => t.Theaters)
                    .OrderBy(t => t.TheaterNetworkId)
                    .FirstOrDefault();
    }

    public Theater? GetTheaterById(int id)
    {
        return _context.Theaters.FirstOrDefault(t => t.TheaterId == id);
    }

    public Hall? CreateNewHall(string hallName, int theaterId)
    {
        if (hallName == string.Empty || theaterId < 1) return null;

        Hall? hall = GetTheaterById(theaterId)?.CreateHall(hallName);
        if (hall is null) return null;

        _context.Halls.Add(hall);
        _context.SaveChanges();

        return hall;
    }
}
