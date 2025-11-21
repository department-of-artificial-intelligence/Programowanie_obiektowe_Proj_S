using Project.Models;

namespace Project.Logic.SortingFiltering
{
    public static class CinemaSortingFiltering
    {
        public static List<Cinema> SortByNumberOfAvailableFilms(List<Cinema> cinemas)
        {
            return [.. cinemas.OrderByDescending(c => c.Items.Count)];
        }

        public static List<Cinema> FilterCinemasByName(List<Cinema> cinemas, string name)
        {
            return [.. cinemas.Where(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase))];
        }

        public static List<Cinema> FilterCinemasWhereFilmAvailable(List<Cinema> cinemas, string filmId)
        {
            return [.. cinemas.Where(c => c.Items.Contains(filmId))];
        }
    }
}