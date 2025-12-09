using System;
using System.Linq;
using Project.DAL;
using Project.Model;

namespace Project 
{
    public static class Raport
    {
        public static void ShowTutorsByRate(SystemManager manager)
        {
            Console.WriteLine("=== RAPORT: Korepetytorzy według stawki (rosnąco) ===");

            var list = manager.GetTutors().OrderBy(t => t.HourlyRate);

            foreach (var t in list)
            {
                Console.WriteLine($"{t.FirstName} {t.LastName} - {t.HourlyRate} PLN");
            }
        }

        public static void ShowSubjectPopularity(SystemManager manager)
        {
            Console.WriteLine("\n=== Popularność przedmiotów ===");

            var stats = manager.GetLessons()
                .GroupBy(l => l.Subject.Name)
                .Select(g => new { Subject = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count);

            foreach (var s in stats)
            {
                Console.WriteLine($"{s.Subject}: {s.Count}");
            }
        }
    }
}