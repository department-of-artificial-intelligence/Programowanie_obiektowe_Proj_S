using Project.Entities;

namespace Project.Logic.SortingFiltering
{
    public static class GenericSorting
    {
        public static List<T> SortByTimeNewest<T>(List<T> entities) where T : BaseEntity
        {
            return [.. entities.OrderByDescending(e => e.CreatedAt)];
        }

        public static List<T> SortByTimeOldest<T>(List<T> entities) where T : BaseEntity
        {
            return [.. entities.OrderBy(e => e.CreatedAt)];
        }
    }
}