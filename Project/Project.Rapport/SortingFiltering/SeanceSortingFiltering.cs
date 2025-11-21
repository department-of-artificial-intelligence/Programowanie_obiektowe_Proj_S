using Project.Models;

namespace Project.Logic.SortingFiltering
{
    public static class SeanceSortingFiltering
    {
        public static List<Seance> FilterSeancesByFilmId(List<Seance> seances, string filmId)
        {
            return [.. seances.Where(s => s.FilmId == filmId)];
        }

        public static List<Seance> FilterSeancesByAuditoriumId(List<Seance> seances, string auditoriumId)
        {
            return [.. seances.Where(s => s.AuditoriumId == auditoriumId)];
        }

        public static List<Seance> SortSeancesByStartTime(List<Seance> seances)
        {
            return [.. seances.OrderBy(s => s.StartTime)];
        }

        public static List<Seance> SortSeancesByPrice(List<Seance> seances)
        {
            return [.. seances.OrderBy(s => s.Price)];
        }

        public static List<Seance> SortSeancesByOccupiedSeats(List<Seance> seances, List<Auditorium> auditoriums)
        {
            return [.. seances.OrderByDescending(s =>
            {
                var auditorium = auditoriums.FirstOrDefault(a => a.Id == s.AuditoriumId);
                return auditorium != null ? s.OccupiedSeatIds.Count / (double)auditorium.Capacity : 0;
            })];
        }
    }
}