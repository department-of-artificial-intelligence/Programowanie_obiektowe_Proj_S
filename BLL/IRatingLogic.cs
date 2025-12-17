using System;
using System.Collections.Generic;
using System.Text;
using RatingSystem.Domain;
namespace BLL
{
    public interface IRatigLogic
    {
        Task SubmitRatingAsync(int userId, int serviceId, int ratingPoints, string? comment);

        Task DeleteRatingAsync(int ratingId, int requestUserId);
        Task<double> GetAverageRatingAsync(int serviceId);
        Task<IEnumerable<Rating>> GetUserRatingAsync(int UserId);
    }
}
