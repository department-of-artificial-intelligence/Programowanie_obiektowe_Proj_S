using DAL;
using System;
using System.Collections.Generic;
using System.Text;
using RatingSystem.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage.Json;
namespace BLL
{
    public class RatingLogic: IRatigLogic
    {
        private readonly IRatingDataLogic _ratingDataLogic;
        private readonly IUserLogic _userLogic;
        private readonly IServiceLogic _serviceLogic;
        RatingLogic(IRatingDataLogic ratingDataLogic, IUserLogic userLogic, IServiceLogic seviceLogic)
        {
            _ratingDataLogic = ratingDataLogic;
            _userLogic = userLogic;
            _serviceLogic = seviceLogic;
        }
        public async Task SubmitRatingAsync( int userId, int serviceId, int value, string comment)
        {
            if(value<1 || value > 5)
            {
                throw new Exception($"Value must be 1<=value<=5");
            }
            User? user = await _userLogic.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new Exception($"Can't find user:{userId} ");
            }
            Service? service = await _serviceLogic.GetServiceByIdAsync(serviceId);
            if (service == null)
            {
                throw new Exception($"service:{serviceId} doesn't exist");
            }
            var newRating = new Rating(userId,serviceId,value,comment?? string.Empty);

            await _ratingDataLogic.AddAsync(newRating);
            await _ratingDataLogic.SaveChangesAsync();
        }
        public async Task<IEnumerable<Rating>> GetUserRatingAsync(int userId)
        {
            var user = await _userLogic.GetUserByIdAsync(userId);
            if(user == null)
            {
                throw new Exception($"can't find user:{userId}");
            }
            var ratings= await _ratingDataLogic.GetByUserIdAsync(userId);
            return ratings.OrderByDescending(r => r.Date);
        }
        public async Task DeleteRatingAsync(int ratingId, int requestingUserId)
        {
            var rating =await _ratingDataLogic.GetByIdAsync(ratingId);
            if(rating == null)
            {
                throw new Exception("Rating couldn't be found");
            }
            if(rating.UserId == requestingUserId)
            {
                throw new UnauthorizedAccessException("You cannot delete, someones else rating");
            }
            await _ratingDataLogic.RemoveAsync(rating);
            await _ratingDataLogic.SaveChangesAsync();
        }
        public async Task<double> GetAverageRatingAsync(int serviceId)
        {
            var ratings = await _ratingDataLogic.GetByServiceIdAsync(serviceId);
            if (!ratings.Any())
            {
                return 0.0;
            }
            return ratings.Average(r => r.Value);
        }

    }
}
