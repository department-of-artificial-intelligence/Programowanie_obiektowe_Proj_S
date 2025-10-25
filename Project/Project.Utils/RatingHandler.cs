using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Utils
{
    public static class RatingHandler
    {
        public static double CalculateRating(uint customersRated, double currentRating, uint mark)
        {
            return ((currentRating * customersRated) + mark) / (customersRated + 1);
        }
    }
}
