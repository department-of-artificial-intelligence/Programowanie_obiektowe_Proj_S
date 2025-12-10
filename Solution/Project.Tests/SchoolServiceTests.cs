using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Project.Logic;
using Xunit;

namespace Project.Tests
{
    public class SchoolServiceTests
    {
        [Fact]
        public void GetStudentsByLanguage_ShouldReturnOnlyEnglishStudents()
        {
            var school = new School
            {
                Id = 1,
                Name = "Test School",
                City = "Test",
                Address = "Test",
                Country = "Test"
            };

            //dodajemy students
            school.Students.Add(new Student
            {
                Id = 1,
                FirstName = "Jan",
                LastName = "Kowalski",
                LanguageOfLearning = "English",
                Balance = 0,
                DateOfBirth = new System.DateOnly(2000, 1, 1),
                Address = "",
                PhoneNumber = "123456789",
                Email = ""
            });

            school.Students.Add(new Student
            {
                Id = 2,
                FirstName = "Patryk",
                LastName = "Nowak",
                LanguageOfLearning = "Germen",
                Balance = 0,
                DateOfBirth = new System.DateOnly(2000, 1, 1),
                Address = "",
                PhoneNumber = "123456789",
                Email = ""
            });

            var service = new SchoolService(school);

            var result = service.GetStudentsByLanguage("English");

            Assert.Single(result);
            Assert.Equal("Jan", result.First().FirstName);
        }
    }
}
