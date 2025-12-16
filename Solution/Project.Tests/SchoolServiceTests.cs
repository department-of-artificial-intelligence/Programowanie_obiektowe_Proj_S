using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Project.Logic;
using Project.DAL;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Project.Tests
{
    public class SchoolServiceTests
    {
        [Fact]
        public void GetStudentsByLanguage_ShouldReturnOnlyEnglishStudents()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_Students_English")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                // Dodajemy dane testowe
                context.Students.Add(new Student
                {
                    Id = 0,
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    LanguageOfLearning = "English",
                    Balance = 0,
                    DateOfBirth = new DateOnly(2000, 1, 1),
                    Address = "Test",
                    PhoneNumber = "123123123",
                    Email = "test@test.pl"
                });

                context.Students.Add(new Student
                {
                    Id = 0,
                    FirstName = "Patryk",
                    LastName = "Nowak",
                    LanguageOfLearning = "German", 
                    Balance = 0,
                    DateOfBirth = new DateOnly(2000, 1, 1),
                    Address = "Test",
                    PhoneNumber = "111222333",
                    Email = "test@test.pl"
                });

                context.SaveChanges(); 
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new SchoolService(context);
                var result = service.GetStudentsByLanguage("English");

                Assert.Single(result);
                Assert.Equal("Jan", result.First().FirstName);
            }
        }
    }
}
