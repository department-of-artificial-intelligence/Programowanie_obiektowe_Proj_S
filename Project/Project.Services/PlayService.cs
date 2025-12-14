using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Model;

namespace Project.Services;

public class PlayService
{
    private readonly ApplicationDbContext _context;

    public PlayService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Play> GetPlays()
    {
        return _context.Plays
            .AsSplitQuery()
            .Include(p => p.Author)
            .Include(p => p.Director)
            .ToList();
    }

    public List<Play> GetPlaysWithActors()
    {
        return _context.Plays
            .AsSplitQuery()
            .Include(p => p.Author)
            .Include(p => p.Director)
            .Include(p => p.Actors)
            .ToList();
    }

    public bool AddNewPlay(string title, Author? author = null, Director? director = null)
    {
        if (string.IsNullOrWhiteSpace(title)) return false;

        Play play = new Play(title, author, director);

        _context.Plays.Add(play);
        _context.SaveChanges();

        return true;
    }
}
