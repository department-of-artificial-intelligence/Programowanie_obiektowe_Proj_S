using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Model;

namespace Project.Services;

public class DirectorService
{
    private readonly ApplicationDbContext _context;

    public DirectorService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Director> GetAllDirectors()
    {
        return _context.Directors
            .Include(d => d.Plays)
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
}
