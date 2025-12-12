using System;
using System.Collections.Generic;
using Project.Model;

namespace Project.DAL
{
    public interface ISystemManager
    {
        List<Tutor> GetTutors();
        List<Student> GetStudents();
        List<Subject> GetSubjects();
        List<Lesson> GetLessons();
        List<Reservation> GetReservations();

        Tutor AddTutor(string firstName, string lastNmae, string email, decimal hourlyRate);
        void AddSpecialtyToTutor(int tutorId, Subject subject);
        Student AddStudent(string firstName, string lastName, string email, string educationalLevel);
        Subject AddSubject(string name, string description);
        TimeSlot AddTimeSlot(Tutor tutor, DateTime start, DateTime end);
        Reservation? BookLesson(Tutor tutor, Student student, Subject subject, TimeSlot slot);
    }
}
