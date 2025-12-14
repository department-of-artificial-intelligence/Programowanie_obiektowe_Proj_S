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

    public List<Actor> GetActorsWithPlays()
    {
        return _context.Actors
            .AsSplitQuery()
            .Include(a => a.Plays)
                .ThenInclude(p => p.Author)
            .Include(a => a.Plays)
                .ThenInclude(p => p.Director)
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

    public bool AddPlay(Actor actor, Play play)
    {
        if (actor is null || play is null) return false;

        if (actor.AddPlay(play))
        {
            _context.SaveChanges();
            return true;
        }

        return false;
    }
    public bool RemovePlay(Actor actor, Play play)
    {
        if (actor is null || play is null) return false;

        if (actor.RemovePlay(play))
        {
            _context.SaveChanges();
            return true;
        }

        return false;
    }
    public bool RemoveAllPlays(Actor actor)
    {
        if (actor is null) return false;

        if (actor.RemoveAllPlays())
        {
            _context.SaveChanges();
            return true;
        }

        return false;
    }
}
