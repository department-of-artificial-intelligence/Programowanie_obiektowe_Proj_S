using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{

	public class SystemManager
	{
		public List<Tutor> Tutors { get; set; } = new();
		public List<Student> Students { get; set; } = new();
		public List<Subject> Subjects { get; set; } = new();
		public List<Lesson> Lessons { get; set; } = new();
		public List<Reservation> Reservations { get; set; } = new();

		private int nextTutorId = 100;
		private int nextStudentId = 200;
		private int nextSubjectId = 1;
		private int nextTimeSlotId = 1;
		private int nextLessonId = 1;
		private int nextReservationId = 1;

		public Tutor AddTutor(string firstName, string lastName, string email, decimal hourlyRate)
		{
			var newTutor=new Tutor(nextTutorId++,  firstName, lastName, email, hourlyRate);
			Tutors.Add(newTutor);
			return newTutor;
		}
		public Student AddStudent(string firstName, string lastName, string email, string educationalLevel)
		{
			var newStudent=new Student(nextStudentId++,  firstName, lastName, email, educationalLevel);
			Students.Add(newStudent);
			return newStudent;
		}
        public Subject AddSubject(string name, string description)
        {
            var newSubject = new Subject(nextSubjectId++, name, description);
            Subjects.Add(newSubject);
            return newSubject;
        }
        public TimeSlot AddTimeSlot(Tutor tutor, DateTime start, DateTime end)
        {
            var newSlot = new TimeSlot(nextTimeSlotId++, tutor.Id, start, end);
            tutor.Availability.Add(newSlot);
            return newSlot;
        }
        public Reservation? BookLesson(Tutor tutor, Student student, Subject subject, TimeSlot slot)
        {
            if (slot.IsBooked){
                Console.WriteLine("Błąd rezerwacji: Ten termin jest już zajęty!");
                return null;
            }
            var lesson = new Lesson(nextLessonId++, tutor, student, subject, slot);
            Lessons.Add(lesson);

            var reservation = new Reservation(nextReservationId++, lesson);
            Reservations.Add(reservation);

            slot.IsBooked = true;
            return reservation;
        }
    }
}