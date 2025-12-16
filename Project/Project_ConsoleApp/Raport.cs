using System;
using System.Linq;
using Project.DAL;
using Project.Model;

namespace Project 
{
    public static class Raport
    {
        public static void ShowTutorsByRate(IUserService userService)
        {
            Console.WriteLine("=== Korepetytorzy według stawki (rosnąco) ===");

            var list = userService.GetTutors()
                .OrderBy(t => t.HourlyRate);

            foreach (var t in list)
            {
                Console.WriteLine($"{t.FirstName} {t.LastName} - {t.HourlyRate} PLN");
            }
        }
    }
}