using AutoMapper;
using Project.DTO;
using Microsoft.EntityFrameworkCore;
using Project.Domain;
using Project.Model;

namespace Project.Services;

public class MovieMarkService : BaseService<MovieMark, int, MovieMarkDto>, IMovieMarkService
{
    public MovieMarkService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public override async Task<IEnumerable<MovieMarkDto>> GetAllAsync()
    {
        var movieMarks = await _dbSet
            .Include(mm => mm.User)
            .Include(mm => mm.Movie)
            .ThenInclude(m => m.Author)
            .ToListAsync();
        
        return _mapper.Map<IEnumerable<MovieMarkDto>>(movieMarks);
    }

    public override async Task<MovieMarkDto?> GetByIdAsync(int id)
    {
        var movieMark = await _dbSet
            .Include(mm => mm.User)
            .Include(mm => mm.Movie)
            .ThenInclude(m => m.Author)
            .FirstOrDefaultAsync(mm => mm.Id == id);
        
        return movieMark == null ? null : _mapper.Map<MovieMarkDto>(movieMark);
    }

    public async Task<IEnumerable<MovieMarkDto>> GetMovieMarksByUserAsync(int userId)
    {
        var movieMarks = await _dbSet
            .Include(mm => mm.User)
            .Include(mm => mm.Movie)
            .ThenInclude(m => m.Author)
            .Where(mm => mm.User.Id == userId)
            .ToListAsync();
        
        return _mapper.Map<IEnumerable<MovieMarkDto>>(movieMarks);
    }

    public async Task<IEnumerable<MovieMarkDto>> GetMovieMarksByTypeAsync(int userId, MovieMarkType type)
    {
        var movieMarks = await _dbSet
            .Include(mm => mm.User)
            .Include(mm => mm.Movie)
            .ThenInclude(m => m.Author)
            .Where(mm => mm.User.Id == userId && mm.Type == type)
            .ToListAsync();
        
        return _mapper.Map<IEnumerable<MovieMarkDto>>(movieMarks);
    }

    public async Task<MovieMarkDto?> GetUserMovieMarkAsync(int userId, int movieId)
    {
        var movieMark = await _dbSet
            .Include(mm => mm.User)
            .Include(mm => mm.Movie)
            .ThenInclude(m => m.Author)
            .FirstOrDefaultAsync(mm => mm.User.Id == userId && mm.Movie.Id == movieId);
        
        return movieMark == null ? null : _mapper.Map<MovieMarkDto>(movieMark);
    }

    public override async Task<MovieMarkDto> CreateAsync(MovieMarkDto dto)
    {
        var movieMark = _mapper.Map<MovieMark>(dto);
        
        // Load User and Movie from database using their IDs
        var user = await _context.Set<User>().FindAsync(dto.UserId);
        if (user == null)
            throw new InvalidOperationException($"User with ID {dto.UserId} not found.");
        
        var movie = await _context.Set<Movie>().Include(m => m.Author).FirstOrDefaultAsync(m => m.Id == dto.MovieId);
        if (movie == null)
            throw new InvalidOperationException($"Movie with ID {dto.MovieId} not found.");
        
        movieMark.User = user;
        movieMark.Movie = movie;
        movieMark.CreatedAt = DateTime.UtcNow;
        movieMark.UpdatedAt = DateTime.UtcNow;
        
        await _dbSet.AddAsync(movieMark);
        await _context.SaveChangesAsync();
        
        return _mapper.Map<MovieMarkDto>(movieMark);
    }

    public override async Task<MovieMarkDto?> UpdateAsync(int id, MovieMarkDto dto)
    {
        var movieMark = await _dbSet
            .Include(mm => mm.User)
            .Include(mm => mm.Movie)
            .ThenInclude(m => m.Author)
            .FirstOrDefaultAsync(mm => mm.Id == id);
        
        if (movieMark == null)
            return null;

        // Update the type
        movieMark.Type = dto.Type;
        movieMark.UpdatedAt = DateTime.UtcNow;
        
        _context.Entry(movieMark).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        
        return _mapper.Map<MovieMarkDto>(movieMark);
    }
}

