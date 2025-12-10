using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Project.DAL;
using Project.Logic;
using Project.Model;
using Project.Model.DTOs;
using Project.Model.Extensions;


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
                    Student person = new Student()
                    {
                        Id = 0,
                        FirstName = "Testowy",
                        LastName = "Janusz",
                        Address = "Baza Danych SQL",
                        DateOfBirth = new DateOnly(1990, 1, 1),
                        PhoneNumber = "111222333",
                        Email = "test@db.com",
                        LanguageOfLearning = "SQL",
                        Balance = 0
                    };

                    context.Persons.Add(person);
                    context.SaveChanges();
                    Console.WriteLine("Dodano rekord do bazy SQL.");
                }
                else
                {
                    Console.WriteLine("Baza już zawiera dane.");
                }
                Console.WriteLine("--------------------------\n");
            }
        }


        School mySchool = new School
        {
            Id = 1,
            Name = "Super Language School",
            City = "Warsaw",
            Address = "Main St",
            Country = "Poland"
        };
        mySchool.SeedData();

        SchoolService service = new SchoolService(mySchool);

        Console.WriteLine("=== SYSTEM SZKOŁY JĘZYKOWEJ ===");
        Console.WriteLine($"Liczba studentów: {mySchool.Students.Count}");
        Console.WriteLine($"Liczba nauczycieli: {mySchool.Teachers.Count}");
        Console.WriteLine($"Liczba grup: {mySchool.Groups.Count}");

        // Zastosowanie LINQ
        Console.WriteLine("\n--- Studenci uczący się angielskiego ---");
        var englishStudents = service.GetStudentsByLanguage("English");

        foreach(var s in englishStudents)
        {
            Console.WriteLine($"Student: {s.FirstName} {s.LastName}");
        }

        // Sortowanie
        Console.WriteLine("\n--- Nauczyciele według zarobków ---");
        var sortedTeachers = service.GetTeachersSortedBySalary();

        foreach(var t in sortedTeachers)
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
        Console.WriteLine("\n--- Wszystkie luudzie w szkole ---");

        List<Person> allPeople = new List<Person>();
        allPeople.AddRange(mySchool.Students);
        allPeople.AddRange(mySchool.Teachers);

        var sortedPeople = allPeople
            .OrderBy(p => p.LastName)
            .ThenBy(p => p.FirstName)
            .ToList();

        foreach(var person in sortedPeople)
        {
            string role = "";
            if(person is Student)
            {
                role = "Student";
            }
            else if(person is Teacher)
            {
                role = "Teacher";
            }

            Console.WriteLine($"{role}: {person.LastName} {person.FirstName} (Wiek: {person.Age})");
        }

        // Testowanie Interfejsu
        Console.WriteLine("\n--- Test Interfejsu IReportable ---");

        List<IReportable> everything = new List<IReportable>();

        everything.AddRange(mySchool.Students);
        everything.AddRange(mySchool.Teachers);
        everything.AddRange(mySchool.Groups);

        foreach(var item in everything)
        {
            Console.WriteLine(item.GetInfo());
        }

        // Test metod rozszerzeń
        Console.WriteLine("\n--- Test metod rozszerzeń ---");

        int totalPeople = mySchool.CountAllPeople();
        Console.WriteLine($"W szkole jest łącznie {totalPeople} osób.");

        var student = mySchool.Students[0];
        Console.WriteLine($"Telefon: {student.PhoneNumber.ToPolishPhoneNumber()}");

        mySchool.Groups.PrintToConsole();

        Console.ReadLine();
    }
}