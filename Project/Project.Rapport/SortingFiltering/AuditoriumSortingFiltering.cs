using Project.Models;

namespace Project.Logic.SortingFiltering
{
    public static class AuditoriumSortingFiltering
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
    }
}