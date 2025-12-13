using Project.Models;
using Project.DAL;

namespace Project.Services
{
    public static class ActorService
    {
        public static List<Actor> GetAll(ApplicationDBContext context)
        {
            return [.. context.Actors];
        }

        public static Actor? GetById(ApplicationDBContext context, string id)
        {
            return context.Actors.FirstOrDefault(a => a.Id == id);
        }

        public static Actor Add(ApplicationDBContext context, string firstName, string lastName, string nationality, DateTime birthDate, 
                                string profileImageUrl, string biography, double popularity)
        {
            Actor actor = new(firstName, lastName, nationality, birthDate, profileImageUrl, biography, popularity);

            context.Actors.Add(actor);
            context.SaveChanges();

            return actor;
        }

        public static void Update(ApplicationDBContext context, Actor actor)
        {
            context.Actors.Update(actor);
            context.SaveChanges();
        }

        public static void Delete(ApplicationDBContext context, string actorId)
        {
            var actor = context.Actors.FirstOrDefault(a => a.Id == actorId);

            if (actor != null)
            {
                var filmsWithActor = context.Films.AsEnumerable().Where(f => f.ActorIds.Contains(actorId)).ToList();

                foreach (var film in filmsWithActor)
                {
                    film.RemoveItem(actorId);
                    context.Films.Update(film);
                }

                context.Actors.Remove(actor);
                context.SaveChanges();
            }
        }

        public static List<Actor> FilterByLastName(ApplicationDBContext context, string lastName)
        {
            return [.. context.Actors.Where(a => a.LastName.Contains(lastName))];
        }

        public static List<Actor> SortByPopularity(ApplicationDBContext context)
        {
            return [.. context.Actors.OrderByDescending(a => a.Popularity)];
        }

        public static List<Film> GetFilmsWithActor(ApplicationDBContext context, string actorId)
        {
            return [.. context.Films.AsEnumerable().Where(f => f.ActorIds.Contains(actorId))];
        }
    }
}