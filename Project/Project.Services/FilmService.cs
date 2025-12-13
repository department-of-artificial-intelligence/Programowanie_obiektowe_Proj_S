using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Models;

namespace Project.Services
{
    public static class FilmService
    {
        public static List<Film> GetAll(ApplicationDBContext context)
        {
            return [.. context.Films];
        }

        public static Film? GetById(ApplicationDBContext context, string id)
        {
            return context.Films.FirstOrDefault(f => f.Id == id);
        }

        public static Film Add(ApplicationDBContext context, string title, string description, uint duration, string director, string genre, 
                               bool ageRestriction, string posterUrl, string trailerUrl)
        {
            var film = new Film(title, description, duration, director, genre, ageRestriction, posterUrl, trailerUrl);

            context.Films.Add(film);
            context.SaveChanges();

            return film;
        }

        public static void Update(ApplicationDBContext context, Film film)
        {
            context.Films.Update(film);
            context.SaveChanges();
        }

        public static void Delete(ApplicationDBContext context, string filmId)
        {
            var film = context.Films.FirstOrDefault(f => f.Id == filmId);

            if (film != null)
            {
                var cinemasWithFilm = context.Cinemas.AsEnumerable().Where(c => c.AvailableFilmIds.Contains(filmId)).ToList();

                foreach (var cinema in cinemasWithFilm)
                {
                    cinema.RemoveItem(filmId);
                    context.Cinemas.Update(cinema);
                }

                var seances = context.Seances
                    .Where(s => s.FilmId == filmId)
                    .ToList();

                foreach (var seance in seances)
                {
                    SeanceService.Delete(context, seance.Id);
                }

                context.Films.Remove(film);
                context.SaveChanges();
            }
        }

        public static List<Film> FilterByDuration(ApplicationDBContext context, uint minDuration, uint maxDuration = uint.MaxValue)
        {
            return [.. context.Films.Where(f => f.DurationMinutes >= minDuration && f.DurationMinutes <= maxDuration)];
        }

        public static List<Film> FilterByGenre(ApplicationDBContext context, string genre)
        {
            return [.. context.Films.Where(f => f.Genre.Contains(genre))];
        }

        public static List<Film> FilterByAgeRestriction(ApplicationDBContext context, bool hasAgeRestriction)
        {
            return [.. context.Films.Where(f => f.HasAgeRestriction == hasAgeRestriction)];
        }

        public static List<Film> FilterByDirector(ApplicationDBContext context, string director)
        {
            return [.. context.Films.Where(f => f.Director.Contains(director))];
        }

        public static List<Film> FilterByTitle(ApplicationDBContext context, string title)
        {
            return [.. context.Films.Where(f => f.Title.Contains(title))];
        }

        public static List<Film> SortByNumberOfActors(ApplicationDBContext context)
        {
            return [.. context.Films.AsEnumerable().OrderByDescending(f => f.ActorIds.Count)];
        }

        public static List<Cinema> GetCinemasWithFilm(ApplicationDBContext context, string filmId)
        {
            return [.. context.Cinemas.AsEnumerable().Where(c => c.AvailableFilmIds.Contains(filmId))];
        }
    }
}