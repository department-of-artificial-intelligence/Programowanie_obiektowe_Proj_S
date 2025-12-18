using Project.DTO;
using Project.Model;

namespace Project.Services;

public interface IReviewService : IBaseService<Review, int, ReviewDto>
{
    Task<IEnumerable<ReviewDto>> GetReviewsByMovieAsync(int movieId);
    Task<IEnumerable<ReviewDto>> GetReviewsByUserAsync(int userId);
    Task<double> GetAverageRatingForMovieAsync(int movieId);
}

