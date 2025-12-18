using AutoMapper;
using Project.DTO;
using Microsoft.EntityFrameworkCore;
using Project.Domain;
using Project.Model;

namespace Project.Services;

public class MovieService : BaseService<Movie, int, MovieDto>, IMovieService
{
    public MovieService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public override async Task<IEnumerable<MovieDto>> GetAllAsync()
    {
        var movies = await _dbSet
            .Include(m => m.Author)
            .ToListAsync();
        
        return _mapper.Map<IEnumerable<MovieDto>>(movies);
    }

    public override async Task<MovieDto?> GetByIdAsync(int id)
    {
        var movie = await _dbSet
            .Include(m => m.Author)
            .FirstOrDefaultAsync(m => m.Id == id);
        
        return movie == null ? null : _mapper.Map<MovieDto>(movie);
    }

    public async Task<IEnumerable<MovieDto>> GetMoviesByGenreAsync(Genre genre)
    {
        var movies = await _dbSet
            .Include(m => m.Author)
            .Where(m => m.Genre == genre)
            .ToListAsync();
        
        return _mapper.Map<IEnumerable<MovieDto>>(movies);
    }

    public async Task<IEnumerable<MovieDto>> GetMoviesByAuthorAsync(int authorId)
    {
        var movies = await _dbSet
            .Include(m => m.Author)
            .Where(m => m.Author.Id == authorId)
            .ToListAsync();
        
        return _mapper.Map<IEnumerable<MovieDto>>(movies);
    }

    public async Task<IEnumerable<MovieDto>> SearchByTitleAsync(string title)
    {
        var movies = await _dbSet
            .Include(m => m.Author)
            .Where(m => m.Title.Contains(title))
            .ToListAsync();
        
        return _mapper.Map<IEnumerable<MovieDto>>(movies);
    }

    public override async Task<MovieDto> CreateAsync(MovieDto dto)
    {
        var movie = _mapper.Map<Movie>(dto);
        
        // Load the Author from the database using AuthorId
        var author = await _context.Set<Author>().FindAsync(dto.AuthorId);
        if (author == null)
            throw new InvalidOperationException($"Author with ID {dto.AuthorId} not found.");
        
        movie.Author = author;
        movie.CreatedAt = DateTime.UtcNow;
        movie.UpdatedAt = DateTime.UtcNow;
        
        await _dbSet.AddAsync(movie);
        await _context.SaveChangesAsync();
        
        return _mapper.Map<MovieDto>(movie);
    }

    public override async Task<MovieDto?> UpdateAsync(int id, MovieDto dto)
    {
        var movie = await _dbSet
            .Include(m => m.Author)
            .FirstOrDefaultAsync(m => m.Id == id);
        
        if (movie == null)
            return null;

        // Map the DTO to the existing entity
        _mapper.Map(dto, movie);
        
        // If AuthorId changed, load the new Author
        if (movie.Author.Id != dto.AuthorId)
        {
            var author = await _context.Set<Author>().FindAsync(dto.AuthorId);
            if (author == null)
                throw new InvalidOperationException($"Author with ID {dto.AuthorId} not found.");
            
            movie.Author = author;
        }
        
        movie.UpdatedAt = DateTime.UtcNow;
        
        _context.Entry(movie).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        
        return _mapper.Map<MovieDto>(movie);
    }
}

