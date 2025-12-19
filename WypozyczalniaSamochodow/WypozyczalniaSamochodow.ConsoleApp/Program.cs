using WypozyczalniaSamochodow.Model;
using WypozyczalniaSamochodow.Logic;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using WypozyczalniaSamochodow.DAL;
using Microsoft.EntityFrameworkCore;

/*IHost _host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
{
    var cns = context.Configuration.GetConnectionString("DefaultConnection");
    services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(cns));
}).Build();

var context = _host.Services.GetService<ApplicationDbContext>();
if(context is not null)
{
    context.Database.Migrate();
    context.Database.EnsureCreated();
    Branch branch = new Branch() { Name = "Rentals", City = "Częstochowa", Address="Dąbrowskiego 1", ContactNumber="111222333" };
    context.Branches.Add(branch);
    context.SaveChanges();
}*/

namespace WypozyczalniaSamochodow.ConsoleApp
{
    internal class Program
    {
        static void Main()
        {
            var branches = new List<Branch>();
            var rentals = new List<Rental>();
            var customers = new List<Customer>();
            var cars = new List<Car>();

            var branchLogic = new BranchLogic(branches);
            var carLogic = new CarLogic(cars);
            var customerLogic = new CustomerLogic(customers);
            var rentalLogic = new RentalLogic();

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n=== SYSTEM ZARZĄDZANIA SIECIĄ WYPOŻYCZALNI SAMOCHODÓW ===");
                Console.WriteLine("1. Wybierz oddział");
                Console.WriteLine("2. Dodaj oddział");
                Console.WriteLine("0. Zamknij program");
                Console.Write("Wybierz opcję: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        if (!branchLogic.HasBranches())
                        {
                            Console.WriteLine("\nBrak oddziałów do wyboru. Dodaj pierwszy oddział.");
                            break;
                        }

                        Console.WriteLine("\nLista oddziałów:");
                        branchLogic.ShowBranches();
                        Console.Write("Podaj ID oddziału: ");
                        if (int.TryParse(Console.ReadLine(), out int branchId))
                        {
                            var selectedBranch = branches.FirstOrDefault(b => b.Id == branchId);
                            if (selectedBranch != null)
                            {
                                BranchContextMenu(selectedBranch, branchLogic, carLogic, customerLogic, rentalLogic);
                            }
                            else
                            {
                                Console.WriteLine("Nie znaleziono oddziału.");
                            }
                        }
                        break;

                    case "2":
                        Console.WriteLine("\nWprowadź dane oddziału:");
                        Console.Write("Nazwa: ");
                        string name = Console.ReadLine();
                        Console.Write("Miasto: ");
                        string city = Console.ReadLine();
                        Console.Write("Adres: ");
                        string address = Console.ReadLine();
                        Console.Write("Numer kontaktowy: ");
                        string contactNumber = Console.ReadLine();

                        branchLogic.AddBranch(name, city, address, contactNumber);
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

        static void BranchContextMenu(Branch branch, BranchLogic branchLogic, CarLogic carLogic, CustomerLogic customerLogic, RentalLogic rentalLogic)
        {
            bool exitBranch = false;

            while (!exitBranch)
            {
                Console.WriteLine($"\n=== MENU ODDZIAŁU {branch.Name} ({branch.City}) ===");
                Console.WriteLine("1. Samochody");
                Console.WriteLine("2. Klienci");
                Console.WriteLine("3. Wypożyczenia");
                Console.WriteLine("4. Usuń oddział");
                Console.WriteLine("0. Powrót do głównego menu");
                Console.Write("Wybierz opcję: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CarMenu(carLogic, branch);
                        break;
                    case "2":
                        CustomerMenu(customerLogic, branch);
                        break;
                    case "3":
                        RentalMenu(rentalLogic, customerLogic, carLogic, branch);
                        break;
                    case "4":
                        branchLogic.RemoveBranch(branch.Id);
                        exitBranch = true;
                        break;
                    case "0":
                        exitBranch = true;
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowa opcja.");
                        break;
                }
            }
        }

        static void CarMenu(CarLogic carLogic, Branch branch)
        {
            Console.WriteLine($"\n--- MENU SAMOCHODÓW {branch.Name} ({branch.City}) ---");
            Console.WriteLine("1. Pokaż samochody");
            Console.WriteLine("2. Dodaj samochód");
            Console.WriteLine("3. Usuń samochód");
            Console.Write("Wybierz opcję: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    carLogic.ShowCars(branch);
                    break;
                case "2":
                    Console.WriteLine("\nWprowadź dane pojazdu:");
                    Console.Write("Marka: ");
                    string brand = Console.ReadLine();
                    Console.Write("Model: ");
                    string model = Console.ReadLine();
                    Console.Write("Rok produkcji: ");
                    int year = int.Parse(Console.ReadLine());
                    Console.Write("Moc (KM): ");
                    int power = int.Parse(Console.ReadLine());
                    Console.Write("Pojemność silnika (l): ");
                    double engineVolume = double.Parse(Console.ReadLine());
                    Console.Write("Średnie spalanie (l/100km): ");
                    double avgConsump = double.Parse(Console.ReadLine());
                    Console.Write("Skrzynia biegów (automatyczna/manualna): ");
                    string gearbox = Console.ReadLine();
                    Console.Write("Typ paliwa (benzyna/diesel): ");
                    string fuelType = Console.ReadLine();
                    Console.Write("Cena (zł)/dzień: ");
                    double price = double.Parse(Console.ReadLine());

                    var car = new Car
                    {
                        Brand = brand,
                        Model = model,
                        ProductionYear = year,
                        Power = power,
                        EngineVolume = engineVolume,
                        AvgConsumption = avgConsump,
                        Gearbox = gearbox,
                        FuelType = fuelType,
                        PricePerDay = price
                    };

                    carLogic.AddCar(car, branch);
                    break;
                case "3":
                    if (!carLogic.HasCars(branch))
                    {
                        Console.WriteLine("Brak samochodów do usunięcia w tym oddziale.");
                        return;
                    }

                    carLogic.ShowCars(branch);
                    Console.Write("Podaj ID samochodu do usunięcia: ");
                    int id = int.Parse(Console.ReadLine());
                    carLogic.RemoveCar(id, branch);
                    break;
            }
        }

        static void CustomerMenu(CustomerLogic customerLogic, Branch branch)
        {
            Console.WriteLine($"\n--- MENU KLIENTÓW {branch.Name} ({branch.City}) ---");
            Console.WriteLine("1. Pokaż klientów");
            Console.WriteLine("2. Dodaj klienta");
            Console.WriteLine("3. Usuń klienta");
            Console.Write("Wybierz opcję: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    customerLogic.ShowCustomers(branch);
                    break;
                case "2":
                    Console.Write("Imię: ");
                    string firstName = Console.ReadLine();
                    Console.Write("Nazwisko: ");
                    string lastName = Console.ReadLine();
                    Console.Write("Numer prawa jazdy: ");
                    string licenseNumber = Console.ReadLine();
                    Console.Write("Email: ");
                    string email = Console.ReadLine();
                    Console.Write("Telefon: ");
                    string phone = Console.ReadLine();

                    var customer = new Customer
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        LicenseNumber = licenseNumber,
                        Email = email,
                        PhoneNumber = phone
                    };
                    customerLogic.AddCustomer(customer, branch);
                    break;
                case "3":
                    if (!customerLogic.HasCustomers(branch))
                    {
                        Console.WriteLine("Brak klientów oddziału do usunięcia");
                        return;
                    }

                    customerLogic.ShowCustomers(branch);
                    Console.Write("Podaj ID klienta do usunięcia: ");
                    int id = int.Parse(Console.ReadLine());
                    customerLogic.RemoveCustomer(id, branch);
                    break;
            }
        }

        static void RentalMenu(RentalLogic rentalLogic, CustomerLogic customerLogic, CarLogic carLogic, Branch branch)
        {
            Console.WriteLine($"\n--- MENU WYPOŻYCZEŃ {branch.Name} ({branch.City}) ---");
            Console.WriteLine("1. Aktywne wypożyczenia");
            Console.WriteLine("2. Wypożycz samochód");
            Console.WriteLine("3. Zwrot samochodu");
            Console.WriteLine("4. Historia wypożyczeń");
            Console.WriteLine("5. Raporty/statystyki");
            Console.Write("Wybierz opcję: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    rentalLogic.ShowRentals(branch);
                    break;
                case "2":
                    if(!carLogic.HasCars(branch) || !customerLogic.HasCustomers(branch))
                    {
                        Console.WriteLine("Nie można utworzyć wypożyczenia. Brak samochodów lub klientów w bazie oddziału.");
                        return;
                    }

                    customerLogic.ShowCustomers(branch);
                    Console.Write("Podaj ID klienta: ");
                    int customerId = int.Parse(Console.ReadLine());

                    carLogic.ShowCars(branch);
                    Console.Write("Podaj ID samochodu: ");
                    int carId = int.Parse(Console.ReadLine());

                    Console.Write("Data rozpoczęcia (DD.MM.YYYY): ");
                    DateTime startDate = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", null);

                    Console.Write("Data zakończenia (DD.MM.YYYY): ");
                    DateTime endDate = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", null);

                    rentalLogic.RentCar(carId, customerId, startDate, endDate, branch);
                    break;
                case "3":
                    if (!rentalLogic.HasRentals(branch))
                    {
                        Console.WriteLine("Brak samochodów do zwrotu w tym oddziale.");
                        return;
                    }

                    rentalLogic.ShowRentals(branch);
                    Console.Write("Podaj ID wypożyczenia: ");
                    int rentalId = int.Parse(Console.ReadLine());
                    rentalLogic.ReturnCar(rentalId, branch);
                    break;
                case "4":
                    rentalLogic.ShowHistory(branch);
                    break;

            }
        }
    }
}
