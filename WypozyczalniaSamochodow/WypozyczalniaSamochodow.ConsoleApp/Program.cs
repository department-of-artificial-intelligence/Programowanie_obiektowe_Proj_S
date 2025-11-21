using System;
using System.Collections.Generic;
using WypozyczalniaSamochodow.Model;
using WypozyczalniaSamochodow.Logic;

class Program
{
    static void Main()
    {
        var branches = new List<Branch>();
        var cars = new List<Car>();
        var customers = new List<Customer>();
        var rentals = new List<Rental>();

        var branchLogic = new BranchLogic(branches);
        var carLogic = new CarLogic(cars, branches);
        var customerLogic = new CustomerLogic(customers);
        var rentalLogic = new RentalLogic(rentals, cars, customers, branches);

        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\n=== SYSTEM WYPOŻYCZALNI SAMOCHODÓW ===");
            Console.WriteLine("1. Oddziały");
            Console.WriteLine("2. Samochody");
            Console.WriteLine("3. Klienci");
            Console.WriteLine("4. Wypożyczenia");
            Console.WriteLine("0. Wyjście");
            Console.Write("Wybierz opcję: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    BranchMenu(branchLogic);
                    break;
                case "2":
                    CarMenu(carLogic, branches);
                    break;
                case "3":
                    CustomerMenu(customerLogic);
                    break;
                case "4":
                    RentalMenu(rentalLogic);
                    break;
                case "0":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Nieprawidłowa opcja.");
                    break;
            }
        }
    }

    static void BranchMenu(BranchLogic branchService)
    {
        Console.WriteLine("\n--- MENU ODDZIAŁÓW ---");
        Console.WriteLine("1. Pokaż oddziały");
        Console.WriteLine("2. Dodaj oddział");
        Console.WriteLine("3. Usuń oddział");
        Console.Write("Wybierz opcję: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                branchService.ShowBranches();
                break;
            case "2":
                Console.Write("Podaj nazwę oddziału: ");
                string name = Console.ReadLine();
                Console.Write("Podaj miasto: ");
                string city = Console.ReadLine();
                branchService.AddBranch(name, city);
                break;
            case "3":
                Console.Write("Podaj ID oddziału do usunięcia: ");
                int id = int.Parse(Console.ReadLine());
                branchService.RemoveBranch(id);
                break;
        }
    }

    static void CarMenu(CarLogic carService, List<Branch> branches)
    {
        Console.WriteLine("\n--- MENU SAMOCHODÓW ---");
        Console.WriteLine("1. Pokaż samochody");
        Console.WriteLine("2. Dodaj samochód");
        Console.WriteLine("3. Usuń samochód");
        Console.Write("Wybierz opcję: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                carService.ShowCars();
                break;
            case "2":
                Console.Write("Marka: ");
                string brand = Console.ReadLine();
                Console.Write("Model: ");
                string model = Console.ReadLine();
                Console.Write("Rok produkcji: ");
                int year = int.Parse(Console.ReadLine());

                //Console.WriteLine("\nDostępne oddziały:");
                //branchService.ShowBranches();
                Console.Write("Podaj ID oddziału: ");
                int branchId = int.Parse(Console.ReadLine());

                var car = new Car
                {
                    Brand = brand,
                    Model = model,
                    ProductionYear = year
                };

                carService.AddCar(car, branchId);
                break;

            case "3":
                Console.Write("Podaj ID samochodu do usunięcia: ");
                int id = int.Parse(Console.ReadLine());
                carService.RemoveCar(id);
                break;
        }
    }

    static void CustomerMenu(CustomerLogic customerService)
    {
        Console.WriteLine("\n--- MENU KLIENTÓW ---");
        Console.WriteLine("1. Pokaż klientów");
        Console.WriteLine("2. Dodaj klienta");
        Console.WriteLine("3. Usuń klienta");
        Console.Write("Wybierz opcję: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                customerService.ShowCustomers();
                break;
            case "2":
                Console.Write("Imię: ");
                string firstName = Console.ReadLine();
                Console.Write("Nazwisko: ");
                string lastName = Console.ReadLine();
                Console.Write("Numer prawa jazdy: ");
                string licenseNumber = Console.ReadLine();

                var customer = new Customer
                {
                    FirstName = firstName,
                    LastName = lastName,
                    LicenseNumber = licenseNumber
                };
                customerService.AddCustomer(customer);
                break;

            case "3":
                Console.Write("Podaj ID klienta do usunięcia: ");
                int id = int.Parse(Console.ReadLine());
                customerService.RemoveCustomer(id);
                break;
        }
    }

    static void RentalMenu(RentalLogic rentalService)
    {
        Console.WriteLine("\n--- MENU WYPOŻYCZEŃ ---");
        Console.WriteLine("1. Pokaż wypożyczenia");
        Console.WriteLine("2. Wypożycz samochód");
        Console.WriteLine("3. Zwróć samochód");
        Console.Write("Wybierz opcję: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                rentalService.ShowRentals();
                break;

            case "2":
                Console.Write("Podaj ID samochodu: ");
                int carId = int.Parse(Console.ReadLine());
                Console.Write("Podaj ID klienta: ");
                int customerId = int.Parse(Console.ReadLine());
                Console.Write("Na ile dni wypożyczyć? ");
                int days = int.Parse(Console.ReadLine());

                rentalService.RentCar(carId, customerId, days);
                break;

            case "3":
                Console.Write("Podaj ID wypożyczenia: ");
                int rentalId = int.Parse(Console.ReadLine());
                rentalService.ReturnCar(rentalId);
                break;
        }
    }
}