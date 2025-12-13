using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project.Model;

namespace Project.DAL
{
    public class UserService: IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Tutor AddTutor(string firstName, string lastName, string email, decimal hourlyRate)
        {
            var newTutor=new Tutor(firstName, lastName, email, hourlyRate);
            _context.Tutors.Add(newTutor);
            _context.SaveChanges();
            return newTutor;
        }
        public Student AddStudent(string firstName, string lastName, string email, string educationalLevel)
        {
            var newStudent=new Student(firstName, lastName, email, educationalLevel);
            _context.Students.Add(newStudent);
            _context.SaveChanges();
            return newStudent;
        }
        public void AddSpecialtyToTutor(int tutorId, Subject subject)
        {
            var tutor=_context.Tutors.Include(t=>t.Specialties) .FirstOrDefault(t => t.Id== tutorId);
            if(tutor !=null && subject!= null && !tutor.Specialties.Any(s=> s.Id == subject.Id))
            {
                tutor.Specialties.Add(subject);
                _context.SaveChanges ();
            }
        }

        public List<Tutor> GetTutors()=> _context.Tutors
            .Include(t=> t.Specialties)
            .Include(t=> t.Availability)
            .ToList();

        public List<Student> GetStudents() => _context.Students.ToList();
    }
}
