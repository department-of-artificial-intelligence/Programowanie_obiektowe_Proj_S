using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class TimeSlot{
        public int Id {  get; set; }
        public int TutorId { get; set; }
        public DateTime StartDateTime {  get; set; }
        public DateTime EndDateTime { get; set; }
        public bool IsBooked {  get; set; }=false;

        public TimeSlot(int id, int tutorId, DateTime startDateTime, DateTime endDateTime)
        {
            Id = id;
            TutorId = tutorId;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
        }
        public TimeSlot()
        {
            Id = 0;
            TutorId = 0;
            StartDateTime = DateTime.MinValue;
            EndDateTime = DateTime.MinValue;
            IsBooked = false;
        }
        public TimeSpan GetDuration(){
            return EndDateTime- StartDateTime;
        }
        public override string ToString()
            => $"{Id}: {StartDateTime:dd-MM HH-mm} - {EndDateTime:dd-MM HH-mm} (Zajęty: {IsBooked})";
    }
}
