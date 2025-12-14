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

    public List<Author> GetAuthorsWithPlays()
    {
        return _context.Authors
            .Include(a => a.Plays)
                .ThenInclude(p => p.Director)
            .ToList();
    }

    public bool AddNewAuthor(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName)) return false;

        Author author = new Author(firstName, lastName);

        _context.Authors.Add(author);
        _context.SaveChanges();

        return true;
    }

    public bool AddPlay(Author author, Play play)
    {
        if (author is null || play is null) return false;
        
        if (author.AddPlay(play))
        {
            _context.SaveChanges();
            return true;
        }

        return false;
    }
    public bool RemovePlay(Author author, Play play)
    {
        if (author is null || play is null) return false;

        if (author.RemovePlay(play))
        {
            _context.SaveChanges();
            return true;
        }

        return false;
    }
    public bool RemoveAllPlays(Author author)
    {
        if (author is null) return false;

        if (author.RemoveAllPlays())
        {
            _context.SaveChanges();
            return true;
        }

        return false;
    }
}
