using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucja.Projekt
{
    public enum Subject
    {
        Math,
        English
    }
    public enum DayOfWeek
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }
    public enum PlaceOfLesson   //At who's house is the lesson to take place at
    {
        Tutor,
        Student
    }

    public class Lesson
    {
        public Subject Subject { get; set; }
        public DayOfWeek Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public PlaceOfLesson Place { get; set; }
        public Decimal Price { get; set; }

        public Lesson(Subject subject, DayOfWeek day, TimeSpan startTime, TimeSpan duration, PlaceOfLesson place, decimal price)
        {
            Subject = subject;
            Day = day;
            StartTime = startTime;
            Duration = duration;
            Place = place;
            Price = price;
        }
    }
}
