using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WypozyczalniaSamochodow.DAL;
using WypozyczalniaSamochodow.Extensions;
using WypozyczalniaSamochodow.Model;
using WypozyczalniaSamochodow.Services;

namespace WypozyczalniaSamochodow.ConsoleApp
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;          

            IHost host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
            {
                var connectionString = context.Configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

                services.AddScoped<IBranchService, BranchService>();
                services.AddScoped<ICarService, CarService>();
                services.AddScoped<ICustomerService, CustomerService>();
                services.AddScoped<IRentalService, RentalService>();
                services.AddScoped<IRaportService, RaportService>();
            }).Build();

            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                try
                {
                    context.Database.Migrate();
                    context.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Błąd inicjalizacji bazy danych: {ex.Message}");
                    Console.WriteLine("\nNaciśnij dowolny klawisz aby zakończyć program");
                    Console.ReadKey();
                    return;
                }
            }

            using (var scope = host.Services.CreateScope())
            {
                var branchService = scope.ServiceProvider.GetRequiredService<IBranchService>();
                var carService = scope.ServiceProvider.GetRequiredService<ICarService>();
                var customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();
                var rentalService = scope.ServiceProvider.GetRequiredService<IRentalService>();
                var raportService = scope.ServiceProvider.GetRequiredService<IRaportService>();

                RunApplication(branchService, carService, customerService, rentalService, raportService);
            }
        }

        static void RunApplication(IBranchService branchService, ICarService carService, ICustomerService customerService, IRentalService rentalService, IRaportService raportService)
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                PrintHeader("SYSTEM ZARZĄDZANIA SIECIĄ WYPOŻYCZALNI SAMOCHODÓW");
                Console.WriteLine();
                Console.WriteLine("1. Wybierz oddział");
                Console.WriteLine("2. Dodaj oddział");
                Console.WriteLine("0. Zamknij program");
                Console.WriteLine();
                Console.Write("Wybierz opcję: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        SelectBranch(branchService, carService, customerService, rentalService, raportService);
                        break;
                    case "2":
                        AddBranch(branchService);
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        PrintError("Nieprawidłowa opcja!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        #region BranchMethods

        static void BranchContextMenu(Branch branch, IBranchService branchService, ICarService carService, ICustomerService customerService, IRentalService rentalService, IRaportService raportService)
        {
            bool exitBranch = false;

            while (!exitBranch)
            {
                Console.Clear();
                PrintHeader($"MENU ODDZIAŁU - {branch.Name} ({branch.City})");
                Console.WriteLine();
                Console.WriteLine("1. 🚗 Zarządzanie samochodami");
                Console.WriteLine("2. 👥 Zarządzanie klientami");
                Console.WriteLine("3. 📋 Zarządzanie wypożyczeniami");
                Console.WriteLine("4. 📈 Raporty i statystyki");
                Console.WriteLine("5. ✏️  Edytuj dane oddziału");
                Console.WriteLine("6. 🗑️  Usuń oddział");
                Console.WriteLine("0. ⬅️  Powrót do głównego menu");
                Console.WriteLine();
                Console.Write("Wybierz opcję: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CarMenu(carService, branch);
                        break;
                    case "2":
                        CustomerMenu(customerService, branch);
                        break;
                    case "3":
                        RentalMenu(rentalService, customerService, carService, branch);
                        break;
                    case "4":
                        ShowRaportsAndStatistics(raportService, branch);
                        break;
                    case "5":
                        EditBranch(branchService, branch);
                        try
                        {
                            branch = branchService.GetBranchById(branch.Id);
                        }
                        catch (Exception ex)
                        {
                            PrintError(ex.Message);
                            Console.ReadKey();
                        }
                        break;
                    case "6":
                        if (ConfirmAction("Czy na pewno chcesz usunąć ten oddział?"))
                        {
                            try
                            {
                                branchService.RemoveBranch(branch.Id);
                                PrintSuccess("✓ Oddział został usunięty!");
                                Console.ReadKey();
                                exitBranch = true;
                            }
                            catch (Exception ex)
                            {
                                PrintError($"Błąd: {ex.Message}");
                                Console.ReadKey();
                            }
                        }
                        break;
                    case "0":
                        exitBranch = true;
                        break;
                    default:
                        PrintError("Nieprawidłowa opcja!");
                        Console.ReadKey();
                        break;
                }
            }
        }
      
        static void SelectBranch(IBranchService branchService, ICarService carService, ICustomerService customerService, IRentalService rentalService, IRaportService raportService)
        {
            var branches = branchService.GetAllBranches().ToList();
            if (branches.Count == 0)
            {
                Console.Clear();
                PrintWarning("Brak oddziałów w systemie. Dodaj pierwszy oddział.");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            PrintHeader("LISTA ODDZIAŁÓW:");
            Console.WriteLine();

            foreach (var branch in branches)
            {
                Console.WriteLine(branch);
            }

            Console.Write("Podaj ID oddziału: ");
            if (int.TryParse(Console.ReadLine(), out int branchId))
            {
                try
                {
                    var selectedBranch = branchService.GetBranchById(branchId);
                    BranchContextMenu(selectedBranch, branchService, carService, customerService, rentalService, raportService);
                }
                catch (Exception ex)
                {
                    PrintError(ex.Message);
                    Console.ReadKey();
                }
            }
            else
            {
                PrintError("Nieprawidłowe ID");
                Console.ReadKey();
            }
        }

        static void AddBranch(IBranchService branchService)
        {
            Console.Clear();
            PrintHeader("DODAWANIE ODDZIAŁU");
            Console.WriteLine();

            Console.Write("Nazwa: ");
            string? name = Console.ReadLine();

            Console.Write("Miasto: ");
            string? city = Console.ReadLine();

            Console.Write("Adres: ");
            string? address = Console.ReadLine();

            Console.Write("Numer kontaktowy: ");
            string? contactNumber = Console.ReadLine();

            var branch = new Branch
            {
                Name = name ?? "",
                City = city ?? "",
                Address = address ?? "",
                ContactNumber = contactNumber ?? ""
            };

            try
            {
                branchService.AddBranch(branch);
                PrintSuccess("✓ Oddział został dodany!");
            }
            catch (Exception ex)
            {
                PrintError($"Błąd: {ex.Message}");
            }

            Console.ReadKey();
        }

        static void EditBranch(IBranchService branchService, Branch branch)
        {
            Console.Clear();
            PrintHeader("EDYCJA ODDZIAŁU");
            Console.WriteLine();
            Console.WriteLine("(Zostaw puste aby zachować obecną wartość)");
            Console.WriteLine();

            try
            {
                Console.Write($"Nazwa [{branch.Name}]: ");
                string? name = Console.ReadLine();

                Console.Write($"Miasto [{branch.City}]: ");
                string? city = Console.ReadLine();

                Console.Write($"Adres [{branch.Address}]: ");
                string? address = Console.ReadLine();

                Console.Write($"Telefon [{branch.ContactNumber}]: ");
                string? contactNumber = Console.ReadLine();

                var updatedBranch = new Branch
                {
                    Id = branch.Id,
                    Name = string.IsNullOrWhiteSpace(name) ? branch.Name : name,
                    City = string.IsNullOrWhiteSpace(city) ? branch.City : city,
                    Address = string.IsNullOrWhiteSpace(address) ? branch.Address : address,
                    ContactNumber = string.IsNullOrWhiteSpace(contactNumber) ? branch.ContactNumber : contactNumber
                };

                branchService.UpdateBranch(updatedBranch);

                PrintSuccess("✓ Oddział został zaktualizowany!");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                PrintError($"Błąd: {ex.Message}");
                Console.ReadKey();
            }
        }

        static void ShowRaportsAndStatistics(IRaportService reportService, Branch branch)
        {
            Console.Clear();
            PrintHeader($"RAPORTY I STATYSTYKI - {branch.Name} ({branch.City})");
            Console.WriteLine();

            var totalRevenue = reportService.GetTotalRevenue(branch.Id);
            Console.WriteLine($"💰 Łączny przychód oddziału: {totalRevenue:C}\n");

            var mostRentedCar = reportService.GetMostRentedCar(branch.Id);
            Console.WriteLine($"🚘 Najczęściej wypożyczany samochód: {(mostRentedCar != null ? $"[{mostRentedCar.Id}] {mostRentedCar.Brand} {mostRentedCar.Model} ({mostRentedCar.ProductionYear})" : "brak danych")}\n");

            var avgDailyRevenue = reportService.GetAverageDailyRevenue(branch.Id);
            Console.WriteLine($"💵 Średni dzienny przychód oddziału: {avgDailyRevenue:C}\n");

            var bestCustomer = reportService.GetBestCustomer(branch.Id);
            Console.WriteLine($"🙎 Najlepszy klient: {(bestCustomer != null ? $"[{bestCustomer.Id}] {bestCustomer.FirstName} {bestCustomer.LastName}" : "brak danych")}\n");

            Console.ReadKey();
        }

        #endregion

        #region CarMethods

        static void CarMenu(ICarService carService, Branch branch)
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                PrintHeader($"ZARZĄDZANIE SAMOCHODAMI ODDZIAŁU - {branch.Name} ({branch.City})");
                Console.WriteLine();
                Console.WriteLine("1. 📋 Pokaż wszystkie samochody");
                Console.WriteLine("2. ➕ Dodaj samochód");
                Console.WriteLine("3. ✏️  Edytuj samochód");
                Console.WriteLine("4. 🗑️  Usuń samochód");
                Console.WriteLine("5. 📅 Zobacz rezerwacje samochodu");
                Console.WriteLine("6. 🔍 Wyszukaj samochód");
                Console.WriteLine("0. ⬅️  Powrót");
                Console.WriteLine();
                Console.Write("Wybierz opcję: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowCars(carService, branch);
                        Console.ReadKey();
                        break;
                    case "2":
                        AddCar(carService, branch);
                        break;
                    case "3":
                        EditCar(carService, branch);
                        break;
                    case "4":
                        RemoveCar(carService, branch);
                        break;
                    case "5":
                        ShowCarReservations(carService, branch);
                        Console.ReadKey();
                        break;
                    case "6":
                        SearchCarMenu(carService, branch);
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        PrintError("Nieprawidłowa opcja!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void ShowCars(ICarService carService, Branch branch)
        {
            var cars = carService.GetCarByBranch(branch.Id).ToList();

            Console.Clear();
            PrintHeader($"LISTA SAMOCHODÓW - {branch.Name} ({branch.City})");
            Console.WriteLine();

            if (cars.Count == 0)
            {
                PrintWarning("Brak samochodów w tym oddziale.");
                return;
            }

            foreach (var car in cars)
            {
                Console.WriteLine(car);
            }
        }

        static void AddCar(ICarService carService, Branch branch)
        {
            Console.Clear();
            PrintHeader("DODAWANIE NOWEGO SAMOCHODU");
            Console.WriteLine();

            try
            {
                Console.Write("Marka: ");
                string? brand = Console.ReadLine();
                Console.Write("Model: ");
                string? model = Console.ReadLine();
                Console.Write("Rok produkcji: "); 
                string? yearStr = Console.ReadLine(); 
                if (!int.TryParse(yearStr, out int productionYear)) { 
                    PrintError("Nieprawidłowy format roku produkcji"); 
                    Console.ReadKey(); 
                    return; 
                }
                Console.Write("Moc (KM): "); 
                string? powerStr = Console.ReadLine(); 
                if (!int.TryParse(powerStr, out int power)) { 
                    PrintError("Nieprawidłowy format mocy"); 
                    Console.ReadKey(); 
                    return; 
                }
                Console.Write("Pojemność silnika (L): "); 
                string? engineVolumeStr = Console.ReadLine(); 
                if (!double.TryParse(engineVolumeStr, out double engineVolume)) { 
                    PrintError("Nieprawidłowy format pojemności silnika"); 
                    Console.ReadKey(); 
                    return; 
                }
                Console.Write("Średnie spalanie (L/100km): "); 
                string? avgConsumptionStr = Console.ReadLine(); 
                if (!double.TryParse(avgConsumptionStr, out double avgConsumption)) { 
                    PrintError("Nieprawidłowy format średniego spalania"); 
                    Console.ReadKey(); 
                    return; 
                }
                Console.Write("Skrzynia biegów (automatyczna/manualna): ");
                string? gearbox = Console.ReadLine();
                Console.Write("Typ paliwa (benzyna/diesel): ");
                string? fuelType = Console.ReadLine();
                Console.Write("Cena za dzień (PLN): "); 
                string? pricePerDayStr = Console.ReadLine(); 
                if (!decimal.TryParse(pricePerDayStr, out decimal pricePerDay)) { 
                    PrintError("Nieprawidły format ceny!"); 
                    Console.ReadKey(); 
                    return; 
                }

                var car = new Car
                {
                    Brand = brand ?? "",
                    Model = model ?? "",
                    ProductionYear = productionYear,
                    Power = power,
                    EngineVolume = engineVolume,
                    AvgConsumption = avgConsumption,
                    Gearbox = gearbox ?? "",
                    FuelType = fuelType ?? "",
                    PricePerDay = pricePerDay
                };

                carService.AddCar(car, branch);
                PrintSuccess("✓ Samochód został pomyślnie dodany!");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                PrintError($"Błąd: {ex.Message}");
                Console.ReadKey();
            }
        }

        static void EditCar(ICarService carService, Branch branch)
        {
            var cars = carService.GetCarByBranch(branch.Id).ToList();
            if (cars.Count == 0)
            {
                Console.Clear();
                PrintWarning("Brak samochodów do edycji w tym oddziale.");
                Console.ReadKey();
                return;
            }

            ShowCars(carService, branch);
            Console.Write("Podaj ID samochodu do edycji: ");

            if (!int.TryParse(Console.ReadLine(), out int carId))
            {
                PrintError("Nieprawidłowe ID samochodu");
                Console.ReadKey();
                return;
            }

            var car = cars.FirstOrDefault(c => c.Id == carId);
            if (car == null)
            {
                PrintError("Samochód o takim ID nie istnieje");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            PrintHeader("EDYCJA SAMOCHODU");
            Console.WriteLine();
            Console.WriteLine("(Zostaw puste aby zachować obecną wartość)");
            Console.WriteLine();

            try
            {
                Console.Write($"Marka [{car.Brand}]: ");
                string? brand = Console.ReadLine();

                Console.Write($"Model [{car.Model}]: ");
                string? model = Console.ReadLine();

                Console.Write($"Rok produkcji [{car.ProductionYear}]: ");
                string? productionYearStr = Console.ReadLine();
                int productionYear = car.ProductionYear;
                if (!string.IsNullOrWhiteSpace(productionYearStr) && !int.TryParse(productionYearStr, out productionYear))
                {
                    PrintError("Nieprawidłowy format roku produkcji");
                    Console.ReadKey();
                    return;
                }

                Console.Write($"Moc [{car.Power} KM]: ");
                string? powerStr = Console.ReadLine();
                int power = car.Power;
                if (!string.IsNullOrWhiteSpace(powerStr) && !int.TryParse(powerStr, out power))
                {
                    PrintError("Nieprawidłowy format mocy");
                    Console.ReadKey();
                    return;
                }

                Console.Write($"Pojemność silnika [{car.EngineVolume} L]: ");
                string? engineVolumeStr = Console.ReadLine();
                double engineVolume = car.EngineVolume;
                if (!string.IsNullOrWhiteSpace(engineVolumeStr) && !double.TryParse(engineVolumeStr, out engineVolume))
                {
                    PrintError("Nieprawidłowy format pojemności silnika");
                    Console.ReadKey();
                    return;
                }

                Console.Write($"Średnie spalanie [{car.AvgConsumption} L/100km]: ");
                string? consStr = Console.ReadLine();
                double avgConsumption = car.AvgConsumption;
                if (!string.IsNullOrWhiteSpace(consStr) && !double.TryParse(consStr, out avgConsumption))
                {
                    PrintError("Nieprawidłowy format średniego spalania");
                    Console.ReadKey();
                    return;
                }

                Console.Write($"Typ skrzyni biegów [{car.Gearbox}]: ");
                string? gearbox = Console.ReadLine();

                Console.Write($"Typ paliwa [{car.FuelType}]: ");
                string? fuel = Console.ReadLine();

                Console.Write($"Cena za dzień [{car.PricePerDay} PLN]: ");
                string? pricePerDayStr = Console.ReadLine();
                decimal pricePerDay = car.PricePerDay;
                if (!string.IsNullOrWhiteSpace(pricePerDayStr) && !decimal.TryParse(pricePerDayStr, out pricePerDay))
                {
                    PrintError("Nieprawidłowy format ceny");
                    Console.ReadKey();
                    return;
                }

                var updatedCar = new Car
                {
                    Id = car.Id,
                    BranchId = car.BranchId,
                    Brand = string.IsNullOrWhiteSpace(brand) ? car.Brand : brand,
                    Model = string.IsNullOrWhiteSpace(model) ? car.Model : model,
                    ProductionYear = productionYear,
                    Power = power,
                    EngineVolume = engineVolume,
                    AvgConsumption = avgConsumption,
                    Gearbox = string.IsNullOrWhiteSpace(gearbox) ? car.Gearbox : gearbox,
                    FuelType = string.IsNullOrWhiteSpace(fuel) ? car.FuelType : fuel,
                    PricePerDay = pricePerDay,
                    IsAvailable = car.IsAvailable
                };

                carService.UpdateCar(updatedCar);

                PrintSuccess("✓ Samochód został zaktualizowany!");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                PrintError($"Błąd: {ex.Message}");
                Console.ReadKey();
            }
        }

        static void RemoveCar(ICarService carService, Branch branch)
        {
            var cars = carService.GetCarByBranch(branch.Id).ToList();
            if (cars.Count == 0)
            {
                Console.Clear();
                PrintWarning("Brak samochodów do usunięcia w tym oddziale.");
                Console.ReadKey();
                return;
            }

            ShowCars(carService, branch);
            Console.Write("Podaj ID samochodu do usunięcia: ");

            if (!int.TryParse(Console.ReadLine(), out int carId))
            {
                PrintError("Nieprawidłowe ID samochodu");
                Console.ReadKey();
                return;
            }

            if (ConfirmAction("Czy na pewno chcesz usunąć ten samochód?"))
            {
                try
                {
                    carService.RemoveCar(carId, branch.Id);
                    PrintSuccess("✓ Samochód został usunięty!");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    PrintError($"Błąd: {ex.Message}");
                    Console.ReadKey();
                }
            }
        }

        static void SearchCarMenu(ICarService carService, Branch branch)
        {
            Console.Clear();
            PrintHeader("WYSZUKIWANIE SAMOCHODÓW");
            Console.WriteLine();

            Console.Write("Marka (opcjonalnie): ");
            string? brand = Console.ReadLine();

            Console.Write("Minimalna moc (KM, opcjonalnie): ");
            string? powerStr = Console.ReadLine();
            int? minPower = string.IsNullOrWhiteSpace(powerStr) ? null : int.Parse(powerStr);

            Console.Write("Maksymalna cena za dzień (PLN, opcjonalnie): ");
            string? priceStr = Console.ReadLine();
            decimal? maxPrice = string.IsNullOrWhiteSpace(priceStr) ? null : decimal.Parse(priceStr);

            Console.Write("Typ skrzyni biegów (np. automatyczna/manualna, opcjonalnie): ");
            string? gearbox = Console.ReadLine();

            var results = carService.SearchCars(branch.Id, brand, minPower, maxPrice, gearbox).ToList();

            Console.Clear();
            PrintHeader("WYNIKI WYSZUKIWANIA");
            Console.WriteLine();

            if (results.Count == 0)
            {
                PrintWarning("Brak samochodów spełniających kryteria.");
                Console.ReadKey();
                return;
            }

            foreach (var car in results)
            {
                Console.WriteLine(car.ToString());
                Console.WriteLine();
            }

            Console.ReadKey();
        }

        static void ShowCarReservations(ICarService carService, Branch branch)
        {
            var cars = carService.GetCarByBranch(branch.Id).ToList();
            if (cars.Count == 0)
            {
                Console.Clear();
                PrintWarning("Brak samochodów w tym oddziale.");
                return;
            }

            ShowCars(carService, branch);
            Console.Write("Podaj ID samochodu do sprawdzenia: ");

            if (!int.TryParse(Console.ReadLine(), out int carId))
            {
                PrintError("Nieprawidłowe ID samochodu");
                return;
            }

            var car = cars.FirstOrDefault(c => c.Id == carId);
            if (car == null)
            {
                PrintError("Samochód o takim ID nie istnieje");
                return;
            }

            Console.Clear();
            PrintHeader($"REZERWACJE: {car.Brand} {car.Model}");
            Console.WriteLine();

            var rentals = car.Rentals.ActiveRentals().OrderBy(r => r.StartDate).ToList();
            if (rentals.Count == 0)
            {
                PrintWarning("Brak aktywnych rezerwacji dla tego samochodu.");
                return;
            }

            foreach (var rental in rentals)
            {
                Console.WriteLine(rental);
            }
        }

        #endregion

        #region CustomerMethods

        static void CustomerMenu(ICustomerService customerService, Branch branch)
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                PrintHeader($"ZARZĄDZANIE KLIENTAMI ODDZIAŁU - {branch.Name} ({branch.City})");
                Console.WriteLine();
                Console.WriteLine("1. 📋 Pokaż wszystkich klientów");
                Console.WriteLine("2. ➕ Dodaj klienta");
                Console.WriteLine("3. ✏️  Edytuj klienta");
                Console.WriteLine("4. 🗑️  Usuń klienta");
                Console.WriteLine("5. 🔍 Wyszukaj klienta");
                Console.WriteLine("0. ⬅️  Powrót");
                Console.WriteLine();
                Console.Write("Wybierz opcję: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowCustomers(customerService, branch);
                        Console.ReadKey();
                        break;
                    case "2":
                        AddCustomer(customerService, branch);
                        break;
                    case "3":
                        EditCustomer(customerService, branch);
                        break;
                    case "4":
                        RemoveCustomer(customerService, branch);
                        break;
                    case "5":
                        SearchCustomerMenu(customerService, branch);
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        PrintError("Nieprawidłowa opcja!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void ShowCustomers(ICustomerService customerService, Branch branch)
        {
            var customers = customerService.GetCustomerByBranch(branch.Id).ToList();

            Console.Clear();
            PrintHeader($"LISTA KLIENTÓW - {branch.Name} ({branch.City})");
            Console.WriteLine();

            if (customers.Count == 0)
            {
                PrintWarning("Brak klientów w tym oddziale.");
                return;
            }

            foreach (var customer in customers)
            {
                Console.WriteLine(customer);
            }
        }

        static void AddCustomer(ICustomerService customerService, Branch branch)
        {
            Console.Clear();
            PrintHeader("DODAWANIE NOWEGO KLIENTA");
            Console.WriteLine();

            try
            {
                Console.Write("Imię: ");
                string? firstName = Console.ReadLine();
                Console.Write("Nazwisko: ");
                string? lastName = Console.ReadLine();
                Console.Write("Numer prawa jazdy: ");
                string? licenseNumber = Console.ReadLine();
                Console.Write("Email: ");
                string? email = Console.ReadLine();
                Console.Write("Numer telefonu: ");
                string? phone = Console.ReadLine();

                var customer = new Customer
                {
                    FirstName = firstName ?? "",
                    LastName = lastName ?? "",
                    LicenseNumber = licenseNumber ?? "",
                    Email = email ?? "",
                    PhoneNumber = phone ?? ""
                };

                customerService.AddCustomer(customer, branch.Id);
                PrintSuccess("✓ Klient został pomyślnie dodany!");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                PrintError($"Błąd: {ex.Message}");
                Console.ReadKey();
            }
        }

        static void EditCustomer(ICustomerService customerService, Branch branch)
        {
            var customers = customerService.GetCustomerByBranch(branch.Id).ToList();
            if (customers.Count == 0)
            {
                Console.Clear();
                PrintWarning("Brak klientów do edycji w tym oddziale.");
                Console.ReadKey();
                return;
            }

            ShowCustomers(customerService, branch);

            Console.Write("Podaj ID klienta do edycji: ");
            if (!int.TryParse(Console.ReadLine(), out int customerId))
            {
                PrintError("Nieprawidłowe ID!");
                Console.ReadKey();
                return;
            }

            var customer = customers.FirstOrDefault(c => c.Id == customerId);
            if (customer == null)
            {
                PrintError("Klient nie istnieje!");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            PrintHeader("EDYCJA KLIENTA");
            Console.WriteLine();
            Console.WriteLine("(Zostaw puste aby zachować obecną wartość)");
            Console.WriteLine();

            try
            {
                Console.Write($"Imię [{customer.FirstName}]: ");
                string? firstName = Console.ReadLine();

                Console.Write($"Nazwisko [{customer.LastName}]: ");
                string? lastName = Console.ReadLine();

                Console.Write($"Numer prawa jazdy [{customer.LicenseNumber}]: ");
                string? licenseNumber = Console.ReadLine();

                Console.Write($"Email [{customer.Email}]: ");
                string? email = Console.ReadLine();

                Console.Write($"Telefon [{customer.PhoneNumber}]: ");
                string? phoneNumber = Console.ReadLine();

                var updatedCustomer = new Customer
                {
                    Id = customer.Id,
                    BranchId = customer.BranchId,
                    FirstName = string.IsNullOrWhiteSpace(firstName) ? customer.FirstName : firstName,
                    LastName = string.IsNullOrWhiteSpace(lastName) ? customer.LastName : lastName,
                    LicenseNumber = string.IsNullOrWhiteSpace(licenseNumber) ? customer.LicenseNumber : licenseNumber,
                    Email = string.IsNullOrWhiteSpace(email) ? customer.Email : email,
                    PhoneNumber = string.IsNullOrWhiteSpace(phoneNumber) ? customer.PhoneNumber : phoneNumber,
                    LoyaltyPoints = customer.LoyaltyPoints
                };

                customerService.UpdateCustomer(updatedCustomer);

                PrintSuccess("✓ Klient został zaktualizowany!");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                PrintError($"Błąd: {ex.Message}");
                Console.ReadKey();
            }
        }

        static void RemoveCustomer(ICustomerService customerService, Branch branch)
        {
            var customers = customerService.GetCustomerByBranch(branch.Id).ToList();
            if (customers.Count == 0)
            {
                Console.Clear();
                PrintWarning("Brak klientów do usunięcia w tym oddziale.");
                Console.ReadKey();
                return;
            }

            ShowCustomers(customerService, branch);
            Console.Write("Podaj ID klienta do usunięcia: ");

            if (!int.TryParse(Console.ReadLine(), out int customerId))
            {
                PrintError("Nieprawidłowe ID klienta");
                Console.ReadKey();
                return;
            }

            if (ConfirmAction("Czy na pewno chcesz usunąć tego klienta?"))
            {
                try
                {
                    customerService.RemoveCustomer(customerId, branch.Id);
                    PrintSuccess("✓ Klient został usunięty!");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    PrintError($"Błąd: {ex.Message}");
                    Console.ReadKey();
                }
            }
        }

        static void SearchCustomerMenu(ICustomerService customerService, Branch branch)
        {
            Console.Clear();
            PrintHeader("WYSZUKIWANIE KLIENTÓW");
            Console.WriteLine();

            Console.Write("Nazwisko (opcjonalnie): ");
            string? lastName = Console.ReadLine();

            Console.Write("Numer prawa jazdy (opcjonalnie): ");
            string? licenseNumber = Console.ReadLine();

            Console.Write("Minimalna liczba punktów lojalnościowych (opcjonalnie): ");
            string? pointsStr = Console.ReadLine();
            int? minPoints = string.IsNullOrWhiteSpace(pointsStr) ? null : int.Parse(pointsStr);

            var results = customerService.SearchCustomers(branch.Id, lastName, licenseNumber, minPoints).ToList();

            Console.Clear();
            PrintHeader("WYNIKI WYSZUKIWANIA");
            Console.WriteLine();

            if (results.Count == 0)
            {
                PrintWarning("Brak klientów spełniających kryteria.");
                Console.ReadKey();
                return;
            }

            foreach (var customer in results)
            {
                Console.WriteLine(customer);
            }

            Console.ReadKey();
        }

        #endregion

        #region RentalMethods

        static void RentalMenu(IRentalService rentalService, ICustomerService customerService, ICarService carService, Branch branch)
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                PrintHeader($"ZARZĄDZANIE WYPOŻYCZENIAMI ODDZIAŁU - {branch.Name} ({branch.City})");
                Console.WriteLine();
                Console.WriteLine("1. 📋 Pokaż aktywne wypożyczenia");
                Console.WriteLine("2. ➕ Wypożycz samochód");
                Console.WriteLine("3. ✅ Zwrot samochodu / Anulowanie rezerwacji");
                Console.WriteLine("4. 📜 Pokaż historie wypożyczeń");
                Console.WriteLine("5. ⭐ Wypożycz za punkty");
                Console.WriteLine("0. ⬅️  Powrót");
                Console.WriteLine();
                Console.Write("Wybierz opcję: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowActiveRentals(rentalService, branch);
                        Console.ReadKey();
                        break;
                    case "2":
                        RentCar(rentalService, customerService, carService, branch);
                        break;
                    case "3":
                        ReturnCar(rentalService, branch);
                        break;
                    case "4":
                        ShowRentalsHistory(rentalService, branch);
                        break;
                    case "5":
                        RentCarWithPoints(rentalService, customerService, carService, branch);
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        PrintError("Nieprawidłowa opcja!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void ShowActiveRentals(IRentalService rentalService, Branch branch)
        {
            var rentals = rentalService.GetActiveRentals(branch.Id).ToList();

            Console.Clear();
            PrintHeader($"LISTA AKTYWNYCH WYPOŻYCZEŃ - {branch.Name} ({branch.City})");
            Console.WriteLine();

            if (rentals.Count == 0)
            {
                PrintWarning("Brak aktywnych wypożyczeń.");
                return;
            }

            foreach (var rental in rentals)
            {
                Console.WriteLine(rental);
            }
        }

        static void RentCar(IRentalService rentalService, ICustomerService customerService, ICarService carService, Branch branch)
        {
            var carsAll = carService.GetCarByBranch(branch.Id).ToList();
            var customersAll = customerService.GetCustomerByBranch(branch.Id).ToList();

            if (carsAll.Count == 0 || customersAll.Count == 0) {
                Console.Clear();
                PrintWarning("Brak samochodów lub klientów w systemie. Musisz ich dodać aby wypożyczyć samochód"); 
                Console.ReadKey(); 
                return; 
            }  

            Console.Clear();
            PrintHeader("NOWE WYPOŻYCZENIE");
            Console.WriteLine();

            Console.Write("Data rozpoczęcia (DD.MM.YYYY): ");
            string? startInput = Console.ReadLine();
            if (!DateTime.TryParseExact(startInput, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out DateTime startDate))
            {
                PrintError("Nieprawidłowa data rozpoczęcia! Użyj formatu DD.MM.YYYY");
                Console.ReadKey();
                return;
            }

            if (startDate.Date < DateTime.Today)
            {
                PrintError("Data rozpoczęcia nie może być w przeszłości.");
                Console.ReadKey();
                return;
            }

            Console.Write("Data zakończenia (DD.MM.YYYY): ");
            string? endInput = Console.ReadLine();
            if (!DateTime.TryParseExact(endInput, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out DateTime endDate))
            {
                PrintError("Nieprawidłowa data zakończenia! Użyj formatu DD.MM.YYYY");
                Console.ReadKey();
                return;
            }

            if (endDate.Date <= startDate.Date)
            {
                PrintError("Data zakończenia musi być późniejsza niż rozpoczęcia!");
                Console.ReadKey();
                return;
            }

            var activeRentals = rentalService.GetActiveRentals(branch.Id).ToList(); 
            var cars = carsAll
                .Where(c => !activeRentals
                .Any(r => r.CarId == c.Id && r.StartDate < endDate && r.EndDate > startDate))
                .ToList();

            if (cars.Count == 0)
            {
                Console.WriteLine();
                PrintWarning("Brak dostępnych samochodów w podanym okresie.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine();
            Console.WriteLine("DOSTĘPNE SAMOCHODY W WYBRANYM TERMINIE:");
            foreach (var c in cars) Console.WriteLine(c);

            Console.WriteLine();
            Console.Write("Podaj ID samochodu: ");
            string? carInput = Console.ReadLine();
            if (!int.TryParse(carInput, out int carId))
            {
                PrintError("Nieprawidłowe ID samochodu");
                Console.ReadKey();
                return;
            }

            var car = cars.FirstOrDefault(c => c.Id == carId);
            if (car is null)
            {
                PrintError("Samochodu o podanym ID nie ma na liście");
                Console.ReadKey();
                return;
            }

            var customers = customerService.GetCustomerByBranch(branch.Id).ToList();
            if (customers.Count == 0)
            {
                PrintWarning("Brak klientów w tym oddziale.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine();
            Console.WriteLine("LISTA KLIENTÓW:");
            foreach (var cust in customers) Console.WriteLine(cust);

            Console.WriteLine();
            Console.Write("Podaj ID klienta: ");
            string? customerInput = Console.ReadLine();
            if (!int.TryParse(customerInput, out int customerId))
            {
                PrintError("Nieprawidłowe ID klienta!");
                Console.ReadKey();
                return;
            }

            var customer = customers.FirstOrDefault(c => c.Id == customerId);
            if (customer is null)
            {
                PrintError("Klient o podanym ID nie istnieje!");
                Console.ReadKey();
                return;
            }

            int days = (endDate.Date - startDate.Date).Days;
            decimal cost = days * car.PricePerDay;

            var rental = new Rental
            {
                CarId = carId,
                CustomerId = customerId,
                BranchId = branch.Id,
                StartDate = startDate.Date,
                EndDate = endDate.Date,
                Days = days,
                Cost = cost,
                IsCompleted = false
            };

            rentalService.RentCar(rental);

            Console.WriteLine();
            PrintSuccess("✓ Wypożyczenie zostało utworzone.");
            Console.WriteLine($"  Okres: {days} dni | Koszt: {cost:C}");

            Console.ReadKey();
        }

        static void ReturnCar(IRentalService rentalService, Branch branch)
        {
            var rentals = rentalService.GetActiveRentals(branch.Id).ToList();
            if (rentals.Count == 0)
            {
                Console.Clear();
                PrintWarning("Brak aktywnych wypożyczeń do zwrotu.");
                Console.ReadKey();
                return;
            }

            ShowActiveRentals(rentalService, branch);
            Console.Write("\nPodaj ID wypożyczenia do zwrotu: ");

            if (!int.TryParse(Console.ReadLine(), out int rentalId))
            {
                PrintError("Nieprawidłowe ID!");
                Console.ReadKey();
                return;
            }

            try
            {
                rentalService.ReturnCar(rentalId, branch.Id);
                PrintSuccess("✓ Samochód został pomyślnie zwrócony!");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                PrintError($"Błąd: {ex.Message}");
                Console.ReadKey();
            }
        }

        static void ShowRentalsHistory(IRentalService rentalService, Branch branch)
        {
            Console.Clear();
            PrintHeader($"HISTORIA WYPOŻYCZEŃ - {branch.Name} ({branch.City})");
            Console.WriteLine();

            var rentals = rentalService.GetCompletedRentals(branch.Id).ToList();
            if (rentals.Count == 0)
            {
                PrintWarning("Brak zakończonych wypożyczeń.");
                Console.ReadKey(); 
                return;
            }

            foreach (var rental in rentals)
            {
                Console.WriteLine(rental);
            }

            Console.ReadKey(); 
        }

        static void RentCarWithPoints(IRentalService rentalService, ICustomerService customerService, ICarService carService, Branch branch)
        {
            var cars = carService.GetCarByBranch(branch.Id).Where(c => c.IsAvailable).ToList();
            var customers = customerService.GetCustomerByBranch(branch.Id).ToList();

            if (cars.Count == 0 || customers.Count == 0)
            {
                Console.Clear();
                PrintWarning("Nie można utworzyć wypożyczenia. Brak dostępnych samochodów lub klientów.");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            PrintHeader("WYPOŻYCZENIE ZA PUNKTY");
            Console.WriteLine();

            Console.WriteLine("LISTA KLIENTÓW:");
            foreach (var c in customers)
            {
                Console.WriteLine(c);
            }

            Console.Write("Podaj ID klienta: ");
            string? customerInput = Console.ReadLine();

            if (!int.TryParse(customerInput, out int customerId))
            {
                PrintError("Nieprawidłowe ID klienta");
                Console.ReadKey();
                return;
            }

            var customer = customers.FirstOrDefault(c => c.Id == customerId);
            if (customer is null)
            {
                PrintError("Klient o podanym ID nie istnieje");
                Console.ReadKey();
                return;
            }


            Console.WriteLine();
            Console.WriteLine("DOSTĘPNE SAMOCHODY:");
            foreach (var c in cars)
            {
                Console.WriteLine(c);
            }

            Console.WriteLine();
            Console.Write("Podaj ID samochodu: ");
            string? carInput = Console.ReadLine();

            if (!int.TryParse(carInput, out int carId))
            {
                PrintError("Nieprawidłowe ID samochodu");
                Console.ReadKey();
                return;
            }

            var car = cars.FirstOrDefault(c => c.Id == carId);
            if (car is null)
            {
                PrintError("Samochodu o podanym ID nie ma na liście");
                Console.ReadKey();
                return;
            }

            try
            {
                var rentalForPoints = new Rental
                {
                    CarId = carId,
                    CustomerId = customerId,
                    BranchId = branch.Id,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(1),
                    Days = 1,
                    Cost = 0,
                    IsCompleted = false
                };

                rentalService.RentCarWithPoints(rentalForPoints);

                Console.WriteLine();
                PrintSuccess("✓ Wypożyczenie zostało utworzone za punkty!");
                Console.WriteLine("  Okres: 1 dzień | Koszt: 0 zł | Punkty: -20");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                PrintError($"Błąd: {ex.Message}");
                Console.ReadKey();
            }
        }

        #endregion

        #region HelperMethods

        static void PrintHeader(string title)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"===== {title} =====");
            Console.ResetColor();
        }

        static void PrintSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  {message}");
            Console.ResetColor();
        }

        static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n  ❌ {message}");
            Console.ResetColor();
        }

        static void PrintWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  ⚠️  {message}");
            Console.ResetColor();
        }

        static bool ConfirmAction(string message)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"  ⚠️  {message} (T/N): ");
            Console.ResetColor();

            string? response = Console.ReadLine()?.ToUpper();
            return response == "T" || response == "TAK";
        }

        #endregion
    }
}