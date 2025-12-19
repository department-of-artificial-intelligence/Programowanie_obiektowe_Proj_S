using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.Abstractions;
using Project.DAL;
using Project.Logic;
using Project.Model;
using Project.Service;

namespace Project
{
    public class Program
    {
        static DriverService _driverService = null!;
        static VehicleService _vehicleService = null!;
        static OrderService _orderService = null!;

        public static void Main(string[] args)
        {
            // Dependency Injection:
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(
                            context.Configuration.GetConnectionString("DefaultConnection")));

                    // Within one scope, the program will use the same instance of a given website:
                    services.AddScoped<DriverService>();
                    services.AddScoped<VehicleService>();
                    services.AddScoped<OrderService>();
                })
                .Build();

            using var scope = host.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            _driverService = scope.ServiceProvider.GetRequiredService<DriverService>();
            _vehicleService = scope.ServiceProvider.GetRequiredService<VehicleService>();
            _orderService = scope.ServiceProvider.GetRequiredService<OrderService>();

            InitializeDatabase(context);
            MainMenu();
        }

        // ===================== MAIN MENU =====================

        static void MainMenu()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== FLEET MANAGEMENT SYSTEM ===");
                Console.WriteLine("1. Drivers (Manage and Assign Vehicles)");
                Console.WriteLine("2. Vehicles (Manage and Check Wear)");
                Console.WriteLine("3. Orders (Manage and Assign to Drivers)");
                Console.WriteLine("0. Exit");
                Console.Write("Choose option: ");

                switch (Console.ReadLine())
                {
                    case "1": DriverMenu(); break;
                    case "2": VehicleMenu(); break;
                    case "3": OrderMenu(); break;
                    case "0": running = false; break;
                    default: Pause("Invalid option"); break;
                }
            }
        }

        // ===================== DRIVER MENU =====================

        static void DriverMenu()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("--- DRIVERS ---");
                Console.WriteLine("1. Show all drivers (with statistics)");
                Console.WriteLine("2. Add driver");
                Console.WriteLine("3. Delete driver");
                Console.WriteLine("4. Show Available Drivers Only [Filter]");
                Console.WriteLine("5. Show Drivers Sorted by Last Name [Sort]");
                Console.WriteLine("6. Assign Vehicle to Driver");
                Console.WriteLine("7. Unassign Vehicle from Driver");
                Console.WriteLine("0. Back");
                Console.Write("Choose: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        var drivers = _driverService.GetDriversWithVehicles();
                        drivers.PrintDriverStatistics();
                        foreach (var d in drivers) Console.WriteLine(d);
                        Pause();
                        break;

                    case "2":
                        Console.Write("First name: "); string fn = Console.ReadLine()!;
                        Console.Write("Last name: "); string ln = Console.ReadLine()!;
                        Console.Write("License number: "); string lic = Console.ReadLine()!;

                        _driverService.Add(new Driver
                        {
                            FirstName = fn,
                            LastName = ln,
                            LicenseNumber = lic
                        });
                        _driverService.Save();
                        Pause("Driver added");
                        break;

                    case "3":
                        Console.Write("Driver ID to delete: ");
                        if (int.TryParse(Console.ReadLine(), out int did))
                        {
                            var d = _driverService.GetById(did);
                            if (d != null)
                            {
                                _driverService.Delete(d);
                                _driverService.Save();
                                Pause("Driver deleted");
                            }
                        }
                        break;

                    case "4":
                        var available = _driverService.GetDriversWithVehicles().FilterAvailable();
                        Console.WriteLine("\n--- Available Drivers ---");
                        foreach (var d in available) Console.WriteLine(d);
                        Pause();
                        break;

                    case "5":
                        var sorted = _driverService.GetDriversWithVehicles().SortByLastName();
                        Console.WriteLine("\n--- Drivers Sorted by Last Name ---");
                        foreach (var d in sorted) Console.WriteLine(d);
                        Pause();
                        break;

                    case "6":
                        AssignVehicleToDriverUI();
                        break;

                    case "7":
                        UnassignVehicleFromDriverUI();
                        break;

                    case "0": back = true; break;
                }
            }
        }

        static void AssignVehicleToDriverUI()
        {
            Console.Write("Enter Driver ID: ");
            if (!int.TryParse(Console.ReadLine(), out int driverId)) return;

            var driver = _driverService.GetDriversWithVehicles().FirstOrDefault(d => d.Id == driverId);
            if (driver == null) { Pause("Driver not found."); return; }

            if (!driver.IsAvailable) { Pause("Driver is currently busy or already has a vehicle."); return; }

            Console.Write("Enter Vehicle ID to assign: ");
            if (!int.TryParse(Console.ReadLine(), out int vehicleId)) return;

            var vehicle = _vehicleService.GetById(vehicleId);
            if (vehicle == null) { Pause("Vehicle not found."); return; }

            try
            {
                driver.AssignVehicle(vehicle);

                vehicle.AssignDriver(driver);

                _driverService.Update(driver);
                _vehicleService.Update(vehicle);
                _driverService.Save();
                _vehicleService.Save();

                Pause($"Success! Vehicle {vehicle.Brand} assigned to {driver.FirstName} {driver.LastName}.");
            }
            catch (Exception ex)
            {
                Pause($"Error assigning vehicle: {ex.Message}");
            }
        }

        static void UnassignVehicleFromDriverUI()
        {
            Console.Write("Enter Driver ID: ");
            if (!int.TryParse(Console.ReadLine(), out int driverId)) return;

            var driver = _driverService.GetDriversWithVehicles().FirstOrDefault(d => d.Id == driverId);
            if (driver == null) { Pause("Driver not found."); return; }

            if (driver.AssignedVehicle == null) { Pause("Driver does not have a vehicle."); return; }

            var vehicle = driver.AssignedVehicle;

            driver.MarkAsAvailable();
            vehicle.MarkAsAvailable();

            _driverService.Update(driver);
            _vehicleService.Update(vehicle);
            _driverService.Save();
            _vehicleService.Save();

            Pause("Vehicle unassigned. Driver is now available.");
        }

        // ===================== VEHICLE MENU =====================
        static void VehicleMenu()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("--- VEHICLES ---");
                Console.WriteLine("1. Show all vehicles (with stats)");
                Console.WriteLine("2. Add vehicle (Car, Truck, Van, Trailer)");
                Console.WriteLine("3. Delete vehicle");
                Console.WriteLine("4. Show Vehicles Sorted by Mileage [Sort]");
                Console.WriteLine("5. Calculate Wear Rate for a Vehicle");
                Console.WriteLine("0. Back");
                Console.Write("Choose: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        var vehicles = _vehicleService.GetAll();
                        vehicles.PrintVehicleStatistics();
                        foreach (var v in vehicles) Console.WriteLine(v);
                        Pause();
                        break;

                    case "2":
                        AddVehicleUI();
                        break;

                    case "3":
                        Console.Write("Vehicle ID to delete: ");
                        if (int.TryParse(Console.ReadLine(), out int vid))
                        {
                            var v = _vehicleService.GetById(vid);
                            if (v != null)
                            {
                                _vehicleService.Delete(v);
                                _vehicleService.Save();
                                Pause("Vehicle deleted");
                            }
                        }
                        break;

                    case "4":
                        var sortedVehicles = _vehicleService.GetAll().SortByMileageDescending();
                        Console.WriteLine("\n--- Vehicles Sorted by Mileage (High to Low) ---");
                        foreach (var v in sortedVehicles) Console.WriteLine(v);
                        Pause();
                        break;

                    case "5":
                        CalculateWearRateUI();
                        break;

                    case "0": back = true; break;
                }
            }
        }

        static void AddVehicleUI()
        {
            Console.Clear();
            Console.WriteLine("--- ADD NEW VEHICLE ---");
            Console.WriteLine("Select vehicle type:");
            Console.WriteLine("1. Company Car");
            Console.WriteLine("2. Delivery Van");
            Console.WriteLine("3. Truck");
            Console.WriteLine("4. Semi-Trailer");
            Console.Write("Choice: ");
            string typeChoice = Console.ReadLine()!;

            Console.WriteLine("\n-- Common Data --");
            Console.Write("Brand: "); string brand = Console.ReadLine()!;
            Console.Write("Model: "); string model = Console.ReadLine()!;
            Console.Write("Registration Number: "); string reg = Console.ReadLine()!;
            Console.Write("VIN: "); string vin = Console.ReadLine()!;

            Console.Write("Production Year: ");
            int.TryParse(Console.ReadLine(), out int year);
            if (year == 0) year = DateTime.Now.Year;

            Console.Write("Engine Size (e.g. 2,0): ");
            float.TryParse(Console.ReadLine(), out float engine);

            Console.Write("Mileage (km): ");
            int.TryParse(Console.ReadLine(), out int mileage);

            Vehicle? newVehicle = null;

            Console.WriteLine("\n-- Specific Data --");

            switch (typeChoice)
            {
                case "1": // CompanyCar
                    Console.Write("Number of Seats: ");
                    int.TryParse(Console.ReadLine(), out int seats);

                    newVehicle = new CompanyCar
                    {
                        NumberOfSeats = seats
                    };
                    break;

                case "2": // DeliveryVan
                    Console.Write("Max Volume (m³): ");
                    float.TryParse(Console.ReadLine(), out float vol);

                    newVehicle = new DeliveryVan
                    {
                        MaxVolumeCubicMeters = vol
                    };
                    break;

                case "3": // Truck
                    Console.Write("Max Payload (kg): ");
                    int.TryParse(Console.ReadLine(), out int payload);

                    newVehicle = new Truck
                    {
                        MaxPayLoadKg = payload
                    };
                    break;

                case "4": // SemiTrailer
                    Console.Write("Max Gross Weight (tons): ");
                    float.TryParse(Console.ReadLine(), out float weight);

                    newVehicle = new SemiTrailer
                    {
                        MaxGrossWeightTons = weight
                    };
                    break;

                default:
                    Pause("Invalid vehicle type selected.");
                    return;
            }

            if (newVehicle != null)
            {
                newVehicle.Brand = brand;
                newVehicle.Model = model;
                newVehicle.RegistrationNumber = reg;
                newVehicle.VinNumber = vin;
                newVehicle.ProductionYear = year;
                newVehicle.EngineSize = engine;
                newVehicle.Mileage = mileage;
                newVehicle.VStatus = VehicleStatus.Available;

                _vehicleService.Add(newVehicle);
                _vehicleService.Save();
                Pause($"{newVehicle.VType} added successfully!");
            }
        }

        static void CalculateWearRateUI()
        {
            Console.Write("Enter Vehicle ID: ");
            if (int.TryParse(Console.ReadLine(), out int vid))
            {
                var v = _vehicleService.GetById(vid);
                if (v != null)
                {
                    float wear = v.CalculateWearRate();
                    Console.WriteLine($"\nVehicle: {v.Brand} {v.Model} ({v.VType})");
                    Console.WriteLine($"Current Mileage: {v.Mileage} km");
                    Console.WriteLine($"Calculated Wear Rate: {wear * 100:F2}%");
                    Pause();
                }
                else
                {
                    Pause("Vehicle not found.");
                }
            }
        }

        // ===================== ORDER MENU =====================

        static void OrderMenu()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("--- ORDERS ---");
                Console.WriteLine("1. Show all orders (with stats)");
                Console.WriteLine("2. Add order");
                Console.WriteLine("3. Delete order");
                Console.WriteLine("4. Assign Order to Driver]");
                Console.WriteLine("5. Show Pending Orders Only [Filter]");
                Console.WriteLine("6. Sort Orders by Loading Address [Sort]");
                Console.WriteLine("0. Back");
                Console.Write("Choose: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        var orders = _orderService.GetAll();
                        orders.PrintOrderStatusSummary();
                        foreach (var o in orders) Console.WriteLine(o);
                        Pause();
                        break;

                    case "2":
                        Console.Write("Description: "); string desc = Console.ReadLine()!;
                        Console.Write("Loading address: "); string la = Console.ReadLine()!;
                        Console.Write("Unloading address: "); string ua = Console.ReadLine()!;

                        _orderService.Add(new Order(0, desc, la, ua));
                        _orderService.Save();
                        Pause("Order added");
                        break;

                    case "3":
                        Console.Write("Order ID to delete: ");
                        if (int.TryParse(Console.ReadLine(), out int oid))
                        {
                            var o = _orderService.GetById(oid);
                            if (o != null)
                            {
                                _orderService.Delete(o);
                                _orderService.Save();
                                Pause("Order deleted");
                            }
                        }
                        break;

                    case "4":
                        AssignOrderUI();
                        break;

                    case "5":
                        var pending = _orderService.GetAll().FilterByStatus(OrderStatus.Pending);
                        Console.WriteLine("\n--- Pending Orders ---");
                        foreach (var o in pending) Console.WriteLine(o);
                        Pause();
                        break;

                    case "6":
                        var sortedOrders = _orderService.GetAll().SortByLoadingAddress();
                        Console.WriteLine("\n--- Orders Sorted by Loading Address ---");
                        foreach (var o in sortedOrders) Console.WriteLine(o);
                        Pause();
                        break;

                    case "0": back = true; break;
                }
            }
        }

        static void AssignOrderUI()
        {
            Console.WriteLine("--- Assign Order to Driver ---");

            Console.Write("Enter Order ID (must be Pending): ");
            if (!int.TryParse(Console.ReadLine(), out int orderId)) return;

            var order = _orderService.GetById(orderId);
            if (order == null) { Pause("Order not found."); return; }

            Console.Write("Enter Driver ID (must have a vehicle): ");
            if (!int.TryParse(Console.ReadLine(), out int driverId)) return;

            var driver = _driverService.GetDriversWithVehicles().FirstOrDefault(d => d.Id == driverId);

            if (driver == null) { Pause("Driver not found."); return; }

            try
            {
                Console.WriteLine("\nProcessing assignment logic...");
                order.AssignOrder(driver);

                _orderService.Update(order);
                _driverService.Update(driver);
                _orderService.Save();
                Pause();
            }
            catch (Exception ex)
            {
                Pause($"System Error: {ex.Message}");
            }
        }

        // ===================== HELPERS =====================

        static void Pause(string? msg = null)
        {
            if (msg != null)
                Console.WriteLine(msg);

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
        }

        // ===================== INIT =====================

        static void InitializeDatabase(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var car = new CompanyCar { VinNumber = "WVGZZZ3CZLE092209", Brand = "Skoda", Model = "Octavia", RegistrationNumber = "KR1234", VStatus = VehicleStatus.Available, Mileage = 150000, ProductionYear = 2019, EngineSize = 2.0f, NumberOfSeats = 5 };
            var truck = new Truck { VinNumber = "VF38ERHHADL004544", Brand = "Volvo", Model = "FH16", RegistrationNumber = "WA9999", VStatus = VehicleStatus.Available, Mileage = 500000, ProductionYear = 2018, EngineSize = 6.2f, MaxPayLoadKg = 24000 };

            context.CompanyCars.Add(car);
            context.Trucks.Add(truck);

            context.Drivers.AddRange(
                new Driver { FirstName = "John", LastName = "Smith", LicenseNumber = "A123" },
                new Driver { FirstName = "Jane", LastName = "Doe", LicenseNumber = "A124" },
                new Driver { FirstName = "Bob", LastName = "Taylor", LicenseNumber = "A125" }
            );

            context.Orders.Add(new Order(0, "Electronics", "Warsaw", "Krakow"));
            context.Orders.Add(new Order(0, "Furniture", "Gdansk", "Poznan"));

            context.SaveChanges();
        }
    }
}