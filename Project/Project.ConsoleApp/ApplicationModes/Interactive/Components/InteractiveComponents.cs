namespace Project.ConsoleApp.ApplicationModes.Interactive.Components
{
    public static class InteractiveComponents
    {
        public static bool YesNoPrompt(string prompt)
        {
            Console.Write($"{prompt} (y/n): ");
            var input = Console.ReadLine();
            return input?.ToLower() == "y";
        }
        
        public static T ValuePrompt<T>(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                var input = Console.ReadLine();

                try
                {
                    return (T)Convert.ChangeType(input, typeof(T))!;
                }
                catch
                {
                    Console.WriteLine("Invalid input. Please enter a valid value.");
                }
            }
        }
        
        public static T SelectPrompt<T>(string prompt, Dictionary<string, T> options)
        {
            Console.WriteLine(prompt);
            
            for (int i = 1; i <= options.Count; i++)
            {
                var optionKey = options.Keys.ElementAt(i - 1);
                Console.WriteLine($"{i}. {optionKey}");
            }
            
            while (true)
            {
                Console.Write("Select an option by number: ");
                
                if (int.TryParse(Console.ReadLine(), out var selection) &&
                    selection > 0 &&
                    selection <= options.Count)
                {
                    var selectedKey = options.Keys.ElementAt(selection - 1);
                    return options[selectedKey];
                }

                Console.WriteLine("Invalid selection. Please try again.");
            }
        }
        
        public static void PressAnyKeyToContinue()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}