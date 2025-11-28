using Project.Models;

namespace Project.Services.SortingFiltering
{
    public static class ActorSortingFiltering
    {
        public static List<Actor> FilterActorsByLastName(List<Actor> actors, string lastName)
        {
            return [.. actors.Where(a => a.LastName.Contains(lastName, StringComparison.OrdinalIgnoreCase))];
        }

        public static List<Actor> SortActorsByPopularity(List<Actor> actors)
        {
            return [.. actors.OrderByDescending(a => a.Popularity)];
        }
    }
}