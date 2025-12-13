using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Model;

namespace Project.Services;

public class AuthorService
{
    private readonly ApplicationDbContext _context;

    public AuthorService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Author> GetAllAuthors()
    {
        return _context.Authors
            .Include(a => a.Plays)
            .ToList();
    }
}
