using System;
using System.Linq;
using Project.DAL;
using Project.Model;

namespace Project 
{
    public static class Raport
    {
        public static void ShowTutorsByRate(ISystemManager manager)
        {
            Console.WriteLine("=== RAPORT: Korepetytorzy według stawki (rosnąco) ===");

            var list = manager.GetTutors().OrderBy(t => t.HourlyRate);

            foreach (var t in list)
            {
                Console.WriteLine($"{t.FirstName} {t.LastName} - {t.HourlyRate} PLN");
            }
        }

        public static void ShowSubjectPopularity(ISystemManager manager)
        {
            Console.WriteLine("\n=== Popularność przedmiotów (najwięcej umówionych lekcji) ===");

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