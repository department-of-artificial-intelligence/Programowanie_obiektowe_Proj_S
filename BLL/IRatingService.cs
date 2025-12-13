using System;
using System.Collections.Generic;
using System.Text;
using RatingSystem.Domain;
using RatingSystem.DAL;
namespace RatingSystem.BLL
{
    public  interface IRatingService 
    {
        Task SubmitRating(Rating newRating);
        //Task<IEnumerable<Rating>> GetRatings(int ServiceId);
        Task<double> CalculateAvgRatingAsync(int ServiceId);

    }
}
