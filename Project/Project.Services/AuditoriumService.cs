using Project.Models;

namespace Project.Services
{
    public class AuditoriumService
    {
        public static List<Auditorium> FilterAuditoriumsByName(List<Auditorium> auditoriums, string name)
        {
            return [.. auditoriums.Where(a => a.Name.Contains(name, StringComparison.OrdinalIgnoreCase))];
        }

        public static List<Auditorium> FilterAuditoriumsByFeature(List<Auditorium> auditoriums, string feature)
        {
            return [.. auditoriums.Where(a => a.Items.Any(f => f.Contains(feature, StringComparison.OrdinalIgnoreCase)))];
        }

        public static List<Auditorium> SortAuditoriumsByFeatures(List<Auditorium> auditoriums)
        {
            return [.. auditoriums.OrderByDescending(a => a.Items.Count)];
        }

        public static List<Auditorium> SortAuditoriumsByMaxCapacity(List<Auditorium> auditoriums)
        {
            return [.. auditoriums.OrderByDescending(a => a.Capacity)];
        }

        public static void DeleteAuditorium(List<Auditorium> auditoriums, List<Seance> seances,
                                            List<Reservation> reservations, List<Ticket> tickets, string auditoriumId)
        {
            var seancesToDelete = seances.Where(s => s.AuditoriumId == auditoriumId).ToList();

            foreach (var seance in seancesToDelete)
            {
                SeanceService.DeleteSeance(seances, reservations, tickets, seance.Id);
            }

            var auditorium = auditoriums.FirstOrDefault(a => a.Id == auditoriumId);
            if (auditorium != null)
            {
                auditoriums.Remove(auditorium);
            }
        }
    }
}
