using AutoMapper;
using Project.DTO;
using Microsoft.EntityFrameworkCore;
using Project.Domain;
using Project.Model;

namespace Project.Services;

public class ReviewService : BaseService<Review, int, ReviewDto>, IReviewService
{
    public ReviewService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public override async Task<IEnumerable<ReviewDto>> GetAllAsync()
    {
        var reviews = await _dbSet
            .Include(r => r.User)
            .Include(r => r.Movie)
            .ThenInclude(m => m.Author)
            .ToListAsync();
        
        return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
    }

    public override async Task<ReviewDto?> GetByIdAsync(int id)
    {
        var review = await _dbSet
            .Include(r => r.User)
            .Include(r => r.Movie)
            .ThenInclude(m => m.Author)
            .FirstOrDefaultAsync(r => r.Id == id);
        
        return review == null ? null : _mapper.Map<ReviewDto>(review);
    }

    public async Task<IEnumerable<ReviewDto>> GetReviewsByMovieAsync(int movieId)
    {
        var reviews = await _dbSet
            .Include(r => r.User)
            .Include(r => r.Movie)
            .ThenInclude(m => m.Author)
            .Where(r => r.Movie.Id == movieId)
            .ToListAsync();
        
        return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
    }

    public async Task<IEnumerable<ReviewDto>> GetReviewsByUserAsync(int userId)
    {
        var reviews = await _dbSet
            .Include(r => r.User)
            .Include(r => r.Movie)
            .ThenInclude(m => m.Author)
            .Where(r => r.User.Id == userId)
            .ToListAsync();
        
        return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
    }

    public async Task<double> GetAverageRatingForMovieAsync(int movieId)
    {
        var reviews = await _dbSet
            .Where(r => r.Movie.Id == movieId)
            .ToListAsync();
        
        return reviews.Any() ? reviews.Average(r => r.Rate) : 0;
    }
}

