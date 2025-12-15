using System.Collections.Generic;
using System.Linq;

namespace Project.Model
{
    public class Tutor : User
    {
        public decimal HourlyRate { get; set; }
        public List<Subject> Specialties { get; set; } = new(); //relacja do subject
        public List<TimeSlot> Availability { get; set; } = new(); //relacja do TimeSlot

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