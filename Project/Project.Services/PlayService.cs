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

    public List<Play> GetPlaysWithoutAuthor(Author author)
    {
        return _context.Plays
            .AsSplitQuery()
            .Include(p => p.Author)
            .Include(p => p.Director)
            .Where(p => p.Author != author)
            .ToList();
    }

    public List<Play> GetPlaysWithoutDirector(Director director)
    {
        return _context.Plays
            .AsSplitQuery()
            .Include(p => p.Author)
            .Include(p => p.Director)
            .Where(p => p.Director != director)
            .ToList();
    }

    public List<Play> GetPlaysWithoutActor(Actor actor)
    {
        return _context.Plays
            .AsSplitQuery()
            .Include(p => p.Author)
            .Include(p => p.Director)
            .Where(p => !p.Actors.Contains(actor))
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
