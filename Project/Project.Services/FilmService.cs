using Project.Models;

namespace Project.Services
{
    public class FilmService
    {
        public static List<Film> FilterFilmsByDuration(List<Film> films, uint minDuration, uint maxDuration = uint.MaxValue)
        {
            return [.. films.Where(f => f.DurationMinutes >= minDuration && f.DurationMinutes <= maxDuration)];
        }

        public static List<Film> FilterFilmsByGenre(List<Film> films, string genre)
        {
            return [.. films.Where(f => f.Genre.Contains(genre, StringComparison.OrdinalIgnoreCase))];
        }

        public static List<Film> FilterFilmsByAgeRestriction(List<Film> films, bool hasAgeRestriction)
        {
            return [.. films.Where(f => f.HasAgeRestriction == hasAgeRestriction)];
        }

        public static List<Film> FilterFilmsByDirector(List<Film> films, string director)
        {
            return [.. films.Where(f => f.Director.Contains(director, StringComparison.OrdinalIgnoreCase))];
        }

        public static List<Film> FilterFilmsByTitle(List<Film> films, string title)
        {
            return [.. films.Where(f => f.Title.Contains(title, StringComparison.OrdinalIgnoreCase))];
        }

        public static List<Film> SortFilmsByNumberOfActors(List<Film> films)
        {
            return [.. films.OrderByDescending(f => f.Items.Count)];
        }

        public static List<Film> FilterFilmsWhereActorIs(List<Film> films, string actorId)
        {
            return [.. films.Where(f => f.Items.Contains(actorId))];
        }

        public static void DeleteFilm(List<Film> films, List<Cinema> cinemas, List<Seance> seances,
                                      List<Reservation> reservations, List<Ticket> tickets, string filmId)
        {
            foreach (var cinema in cinemas)
            {
                cinema.RemoveItem(filmId);
            }

            var seancesToDelete = seances.Where(s => s.FilmId == filmId).ToList();

            foreach (var seance in seancesToDelete)
            {
                SeanceService.DeleteSeance(seances, reservations, tickets, seance.Id);
            }

            var film = films.FirstOrDefault(f => f.Id == filmId);
            if (film != null)
            {
                films.Remove(film);
            }
        }
    }
}
