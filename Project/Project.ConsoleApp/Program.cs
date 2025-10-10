using Project.Model;

namespace Project.ConsoleApp
{
    internal class Program
    {
        private static void Main()
        {
            var hotel = new Hotel() { Address = "aa", Name = "bb" };

            Console.WriteLine(hotel);
        }
    }
}
