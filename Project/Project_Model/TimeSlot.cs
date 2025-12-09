using System;

namespace Project.Model
{
    public class TimeSlot
    {
        public int Id { get; set; }
        public int TutorId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public bool IsBooked { get; set; } = false;

        public TimeSlot(int tutorId, DateTime start, DateTime end)
        {
            TutorId = tutorId;
            StartDateTime = start;
            EndDateTime = end;
        }
        public TimeSlot() { }

        public TimeSpan GetDuration() => EndDateTime - StartDateTime;

        public override string ToString()
            => $"{Id}: {StartDateTime:dd-MM HH:mm} - {EndDateTime:HH:mm} (Zajęty: {IsBooked})";
    }
}