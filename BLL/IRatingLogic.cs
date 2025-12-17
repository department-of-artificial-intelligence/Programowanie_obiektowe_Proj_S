
using RatingSystem.Domain;
namespace RatingSystem.BLL
{
    public interface IRatingLogic
    {
        Task SubmitRatingAsync(int userId, int serviceId, int ratingPoints, string? comment);

        Task DeleteRatingAsync(int ratingId, int requestUserId);
        Task<double> GetAverageRatingAsync(int serviceId);
        
        Task<IEnumerable<Rating>> GetUserRatingAsync(int UserId);
    }
}
