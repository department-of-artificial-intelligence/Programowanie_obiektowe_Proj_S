/*PierwszaKlasa pk = new PierwszaKlasa();

Person p1 = new Project.Model.Person() { FirstName = "Jan", LastName = "Kowalski", Age = 40 };
Console.WriteLine($"{p1.FirstName} {p1.LastName} {p1.Age}");*/

using System;

namespace Project.Model
{
    public class Program
    {
        static void Main(string[] args)
        {
            Driver driver = new Driver (120550, "John", "Smith");

            Vehicle vehicle = new Vehicle(120550, "4Y1SL65848Z411439", 2020, 2.0f, 255000, "Kia", "Sportage", "SCZ F329");

            vehicle.VType = VehicleType.CompanyCar;

            Console.WriteLine("=== Vehicle data after creation ===");
            Console.WriteLine(vehicle);

            vehicle.AssignDriver(driver);
            Console.WriteLine("\n=== After assigning a driver ===");
            Console.WriteLine(vehicle);

            vehicle.MarkAsAvailable();
            Console.WriteLine("\n=== After marked as available ===");
            Console.WriteLine(vehicle);
        }
    }
}


