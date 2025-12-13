using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using RatingSystem.Domain;
namespace RatingSystem.Logic
{
    public  class RatingService: IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IService _service;
        private readonly IUserService _userService;

        public RatingService(IRatingRepository RR, IUserService US, IService S)
        {
            _ratingRepository = RR;
            _userService = US;
            _service = S;
        }
        
        public async Task SubmitRating(Rating rating ) 
        {
           
        if(rating.Value<1 || rating.Value > 5)
            {
                throw new ArgumentOutOfRangeException("your grade must be between 1-5, if it happens again i will shut the console"); 

            }
            var service = await _service.GetServiceByIdAsync(rating.ServiceId);
            if (service == null)
            {
                throw new Exception("Rating System does not containt this service and probably is never going to, so try to check on google or smthng(");
            }
            var user = await _userService.GetUserByIdAsync(rating.UserId);
            if (user == null)
            {
                throw new Exception("user does not exist");
            }
            rating.Date = DateTime.Now;
            await _ratingRepository.AddAsync(rating);

        }
        public async Task<float> CalculateAvgRatingAsync(int serviceId)
        {
            if (serviceId <= 0) throw new ArgumentException("wrong id");
            var ratings = await _ratingRepository.GetByServiceIdAsync(serviceId);

            if (ratings == null || !ratings.Any()) { return 0.0f; }
            float average = ratings.Average(r => r.Value);
            return average;

        }
        public async Task<IEnumerable<Rating>> GetRatingsByServiceAsync(int serviceId)
        {
            return await _ratingRepository.GetByServiceIdAsync(serviceId);
        }
    }
}
