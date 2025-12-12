using System;
using System.Collections.Generic;
using System.Text;
using RatingSystem.Domain;
namespace RatingSystem.Logic
{
    public  interface IRatingService 
    {
        Task SubmitRating(Rating newRating);
        Task<IEnumerable<Rating>> GetRatings(int ServiceId);
        Task<float> CalculateAvgRatingAsync(int ServiceId);

    }
}
