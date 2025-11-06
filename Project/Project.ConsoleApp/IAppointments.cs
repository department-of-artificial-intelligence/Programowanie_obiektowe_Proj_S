using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ConsoleApp
{
    public interface IAppointments
    {
        void AddAppointment(Appointment appointment);
        void RemoveAppointment(Appointment appointment);
        List<Appointment> GetAppointmentsByDate(DateTime date);
    }
}
