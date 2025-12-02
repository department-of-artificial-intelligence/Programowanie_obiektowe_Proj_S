using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.Extensions
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
            return phoneNumber;
        }

        public static int CountAllPeople(this School school)
        {
            return (school.Students?.Count ?? 0) + (school.Teachers?.Count ?? 0);
        }

        public static void PrintToConsole(this System.Collections.Generic.IEnumerable<IReportable> list)
        {
            Console.WriteLine("--- Raport automatyczny ---");
            foreach (var item in list)
            {
                Console.WriteLine(item.GetInfo());
            }

        }
    }
}
