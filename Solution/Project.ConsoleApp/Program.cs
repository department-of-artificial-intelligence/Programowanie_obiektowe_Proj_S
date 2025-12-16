using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Project.DAL;
using Project.Logic;
using Project.Model;
using Project.Model.DTOs;
using System;
using System.Linq;


class Program
{
    public static void Main(string[] args)
    {
        //konfiguracja BD
        IHost _host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
        {
            var cns = context.Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(cns));
        }).Build();

        using (var scope = _host.Services.CreateScope())
        {
            var context = _host.Services.GetService<ApplicationDbContext>();
            if (context != null)
            {
                context.Database.Migrate();
                //context.Database.EnsureCreated();

                Console.WriteLine("--- STATUS BAZY DANYCH ---");
                Console.WriteLine("Połączono z bazą SQL.");

                if (!context.Persons.Any())
                {
                    var t1 = new Teacher { Id = 0, FirstName = "Adam", LastName = "Nowak", DateOfBirth = new DateOnly(1980, 5, 20), Address = "Wwa", PhoneNumber = "500500500", Email = "a@a.pl", LanguageOfTeaching = "English", Salary = 4500m, HoursWorked = 160 };
                    var t2 = new Teacher { Id = 0, FirstName = "Ewa", LastName = "Kowalska", DateOfBirth = new DateOnly(1985, 3, 15), Address = "Krk", PhoneNumber = "600600600", Email = "e@e.pl", LanguageOfTeaching = "German", Salary = 4200m, HoursWorked = 140 };
                    context.Teachers.AddRange(t1, t2);

                    var c1 = new Course { CourseId = 0, Language = "English", Level = "B2", PricePerHour = 50m, DurationInHours = 60 };
                    var c2 = new Course { CourseId = 0, Language = "German", Level = "A1", PricePerHour = 45m, DurationInHours = 60 };
                    context.Courses.AddRange(c1, c2);

                    context.SaveChanges();

                    var g1 = new Group { GroupId = 0, GroupName = "Angielski Rano", CourseId = c1.CourseId, TeacherId = t1.Id, MaxStudents = 10, Schedule = "Mon 8:00", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(3) };
                    var g2 = new Group { GroupId = 0, GroupName = "Niemiecki Wieczór", CourseId = c2.CourseId, TeacherId = t2.Id, MaxStudents = 8, Schedule = "Tue 18:00", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(3) };
                    context.Groups.AddRange(g1, g2);

                    var s1 = new Student { Id = 0, FirstName = "Jan", LastName = "Wiśniewski", DateOfBirth = new DateOnly(2000, 1, 1), Address = "Poznań", PhoneNumber = "700", Email = "j@j.pl", LanguageOfLearning = "English", Balance = 0 };
                    var s2 = new Student { Id = 0, FirstName = "Anna", LastName = "Zielińska", DateOfBirth = new DateOnly(1999, 12, 5), Address = "Gdańsk", PhoneNumber = "800", Email = "a@z.pl", LanguageOfLearning = "German", Balance = 0 };
                    context.Students.AddRange(s1, s2);

                    context.SaveChanges();

                    context.Enrollments.Add(new Enrollment { EnrollmentId = 0, StudentId = s1.Id, GroupId = g1.GroupId, EnrollmentDate = DateTime.Now, Status = EnrollmentStatus.Active, AmountPaid = 0 });
                    context.Enrollments.Add(new Enrollment { EnrollmentId = 0, StudentId = s2.Id, GroupId = g2.GroupId, EnrollmentDate = DateTime.Now, Status = EnrollmentStatus.Active, AmountPaid = 0 });

                    context.SaveChanges();
                    Console.WriteLine("Dane zastały zapisane do bazy SQL.");
                }
                else
                {
                    Console.WriteLine("Baza już zawiera dane.");
                }
                Console.WriteLine("--------------------------\n");

                SchoolService service = new SchoolService(context);

                Console.WriteLine("=== SYSTEM SZKOŁY JĘZYKOWEJ ===");
                Console.WriteLine($"Liczba studentów: {context.Students.Count()}");
                Console.WriteLine($"Liczba nauczycieli: {context.Teachers.Count()}");
                Console.WriteLine($"Liczba grup: {context.Groups.Count()}");

                // Zastosowanie LINQ
                Console.WriteLine("\n--- Studenci uczący się angielskiego ---");
                var englishStudents = service.GetStudentsByLanguage("English");

                foreach (var s in englishStudents)
                {
                    Console.WriteLine($"Student: {s.FirstName} {s.LastName}");
                }

                // Sortowanie
                Console.WriteLine("\n--- Nauczyciele według zarobków ---");
                var sortedTeachers = service.GetTeachersSortedBySalary();

                foreach (var t in sortedTeachers)
                {
                    Console.WriteLine($"{t.LastName} {t.FirstName} : {t.Salary} PLN");
                }

                // Statystyki
                Console.WriteLine("\n--- Statystyki kursów ---");
                var stats = service.GetCourseStatistics();
                if (stats.mostExpensive != null)
                {
                    Console.WriteLine($"Srednia cena za godzinę: {stats.avg:F2} PLN");
                    Console.WriteLine($"Najwyższa cena: {stats.max} PLN");
                    Console.WriteLine($"Najdroższy kurs to: {stats.mostExpensive.Language} ({stats.mostExpensive.Level})");
                }

                // Polimorfizm
                Console.WriteLine("\n--- Wszystkie ludzie w szkole ---");

                List<Person> allPeople = new List<Person>();
                allPeople.AddRange(context.Students);
                allPeople.AddRange(context.Teachers);

                var sortedPeople = allPeople
                    .OrderBy(p => p.LastName)
                    .ThenBy(p => p.FirstName)
                    .ToList();

                foreach (var person in sortedPeople)
                {
                    string role = "";
                    if (person is Student)
                    {
                        role = "Student";
                    }
                    else if (person is Teacher)
                    {
                        role = "Teacher";
                    }

                    Console.WriteLine($"{role}: {person.LastName} {person.FirstName} (Wiek: {person.Age})");
                }

                // Testowanie Interfejsu
                Console.WriteLine("\n--- Test Interfejsu IReportable ---");

                List<IReportable> everything = new List<IReportable>();

                everything.AddRange(context.Students.ToList());
                everything.AddRange(context.Teachers.ToList());
                everything.AddRange(context.Groups.ToList());

                foreach (var item in everything)
                {
                    Console.WriteLine(item.GetInfo());
                }

                // Test metod rozszerzeń
                Console.WriteLine("\n--- Test metod rozszerzeń ---");

                int totalPeople = context.CountAllPeople();
                Console.WriteLine($"W szkole jest łącznie {totalPeople} osób.");

                var studentsList = context.Students.ToList();
                var student = context.Students.First();
                Console.WriteLine($"Telefon: {student.PhoneNumber.ToPolishPhoneNumber()}");

                var groupsList = context.Groups.ToList();
                groupsList.PrintToConsole();

                Console.ReadLine();
            }
        }
    }
}