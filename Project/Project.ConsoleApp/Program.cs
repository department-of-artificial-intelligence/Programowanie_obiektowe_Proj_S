using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Project.DAL;
using Project.Model;
using Project.Logic;
using Project.Service;

namespace Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost _host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
            {
                var cns = context.Configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(cns));
            }).Build();

            using var scope = _host.Services.CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            if (context != null)
            {
                InitializeDatabase(context);

                bool running = true;
                while (running)
                {
                    Console.Clear();
                    Console.WriteLine("==============================================");
                    Console.WriteLine("   FLEET MANAGEMENT SYSTEM ");
                    Console.WriteLine("==============================================");
                    Console.WriteLine("1. Driver List");
                    Console.WriteLine("2. Fleet Statistics");
                    Console.WriteLine("3. Order Summary");
                    Console.WriteLine("0. Quit");
                    Console.WriteLine("==============================================");
                    Console.Write("Select an option: ");

                    string? input = Console.ReadLine();
                    switch (input)
                    {
                        case "1":
                            ShowDrivers(context);
                            break;
                        case "2":
                            var vehicles = context.Set<Vehicle>().ToList();
                            vehicles.PrintVehicleStatistics();
                            break;
                        case "3":
                            context.Orders.ToList().PrintOrderStatusSummary();
                            break;
                        case "0":
                            running = false;
                            break;
                        default:
                            Console.WriteLine("Invalid selection.");
                            break;
                    }

                    if (running)
                    {
                        Console.WriteLine("\nPress any key to return to the menu...");
                        Console.ReadKey();
                    }
                }
            }
        }

        private static void ShowDrivers(ApplicationDbContext context)
        {
            var drivers = context.Drivers.Include(d => d.AssignedVehicle).ToList();
            Console.WriteLine("\n--- DRIVERS ---");
            drivers.ForEach(d => Console.WriteLine(d));
        }

        private static void InitializeDatabase(ApplicationDbContext context)
        {
            Console.WriteLine("Database initializing...");
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Driver driver1 = new Driver() { FirstName = "John", LastName = "Smith", LicenseNumber = "A123" };
            Driver driver2 = new Driver() { FirstName = "Jane", LastName = "Doe", LicenseNumber = "A124" };
            Driver driver3 = new Driver() { FirstName = "Anna", LastName = "Taylor", LicenseNumber = "A125" };
            Driver driver4 = new Driver() { FirstName = "Adam", LastName = "Williams", LicenseNumber = "A126" };
            context.Drivers.AddRange(driver1, driver2, driver3, driver4);

            CompanyCar car1 = new CompanyCar(0, "VIN1", 2022, 1.6f, 10000, "Skoda", "Octavia", "W0 1", 5);
            Truck truck1 = new Truck(0, "VIN_TR_002", 2020, 12.0f, 250000, "Volvo", "FH16", "W0 99887", 24000);
            DeliveryVan van1 = new DeliveryVan(0, "VIN_DV_003", 2021, 2.3f, 80000, "Renault", "Master", "W0 55443", 12.5f);
            SemiTrailer trailer1 = new SemiTrailer(0, "VIN_ST_004", 2019, 0, 120000, "Schmitz", "Cargobull", "W0 11223", 35.0f);
            context.CompanyCars.Add(car1);
            context.Trucks.Add(truck1);
            context.DeliveryVans.Add(van1);
            context.SemiTrailers.Add(trailer1);
            context.SaveChanges();

            driver1.AssignVehicle(car1);
            driver2.AssignVehicle(truck1);
            driver3.AssignVehicle(van1);
            context.SaveChanges();

            Order order1 = new Order(0, "Electronics for wholesalers", "Warszawa, ul. Prosta 1", "Kraków, ul. Zawiła 10");
            Order order2 = new Order(0, "Building materials", "Gdańsk, Portowa 5", "Wrocław, Fabryczna 2");

            context.Orders.Add(order1);
            context.Orders.Add(order2);
            context.SaveChanges();

            Console.WriteLine("Database initialized successfully.");
        }
    }
}

