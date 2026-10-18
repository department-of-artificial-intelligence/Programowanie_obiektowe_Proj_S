
namespace CinemaNetworkManagement;

    internal class Program
    {
        static void Main()
        {

        Employee employee = new Employee(
    1,
    "Jan",
    "Kowalski",
    "Kasjer",
    5000m,
    false);

        Cinema cinema = new Cinema();

        bool wasHired = cinema.HireEmployee(employee);

        Console.WriteLine($"Zatrudniono: {wasHired}");
        Console.WriteLine($"Pracuje: {employee.IsEmployed}");

        bool wasFired = cinema.FireEmployee(employee);

        Console.WriteLine($"Zwolniono: {wasFired}");
        Console.WriteLine($"Pracuje: {employee.IsEmployed}");



    }
    }

