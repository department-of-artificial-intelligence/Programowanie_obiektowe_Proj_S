namespace Project.ConsoleApp
{

    using Project.Model;

    internal class Program
    {
        static void Main(string[] args)
        {

            PierwszaKlasa klas = new PierwszaKlasa();

            Person person = new Person() { FirstName = "Jan", LastName = "Kowalski", Age = 11 };

            person.Age = 12;

            //Console.WriteLine($"{person.LastName}");

            //Console.WriteLine("Hello, World!");

            Console.WriteLine(person);
        }
    }
}
