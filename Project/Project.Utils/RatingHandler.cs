namespace Project.Utils
{
    public static class RatingCalculator
    {
        public static double CalculateNewRating(uint currentTotalRatings, double currentRating, uint newRating)
        {
            return ((currentRating * currentTotalRatings) + newRating) / (currentTotalRatings + 1);
        }
    }
}