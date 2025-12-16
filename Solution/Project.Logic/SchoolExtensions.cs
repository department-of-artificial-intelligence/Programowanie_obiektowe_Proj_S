using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.DAL;
using Project.Model;

namespace Project.Logic
{
    public static class SchoolExtensions
    {
        public static string ToPolishPhoneNumber(this string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return "Brak numeru";
            }
            if (!phoneNumber.StartsWith("+48"))
            {
                return $"+48 {phoneNumber}";
            }
            if (phoneNumber.Length == 9)
            {
                return $"{phoneNumber.Substring(0, 3)}-{phoneNumber.Substring(3, 3)}-{phoneNumber.Substring(6, 3)}";
            }
            return phoneNumber;
        }

        public static int CountAllPeople(this ApplicationDbContext context)
        {
            return context.Students.Count() + context.Teachers.Count();
        }

        public static void PrintToConsole(this List<Group> groups)
        {
            Console.WriteLine("--- Lista Grup ---");
            foreach (var group in groups)
            {
                Console.WriteLine($"ID: {group.GroupId}, Nazwa: {group.GroupName}");
            }
        }
    }
}
