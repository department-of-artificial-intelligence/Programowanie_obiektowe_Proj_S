using System;

namespace Project.ConsoleApp.Helpers
{
    public static class ConsoleHelper
    {
        public static void WaitForKey()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        public static string ReadRequiredString(string fieldName)
        {
            while (true)
            {
                string? input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input.Trim();
                }

                Console.Write($"{fieldName} cannot be empty. Please enter {fieldName.ToLower()}: ");
            }
        }

        public static uint ReadUInt(uint defaultValue = 0)
        {
            while (true)
            {
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input) && defaultValue != 0)
                {
                    return defaultValue;
                }

                if (uint.TryParse(input, out uint result))
                {
                    return result;
                }

                Console.Write("Please enter a valid positive number: ");
            }
        }

        public static int ReadInt(int defaultValue = 0)
        {
            while (true)
            {
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input) && defaultValue != 0)
                {
                    return defaultValue;
                }

                if (int.TryParse(input, out int result))
                {
                    return result;
                }

                Console.Write("Please enter a valid number: ");
            }
        }

        public static double ReadDouble()
        {
            while (true)
            {
                string? input = Console.ReadLine();

                if (double.TryParse(input, out double result))
                {
                    return result;
                }

                Console.Write("Please enter a valid number: ");
            }
        }

        public static decimal ReadDecimal()
        {
            while (true)
            {
                string? input = Console.ReadLine();

                if (decimal.TryParse(input, out decimal result))
                {
                    return result;
                }

                Console.Write("Please enter a valid decimal number: ");
            }
        }

        public static DateTime ReadDateTime()
        {
            while (true)
            {
                string? input = Console.ReadLine();

                if (DateTime.TryParse(input, out DateTime result))
                {
                    return result;
                }

                Console.Write("Please enter a valid date (yyyy-mm-dd) or date with time (yyyy-mm-dd hh:mm): ");
            }
        }

        public static bool ReadBoolean()
        {
            while (true)
            {
                string? input = Console.ReadLine();

                if (bool.TryParse(input, out bool result))
                {
                    return result;
                }

                if (input?.ToLower() == "true" || input == "1" || input?.ToLower() == "yes")
                    return true;

                if (input?.ToLower() == "false" || input == "0" || input?.ToLower() == "no")
                    return false;

                Console.Write("Please enter 'true' or 'false': ");
            }
        }
    }
}