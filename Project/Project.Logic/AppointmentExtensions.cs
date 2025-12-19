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
        
        public static IEnumerable<Appointment>
            ByAnimal(this IEnumerable<Appointment> appointments, int animalId)
        {
            return appointments.Where(a => a.AnimalId == animalId);
        }

     
        public static IEnumerable<Appointment>
            ByVeterinarian(this IEnumerable<Appointment> appointments, int veterinarianId)
        {
            return appointments.Where(a => a.VeterinarianId == veterinarianId);
        }

        
        public static IEnumerable<Appointment>
            SortByDate(this IEnumerable<Appointment> appointments)
        {
            return appointments.OrderBy(a => a.Date);
        }

       
        public static IEnumerable<(int VeterinarianId, int AppointmentCount)>
            AppointmentsCountPerVeterinarian(this IEnumerable<Appointment> appointments)
        {
            return appointments
                .GroupBy(a => a.VeterinarianId)
                .Select(g => (VeterinarianId: g.Key, AppointmentCount: g.Count()));
        }
    }
}
