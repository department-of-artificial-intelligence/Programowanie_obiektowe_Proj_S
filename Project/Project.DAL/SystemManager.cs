using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project.Model;

namespace Project.DAL
{
    public class SystemManager
    {
        private readonly ApplicationDbContext _context;

        public SystemManager(ApplicationDbContext context)
        {
            _context = context;
        }

        // --- POBIERANIE DANYCH 
        public List<Tutor> GetTutors()
            => _context.Tutors
                .Include(t => t.Specialties)
                .Include(t => t.Availability)
                .ToList();

        public List<Student> GetStudents() => _context.Students.ToList();

        public List<Subject> GetSubjects() => _context.Subjects.ToList();

        public List<Lesson> GetLessons()
            => _context.Lessons
                .Include(l => l.Tutor)
                .Include(l => l.Student)
                .Include(l => l.Subject)
                .ToList();

        public List<Reservation> GetReservations()
            => _context.Reservations
                .Include(r => r.Lesson)
                .ToList();

        // --- DODAWANIE DANYCH
        public Tutor AddTutor(string firstName, string lastName, string email, decimal hourlyRate)
        {
            var newTutor = new Tutor(firstName, lastName, email, hourlyRate);
            _context.Tutors.Add(newTutor);
            _context.SaveChanges();
            return newTutor;
        }

        public void AddSpecialtyToTutor(int tutorId, Subject subject)
        {
            var tutor = _context.Tutors.Include(t => t.Specialties).FirstOrDefault(t => t.Id == tutorId);
            if (tutor != null && subject != null && !tutor.Specialties.Any(s => s.Id == subject.Id))
            {
                tutor.Specialties.Add(subject);
                _context.SaveChanges();
            }
        }

        public Student AddStudent(string firstName, string lastName, string email, string educationalLevel)
        {
            var newStudent = new Student(firstName, lastName, email, educationalLevel);
            _context.Students.Add(newStudent);
            _context.SaveChanges();
            return newStudent;
        }

        public Subject AddSubject(string name, string description)
        {
            var newSubject = new Subject(name, description);
            _context.Subjects.Add(newSubject);
            _context.SaveChanges();
            return newSubject;
        }

        public TimeSlot AddTimeSlot(Tutor tutor, DateTime start, DateTime end)
        {
            var newSlot = new TimeSlot(tutor.Id, start, end);
            _context.TimeSlots.Add(newSlot);
            _context.SaveChanges();
            return newSlot;
        }

        public Reservation? BookLesson(Tutor tutor, Student student, Subject subject, TimeSlot slot)
        {
            if (slot.IsBooked) return null;

            var lesson = new Lesson(tutor, student, subject, slot);
            _context.Lessons.Add(lesson);
            slot.IsBooked = true;

            var reservation = new Reservation(lesson);
            _context.Reservations.Add(reservation);

            _context.SaveChanges();
            return reservation;
        }
    }
}