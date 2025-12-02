using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class School
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Country { get; set; }
        public required string City { get; set; }
        public required string Address { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<Student> Students { get; set; }
        public List<Course> Courses { get; set; }
        public List<Group> Groups { get; set; }

        public School()
        {
            Teachers = new List<Teacher>();
            Students = new List<Student>();
            Courses = new List<Course>();
            Groups = new List<Group>();
        }

        public void SeedData()
        {
            // Dodawanie Nauczycieli
            var t1 = new Teacher
            {
                Id = 1,
                FirstName = "Adam",
                LastName = "Nowak",
                DateOfBirth = new DateOnly(1980, 5, 20),
                Address = "Warszawa, Złota 5",
                PhoneNumber = "500100100",
                Email = "adam.nowak@school.com",
                LanguageOfTeaching = "English",
                Salary = 4500m,
                HoursWorked = 160
            };
            var t2 = new Teacher
            {
                Id = 2,
                FirstName = "Ewa",
                LastName = "Kowalska",
                DateOfBirth = new DateOnly(1985, 3, 15),
                Address = "Kraków, Długa 10",
                PhoneNumber = "600200200",
                Email = "ewa.kowalska@school.com",
                LanguageOfTeaching = "German",
                Salary = 4200m,
                HoursWorked = 140
            };
            Teachers.AddRange(new[] { t1, t2 });

            // Dodawanie Kursów
            var c1 = new Course { CourseId = 1, Language = "English", Level = "B2", PricePerHour = 50m, DurationInHours = 60 };
            var c2 = new Course { CourseId = 2, Language = "German", Level = "A1", PricePerHour = 45m, DurationInHours = 60 };
            Courses.AddRange(new[] { c1, c2 });

            // Dodawanie Grup
            var g1 = new Group
            {
                GroupId = 1,
                GroupName = "English-B2-Morning",
                CourseId = c1.CourseId,
                Course = c1,
                TeacherId = t1.Id,
                Teacher = t1,
                MaxStudents = 10,
                Schedule = "Mon-Wed 08:00",
                StartDate = DateTime.Now.AddDays(-30),
                EndDate = DateTime.Now.AddDays(30)
            };
            var g2 = new Group
            {
                GroupId = 2,
                GroupName = "German-A1-Evening",
                CourseId = c2.CourseId,
                Course = c2,
                TeacherId = t2.Id,
                Teacher = t2,
                MaxStudents = 8,
                Schedule = "Tue-Thu 18:00",
                StartDate = DateTime.Now.AddDays(-10),
                EndDate = DateTime.Now.AddMonths(2)
            };
            Groups.AddRange(new[] { g1, g2 });

            // Powiązanie grup z nauczycielami i kursami
            t1.AssignedGroups.Add(g1);
            t2.AssignedGroups.Add(g2);
            c1.Groups.Add(g1);
            c2.Groups.Add(g2);

            // Dodawanie Studentów
            var s1 = new Student
            {
                Id = 101,
                FirstName = "Jan",
                LastName = "Wiśniewski",
                DateOfBirth = new DateOnly(2000, 1, 10),
                Address = "Poznań, Polna 1",
                PhoneNumber = "700300300",
                Email = "jan.w@gmail.com",
                LanguageOfLearning = "English",
                Balance = 0
            };
            var s2 = new Student
            {
                Id = 102,
                FirstName = "Anna",
                LastName = "Zielińska",
                DateOfBirth = new DateOnly(1999, 12, 5),
                Address = "Gdańsk, Morska 3",
                PhoneNumber = "800400400",
                Email = "anna.z@gmail.com",
                LanguageOfLearning = "German",
                Balance = 0
            };
            var s3 = new Student
            {
                Id = 103,
                FirstName = "Piotr",
                LastName = "Czarny",
                DateOfBirth = new DateOnly(2001, 7, 20),
                Address = "Wrocław, Rynek 5",
                PhoneNumber = "900500500",
                Email = "piotr.c@gmail.com",
                LanguageOfLearning = "English",
                Balance = 0
            };
            Students.AddRange(new[] { s1, s2, s3 });

            // Zapisywanie do grup 
            s1.AddGroup(g1); // Jan do angielskiego
            s3.AddGroup(g1); // Piotr do angielskiego
            s2.AddGroup(g2); // Anna do niemieckiego
        }

    }
}
