using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;
using Project.Model.DTOs;

namespace Project.Logic
{
    public  class SchoolService
    {
        private readonly School _school;

        public SchoolService(School school)
        {
            _school = school;
        }

        public List<Student> GetStudentsByLanguage(string language)
        {
            return _school.Students
                .Where(s => s.LanguageOfLearning ==  language)
                .ToList();
        }

        public List<Teacher> GetTeachersSortedBySalary()
        {
            return _school.Teachers
                .OrderBy(testc => testc.Salary)
                .ToList();
        }

        public (decimal avg, decimal max, Course? mostExpensive) GetCourseStatistics()
        {
            if (!_school.Courses.Any())
            {
                return (0, 0, null);
            }

            decimal avg = _school.Courses.Average(c => c.PricePerHour);
            decimal max = _school.Courses.Max(c => c.PricePerHour);
            var expensive = _school.Courses.OrderByDescending(c => c.PricePerHour).FirstOrDefault();

            return (avg, max, expensive);
        }
    }
}
