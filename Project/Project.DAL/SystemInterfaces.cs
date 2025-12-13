using System;
using System.Collections.Generic;
using Project.Model;

namespace Project.DAL
{
    public interface IUserService
    {
        Tutor AddTutor(string firstName, string lastName, string email, decimal hourlyRate);
        Student AddStudent(string firstName, string lastName, string email, string educationalLevel);
        void AddSpecialtyToTutor(int tutorId, Subject subject);
        List<Tutor> GetTutors();
        List<Student>GetStudents();
    }

    public interface ICatalogService
    {
        Subject AddSubject(string name, string description);
        List<Subject> GetSubjects();
    }
    
    public interface IBookingService
    {
        TimeSlot AddTimeSlot(Tutor tutor, DateTime start, DateTime end);
        Lesson? BookLesson(Tutor tutor, Student student, Subject subject, TimeSlot slot);
        List<Lesson> GetLessons();
    }
}
