using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Tutor : User
    {
        public decimal HourlyRate {  get; set; }
        public List<Subject> Specialties { get; set; } = new List<Subject>();
        public List<TimeSlot> Availability {  get; set; } = new List<TimeSlot>();



        public Tutor(int id, string firstname, string lastname, string email, decimal hourlyRate)
            : base(id, firstname, lastname, email)
        {
            HourlyRate=hourlyRate;
        }
        public Tutor()
            : base(0, string.Empty, string.Empty, string.Empty)
        {
            HourlyRate = 0m;
            Specialties = new List<Subject>();
            Availability = new List<TimeSlot>();
        }

        public void AddSpecialty(Subject subject)=>Specialties.Add(subject);

        public override string ToString() 
            => $"{base.ToString()}, Stawka: {HourlyRate:C}/h, Specjalizacje: {string.Join(", ", Specialties.Select(s => s.Name))}";
    }
    
}
