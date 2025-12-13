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
}
