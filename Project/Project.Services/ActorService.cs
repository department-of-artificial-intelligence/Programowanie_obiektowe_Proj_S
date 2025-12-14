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

    public bool AddNewActor(string firstName, string lastName, decimal salary)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || salary < 0) return false;

        Actor actor = new Actor(firstName, lastName, salary);

        _context.Actors.Add(actor);
        _context.SaveChanges();

        return true;
    }
}
