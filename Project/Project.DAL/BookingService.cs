using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project.Model;

namespace Project.DAL
{
    public class BookingService : IBookingService
    {
        public readonly ApplicationDbContext _context; //dostęp do bazy

        public BookingService(ApplicationDbContext context)
        {
            _context = context;
        }
        public TimeSlot AddTimeSlot(Tutor tutor, DateTime start, DateTime end)
        {
            if (start >= end)
            {
                throw new ArgumentException("Koniec musi być później niż start");
            }
            bool nakladaSie = _context.TimeSlots.Any(ts =>
                ts.TutorId == tutor.Id &&
                start < ts.EndDateTime &&
                end > ts.StartDateTime
            );

            if (nakladaSie) {
                throw new InvalidOperationException("Nie można dodać terminu, nakłada się na inny ");
            }

            var newSlot = new TimeSlot(tutor.Id, start, end);// stworzenie nowego wolnego terminu
            _context.TimeSlots.Add(newSlot);
            _context.SaveChanges();
            return newSlot;
        }

        public Lesson? BookLesson(Tutor tutor, Student student, Subject subject, TimeSlot slot)
        {
            if (slot.IsBooked) { 
                return null; 
            }
            if (!tutor.Specialties.Any(s => s.Id == subject.Id)){// czy korepetytor uczy tego przedmiotu
                throw new ArgumentException($"Nauczyciel {tutor.LastName} nie uczy {subject.Name}");
            }
            var lesson = new Lesson(tutor, student, subject, slot);// stworzenie nowej lekcji
            _context.Lessons.Add(lesson);

            slot.IsBooked = true;

            _context.SaveChanges();
            return lesson;
        }

        public List<Lesson> GetLessons() => _context.Lessons
            .Include(l => l.Tutor)
            .Include(l => l.Student)
            .Include(l => l.Subject)
            .ToList();

    }
}
