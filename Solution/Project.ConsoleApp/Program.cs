using Project.Model;
using Project.Logic;
using Project.Model.DTOs;
using Project.Model.Extensions;

class Program
{
    public static void Main(string[] args)
    {
        School mySchool = new School
        {
            Id = 1,
            Name = "Super Language School",
            City = "Warsaw",
            Address = "Main St",
            Country = "Poland"
        };

        mySchool.SeedData();

        Console.WriteLine("=== SYSTEM SZKOŁY JĘZYKOWEJ ===");
        Console.WriteLine($"Liczba studentów: {mySchool.Students.Count}");
        Console.WriteLine($"Liczba nauczycieli: {mySchool.Teachers.Count}");
        Console.WriteLine($"Liczba grup: {mySchool.Groups.Count}");

        // Zastosowanie LINQ
        Console.WriteLine("\n--- Studenci uczący się angielskiego ---");
        var englishStudents = mySchool.Students
            .Where(s => s.LanguageOfLearning == "English")
            .ToList();

        foreach(var s in englishStudents)
        {
            Console.WriteLine($"Student: {s.FirstName} {s.LastName}");
        }

        // Sortowanie
        Console.WriteLine("\n--- Nauczyciele według zarobków ---");
        var sortedTeachers = mySchool.Teachers
            .OrderBy(t => t.Salary)
            .ToList();

        foreach(var t in sortedTeachers)
        {
            Console.WriteLine($"{t.LastName} {t.FirstName} : {t.Salary} PLN");
        }

        // Proste statystyki
        Console.WriteLine("\n--- Statystyki kursów ---");
        if (mySchool.Courses.Any())
        {
            decimal avgPrice = mySchool.Courses.Average(c => c.PricePerHour);
            decimal maxPrice = mySchool.Courses.Max(c => c.PricePerHour);

            var mostExpensiveCourse = mySchool.Courses
                .OrderByDescending(c => c.PricePerHour)
                .FirstOrDefault();

            Console.WriteLine($"Srednia cena za godzinę: {avgPrice:F2} PLN");
            Console.WriteLine($"Najwyższa cena: {maxPrice} PLN");
            Console.WriteLine($"Najdroższy kurs to: {mostExpensiveCourse?.Language} ({mostExpensiveCourse?.Level})");
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
    }
}