using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Model
{
    public class Lesson{
        public int Id { get; set; }
        public Tutor Tutor { get; set; }
        public Student Student { get; set; }
        public Subject Subject { get; set; }
        public DateTime StartDateTime { get; set; }
        public TimeSpan Duration {  get; set; }

        /*public Lesson()
        {
            Id = 0;
            Tutor = null;
            Student = null;
            Subject=null;
            StartDateTime= DateTime.MinValue;
            Duration = TimeSpan.Zero;
        }*/
        public Lesson(int id, Tutor tutor, Student student, Subject subject, TimeSlot slot)
        {
            Id = id;
            Tutor = tutor;
            Student = student;
            Subject = subject;
            StartDateTime=slot.StartDateTime;   
            //Duration = slot.Duration;
        }
        public override string ToString()
            => $"Lekcja ID {Id}: {Subject.Name}, {Tutor.FirstName}, --> {Student.FirstName}, {StartDateTime:dd-MM HH:mm}";
    }
}
