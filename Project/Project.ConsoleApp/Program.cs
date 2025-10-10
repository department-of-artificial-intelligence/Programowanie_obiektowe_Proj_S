using Project.Model;

namespace Project.ConsoleApp
{
    internal class Program
    {
        private static void Main()
        {
            var person = new Person()
            {
                FirstName = "Jan",
                LastName = "Kowalski",
                Age = 40
            };

            Console.WriteLine(person);

            Console.WriteLine(ProjectProperties.Text);
        }
    }
}
