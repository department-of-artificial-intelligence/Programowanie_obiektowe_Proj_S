namespace Project.ConsoleApp;

internal static class ConsoleHelper
{
    internal static string UserInput(string? prompt = null)
    {
        string? userInput = null;
        while (string.IsNullOrWhiteSpace(userInput))
        {
            Console.Write("> " + prompt);
            userInput = Console.ReadLine();
        }
        return userInput!;
    }

    internal static T GetById<T>(IEnumerable<T> source, Func<T, int> idSelector, string prompt) where T : class
    {
        T? result = null;
        int id;

        while (result is null)
        {
            var input = UserInput(prompt);

            if (!int.TryParse(input, out id))
            {
                Console.WriteLine("To nie jest liczba");
                continue;
            }

            result = source.FirstOrDefault(x => idSelector(x) == id);

            if (result is null)
            {
                Console.WriteLine("Nieprawidłowe ID");
            }
        }
        return result;
    }
}
