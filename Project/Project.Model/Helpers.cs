using System;
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
                string s = Console.ReadLine() ?? "";
                if (!string.IsNullOrWhiteSpace(s)) return s.Trim();
                Console.WriteLine("Pole nie może być puste.");
            }
        }

        public static string GetOptionalString(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine() ?? "";
        }

        public static string GetNonEmptyAlpha(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string s = Console.ReadLine() ?? "";
                if (string.IsNullOrWhiteSpace(s)) { Console.WriteLine("Pole nie może być puste."); continue; }
                if (!Regex.IsMatch(s.Trim(), @"^[A-Za-zÀ-ÿ\- ]+$")) { Console.WriteLine("Dozwolone tylko litery, spacje i myślniki."); continue; }
                return s.Trim();
            }
        }

        public static int GetIntInRange(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                string s = Console.ReadLine() ?? "";
                if (int.TryParse(s, out int v) && v >= min && v <= max) return v;
                Console.WriteLine($"Wprowadź liczbę całkowitą z zakresu {min}-{max}.");
            }
        }

        public static int GetOptionalIntInRange(string prompt, int min, int max, int defaultValue)
        {
            while (true)
            {
                Console.Write(prompt);
                string s = Console.ReadLine() ?? "";
                if (string.IsNullOrWhiteSpace(s)) return defaultValue;
                if (int.TryParse(s, out int v) && v >= min && v <= max) return v;
                Console.WriteLine($"Wprowadź liczbę całkowitą z zakresu {min}-{max} lub zostaw puste.");
            }
        }

        public static int GetIntPositive(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string s = Console.ReadLine() ?? "";
                if (int.TryParse(s, out int v) && v > 0) return v;
                Console.WriteLine("Wprowadź liczbę całkowitą większą od 0.");
            }
        }

        public static decimal GetDecimalPositive(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string s = Console.ReadLine() ?? "";
                if (decimal.TryParse(s, out decimal v) && v >= 0) return v;
                Console.WriteLine("Wprowadź poprawną liczbę (np. 199.99). Nie może być ujemna.");
            }
        }

        public static DateTime GetDateNotFuture(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string s = Console.ReadLine() ?? "";
                if (DateTime.TryParseExact(s, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime d) && d <= DateTime.Now) return d;
                Console.WriteLine("Wprowadź poprawną datę w formacie dd-MM-yyyy, nie późniejszą niż dziś.");
            }
        }

        public static DateTime GetFutureOrTodayDate(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string s = Console.ReadLine() ?? "";
                if (DateTime.TryParseExact(s, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime d) && d >= DateTime.Today) return d;
                Console.WriteLine("Wprowadź poprawną datę w formacie dd-MM-yyyy (dzisiaj lub później).");
            }
        }

        public static DateTime GetDate(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string s = Console.ReadLine() ?? "";
                if (DateTime.TryParseExact(s, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime d)) return d;
                Console.WriteLine("Wprowadź poprawną datę w formacie dd-MM-yyyy.");
            }
        }

        public static string GetValidatedPhone9(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string s = Console.ReadLine() ?? "";
                string cleaned = s.Trim();
                if (cleaned.Length == 9 && long.TryParse(cleaned, out _)) return cleaned;
                Console.WriteLine("Wprowadź poprawny numer telefonu (9 cyfr).");
            }
        }


        public static string GetValidatedEmail(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string s = Console.ReadLine() ?? "";
                if (Regex.IsMatch(s, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")) return s.Trim();
                Console.WriteLine("Wprowadź poprawny adres e-mail.");
            }
        }

        public static string GetYesNo(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string s = (Console.ReadLine() ?? "").Trim().ToLower();
                if (s == "t" || s == "n") return s;
                Console.WriteLine("Wpisz 't' (tak) lub 'n' (nie).");
            }
        }
    }
}
