namespace Project.Utils
{
    public static class CollectionHelper
    {
        public static bool AddUniqueItem<T>(List<T> collection, T item, int? maxItems = null)
        {
            if (collection.Contains(item) || (maxItems.HasValue && collection.Count >= maxItems)) return false;

            collection.Add(item);
            return true;
        }

        public static bool RemoveItem<T>(List<T> collection, T item)
        {
            return collection.Remove(item);
        }

        public static string ToString<T>(IEnumerable<T> collection, string separator = ", ")
        {
            return string.Join(separator, collection);
        }
    }
}