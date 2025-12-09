using System.Collections.Generic;
using System.Linq;

namespace Project.Model
{
    public class Tutor : User
    {
        public decimal HourlyRate { get; set; }
        public List<Subject> Specialties { get; set; } = new();
        public List<TimeSlot> Availability { get; set; } = new();

        public Tutor(string firstname, string lastname, string email, decimal hourlyRate)
            : base(firstname, lastname, email)
        {
            HourlyRate = hourlyRate;
        }

        public Tutor() { }

        public override string ToString()
            => $"{base.ToString()}, Stawka: {HourlyRate:C}/h";
    }
}