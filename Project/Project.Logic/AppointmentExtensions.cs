using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;

namespace Project.Logic
{
    public static class AppointmentExtensions
    {
        public static IEnumerable<Appointment> Upcoming(this IEnumerable<Appointment> appointments, DateTime from)
            => appointments?.Where(a => a.Date >= from).OrderBy(a => a.Date) ?? Enumerable.Empty<Appointment>();

        public static IEnumerable<Appointment> Past(this IEnumerable<Appointment> appointments, DateTime before)
            => appointments?.Where(a => a.Date < before).OrderByDescending(a => a.Date) ?? Enumerable.Empty<Appointment>();

        public static IEnumerable<Appointment> ByVeterinarian(this IEnumerable<Appointment> appointments, int veterinarianId)
            => appointments?.Where(a => a.VeterinarianId == veterinarianId) ?? Enumerable.Empty<Appointment>();
    }
}
