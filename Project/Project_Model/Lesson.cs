using System;

namespace Project.Model
{
    public class Lesson
    {
        public int Id { get; set; }
        public Tutor Tutor { get; set; } = null!;
        public Student Student { get; set; } = null!;
        public Subject Subject { get; set; } = null!;
        public DateTime StartDateTime { get; set; }
        public TimeSpan Duration { get; set; }
        public Lesson(Tutor tutor, Student student, Subject subject, TimeSlot slot)
        {
            Tutor = tutor;
            Student = student;
            Subject = subject;
            StartDateTime = slot.StartDateTime;
            Duration = slot.GetDuration();
        }
        public Lesson() { }

        public override string ToString()
            => $"Lekcja {Id}: {Subject?.Name}, {Tutor?.LastName} ---> {Student?.LastName}, {StartDateTime:dd-MM HH:mm}";
    }
}