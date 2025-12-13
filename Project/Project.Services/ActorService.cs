using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Model;

namespace Project.Services;

public class ActorService
{
    private readonly ApplicationDbContext _context;

    public ActorService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Actor> GetAllActors()
    {
        return _context.Actors
            .Include(a => a.Plays)
            .ToList();
    }
}
