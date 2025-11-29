using Project.Interfaces;

namespace Project.Services.Common
{
    public static class RatableService
    {
        public static List<T> SortByRating<T>(List<T> entities) where T : IRatable
        {
            return [.. entities.OrderByDescending(e => e.Rating)];
        }

        public static List<T> SortByPopularity<T>(List<T> entities) where T : IRatable
        {
            return [.. entities.OrderByDescending(e => e.TotalRatings)];
        }
    }
}