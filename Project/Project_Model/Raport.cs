using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;

namespace Project.Model
{
    public static class Raport
    {
        public static void ShowTutorsByRate(SystemManager manager)
        {
            Console.WriteLine("=== RAPORT: Korepetytorzy według stawki (rosnąco) ===");

            if (!manager.Tutors.Any())
            {
                Console.WriteLine("Brak korepetytorów w systemie.");
                return;
            }
            var sortedTutors = manager.Tutors
                .OrderBy(t => t.HourlyRate);

            foreach (var t in sortedTutors)
            {
                Console.WriteLine($"{t.FirstName} {t.LastName} - {t.HourlyRate:C}/h");
            }
        }

        public static void ShowSubjectPopularity(SystemManager manager)
        {
            Console.WriteLine("\n=== Popularność przedmiotów (wg liczby lekcji) ===");

            if (!manager.Lessons.Any())
            {
                Console.WriteLine("Brak lekcji do analizy.");
                return;
            }
            var stats = manager.Lessons
                .GroupBy(l => l.Subject.Name)
                .Select(g => new { Subject = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count);

            foreach (var s in stats)
            {
                Console.WriteLine($"{s.Subject}: {s.Count} lekcji");
            }
        }
    }
}
