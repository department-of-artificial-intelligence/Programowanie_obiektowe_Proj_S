using System;
using System.Collections.Generic;
using System.Text;

namespace RatingSystem.BLL
{
    public  static class RatingExtensions
    {
        public static string ToStars(this int score)
        {
            
            return new string('*', Math.Clamp(score, 0, 5));
        }

        public static string ToStars(this double score)
        {
            
            return ((int)Math.Round(score)).ToStars();
        }
    }
}
