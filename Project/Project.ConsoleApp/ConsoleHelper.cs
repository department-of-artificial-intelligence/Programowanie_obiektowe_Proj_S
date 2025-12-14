using Microsoft.Identity.Client;
using Project.Model;
using System.Globalization;
using System.Net;

namespace Project.ConsoleApp;

internal static class ConsoleHelper
{
    internal static string UserInput(string? prompt = null)
    {
        string? userInput;
        while (true)
        {
            Console.Write("> " + prompt);
            userInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine("Nic nie podano");
            } 
            else
            {
                break;
            }
        }
        return userInput;
    }
    internal static int UserInputInt(string? prompt = null)
    {
        int intValue;
        while (true)
        {
            string input = UserInput(prompt);
            if (!int.TryParse(input, out intValue))
            {
                Console.WriteLine("Niepoprawnie podano liczbę (int)");
            }
            else
            {
                break;
            }
        }
        return intValue;
    }
    internal static decimal UserInputDecimal(string? prompt = null)
    {
        decimal decimalValue;
        while (true)
        {
            string input = UserInput(prompt);
            if (!decimal.TryParse(input, out decimalValue))
            {
                Console.WriteLine("Niepoprawnie podano liczbę (decimal)");
            }
            else
            {
                break;
            }
        }
        return decimalValue;
    }
    internal static DateTime UserInputDateTime(string? prompt = null)
    {
        DateTime dateTimeValue;
        while (true)
        {
            string input = UserInput(prompt + " (format:dd.MM.yyyy HH:mm): ");
            if (!DateTime.TryParseExact(input, 
                "dd.MM.yyyy HH:mm", 
                CultureInfo.InvariantCulture, 
                DateTimeStyles.None, 
                out dateTimeValue))
            {
                Console.WriteLine("Niepoprawna data");
            }
            else
            {
                break;
            }
        }
        return dateTimeValue;
    }
    internal static bool UserInputBool(string? prompt = null)
    {
        bool boolValue = false;
        while (true)
        {
            string input = UserInput(prompt + " (tak/nie): ");
            if (input == "tak" || input == "TAK" || input == "Tak")
            {
                boolValue = true;
                break;
            }
            if (input == "nie" || input == "NIE" || input == "Nie")
            {
                boolValue = false;
                break;
            }
            Console.WriteLine("Niepoprawny wybór");
        }
        return boolValue;
    }
    internal static T GetById<T>(IEnumerable<T> source, Func<T, int> idSelector, string prompt) where T : class
    {
        T? result;
        while (true)
        {
            int id = UserInputInt(prompt);

            result = source.FirstOrDefault(x => idSelector(x) == id);

            if (result is null)
            {
                Console.WriteLine("Nieprawidłowe ID");
            }
            else
            {
                break;
            }
        }
        return result;
    }
}
