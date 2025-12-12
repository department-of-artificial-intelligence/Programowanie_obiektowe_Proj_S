using System;

namespace Project.Model
{
    public class Reservation
    {
        public int Id { get; set; }
        public Lesson? Lesson { get; set; }
        public DateTime CreationDate { get; set; }

        public Reservation(Lesson lesson)
        {
            Lesson = lesson;
            CreationDate = DateTime.Now;
        }
        public Reservation() { }
    }
}