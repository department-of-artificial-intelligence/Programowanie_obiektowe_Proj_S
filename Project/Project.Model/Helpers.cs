using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Project.Model
{
    public static class Helpers
    {
        public static string GetNonEmptyString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine()?.Trim();
                if (!string.IsNullOrWhiteSpace(input)) return input;
                Console.WriteLine("Pole nie może być puste.");
            }
        }

        public static int GetIntInRange(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
                    return value;
                Console.WriteLine($"Wprowadź liczbę całkowitą z zakresu {min}-{max}.");
            }
        }

        public static int GetIntPositive(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value) && value > 0) return value;
                Console.WriteLine("Wprowadź liczbę większą od 0.");
            }
        }

        public static decimal GetDecimalPositive(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string s = Console.ReadLine()?.Replace(",", ".") ?? "";
                if (decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal value) && value >= 0)
                    return value;
                Console.WriteLine("Wprowadź poprawną liczbę dodatnią (np. 199.99).");
            }
        }

        public static decimal GetDecimalInRange(string prompt, decimal min, decimal max)
        {
            while (true)
            {
                Console.Write(prompt);
                string s = Console.ReadLine()?.Replace(",", ".") ?? "";
                if (decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal value))
                {
                    if (value >= min && value <= max) return value;
                }
                Console.WriteLine($"Wprowadź liczbę z zakresu {min}-{max}.");
            }
        }

        public static DateTime GetDate(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (DateTime.TryParseExact(Console.ReadLine(), "dd-MM-yyyy", null, DateTimeStyles.None, out DateTime date))
                    return date;
                Console.WriteLine("Niepoprawny format daty. Wprowadź DD-MM-YYYY.");
            }
        }

        public static DateTime GetFutureOrTodayDate(string prompt)
        {
            while (true)
            {
                var date = GetDate(prompt);
                if (date >= DateTime.Today) return date;
                Console.WriteLine("Data musi być dzisiaj lub w przyszłości.");
            }
        }

        public static string GetValidatedPhone9(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var s = Console.ReadLine()?.Trim() ?? "";
                if (s.Length == 9 && long.TryParse(s, out _)) return s;
                Console.WriteLine("Wprowadź poprawny numer telefonu (9 cyfr).");
            }
        }

        public static string GetValidatedEmail(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var s = Console.ReadLine()?.Trim() ?? "";
                if (Regex.IsMatch(s, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")) return s;
                Console.WriteLine("Niepoprawny email.");
            }
        }

        public static string GetNonEmptyAlpha(string prompt)
        {
            while (true)
            {
                var s = GetNonEmptyString(prompt);
                if (Regex.IsMatch(s, @"^[a-zA-ZżźćńółęąśŻŹĆŃÓŁĘĄŚ\s\-]+$")) return s;
                Console.WriteLine("Można używać tylko liter i spacji.");
            }
        }
    }
}
