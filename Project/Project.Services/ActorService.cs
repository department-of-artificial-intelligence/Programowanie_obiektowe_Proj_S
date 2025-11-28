using Project.Models;

namespace Project.Services
{
    public class ActorService
    {
        public static List<Actor> FilterActorsByLastName(List<Actor> actors, string lastName)
        {
            return [.. actors.Where(a => a.LastName.Contains(lastName, StringComparison.OrdinalIgnoreCase))];
        }

        public static List<Actor> SortActorsByPopularity(List<Actor> actors)
        {
            return [.. actors.OrderByDescending(a => a.Popularity)];
        }

        public static void DeleteActor(List<Actor> actors, List<Film> films, string actorId)
        {
            foreach (var film in films)
            {
                film.RemoveItem(actorId);
            }

            var actor = actors.FirstOrDefault(a => a.Id == actorId);

            if (actor != null)
            {
                actors.Remove(actor);
            }
        }
    }
}
