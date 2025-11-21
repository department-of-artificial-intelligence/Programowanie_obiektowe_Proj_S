using Project.Interfaces;

namespace Project.Logic.SortingFiltering
{
    public static class RatableSorting
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