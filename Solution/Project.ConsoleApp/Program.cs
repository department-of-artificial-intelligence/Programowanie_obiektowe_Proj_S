using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
        IHost _host = Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();              
                logging.AddConsole();                 
                logging.SetMinimumLevel(LogLevel.Warning);
                logging.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning);
            })
            .ConfigureServices((context, services) =>
            {
                var cns = context.Configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(cns));
                services.AddScoped<SchoolService>();
            }).Build();

        using (var scope = _host.Services.CreateScope())
        {
            var context = _host.Services.GetService<ApplicationDbContext>();
            var service = scope.ServiceProvider.GetService<SchoolService>();

            if (context != null)
            {
                context.Database.Migrate();
                //context.Database.EnsureCreated();

                Console.WriteLine("--- STATUS BAZY DANYCH ---");
                Console.WriteLine("Połączono z bazą SQL.");

                if (!context.Persons.Any())
                {
                    Console.WriteLine("Baza jest pusta. Dodaję dane startowe...");
                    SeedData(context);
                }

                bool exit = false;
                while (!exit)
                {
                    Console.Clear();
                    Console.WriteLine("----- SYSTEM ZARZĄDZANIA SZKOŁĄ JĘZYKOWĄ -----");
                    Console.WriteLine("1. Zarządzanie Nauczycielami (Sortowanie według zarobków)");
                    Console.WriteLine("2. Zarządzanie Studentami (Filtrowanie według języka)");
                    Console.WriteLine("3. Statystyki Grup i Kursów");
                    Console.WriteLine("4. Raport o szkole (Polimorfizm + Interfejs + Extensions)");
                    Console.WriteLine("5. Dodawanie nowego studenta");
                    Console.WriteLine("0. Wyjście");
                    Console.WriteLine("\nWybierz opcję: (liczbę od 0 do 5): ");

                    var input = Console.ReadLine();
                    switch (input)
                    {
                        case "1":
                            TeacherMenu(service);
                            break;
                        case "2":
                            StudentMenu(service);
                            break;
                        case "3":
                            StatisticsMenu(service);
                            break;
                        case "4":
                            SchoolReportMenu(context);
                            break;
                        case "5":
                            AddStudentMenu(service);
                            break;
                        case "0":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Nieznana opcja. Wciśnij Enter żeby zacząć od nowa.");
                            Console.ReadLine();
                            break;
                    }

                }
            }
        }
    }

    // Sortowanie
    static void TeacherMenu(SchoolService service)
    {
        Console.WriteLine("\n--- Nauczyciele według zarobków ---");
        var sortedTeachers = service.GetTeachersSortedBySalary();

        foreach (var t in sortedTeachers)
        {
            Console.WriteLine($"{t.LastName} {t.FirstName} : {t.Salary} PLN");
        }
        Console.WriteLine("\nWciśnij Enter...");
        Console.ReadLine();
    }

    // Zastosowanie LINQ
    static void StudentMenu(SchoolService service)
    {
        Console.WriteLine("\n--- Studenci uczący się angielskiego ---");
        var englishStudents = service.GetStudentsByLanguage("English");

        if (!englishStudents.Any()) Console.WriteLine("Brak studentów.");

        foreach (var s in englishStudents)
        {
            Console.WriteLine($"Student: {s.FirstName} {s.LastName}");
        }
        Console.WriteLine("\nWciśnij Enter...");
        Console.ReadLine();
    }

    // Statystyki
    static void StatisticsMenu(SchoolService service)
    {
        Console.WriteLine("\n--- Statystyki kursów ---");
        var stats = service.GetCourseStatistics();
        if (stats.mostExpensive != null)
        {
            Console.WriteLine($"Srednia cena za godzinę: {stats.avg:F2} PLN");
            Console.WriteLine($"Najwyższa cena: {stats.max} PLN");
            Console.WriteLine($"Najdroższy kurs to: {stats.mostExpensive.Language} ({stats.mostExpensive.Level})");
        }

        Console.WriteLine("\n--- Statystyki Grup ---");
        var groupStats = service.GetGroupStatistics(); //DTO
        foreach (var g in groupStats)
        {
            Console.WriteLine($"Grupa: {g.GroupName} | Obłożenie: {g.FillPercent:F1}%");
        }
        Console.WriteLine("\nWciśnij Enter...");
        Console.ReadLine();
    }

    static void SchoolReportMenu(ApplicationDbContext context)
    {
        Console.WriteLine("\n--- PEŁNY RAPORT O SZKOLE ---");

        //UŻYCIE METODY ROZSZERZEŃ
        int totalPeople = context.CountAllPeople();
        Console.WriteLine($"[INFO] Łączna liczba osób w systemie (Students + Teachers): {totalPeople}");
        Console.WriteLine("-------------------------------------------------------------");

        //UŻYCIE POLIMORFIZMU I INTERFEJSU (IReportable)
        List<IReportable> schoolObjects = new List<IReportable>();

        schoolObjects.AddRange(context.Students.ToList());
        schoolObjects.AddRange(context.Teachers.ToList());
        schoolObjects.AddRange(context.Groups.ToList());

        foreach (var item in schoolObjects)
        {
            Console.WriteLine(item.GetInfo());
        }

        Console.WriteLine("\nRaport wygenerowany. Wciśnij Enter...");
        Console.ReadLine();
    }

    static void AddStudentMenu(SchoolService service)
    {
        Console.WriteLine("\n--- Dodawanie Studenta ---");
        string firstName = "";
        while (string.IsNullOrWhiteSpace(firstName))
        {
            Console.Write("Imię: ");
            firstName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(firstName)) Console.WriteLine("Błąd: Imię nie może być puste!");
        }

        string lastName = "";
        while (string.IsNullOrWhiteSpace(lastName))
        {
            Console.Write("Nazwisko: ");
            lastName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(lastName)) Console.WriteLine("Błąd: Nazwisko nie może być puste!");
        }

        DateOnly birthDate = DateOnly.MinValue;
        while (birthDate == DateOnly.MinValue)
        {
            Console.Write("Data urodzenia (RRRR-MM-DD): ");
            string inputData = Console.ReadLine();
            if (!DateOnly.TryParse(inputData, out birthDate))
            {
                Console.WriteLine("Błąd: Nieprawidłowy format daty! Spróbuj np. 2000-05-20");
            }
            else if (birthDate > DateOnly.FromDateTime(DateTime.Now))
            {
                Console.WriteLine("Błąd: Data urodzenia nie może być z przyszłości!");
                birthDate = DateOnly.MinValue; 
            }
        }

        Console.Write("Miasto/Adres: ");
        string address = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(address)) address = "Nieznany"; 

        Console.Write("Numer Telefonu: ");
        string inputPhone = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(inputPhone))
        {
            inputPhone = "000000000";
        }
        string phoneNumber = inputPhone.ToPolishPhoneNumber();

        // Opcjonalnie: Pokaż użytkownikowi, jak sformatowałaś numer
        Console.WriteLine($"Info: Zapiszemy numer jako: {phoneNumber}");

        string email = "";
        while (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
        {
            Console.Write("Email: ");
            email = Console.ReadLine();
            if (!email.Contains("@")) Console.WriteLine("Błąd: Email musi zawierać znak '@'.");
        }

        string language = "";
        while (language != "English" && language != "German")
        {
            Console.Write("Język (English/German): ");
            language = Console.ReadLine();
            if (language != "English" && language != "German") Console.WriteLine("Błąd: Obsługujemy tylko English lub German.");
        }

        try
        {
            service.AddStudent(firstName, lastName, birthDate, address, phoneNumber, email, language);
            Console.WriteLine("Sukces! Student dodany.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił błąd: {ex.Message}");
        }

        Console.WriteLine("Dodano do bazy! Wciśnij Enter...");
        Console.ReadLine();
    }

    static void SeedData(ApplicationDbContext context)
    {
        var t1 = new Teacher { Id = 0, FirstName = "Adam", LastName = "Nowak", DateOfBirth = new DateOnly(1980, 5, 20), Address = "Warszawa", PhoneNumber = "500", Email = "a@a.pl", LanguageOfTeaching = "English", Salary = 4500m, HoursWorked = 160 };
        var t2 = new Teacher { Id = 0, FirstName = "Ewa", LastName = "Kowalska", DateOfBirth = new DateOnly(1985, 3, 15), Address = "Kraków", PhoneNumber = "600", Email = "e@e.pl", LanguageOfTeaching = "German", Salary = 4200m, HoursWorked = 140 };
        context.Teachers.AddRange(t1, t2);
        context.SaveChanges();

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

        Console.WriteLine("Dane startowe dodane.");
    }       
}
            
        
    
            