using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Model;
using Project.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Project.Logic
{
    public class SchoolService
    {
        private readonly ApplicationDbContext _context;

        public SchoolService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Student> GetStudentsByLanguage(string language)
        {
            return _context.Students
                .Where(s => s.LanguageOfLearning ==  language)
                .ToList();
        }

        public List<Teacher> GetTeachersSortedBySalary()
        {
            return _context.Teachers
                .OrderBy(t => t.Salary)
                .ToList();
        }

        public (decimal avg, decimal max, Course? mostExpensive) GetCourseStatistics()
        {
            if (!_context.Courses.Any())
            {
                return (0, 0, null);
            }

            decimal avg = _context.Courses.Average(c => c.PricePerHour);
            decimal max = _context.Courses.Max(c => c.PricePerHour);

            var expensive = _context.Courses
                .OrderByDescending(c => c.PricePerHour)
                .FirstOrDefault();

            return (avg, max, expensive);
        }

        public List<GroupStatDto> GetGroupStatistics()
        {
            var groups = _context.Groups
                .Include(g => g.Teacher)
                .Include(g => g.Enrollments)
                .ToList();

            var stats = groups.Select(g => new GroupStatDto
            {
                GroupName = g.GroupName,
                TeacherName = g.Teacher != null ? $"{g.Teacher.FirstName} {g.Teacher.LastName}" : "Brak",

                MaxStudents = g.MaxStudents,
                StudentCount = g.Enrollments.Count,

                FillPercent = g.MaxStudents > 0 ? (double)g.Enrollments.Count / g.MaxStudents * 100 : 0
            })
            .OrderByDescending(s => s.FillPercent) 
            .ToList();

            return stats;
        }

        public void AddStudent(string firstName, string lastName, DateOnly birthDate, string address, string phoneNumber, string email, string language)
        {
            var newStudent = new Student
            {
                Id = 0,
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = birthDate,
                Address = address,    
                PhoneNumber = phoneNumber,
                Email = email,
                LanguageOfLearning = language,
                Balance = 0
            };

            _context.Students.Add(newStudent);
            _context.SaveChanges();
        }
    }
}
