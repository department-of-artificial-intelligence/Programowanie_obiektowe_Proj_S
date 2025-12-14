using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Model;
using System.IO;

namespace Project.Services;

public class DirectorService
{
    private readonly ApplicationDbContext _context;

    public DirectorService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Director> GetDirectorsWithPlays()
    {
        return _context.Directors
            .Include(d => d.Plays)
                .ThenInclude(p => p.Author)
            .ToList();
    }

    public bool AddNewDirector(string firstName, string lastName, int yearsOfExperience, decimal salary)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || yearsOfExperience < 0 || salary < 0) return false;

        Director director = new Director(firstName, lastName, yearsOfExperience,salary);

        _context.Directors.Add(director);
        _context.SaveChanges();

        return true;
    }

    public bool AddPlay(Director director, Play play)
    {
        if (director is null || play is null) return false;

        if (director.AddPlay(play))
        {
            _context.SaveChanges();
            return true;
        }

        return false;
    }
    public bool RemovePlay(Director director, Play play)
    {
        if (director is null || play is null) return false;

        if (director.RemovePlay(play))
        {
            _context.SaveChanges();
            return true;
        }

        return false;
    }
    public bool RemoveAllPlays(Director director)
    {
        if (director is null) return false;

        if (director.RemoveAllPlays())
        {
            _context.SaveChanges();
            return true;
        }

        return false;
    }
}
