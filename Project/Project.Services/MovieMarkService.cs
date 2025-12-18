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
}

