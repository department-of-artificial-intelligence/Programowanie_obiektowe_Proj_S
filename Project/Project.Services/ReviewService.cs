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

    public override async Task<ReviewDto> CreateAsync(ReviewDto dto)
    {
        var review = _mapper.Map<Review>(dto);
        
        // Load User and Movie from database using their IDs
        var user = await _context.Set<User>().FindAsync(dto.UserId);
        if (user == null)
            throw new InvalidOperationException($"User with ID {dto.UserId} not found.");
        
        var movie = await _context.Set<Movie>().Include(m => m.Author).FirstOrDefaultAsync(m => m.Id == dto.MovieId);
        if (movie == null)
            throw new InvalidOperationException($"Movie with ID {dto.MovieId} not found.");
        
        review.User = user;
        review.Movie = movie;
        review.CreatedAt = DateTime.UtcNow;
        review.UpdatedAt = DateTime.UtcNow;
        
        await _dbSet.AddAsync(review);
        await _context.SaveChangesAsync();
        
        return _mapper.Map<ReviewDto>(review);
    }

    public override async Task<ReviewDto?> UpdateAsync(int id, ReviewDto dto)
    {
        var review = await _dbSet
            .Include(r => r.User)
            .Include(r => r.Movie)
            .ThenInclude(m => m.Author)
            .FirstOrDefaultAsync(r => r.Id == id);
        
        if (review == null)
            return null;

        // Update simple properties
        review.Rate = dto.Rate;
        review.Comment = dto.Comment;
        review.UpdatedAt = DateTime.UtcNow;
        
        _context.Entry(review).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        
        return _mapper.Map<ReviewDto>(review);
    }
}

