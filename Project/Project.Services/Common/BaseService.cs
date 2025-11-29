using Project.Models.Common;

namespace Project.Services.Common
{
    public static class BaseService
    {
        public static List<T> SortByTimeNewest<T>(List<T> entities) where T : Base
        {
            return [.. entities.OrderByDescending(e => e.CreatedAt)];
        }

        public static List<T> SortByTimeOldest<T>(List<T> entities) where T : Base
        {
            return [.. entities.OrderBy(e => e.CreatedAt)];
        }
    }
}