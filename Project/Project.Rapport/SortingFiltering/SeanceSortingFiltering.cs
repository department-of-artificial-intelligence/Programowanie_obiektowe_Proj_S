using Project.Models;
using System.Collections.Generic;
using System.Linq;

namespace Project.Logic.SortingFiltering
{
    public static class SeanceSortingFiltering
    {
        public static List<Seance> FilterSeancesByFilmId(List<Seance> seances, string filmId)
        {
            if (seances == null || string.IsNullOrEmpty(filmId))
                return [];

            return [.. seances.Where(s => s.FilmId == filmId)];
        }

        public static List<Seance> FilterSeancesByAuditoriumId(List<Seance> seances, string auditoriumId)
        {
            if (seances == null || string.IsNullOrEmpty(auditoriumId))
                return [];

            return [.. seances.Where(s => s.AuditoriumId == auditoriumId)];
        }

        public static List<Seance> SortSeancesByStartTime(List<Seance> seances)
        {
            if (seances == null) return [];
            return [.. seances.OrderBy(s => s.StartTime)];
        }

        public static List<Seance> SortSeancesByPrice(List<Seance> seances)
        {
            if (seances == null) return [];
            return [.. seances.OrderBy(s => s.Price)];
        }

        public static List<Seance> SortSeancesByOccupiedSeats(List<Seance> seances, List<Auditorium> auditoriums)
        {
            if (seances == null || auditoriums == null) return [];

            return [.. seances.OrderByDescending(s =>
            {
                var auditorium = auditoriums.FirstOrDefault(a => a.Id == s.AuditoriumId);
                return auditorium != null && auditorium.Capacity > 0
                    ? s.OccupiedSeatIds.Count / (double)auditorium.Capacity
                    : 0;
            })];
        }
    }
}
