using CarsRental.Model;

namespace CarsRental.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager();
            CustomerManager customerManager = new CustomerManager();
            DepartmentManager departmentManager = new DepartmentManager();
            ReservationManager reservationManager = new ReservationManager();

            var allCars = carManager.GetAll();
            var allCustomers = customerManager.GetAll();
            var allDepartments = departmentManager.GetAll();
            var allReservations = reservationManager.GetAll();

            departmentManager.Add(new Department(0, "BMW LOVERZZ", "Granicza 55, Wrocław", "+48 123 321 890", "bmwloverzz@bmw.pl"));
            //departmentManager.Add(new Department(0, "BMW LOVERZZ", "Granicza 55, Wrocław", "+48 123 321 890", "bmwloverzz@bmw.pl")); // test; próba ponownego dodania
            //departmentManager.Add(new Department(0, "", "", "", "")); // test; próba dodania pustych pól
            //departmentManager.Add(new Department()); // test; próba dodania pustych pól
            departmentManager.Add(new Department(0, "Luxury Cars", "Poprzeczna 28, Warszawa", "+48 567 321 890", "luxurycars@cars.pl"));

            Department dep1 = allDepartments[0];
            Department dep2 = allDepartments[1];

            carManager.Add(new Car(0, "BMW", "M3", 2022, 3.0, 480, 550, 4.1, "RWD", "Automatyczna", 5, 350.0, "Dostępny", dep1));
            carManager.Add(new Car(0, "Audi", "A6", 2021, 2.0, 245, 370, 6.2, "AWD", "Automatyczna", 5, 280.0, "Dostępny", dep2));
            carManager.Add(new Car(0, "Toyota", "Corolla", 2020, 1.8, 140, 180, 9.5, "FWD", "Manualna", 5, 180.0, "Dostępny", dep2));

            Console.WriteLine("=== Lista wypożyczalni ===\n");
            foreach (Department department in allDepartments)
            {
                Console.WriteLine(department);
            }

            foreach (var departament in allDepartments)
            {
                Console.WriteLine($"\n=== Samochody w wypożyczalni '{departament.Name}' ===\n");

                var carsInDep = carManager.GetByDepartment(departament);

                if (carsInDep.Count == 0)
                    Console.WriteLine("Brak samochodów w tej wypożyczalni.\n");
                else
                    carsInDep.ForEach(c => Console.WriteLine(c));
            }

            customerManager.Add(new Customer(0, "Patryk", "Nowak", "patryk123@123.pl", "+48 123 123 123"));
            customerManager.Add(new Customer(0, "Andrzej", "Buldog", "andrzejbuldog@123.pl", "+48 321 321 321"));

            Console.WriteLine("\n=== Lista klientów wypożyczalni ===\n");
            foreach (Customer customer in allCustomers)
            {
                Console.WriteLine(customer);
            }

            var reservation1 = new Reservation(0, allCustomers[0], allCars[0], DateTime.Today, DateTime.Today.AddDays(5));
            var reservation2 = new Reservation(0, allCustomers[1], allCars[2], DateTime.Today, DateTime.Today.AddDays(3));

            reservationManager.Add(reservation1);
            reservationManager.Add(reservation2);

            Console.WriteLine("\n=== Lista rezerwacji ===\n");
            foreach (Reservation reservation in allReservations)
                Console.WriteLine(reservation);

            Console.WriteLine("\n=== Lista samochodów w wypożyczalniach ===\n");
            foreach (Car car in allCars)
                Console.WriteLine(car);

            Console.WriteLine("\nNaciśnij dowolny klawisz, aby zakończyć...");
            Console.ReadKey();
        }
    }
}