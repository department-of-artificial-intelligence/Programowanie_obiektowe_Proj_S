using RatingSystem.DAL;
using RatingSystem.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RatingSystem.BLL
{
    public class RatingSorter
    {
        private readonly IRatingDataLogic _ratingDataLogic;
        public RatingSorter(IRatingDataLogic ratingDataLogic)
        {
            _ratingDataLogic = ratingDataLogic;
        }
        public async Task<IEnumerable<Rating>> GetServiceRatingsSortedByDateAsync(int serviceId, bool descending = false)
        {
            var ratings = await _ratingDataLogic.GetByServiceIdAsync(serviceId);
            return descending ? ratings.OrderByDescending(r => r.Created) : ratings.OrderBy(r=>r.Created);
        }
        public async Task<IEnumerable<Rating>> GetServiceRatingsSortedByScoreAsync(int serviceId, bool descending = false)
        {
            var ratings = await _ratingDataLogic.GetByServiceIdAsync(serviceId);

            return descending ? ratings.OrderByDescending(r => r.Value): ratings.OrderBy(r => r.Value);
        }

    }
}
