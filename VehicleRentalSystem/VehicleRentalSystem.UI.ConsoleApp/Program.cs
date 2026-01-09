using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Numerics;
using VehicleRentalSystem.DAL;
using VehicleRentalSystem.Model;
using VehicleRentalSystem.Model.Extensions;
using VehicleRentalSystem.Model.Service;
using VehicleRentalSystem.UI;

namespace VehicleRentalSystem
{
    class Program
    {
        static async Task Main()
        {
            IHost host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
                {
                    var connectionString = context.Configuration.GetConnectionString("DefaultConnection");

                    services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

                    services.AddScoped<IVehicleService, VehicleService>();
                    services.AddScoped<ICustomerService, CustomerService>();
                    services.AddScoped<IDepartmentService, DepartmentService>();
                    services.AddScoped<IEmployeeService, EmployeeService>();
                    services.AddScoped<IReservationService, ReservationService>();

                    services.AddTransient<MainMenu>();
                })
                .Build();

            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                try 
                { 
                    await context.Database.EnsureCreatedAsync();
                    Console.WriteLine("Baza danych zainicjalizowana pomyślnie!\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Błąd inicjalizacji bazy danych: {ex.Message}\n");
                }
            }

            using (var scope = host.Services.CreateScope())
            {
                var mainMenu = scope.ServiceProvider.GetRequiredService<MainMenu>();
                await mainMenu.Show();
            }
        }
    }
}

namespace VehicleRentalSystem.UI
{
    public class MainMenu
    {
        private readonly ApplicationDbContext _context;
        private readonly IVehicleService _vehicleService;
        private readonly ICustomerService _customerService;
        private readonly IDepartmentService _departmentService;
        private readonly IEmployeeService _employeeService;
        private readonly IReservationService _reservationService;

        public MainMenu(
            ApplicationDbContext context,
            IVehicleService vehicleService,
            ICustomerService customerService,
            IDepartmentService departmentService,
            IEmployeeService employeeService,
            IReservationService reservationService)
        {
            _context = context;
            _vehicleService = vehicleService;
            _customerService = customerService;
            _departmentService = departmentService;
            _employeeService = employeeService;
            _reservationService = reservationService;
        }

        public async Task Show()
        {
            bool exit = false;

            while (!exit)
            {
                var departments = await _departmentService.GetAllDepartments();

                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════════════╗");
                Console.WriteLine("║          SYSTEM WYPOŻYCZALNI SAMOCHODÓW        ║");
                Console.WriteLine("╚════════════════════════════════════════════════╝\n");

                if (!departments.Any())
                {
                    Console.WriteLine(" [1] Zarządzanie Placówkami");
                    Console.WriteLine(" [0] Zamknij program");
                }
                else
                {
                    Console.WriteLine(" [1] Zarządzanie Placówkami");
                    Console.WriteLine(" [2] Zarządzanie Pracownikami");
                    Console.WriteLine(" [3] Zarządzanie Pojazdami");
                    Console.WriteLine(" [4] Zarządzanie Klientami");
                    Console.WriteLine(" [5] Zarządzanie Rezerwacjami");
                    Console.WriteLine(" [6] Raporty i Statystyki");
                    Console.WriteLine(" [0] Zamknij program");
                }

                Console.WriteLine();
                Console.Write("Wybierz opcję: ");

                var key = Console.ReadKey(true);
                Console.WriteLine();

                switch (key.KeyChar)
                {
                    case '1':
                        await DepartmentMenu();
                        break;
                    case '2':
                        if (!departments.Any())
                        {
                            Console.WriteLine("Nieprawidłowa opcja. Naciśnij dowolny klawisz aby wybrać ponownie...");
                        } 
                        else
                        { 
                            await EmployeeMenu();
                        }
                        break;
                    case '3':
                        if (!departments.Any())
                        {
                            Console.WriteLine("Nieprawidłowa opcja. Naciśnij dowolny klawisz aby wybrać ponownie...");
                        }
                        else
                        {
                            await VehicleMenu();
                        }
                        break;
                    case '4':
                        if (!departments.Any())
                        {
                            Console.WriteLine("Nieprawidłowa opcja. Naciśnij dowolny klawisz aby wybrać ponownie...");
                        }
                        else
                        {
                            await CustomerMenu();
                        }
                        break;
                    case '5':
                        if (!departments.Any())
                        {
                            Console.WriteLine("Nieprawidłowa opcja. Naciśnij dowolny klawisz aby wybrać ponownie...");
                        }
                        else
                        {
                            await ReservationMenu();
                        }
                        break;
                    case '6':
                        if (!departments.Any())
                        {
                            Console.WriteLine("Nieprawidłowa opcja. Naciśnij dowolny klawisz aby wybrać ponownie...");
                        }
                        else
                        {
                            await ReportsMenu();
                        }
                        break;
                    case '0':
                        exit = true;
                        Console.WriteLine("Zamykanie systemu...");
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowa opcja. Naciśnij dowolny klawisz aby wybrać ponownie...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private async Task VehicleMenu()
        {
            bool back = false;

            while (!back)
            {
                var vehicles = await _vehicleService.GetAllVehicles();

                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════════════╗");
                Console.WriteLine("║              ZARZĄDZANIE POJAZDAMI             ║");
                Console.WriteLine("╚════════════════════════════════════════════════╝\n");

                if (!vehicles.Any())
                {
                    Console.WriteLine(" [1] Dodaj pojazd");
                    Console.WriteLine(" [0] Powrót");
                }
                else
                {
                    Console.WriteLine(" [1] Dodaj pojazd");
                    Console.WriteLine(" [2] Wyświetl wszystkie pojazdy");
                    Console.WriteLine(" [3] Wyszukaj pojazd");
                    Console.WriteLine(" [4] Usuń pojazd");
                    Console.WriteLine(" [5] Serwis pojazdu");
                    Console.WriteLine(" [6] Szczegóły pojazdu");
                    Console.WriteLine(" [0] Powrót");
                }
                Console.WriteLine();
                Console.Write("Wybierz opcję: ");

                var key = Console.ReadKey(true);
                Console.WriteLine();

                switch (key.KeyChar)
                {
                    case '1':
                        await AddVehicle();
                        break;
                    case '2':
                        if (!vehicles.Any())
                        {
                            Console.WriteLine("Nieprawidłowa opcja. Naciśnij dowolny klawisz aby wybrać ponownie...");
                        }
                        else
                        {
                            await DisplayAllVehicles();
                        }
                        break;
                    case '3':
                        if (!vehicles.Any())
                        {
                            Console.WriteLine("Nieprawidłowa opcja. Naciśnij dowolny klawisz aby wybrać ponownie...");
                        }
                        else
                        {
                            await SearchVehicles();
                        }
                        break;
                    case '4':
                        if (!vehicles.Any())
                        {
                            Console.WriteLine("Nieprawidłowa opcja. Naciśnij dowolny klawisz aby wybrać ponownie...");
                        }
                        else
                        {
                            await DeleteVehicle();
                        }
                        break;
                    case '5':
                        if (!vehicles.Any())
                        {
                            Console.WriteLine("Nieprawidłowa opcja. Naciśnij dowolny klawisz aby wybrać ponownie...");
                        }
                        else
                        {
                            await ServiceVehicle();
                        }
                        break;
                    case '6':
                        if (!vehicles.Any())
                        {
                            Console.WriteLine("Nieprawidłowa opcja. Naciśnij dowolny klawisz aby wybrać ponownie...");
                        }
                        else
                        {
                            await VehicleDetails();
                        }
                        break;
                    case '0':
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowa opcja.");
                        Pause();
                        break;
                    
                }
            }
        }

        private async Task AddVehicle()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║               DODAWANIE POJAZDU                ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");

            try
            {
                Console.Write("[MARKA]: ");
                var brand = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(brand) || brand.Length < 1 || brand.Length > 50)
                {
                    Console.WriteLine("\n[BŁĄD] Marka musi mieć od 1 do 50 znaków!");
                    Pause();
                    return;
                }

                Console.Write("[MODEL]: ");
                var model = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(model) || model.Length < 1 || model.Length > 50)
                {
                    Console.WriteLine("\n[BŁĄD] Model musi mieć od 1 do 50 znaków!");
                    Pause();
                    return;
                }

                Console.Write("[ROK PRODUKCJI (np. '2020')]: ");
                var year = int.Parse(Console.ReadLine() ?? "2020");
                if (year > DateTime.Now.Year)
                {
                    Console.WriteLine("\n[BŁĄD] Pojazd nie może być wyprodukowany w przyszłości!");
                    Pause();
                    return;
                }

                Console.Write("[NUMER REJESTRACYJNY (np. 'SCZ12345')]: ");
                var regNumber = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(regNumber) || regNumber.Length < 3 || regNumber.Length > 10)
                {
                    Console.WriteLine("\n[BŁĄD] Rejestracja pojazdu musi mieć od 3 do 10 znaków");
                    Pause();
                    return;
                }

                Console.WriteLine("[NAPĘD]");
                Console.WriteLine(" [0] Na tylnie koła");
                Console.WriteLine(" [1] Na przednie koła");
                Console.WriteLine(" [2] Na wszystkie koła");
                Console.Write("Wybierz opcję (0-2): ");
                var inputDrive = Console.ReadLine();
                if (!int.TryParse(inputDrive, out int driveInt) || driveInt < 0 || driveInt > 2)
                {
                    Console.WriteLine("\n[BŁĄD] Wybierz cyfrę z zakresu!");
                    Pause();
                    return;
                }
                var drive = (WheelDrive)driveInt;

                Console.WriteLine("[SKRZYNIA BIEGÓW]");
                Console.WriteLine(" [0] Manualna");
                Console.WriteLine(" [1] Automatyczna");
                Console.WriteLine(" [2] Półautomatyczna");
                Console.Write("Wybierz opcję (0-2): ");
                var inputTransmission = Console.ReadLine();
                if (!int.TryParse(inputTransmission, out int transmissionInt) || transmissionInt < 0 || transmissionInt > 2)
                {
                    Console.WriteLine("\n[BŁĄD] Wybierz cyfrę z zakresu!");
                    Pause();
                    return;
                }
                var transmission = (Transmission)transmissionInt;

                Console.Write("[LICZBA MIEJSC (np. '5')]: ");
                var seats = int.Parse(Console.ReadLine() ?? "5");
                if (seats <= 0 || seats > 100)
                {
                    Console.WriteLine("\n[BŁĄD] Pojazd musi posiadać conajmniej jedno miejsce oraz musi być to realna liczba miejsc!");
                    Pause();
                    return;
                }

                Console.WriteLine("[PALIWO]");
                Console.WriteLine(" [0] Benzyna");
                Console.WriteLine(" [1] Diesel");
                Console.WriteLine(" [2] Elektryczne");
                Console.WriteLine(" [3] Hybrydowe");
                Console.Write("Wybierz opcję (0-3): ");
                var inputFuel = Console.ReadLine();
                if (!int.TryParse(inputFuel, out int fuelInt) || fuelInt < 0 || fuelInt > 3)
                {
                    Console.WriteLine("\n[BŁĄD] Wybierz cyfrę z zakresu!");
                    Pause();
                    return;
                }
                var fuel = (FuelType)fuelInt;

                Console.Write("[POJMNOŚĆ SILNIKA L (np. '3,2')]: ");
                var engineVolume = double.Parse(Console.ReadLine() ?? "1.6");
                if (engineVolume <= 0 || engineVolume > 10)
                {
                    Console.WriteLine("\n[BŁĄD] Pojemność silnika musi być dodatnia, ale nie większa od 10 litrów!");
                    Pause();
                    return;
                }

                Console.Write("[MOC KM (np. '350')]: ");
                var horsePower = int.Parse(Console.ReadLine() ?? "120");
                if (horsePower <= 0 || horsePower > 5000)
                {
                    Console.WriteLine("\n[BŁĄD] Moc pojazdu musi być dodatnia oraz realna!");
                    Pause();
                    return;
                }

                Console.Write("[MOMENT OBROTOWY NM (np. '470')]: ");
                var torque = int.Parse(Console.ReadLine() ?? "160");
                if (torque <= 0 || torque > 3000)
                {
                    Console.WriteLine("\n[BŁĄD] Moment obrotowy pojazdu musi być dodatni oraz realny!");
                    Pause();
                    return;
                }

                Console.Write("[CENA ZA DZIEŃ PLN (np. '350')]: ");
                var price = double.Parse(Console.ReadLine() ?? "150");
                if (price <= 0 || price > 7000)
                {
                    Console.WriteLine("\n[BŁĄD] Cena za dzień musi być dodatnia oraz realna!");
                    Pause();
                    return;
                }

                Console.Write("[OSTATNI SERWIS (RRRR-MM-DD)]: ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime lastService))
                {
                    Console.WriteLine("\n[BŁĄD] Nieprawidłowa data!");
                    Pause();
                    return;
                }

                if(lastService > DateTime.Now)
                {
                    Console.WriteLine("\n[BŁĄD] Serwis nie mógł się odbyć w przyszłości!");
                    Pause();
                    return;
                }

                if (lastService.Year < year)
                {
                    Console.WriteLine("\n[BŁĄD] Data ostatniego serwisu nie może być wcześniejsza niż rok produkcji pojazdu!");
                    Pause();
                    return;
                }

                var departments = await _departmentService.GetAllDepartments();
                Console.WriteLine("\n[DOSTĘPNE PLACÓWKI]");
                foreach (var dept in departments)
                {
                    Console.WriteLine($"    [{dept.Id}] {dept.Name} - {dept.City}");
                }

                Console.Write("\nWybierz ID placówki: ");
                if (!int.TryParse(Console.ReadLine(), out int deptId))
                {
                    Console.WriteLine("\n[BŁĄD] Musi zostać wybrana któraś placówka!");
                    Pause();
                    return;
                }

                var vehicle = new Vehicle
                {
                    Brand = brand,
                    Model = model,
                    ProdYear = year,
                    RegistrationNumber = regNumber,
                    Drive = drive,
                    Transmission = transmission,
                    SeatsAmount = seats,
                    Fuel = fuel,
                    EngineVolume = engineVolume,
                    HorsePower = horsePower,
                    Torque = torque,
                    PriceForDay = price,
                    IsRented = false,
                    DepartmentId = deptId,
                    LastService = lastService,
                    NextService = lastService.AddMonths(6)
                };

                await _vehicleService.AddVehicle(vehicle);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n{ex.Message}");
            }

            Pause();
        }

        private async Task DisplayAllVehicles()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║                 LISTA POJAZDÓW                 ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");

            var vehicles = await _vehicleService.GetAllVehicles();

            if (!vehicles.Any())
            {
                Console.WriteLine("[BŁĄD] Brak pojazdów w bazie danych!");
            }
            else
            {
                foreach (var vehicle in vehicles)
                {
                    Console.WriteLine($"[{vehicle.Id}] {vehicle.Brand} {vehicle.Model} {vehicle.ProdYear}");
                    Console.WriteLine($"    {vehicle.HorsePower}KM {vehicle.EngineVolume}L {vehicle.Fuel}");
                    Console.WriteLine($"    NR.REJ.: {vehicle.RegistrationNumber}");
                    Console.WriteLine($"    ODDZIAŁ: {vehicle.Department?.Name ?? "Brak"}");
                    Console.WriteLine($"    STATUS: {(vehicle.IsRented ? "WYPOŻYCZONY" : "DOSTĘPNY")}");
                    Console.WriteLine($"    CENA: {vehicle.PriceForDay} PLN/dzień");
                    Console.WriteLine($"    DO NASTĘPNEGO SERWISU: {vehicle.DaysToService()} dni");

                    if (vehicle.NeedsServiceSoon())
                    {
                        Console.WriteLine($"    \n[UWAGA] Pojazd będzie wymagał niedługo serwisu!\n");
                    }
                    if (vehicle.NeedsServiceNow())
                    {
                        Console.WriteLine($"    \n[UWAGA] Pojazd wymaga natychmiastowego serwisu!\n");
                    }

                    Console.WriteLine();
                }
            }

            Pause();
        }

        private async Task SearchVehicles()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║              WYSZUKIWANIE POJAZDU              ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");

            var vehiclesCheck = await _vehicleService.GetAllVehicles();
            if (!vehiclesCheck.Any())
            {
                Console.WriteLine("[BŁĄD] Brak pojazdów w systemie!");
                Pause();
                return;
            }
            Console.Write("Wpisz frazę do wyszukania (marka/model/rok): ");
            var searchTerm = Console.ReadLine() ?? string.Empty;

            var vehicles = await _vehicleService.SearchVehicles(searchTerm);

            Console.WriteLine($"\nZnaleziono {vehicles.Count} pojazd(ów):\n");

            foreach (var vehicle in vehicles)
            {
                Console.WriteLine($"[{vehicle.Id}] {vehicle.Brand} {vehicle.Model} {vehicle.ProdYear}");
                Console.WriteLine($"    {vehicle.HorsePower}KM  {vehicle.EngineVolume}L  {vehicle.Fuel}");
                Console.WriteLine($"    NR.REJ.: {vehicle.RegistrationNumber}");
                Console.WriteLine($"    ODDZIAŁ: {vehicle.Department?.Name ?? "Brak"}");
                Console.WriteLine($"    STATUS: {(vehicle.IsRented ? "WYPOŻYCZONY" : "DOSTĘPNY")}");
                Console.WriteLine($"    CENA: {vehicle.PriceForDay} PLN/dzień");
                Console.WriteLine($"    DO NASTĘPNEGO SERWISU: {vehicle.DaysToService()}");

                if (vehicle.NeedsServiceSoon())
                {
                    Console.WriteLine($"    \n[UWAGA] Pojazd będzie wymagał niedługo serwisu!\n");
                }
                if (vehicle.NeedsServiceNow())
                {
                    Console.WriteLine($"    \n[UWAGA] Pojazd wymaga natychmiastowego serwisu!\n");
                }

                Console.WriteLine();
            }

            Pause();
        }

        private async Task DeleteVehicle()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║                USUWANIE POJAZDU                ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");

            var vehicles = await _vehicleService.GetAllVehicles();
            if (!vehicles.Any())
            {
                Console.WriteLine("[BŁĄD] Brak pojazdów w systemie!");
                Pause();
                return;
            }

            Console.WriteLine("DOSTĘPNE POJAZDY:\n");
            foreach (var v in vehicles)
            {
                Console.WriteLine($"    [{v.Id}] {v.GetFullName()} {v.RegistrationNumber}");
            }

            Console.Write("\nPodaj ID pojazdu do usunięcia: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Console.Write($"\nCzy na pewno usunąć pojazd [ID: {id}]? (T/N)");
                var confirm = Console.ReadKey(true);
                Console.WriteLine();

                if (confirm.Key == ConsoleKey.T)
                {
                    var vehicle = await _vehicleService.DeleteVehicle(id);
                    if (!vehicle)
                    {
                        Console.WriteLine("\n[INFO] Usunięcie nie powiodło się.");
                    }
                }
                else
                {
                    Console.WriteLine("\n[INFO] Anulowanie...");
                }
            }
            else
            {
                Console.WriteLine("\n[BŁĄD] Podano nieprawidłowe ID!");
            }

            Pause();
        }


        private async Task ServiceVehicle()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║                 SERWIS POJAZDU                 ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");
            var vehicles = await _vehicleService.GetAllVehicles();
            if (!vehicles.Any())
            {
                Console.WriteLine("[BŁĄD] Brak pojazdów w systemie!");
                Pause();
                return;
            }
            Console.WriteLine("DOSTĘPNE POJAZDY:\n");
            foreach (var v in vehicles)
            {
                Console.WriteLine($"    [{v.Id}] {v.GetFullName()} {v.RegistrationNumber}");
                if(v.DaysSinceLastService() == 0)
                {
                    Console.WriteLine($"    OSTATNI SERWIS: Pojazd dzisiaj wrócił z serwisu\n");
                } 
                else if(v.DaysSinceLastService() == 1)
                {
                    Console.WriteLine($"    OSTATNI SERWIS: {v.DaysSinceLastService()} dzień temu\n");
                }
                else
                {
                    Console.WriteLine($"    OSTATNI SERWIS: {v.DaysSinceLastService()} dni temu\n");
                }
            }
            Console.Write("\nPodaj ID pojazdu, który był w serwisie: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var vehicle = await _vehicleService.ServiceVehicle(id);
                if (!vehicle)
                {
                    Console.WriteLine("\n[INFO] Oznaczenie serwisu nie powiodło się!");
                }
            }
            else
            {
                Console.WriteLine("\n[BŁĄD] Podano nieprawidłowe ID!");
            }

            Pause();
        }

        private async Task VehicleDetails()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║            SZCZEGÓŁOWE DANE POJAZDU            ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");

            var vehicles = await _vehicleService.GetAllVehicles();
            if (!vehicles.Any())
            {
                Console.WriteLine("[BŁĄD] Brak pojazdów w systemie!");
                Pause();
                return;
            }

            Console.WriteLine("DOSTĘPNE POJAZDY:\n");
            foreach (var v in vehicles)
            {
                Console.WriteLine($"    [{v.Id}] {v.GetFullName()} {v.RegistrationNumber}");
            }

            Console.Write("\nPodaj ID pojazdu: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("\n[BŁĄD] Nieprawidłowe ID!");
                Pause();
                return;
            }

            var vehicle = await _vehicleService.GetVehicleById(id);
            if (vehicle == null)
            {
                Console.WriteLine("\n[BŁĄD] Nie znaleziono pojazdu!");
                Pause();
                return;
            }

            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║            SZCZEGÓŁOWE DANE POJAZDU            ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");

            Console.WriteLine(vehicle.ToString());

            Pause();
        }

        private async Task CustomerMenu()
        {
            bool back = false;

            while (!back)
            {
                var customers = await _customerService.GetAllCustomers();

                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════════════╗");
                Console.WriteLine("║             ZARZĄDZANIE KLIENTAMI              ║");
                Console.WriteLine("╚════════════════════════════════════════════════╝\n");

                if (!customers.Any())
                {
                    Console.WriteLine(" [1] Dodaj klienta");
                    Console.WriteLine(" [0] Powrót");
                }
                else
                {
                    Console.WriteLine(" [1] Dodaj klienta");
                    Console.WriteLine(" [2] Wyświetl wszystkich klientów");
                    Console.WriteLine(" [3] Edytuj klienta");
                    Console.WriteLine(" [4] Usuń klienta");
                    Console.WriteLine(" [5] Szczegóły klienta");
                    Console.WriteLine(" [0] Powrót");
                }

                Console.WriteLine();
                Console.Write("Wybierz opcję: ");

                var key = Console.ReadKey(true);
                Console.WriteLine();

                switch (key.KeyChar)
                {
                    case '1':
                        await AddCustomer();
                        break;
                    case '2':
                        if (!customers.Any())
                        {
                            Console.WriteLine("Nieprawidłowa opcja. Naciśnij dowolny klawisz aby wybrać ponownie...");
                        }
                        else
                        {
                            await DisplayAllCustomers();
                        }
                        break;
                    case '3':
                        if (!customers.Any())
                        {
                            Console.WriteLine("Nieprawidłowa opcja. Naciśnij dowolny klawisz aby wybrać ponownie...");
                        }
                        else
                        {
                            await EditCustomer();
                        }
                        break;
                    case '4':
                        if (!customers.Any())
                        {
                            Console.WriteLine("Nieprawidłowa opcja. Naciśnij dowolny klawisz aby wybrać ponownie...");
                        }
                        else
                        {
                            await DeleteCustomer();
                        }
                        break;
                    case '5':
                        if (!customers.Any())
                        {
                            Console.WriteLine("Nieprawidłowa opcja. Naciśnij dowolny klawisz aby wybrać ponownie...");
                        }
                        else
                        {
                            await CustomerDetails();
                        }
                        break;
                    case '0':
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowa opcja.");
                        Pause();
                        break;
                }
            }
        }

        private async Task AddCustomer()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║           DODAWANIE NOWEGO KLIENTA             ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");

            try
            {
                Console.Write("[IMIĘ]: ");
                var firstName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(firstName) || firstName.Length < 1 || firstName.Length > 50)
                {
                    Console.WriteLine("\n[BŁĄD] Imię musi posiadać od 1 do 50 znaków!");
                    Pause();
                    return;
                }

                Console.Write("[NAZWISKO]: ");
                var lastName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(lastName) || lastName.Length < 1 || lastName.Length > 50)
                {
                    Console.WriteLine("\n[BŁĄD] Nazwisko musi posiadać od 1 do 50 znaków!");
                    Pause();
                    return;
                }

                Console.Write("[EMAIL (np. ssss@ss.ss]: ");
                var email = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                {
                    Console.WriteLine("\n[BŁĄD] Nieprawidłowa formuła emaila!");
                    Pause();
                    return;
                }

                Console.Write("[TELEFON (np. 123123123]: ");
                var phone = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(phone) || phone.Length < 7 || phone.Length > 20)
                {
                    Console.WriteLine("\n[BŁĄD] Nieprawidłowy format numeru telefonu!");
                    Pause();
                    return;
                }

                Console.Write("[NUMER PRAWO JAZDY (np. YY12345)]: ");
                var licenseNumber = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(licenseNumber) || licenseNumber.Length < 4 || licenseNumber.Length > 10)
                {
                    Console.WriteLine("\n[BŁĄD] Nieprawidłowa formuła numeru prawo jazdy!");
                    Pause();
                    return;
                }

                Console.Write("[DATA WAŻNOŚCI PRAWO JAZDY (RRRR-MM-DD)]: ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime licenseExpiration))
                {
                    Console.WriteLine("\n[BŁĄD] Nieprawidłowa data!");
                    Pause();
                    return;
                }

                var customer = new Customer
                {
                    FirstName = firstName,
                    LastName = lastName,
                    EmailAddress = email,
                    PhoneNumber = phone,
                    DriverLicenseNumber = licenseNumber,
                    DriverLicenseExpiration = licenseExpiration
                };

                await _customerService.AddCustomer(customer);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[BŁĄD]: {ex.Message}");
            }

            Pause();
        }

        private async Task DisplayAllCustomers()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║           LISTA WSZYSTKICH KLIENTÓW            ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");

            var customers = await _customerService.GetAllCustomers();

            if (!customers.Any())
            {
                Console.WriteLine("[BŁĄD] Brak klientów w bazie danych.");
            }
            else
            {
                foreach (var customer in customers)
                {
                    Console.WriteLine($"[{customer.Id}] KLIENT: {customer.FirstName} {customer.LastName}");
                    Console.WriteLine($"    E-MAIL: {customer.EmailAddress}  NR.TEL.: {customer.FormatPhone()}");
                    Console.WriteLine($"    PRAWO JAZDY: {customer.DriverLicenseNumber} (ważne do: {customer.DriverLicenseExpiration:yyyy-MM-dd})");

                    if (!customer.HasValidDriverLicense())
                        Console.WriteLine($"\n    [UWAGA] Przeterminowane prawo jazdy! [UWAGA]\n");
                    else if (customer.LicenseExpiresSoon())
                        Console.WriteLine($"\n    [UWAGA] Prawo jazdy wygasa wkrótce! [UWAGA]\n");

                    Console.WriteLine($"    REZERWACJE: {customer.Reservations?.Count ?? 0}  AKTYWNE: {customer.GetActiveReservationsCount()}");
                    Console.WriteLine();
                }
            }

            Pause();
        }

        private async Task EditCustomer()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║           EDYTOWANIE DANYCH KLIENTA            ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");

            var customers = await _customerService.GetAllCustomers();
            if (!customers.Any())
            {
                Console.WriteLine("[BŁĄD] Brak klientów w systemie!");
                Pause();
                return;
            }

            Console.WriteLine("KLIENCI W SYSTEMIE:\n");
            foreach (var c in customers)
            {
                Console.WriteLine($"    [{c.Id}] {c.GetFullName()} {c.EmailAddress}");
            }

            Console.Write("\nPodaj ID klienta, którego dane chcesz edytować: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("\n[BŁĄD] Podane ID jest nieprawidłowe!");
                Pause();
                return;
            }

            var customer = await _customerService.GetCustomerById(id);
            if (customer == null)
            {
                Pause();
                return;
            }

            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║           EDYTOWANIE DANYCH KLIENTA            ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");
            Console.WriteLine("TIP: Gdy któreś pole się zgadza i chcesz je pominąć wciśnij ENTER\n");
            Console.WriteLine($"EDYTOWANY KLIENT: [{customer.Id}] {customer.GetFullName()}\n");

            Console.Write($"[IMIĘ] AKTUALNE: {customer.FirstName}\n[IMIĘ] NOWE: ");
            var firstName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(firstName))
            {
                
            } 
            else if (firstName.Length < 1 || firstName.Length > 50)
            {
                Console.WriteLine("\n[BŁĄD] Imię musi posiadać od 1 do 50 znaków!");
                Pause();
                return;
            }
            else
            {
                customer.FirstName = firstName;
            }

            Console.Write($"[NAZWISKO] AKTUALNE: {customer.LastName}\n[NAZWISKO] NOWE: ");
            var lastName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(lastName))
            {

            }
            else if (lastName.Length < 1 || lastName.Length > 50)
            {
                Console.WriteLine("\n[BŁĄD] Nazwisko musi posiadać od 1 do 50 znaków!");
                Pause();
                return;
            }
            else
            {
                customer.LastName = lastName;
            }

            Console.Write($"[EMAIL] AKTUALNE: {customer.EmailAddress}\n[EMAIL] NOWE: ");
            var email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email))
            {
                
            }
            else if (!email.Contains("@"))
            {
                Console.WriteLine("\n[BŁĄD] Nieprawidłowa formuła emaila!");
                Pause();
                return;
            }
            else
            {
                customer.EmailAddress = email;
            }

            Console.Write($"[NUMER TELEFONU] AKTUALNE: {customer.PhoneNumber}\n[NUMER TELEFONU] NOWE: ");
            var phone = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(phone))
            {
                
            }
            else if (phone.Length < 7 || phone.Length > 50)
            {
                Console.WriteLine("\n[BŁĄD] Nieprawidłowy format numeru telefonu");
                Pause();
                return;
            }
            else
            {
                customer.PhoneNumber = phone;
            }

            Console.Write($"[NUMER PRAWO JAZDY] AKTUALNE: {customer.DriverLicenseNumber}\n[NUMER PRAWO JAZDY] NOWE: ");
            var license = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(license))
            {
                
            }
            else if (license.Length < 4 || license.Length > 10)
            {
                Console.WriteLine("\n[BŁĄD] Nieprawidłowa formuła numeru prawo jazdy!");
                Pause();
                return;
            }
            else
            {
                customer.DriverLicenseNumber = license;
            }

            Console.Write($"[DATA WAŻNOŚCI PRAWA JAZDY] AKTUALNE: {customer.DriverLicenseExpiration:yyyy-MM-dd}\n[DATA WAŻNOŚCI PRAWA JAZDY] NOWE: ");
            var expDate = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(expDate)) { }
            else if (!DateTime.TryParse(expDate, out var parsedDate))
            {
                Console.WriteLine("\n[BŁĄD] Nieprawidłowa data!");
                Pause();
                return;
            }
            else
            {
                customer.DriverLicenseExpiration = parsedDate;
            }

            var customerUpdate = await _customerService.UpdateCustomer(customer);
            if (!customerUpdate)
            {
                Console.WriteLine("\n[BŁĄD] Aktualizacja nie powiodła się!");
            }

            Pause();
        }


        private async Task DeleteCustomer()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║            USUWANIE DANYCH KLIENTA             ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");

            var customers = await _customerService.GetAllCustomers();
            if (!customers.Any())
            {
                Console.WriteLine("[BŁĄD] Brak klientów w systemie!");
                Pause();
                return;
            }

            Console.WriteLine("ZAPISANI KLIENCI:\n");
            foreach (var c in customers)
            {
                Console.WriteLine($"    [{c.Id}] {c.GetFullName()} {c.EmailAddress}");
            }

            Console.Write("\nPodaj ID klienta do usunięcia: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("\n[BŁĄD] Podane ID jest nieprawidłowe!");
                Pause();
                return;
            }

            Console.Write($"\nCzy na pewno usunąć klienta [ID {id}]? (T/N)");
            var confirm = Console.ReadKey(true);
            Console.WriteLine();

            if (confirm.Key == ConsoleKey.T)
            {
                var deleteCustomer = await _customerService.DeleteCustomer(id);
                if (!deleteCustomer)
                {
                    Console.WriteLine("\n[INFO] Usunięcie nie powiodło się.");
                }
            }
            else
            {
                Console.WriteLine("Anulowanie...");
            }

            Pause();
        }


        private async Task CustomerDetails()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║           SZCZEGÓŁOWE DANE KLIENTA             ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");

            var customers = await _customerService.GetAllCustomers();
            if (!customers.Any())
            {
                Console.WriteLine("[BŁĄD] Brak klientów w systemie!");
                Pause();
                return;
            }

            Console.WriteLine("ZAPISANI KLIENCI:\n");
            foreach (var c in customers)
            {
                Console.WriteLine($"    [{c.Id}] {c.GetFullName()} {c.EmailAddress}");
            }

            Console.Write("\nPodaj ID klienta: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("\n[BŁĄD] Podane ID jest nieprawidłowe!");
                Pause();
                return;
            }

            var customer = await _customerService.GetCustomerDetails(id);
            if (customer == null)
            {
                Console.WriteLine("\n[BŁĄD] Klient o podanym ID nie został znaleziony!");
                Pause();
                return;
            }

            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║           SZCZEGÓŁOWE DANE KLIENTA             ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");
            Console.WriteLine($"    ID: {customer.Id}");
            Console.WriteLine($"    Imię i nazwisko: {customer.GetFullName()}");
            Console.WriteLine($"    Email: {customer.EmailAddress}");
            Console.WriteLine($"    Telefon: {customer.PhoneNumber}");
            Console.WriteLine($"    Prawo jazdy: {customer.DriverLicenseNumber}");
            Console.WriteLine($"    Ważność: {customer.DriverLicenseExpiration:yyyy-MM-dd}");
            Console.WriteLine($"    Status prawa jazdy: {(customer.HasValidDriverLicense() ? "WAŻNE" : "PRZEDAWNIONE")}");
            Console.WriteLine();
            Console.WriteLine($"    === Statystyki ===");
            Console.WriteLine($"    Łączna liczba rezerwacji: {customer.Reservations?.Count ?? 0}");
            Console.WriteLine($"    Aktywne rezerwacje: {customer.GetActiveReservationsCount()}");
            Console.WriteLine($"    Zakończone rezerwacje: {customer.GetCompletedReservations().Count()}");
            Console.WriteLine($"    Łączne wydatki: {customer.GetTotalSpent():F2} PLN");

            if (customer.Reservations?.Any() == true)
            {
                Console.WriteLine($"\nOstatnie rezerwacje:");
                foreach (var res in customer.Reservations.OrderByDescending(r => r.StartDate).Take(5))
                {
                    Console.WriteLine($"  [{res.Id}] {res.Vehicle?.GetFullName()} - {res.StartDate:yyyy-MM-dd} do {res.EndDate:yyyy-MM-dd}");
                    Console.WriteLine($"      Status: {res.Status} | Cena: {res.Price:F2} PLN");
                }
            }

            Pause();
        }


        private async Task ReservationMenu()
        {
            bool back = false;

            while (!back)
            {
                var reservations = await _reservationService.GetAllReservations();

                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════════════╗");
                Console.WriteLine("║           ZARZĄDZANIE REZERWACJAMI             ║");
                Console.WriteLine("╚════════════════════════════════════════════════╝\n");

                if (!reservations.Any())
                {
                    Console.WriteLine(" [1] Utwórz rezerwację");
                    Console.WriteLine(" [0] Powrót");
                }
                else
                {
                    Console.WriteLine(" [1] Utwórz rezerwację");
                    Console.WriteLine(" [2] Wyświetl wszystkie rezerwacje");
                    Console.WriteLine(" [3] Szczegóły rezerwacji");
                    Console.WriteLine(" [4] Anuluj rezerwację");
                    Console.WriteLine(" [5] Zakończ rezerwację");
                    Console.WriteLine(" [0] Powrót");
                }

                Console.WriteLine();
                Console.Write("Wybierz opcję: ");

                var key = Console.ReadKey(true);
                Console.WriteLine();

                switch (key.KeyChar)
                {
                    case '1':
                        await CreateReservation();
                        break;
                    case '2':
                        if (!reservations.Any())
                        {
                            Console.WriteLine("Nieprawidłowa opcja. Naciśnij dowolny klawisz aby wybrać ponownie...");
                        }
                        else
                        {
                            await DisplayAllReservations();
                        }
                        break;
                    case '3':
                        if (!reservations.Any())
                        {
                            Console.WriteLine("Nieprawidłowa opcja. Naciśnij dowolny klawisz aby wybrać ponownie...");
                        }
                        else
                        {
                            await ReservationDetails();
                        }
                        break;
                    case '4':
                        if (!reservations.Any())
                        {
                            Console.WriteLine("Nieprawidłowa opcja. Naciśnij dowolny klawisz aby wybrać ponownie...");
                        }
                        else
                        {
                            await CancelReservation();
                        }
                        break;
                    case '5':
                        if (!reservations.Any())
                        {
                            Console.WriteLine("Nieprawidłowa opcja. Naciśnij dowolny klawisz aby wybrać ponownie...");
                        }
                        else
                        {
                            await CompleteReservation();
                        }
                        break;
                    case '0':
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowa opcja.");
                        Pause();
                        break;
                }
            }
        }

        private async Task CreateReservation()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║           TWORZENIE NOWEJ REZERWACJI           ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");

            try
            {
                var customers = await _customerService.GetAllCustomers();
                if (!customers.Any())
                {
                    Console.WriteLine("[BŁĄD] Brak klientów w systemie. Musisz dodać jakiegoś klienta aby móc utworzyć rezerwację!");
                    Pause();
                    return;
                }

                var vehicles = await _vehicleService.GetAllVehicles();
                if (!vehicles.Any())
                {
                    Console.WriteLine("[BŁĄD] Brak pojazdów w systemie. Musisz dodać jakiś aby móc utworzyć rezerwację!");
                }

                Console.WriteLine("DOSTĘPNI KLIENCI:\n");
                foreach (var c in customers)
                {
                    Console.WriteLine($"  [{c.Id}] {c.GetFullName()} - {c.EmailAddress}");
                }

                Console.Write("\nWybierz ID klienta: ");
                if (!int.TryParse(Console.ReadLine(), out int customerId))
                {
                    Console.WriteLine("\n[BŁĄD] Nieprawidłowe ID!");
                    Pause();
                    return;
                }

                var availableVehicles = vehicles.GetAvailableVehicles();
                if (!availableVehicles.Any())
                {
                    Console.WriteLine("\n[BŁĄD] Brak dostępnych pojazdów!");
                    Pause();
                    return;
                }

                Console.WriteLine("\nDOSTĘPNE POJAZDY:\n");
                foreach (var v in availableVehicles)
                {
                    Console.WriteLine($"    [{v.Id}] {v.GetFullName()} - {v.PriceForDay} PLN/dzień");
                    Console.WriteLine($"        Placówka: {v.Department?.Name ?? "Brak"}");
                }

                Console.Write("\nWybierz ID pojazdu: ");
                if (!int.TryParse(Console.ReadLine(), out int vehicleId))
                {
                    Console.WriteLine("\n[BŁĄD] Nieprawidłowe ID!");
                    Pause();
                    return;
                }

                Console.Write("\n[DATA ROZPOCZĘCIA] (RRRR-MM-DD): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
                {
                    Console.WriteLine("\n[BŁĄD] Nieprawidłowa data!");
                    Pause();
                    return;
                }

                Console.Write("[DATA ZAKOŃCZENIA] (RRRR-MM-DD): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
                {
                    Console.WriteLine("\n[BŁĄD] Nieprawidłowa data!");
                    Pause();
                    return;
                }

                Console.WriteLine("\nPotwierdzasz rezerwację? (T/N)");
                var confirm = Console.ReadKey(true);

                if (confirm.Key != ConsoleKey.T)
                {
                    Console.WriteLine("\n[INFO] Anulowano...");
                    Pause();
                    return;
                }

                await _reservationService.CreateReservation(customerId, vehicleId, startDate, endDate);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[BŁĄD]: {ex.Message}");
            }

            Pause();
        }


        private async Task DisplayAllReservations()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║          LISTA WSZYSTKICH REZERWACJI           ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");

            var reservations = await _reservationService.GetAllReservations();

            if (!reservations.Any())
            {
                Console.WriteLine("[BŁĄD] Brak rezerwacji w bazie danych.");
            }
            else
            {
                foreach (var res in reservations)
                {
                    Console.WriteLine($"[{res.Id}] KLIENT: {res.Customer?.GetFullName() ?? "Brak"} - {res.Customer?.EmailAddress}");
                    Console.WriteLine($"    POJAZD: {res.Vehicle?.GetFullName() ?? "Brak"}");
                    Console.WriteLine($"    ODDZIAŁ: {res.Department?.Name ?? "Brak"}");
                    Console.WriteLine($"    OKRES: {res.StartDate:yyyy-MM-dd} - {res.EndDate:yyyy-MM-dd} ({res.GetDurationInDays()} dni)");
                    Console.WriteLine($"    CENA: {res.Price:F2} PLN");
                    Console.WriteLine($"    STATUS: {res.Status}");

                    if (res.IsActive() && res.EndsToday())
                        Console.WriteLine("    [UWAGA] Rezerwacja kończy się dzisiaj!");

                    Console.WriteLine();
                }
            }

            Pause();
        }

        private async Task ReservationDetails()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║          SZCZEGÓŁOWE DANE REZERWACJI           ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");

            var reservations = await _reservationService.GetAllReservations();
            if (!reservations.Any())
            {
                Console.WriteLine("[BŁĄD] Brak rezerwacji w systemie!");
                Pause();
                return;
            }

            Console.WriteLine("REZERWACJE:\n");
            foreach (var r in reservations)
            {
                Console.WriteLine($"    [{r.Id}] {r.Customer?.GetFullName()} - {r.Vehicle?.GetFullName()}");
            }

            Console.Write("\nPodaj ID rezerwacji: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("\n[BŁĄD] Nieprawidłowe ID!");
                Pause();
                return;
            }

            var reservation = await _reservationService.GetReservation(id);
            if (reservation == null)
            {
                Console.WriteLine("\n[BŁĄD] Nie znaleziono rezerwacji!");
                Pause();
                return;
            }

            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║          SZCZEGÓŁOWE DANE REZERWACJI           ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");
            Console.WriteLine($"[REZERWACJA ID: {reservation.Id}]");
            Console.WriteLine($"    STATUS: {reservation.Status}");
            Console.WriteLine($"    KLIENT: {reservation.Customer?.GetFullName() ?? "Brak"}, {reservation.Customer?.EmailAddress} / {reservation.Customer?.FormatPhone()}");
            Console.WriteLine($"    POJAZD: {reservation.Vehicle?.GetFullName() ?? "Brak"}");
            Console.WriteLine($"    CENA: {reservation.Vehicle?.PriceForDay} PLN/dzień");
            Console.WriteLine($"    ODDZIAŁ: {reservation.Department?.Name ?? "Brak"} - {reservation.Department?.City}");
            Console.WriteLine($"    OKRES REZERWACJI: {reservation.StartDate:yyyy-MM-dd} - {reservation.EndDate:yyyy-MM-dd}, do zakończenia: {reservation.DaysUntilEnd()}");
            Console.WriteLine($"    LICZBA DNI: {reservation.GetDurationInDays()}");
            Console.WriteLine($"    CENA: {reservation.Price:F2} PLN, za dzień: {reservation.GetPricePerDay():F2} PLN");

            if (reservation.CanBeCancelled())
                Console.WriteLine($"\n[INFO] Rezerwację można anulować (24h przed jej rozpoczęciem)");

            Pause();
        }


        private async Task CancelReservation()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║             ANULOWANIE REZERWACJI              ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");

            var reservations = await _reservationService.GetAllReservations();
            var activeReservations = reservations.Where(r => r.Status == ActualStatus.Potwierdzona).ToList();

            if (!activeReservations.Any())
            {
                Console.WriteLine("[BŁĄD] Brak aktywnych rezerwacji!");
                Pause();
                return;
            }

            Console.WriteLine("AKTYWNE REZERWACJE:");
            foreach (var r in activeReservations)
            {
                Console.WriteLine($"  [{r.Id}] {r.Customer?.GetFullName()} - {r.Vehicle?.GetFullName()}");
            }

            Console.Write("\nPodaj ID rezerwacji do anulowania: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("\n[BŁĄD] Nieprawidłowe ID!");
                Pause();
                return;
            }

            Console.Write("\nCzy na pewno anulować? (T/N): ");
            var confirm = Console.ReadKey(true);
            Console.WriteLine();

            if (confirm.Key == ConsoleKey.T)
            {
                var success = await _reservationService.CancelReservation(id);
                if (!success)
                {
                    Console.WriteLine("\n[INFO] Anulowanie nie powiodło się.");
                }
            }
            else
            {
                Console.WriteLine("\n[INFO] Anulowananie operacji...");
            }

            Pause();
        }

        private async Task CompleteReservation()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║           ZAKOŃCZENIE REZERWACJI               ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");

            var reservations = await _reservationService.GetAllReservations();
            var startedReservations = reservations.Where(r => r.Status == ActualStatus.Rozpoczęta).ToList();

            if (!startedReservations.Any())
            {
                Console.WriteLine("[BŁĄD] Brak rozpoczętych rezerwacji!");
                Pause();
                return;
            }

            Console.WriteLine("ROZPOCZĘTE REZERWACJE:");
            foreach (var r in startedReservations)
            {
                Console.WriteLine($"  [{r.Id}] {r.Customer?.GetFullName()} - {r.Vehicle?.GetFullName()}");
            }

            Console.Write("Podaj ID rezerwacji do zakończenia: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("\n[BŁĄD] Nieprawidłowe ID!");
                Pause();
                return;
            }

            Console.Write("\nCzy na pewno zakończyć rezerwację? (T/N): ");
            var confirm = Console.ReadKey(true);
            Console.WriteLine();

            if (confirm.Key == ConsoleKey.T)
            {
                var success = await _reservationService.CompleteReservation(id);
                if (!success)
                {
                    Console.WriteLine("\n[BŁĄD] Zakończenie nie powiodło się.");
                }
            }
            else
            {
                Console.WriteLine("\n[INFO] Anulowanie operację...");
            }

            Pause();
        }

        private async Task DepartmentMenu()
        {
            bool back = false;

            while (!back)
            {
                var departments = await _departmentService.GetAllDepartments();

                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════════════╗");
                Console.WriteLine("║            ZARZĄDZANIE PLACÓWKAMI              ║");
                Console.WriteLine("╚════════════════════════════════════════════════╝\n");

                if (!departments.Any())
                {
                    Console.WriteLine(" [1] Dodaj placówkę");
                    Console.WriteLine(" [0] Powrót");
                }
                else
                {
                    Console.WriteLine(" [1] Dodaj placówkę");
                    Console.WriteLine(" [2] Wyświetl wszystkie placówki");
                    Console.WriteLine(" [3] Edytuj placówkę");
                    Console.WriteLine(" [4] Usuń placówkę");
                    Console.WriteLine(" [5] Szczegóły placówki");
                    Console.WriteLine(" [0] Powrót");
                }
                Console.WriteLine();
                Console.Write("Wybierz opcję: ");

                var key = Console.ReadKey(true);
                Console.WriteLine();

                switch (key.KeyChar)
                {
                    case '1':
                        await AddDepartment();
                        break;
                    case '2':
                        if (!departments.Any())
                        {
                            Console.WriteLine("Nieprawidłowa opcja. Naciśnij dowolny klawisz aby wybrać ponownie...");
                        }
                        else
                        {
                            await DisplayAllDepartments();
                        }
                        break;
                    case '3':
                        if (!departments.Any())
                        {
                            Console.WriteLine("Nieprawidłowa opcja. Naciśnij dowolny klawisz aby wybrać ponownie...");
                        }
                        else
                        {
                            await EditDepartment();
                        }
                        break;
                    case '4':
                        if (!departments.Any())
                        {
                            Console.WriteLine("Nieprawidłowa opcja. Naciśnij dowolny klawisz aby wybrać ponownie...");
                        }
                        else
                        {
                            await DeleteDepartment();
                        }
                        break;
                    case '5':
                        if (!departments.Any())
                        {
                            Console.WriteLine("Nieprawidłowa opcja. Naciśnij dowolny klawisz aby wybrać ponownie...");
                        }
                        else
                        {
                            await DepartmentDetails();
                        }
                        break;
                    case '0':
                        back = true;
                        break;
                    default:
                        Console.WriteLine("\n[BŁĄD] Nieprawidłowa opcja.");
                        Pause();
                        break;
                }
            }
        }

        private async Task AddDepartment()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║           DODAWANIE NOWEJ PLACÓWKI             ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");

            try
            {
                Console.Write("[NAZWA PLACÓWKI]: ");
                var name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name) || name.Length < 1 || name.Length > 50)
                {
                    Console.WriteLine("\n[BŁĄD] Nazwa musi posiadać od 1 do 50 znaków!");
                    Pause();
                    return;
                }

                Console.Write("[MIASTO]: ");
                var city = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(city) || city.Length < 1 || city.Length > 50)
                {
                    Console.WriteLine("\n[BŁĄD] Miasto musi posiadać od 1 do 50 znaków!");
                    Pause();
                    return;
                }

                Console.Write("[ULICA (np. 'Kamienna 8'): ");
                var streetAddress = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(streetAddress))
                {
                    Console.WriteLine("\n[BŁĄD] Nie można dodać pustych danych!");
                    Pause();
                    return;
                }

                Console.Write("[ADRES E-MAIL (np. 'jankowalski@domena.pl')]: ");
                var emailAddress = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(emailAddress) || !emailAddress.Contains("@"))
                {
                    Console.WriteLine("\n[BŁĄD] Nieprawidłowa formuła emaila!");
                    Pause();
                    return;
                }

                Console.Write("[NUMER TELEFONU (np. '111222333')]: ");
                var phoneNumber = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(phoneNumber) || phoneNumber.Length < 7 || phoneNumber.Length > 20)
                {
                    Console.WriteLine("\n[BŁĄD] Nieprawidłowy format numeru telefonu!");
                    Pause();
                    return;
                }

                var department = new Department
                {
                    Name = name,
                    City = city,
                    StreetAddress = streetAddress,
                    EmailAddress = emailAddress,
                    PhoneNumber = phoneNumber
                };

                await _departmentService.AddDepartment(department);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[BŁĄD]: {ex.Message}");
            }

            Pause();
        }

        private async Task DisplayAllDepartments()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║           LISTA WSZYSTKICH PLACÓWEK            ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");

            var departments = await _departmentService.GetAllDepartments();

            if (!departments.Any())
            {
                Console.WriteLine("[INFO] Brak placówek w systemie!");
            }
            else
            {
                foreach (var department in departments)
                {
                    Console.WriteLine($"[{department.Id}] {department.Name}");
                    Console.WriteLine($"    ADRES: {department.GetFullAddress()}");
                    Console.WriteLine($"    KONTAKT: {department.EmailAddress}, {department.PhoneNumber}");
                    Console.WriteLine($"    LICZBA PRACOWNIKÓW: {department.GetEmployeeCount()}");
                    Console.WriteLine($"    LICZBA POJAZDÓW: {department.Vehicles.Count}");
                    Console.WriteLine();
                }
            }

            Pause();
        }

        private async Task EditDepartment()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║           EDYTOWANIE DANYCH PLACÓWKI           ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");

            var departments = await _departmentService.GetAllDepartments();
            if (!departments.Any())
            {
                Console.WriteLine("[INFO] Brak placówek w systemie!");
                Pause();
                return;
            }

            Console.WriteLine("ISTNIEJĄCE PLACÓWKI:\n");
            foreach (var d in departments)
            {
                Console.WriteLine($"    [{d.Id}] {d.Name} {d.GetFullAddress()}");
            }

            Console.Write("\nPodaj ID placówki, której dane chcesz edytować: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("\n[BŁĄD] Podane ID jest nieprawidłowe!");
                Pause();
                return;
            }

            var department = await _departmentService.GetDepartment(id);
            if (department == null)
            {
                Pause();
                return;
            }

            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║          EDYTOWANIE DANYCH PLACÓWKI            ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");
            Console.WriteLine("TIP: Gdy chcesz pominąć jakieś pole wciśnij ENTER\n");
            Console.WriteLine($"EDYTOWANA PLACÓWKA: [{department.Id}] {department.Name}\n");

            Console.Write($"[NAZWA] AKTUALNE: {department.Name}\n[NAZWA] NOWE: ");
            var name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name)) { }
            else if (name.Length < 2 || name.Length > 100)
            {
                Console.WriteLine("\n[BŁĄD] Nazwa placówki musi mieć od 2 do 100 znaków!");
                Pause();
                return;
            }
            else
            {
                department.Name = name;
            }

            Console.Write($"[MIASTO] AKTUALNE: {department.City}\n[MIASTO] NOWE: ");
            var city = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(city)) { }
            else if (city.Length < 2 || city.Length > 50)
            {
                Console.WriteLine("\n[BŁĄD] Miasto musi mieć od 2 do 50 znaków!");
                Pause();
                return;
            }
            else
            {
                department.City = city;
            }

            Console.Write($"[ULICA] AKTUALNE: {department.StreetAddress}\n[ULICA] NOWE: ");
            var street = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(street)) { }
            else if (street.Length < 3 || street.Length > 100)
            {
                Console.WriteLine("\n[BŁĄD] Ulica musi mieć od 3 do 100 znaków!");
                Pause();
                return;
            }
            else
            {
                department.StreetAddress = street;
            }

            Console.Write($"[EMAIL] AKTUALNE: {department.EmailAddress}\n[EMAIL] NOWE: ");
            var deptEmail = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(deptEmail)) { }
            else if (!deptEmail.Contains("@"))
            {
                Console.WriteLine("\n[BŁĄD] Nieprawidłowy adres e-mail!");
                Pause();
                return;
            }
            else
            {
                department.EmailAddress = deptEmail;
            }

            Console.Write($"[NR.TEL.] AKTUALNE: {department.PhoneNumber}\n[NR.TEL.] NOWE: ");
            var deptPhone = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(deptPhone)) { }
            else if (deptPhone.Length < 7 || deptPhone.Length > 50)
            {
                Console.WriteLine("\n[BŁĄD] Numer telefonu powinien posiadać od 7 do 50 znaków!");
                Pause();
                return;
            }
            else
            {
                department.PhoneNumber = deptPhone;
            }

            await _departmentService.UpdateDepartment(department);

            Pause();
        }

        private async Task DeleteDepartment()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║            USUWANIE DANYCH PLACÓWKI            ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");

            var departments = await _departmentService.GetAllDepartments();
            if (!departments.Any())
            {
                Console.WriteLine("[INFO] Brak placówek w systemie!");
                Pause();
                return;
            }

            foreach (var d in departments)
            {
                Console.WriteLine($"    [{d.Id}] {d.Name} {d.GetFullAddress()}");
            }

            Console.Write("\nPodaj ID placówki do usunięcia: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("\n[BŁĄD] Podane ID jest nieprawidłowe!");
                Pause();
                return;
            }

            Console.Write($"\nCzy na pewno usunąć placówkę o ID {id}? (T/N): ");
            var confirm = Console.ReadKey(true);
            Console.WriteLine();

            if (confirm.Key == ConsoleKey.T)
            {
                await _departmentService.DeleteDepartment(id);
            }
            else
            {
                Console.WriteLine("\n[INFO] Anulowanie...");
            }

            Pause();
        }

        private async Task DepartmentDetails()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║           SZCZEGÓŁOWE DANE PLACÓWKI            ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");

            var departments = await _departmentService.GetAllDepartments();
            if (!departments.Any())
            {
                Console.WriteLine("[INFO] Brak placówek w systemie!");
                Pause();
                return;
            }

            Console.WriteLine("ISTNIEJĄCE PLACÓWKI:\n");
            foreach (var d in departments)
            {
                Console.WriteLine($"    [{d.Id}] {d.Name} {d.GetFullAddress()}");
            }

            Console.Write("\nPodaj ID placówki: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("\n[BŁĄD] Podane ID jest nieprawidłowe!");
                Pause();
                return;
            }

            var department = await _departmentService.GetDepartment(id);
            if (department == null)
            {
                Pause();
                return;
            }

            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║           SZCZEGÓŁOWE DANE PLACÓWKI            ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");
            Console.WriteLine($" [INFORMACJE]");
            Console.WriteLine($"    ID: {department.Id}");
            Console.WriteLine($"    Nazwa: {department.Name}");
            Console.WriteLine($"    Miasto: {department.City}");
            Console.WriteLine($"    Ulica: {department.StreetAddress}");
            Console.WriteLine($"    Email: {department.EmailAddress}");
            Console.WriteLine($"    Telefon: {department.PhoneNumber}");
            Console.WriteLine();
            Console.WriteLine($" [STATYSTYKI]");
            Console.WriteLine($"    Łączna liczba pojazdów we flocie: {department.Vehicles.Count}");
            Console.WriteLine($"    Liczba wypożyczonych pojazdów: {department.GetRentedVehiclesCount()}");
            Console.WriteLine($"    Liczba dostępnych pojazdów: {department.GetAvailableVehiclesCount()}");

            Pause();
        }


        private async Task EmployeeMenu()
        {
            bool back = false;

            while (!back)
            {
                var employees = await _employeeService.GetAllEmployees();

                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════════════╗");
                Console.WriteLine("║             ZARZĄDZANIE PRACOWNIKAMI           ║");
                Console.WriteLine("╚════════════════════════════════════════════════╝\n");

                if (!employees.Any())
                {
                    Console.WriteLine(" [1] Zatrudnij pracownika");
                    Console.WriteLine(" [0] Powrót");
                }
                else
                { 
                    Console.WriteLine(" [1] Zatrudnij pracownika");
                    Console.WriteLine(" [2] Wyświetl zarządców");
                    Console.WriteLine(" [3] Wyświetl wszystkich pracowników");
                    Console.WriteLine(" [4] Awansuj pracownika");
                    Console.WriteLine(" [5] Zwolnij pracownika");
                    Console.WriteLine(" [6] Szczegóły pracownika");
                    Console.WriteLine(" [0] Powrót");
                }

                Console.WriteLine();
                Console.Write("Wybierz opcję: ");

                var key = Console.ReadKey(true);
                Console.WriteLine();

                switch (key.KeyChar)
                {
                    case '1':
                        await HireEmployee();
                        break;
                    case '2':
                        if (!employees.Any())
                        {
                            Console.WriteLine("Nieprawidłowa opcja. Naciśnij dowolny klawisz aby wybrać ponownie...");
                        }
                        else
                        {
                            await DisplayManagment();
                        }
                        break;
                    case '3':
                        if (!employees.Any())
                        {
                            Console.WriteLine("Nieprawidłowa opcja. Naciśnij dowolny klawisz aby wybrać ponownie...");
                        }
                        else
                        {
                            await DisplayAllEmployees();
                        }
                        break;
                    case '4':
                        if (!employees.Any())
                        {
                            Console.WriteLine("Nieprawidłowa opcja. Naciśnij dowolny klawisz aby wybrać ponownie...");
                        }
                        else
                        {
                            await PromoteEmployee();
                        }
                        break;
                    case '5':
                        if (!employees.Any())
                        {
                            Console.WriteLine("Nieprawidłowa opcja. Naciśnij dowolny klawisz aby wybrać ponownie...");
                        }
                        else
                        {
                            await FireEmployee();
                        }
                        break;
                    case '6':
                        if (!employees.Any())
                        {
                            Console.WriteLine("Nieprawidłowa opcja. Naciśnij dowolny klawisz aby wybrać ponownie...");
                        }
                        else
                        {
                            await EmployeeDetails();
                        }
                        break;
                    case '0':
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowa opcja.");
                        Pause();
                        break;
                }
            }
        }

        private async Task HireEmployee()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║         ZATRUDNIANIE NOWEGO PRACOWNIKA         ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");

            Console.Write("[IMIĘ]: ");
            var firstName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(firstName) || firstName.Length < 1 || firstName.Length > 50)
            {
                Console.WriteLine("\n[BŁĄD] Imię musi mieć od 1 do 50 znaków!");
                Pause();
                return;
            }

            Console.Write("[NAZWISKO]: ");
            var lastName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length < 1 || lastName.Length > 50)
            {
                Console.WriteLine("\n[BŁĄD] Imię musi mieć od 1 do 50 znaków!");
                Pause();
                return;
            }

            Console.WriteLine("[STANOWISKO]:");
            Console.WriteLine(" [0] Stażysta");
            Console.WriteLine(" [1] Asystent");
            Console.WriteLine(" [2] Pracownik");
            Console.WriteLine(" [3] Manager");
            Console.WriteLine(" [4] Kierownik");
            Console.WriteLine(" [5] Dyrektor");
            Console.Write("Wybierz stanowisko (0-5): ");
            if (!int.TryParse(Console.ReadLine(), out int titleInt) || titleInt < 0 || titleInt > 5)
            {
                Console.WriteLine("\n[BŁĄD] Nieprawidłowe stanowisko wybierz stanowisko z zakresu!");
                Pause();
                return;
            }
            var title = (JobTitle)titleInt;

            var departments = await _departmentService.GetAllDepartments();
            Console.WriteLine("[DOSTĘPNE PLACÓWKI]:");
            foreach (var dept in departments)
            {
                Console.WriteLine($"  [{dept.Id}] {dept.Name} - {dept.City}");
            }
            Console.Write("Wybierz ID placówki: ");
            if (!int.TryParse(Console.ReadLine(), out int deptId))
            {
                Pause();
                return;
            }

            var employee = new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                Title = title,
                DepartmentId = deptId
            };

            await _employeeService.HireEmployee(employee);
            Pause();
        }

        private async Task DisplayManagment()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║                LISTA ZARZĄDCÓW                 ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");

            var employees = await _employeeService.GetAllEmployees();
            var managment = employees.GetManagementStaff();

            if (!managment.Any())
            {
                Console.WriteLine("[BŁĄD] Brak zarządców w bazie danych.");
            }
            else
            {
                foreach(var m in managment)
                {
                    Console.WriteLine($"[{m.Id}] {m.GetFullName()}");
                    Console.WriteLine($"    Stanowisko: {m.Title}");
                    Console.WriteLine($"    Placówka: {m.Department?.Name}\n");
                }
                
            }

            Pause();
        }

        private async Task DisplayAllEmployees()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║         LISTA WSZYSTKICH PRACOWNIKÓW           ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");

            var employees = await _employeeService.GetAllEmployees();

            if (!employees.Any())
            {
                Console.WriteLine("[BŁĄD] Brak pracowników w bazie danych.");
            }
            else
            {
                foreach (var e in employees.OrderByTitle())
                {
                    Console.WriteLine($"[{e.Id}] {e.GetFullName()}");
                    Console.WriteLine($"    Stanowisko: {e.Title}");
                    Console.WriteLine($"    Placówka: {e.Department?.Name}\n");
                }
            }

            Pause();
        }

        private async Task PromoteEmployee()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║            AWANSOWANIE PRACOWNIKA              ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");

            var employees = await _employeeService.GetEmployeesToRankup();
            if (!employees.Any())
            {
                Console.WriteLine("[BŁĄD] Brak pracowników mogących otrzymać awans!");
                Pause();
                return;
            }

            Console.WriteLine("AKTUALNI PRACOWNICY:\n");
            foreach (var e in employees)
            {
                Console.WriteLine($"    [{e.Id}] {e.GetFullName()} {e.Title}");
            }

            Console.Write("\nPodaj ID pracownika: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("\n[BŁĄD] Nieprawidłowe ID!");
                Pause();
                return;
            }

            var employee = await _employeeService.GetEmployee(id);
            if (employee == null)
            {
                Console.WriteLine("\n[BŁĄD] Nie znaleziono pracownika!");
                Pause();
                return;
            }

            var oldTitle = employee.Title;
            var newTitle = employee.GetNextTitle();

            Console.WriteLine($"\nPracownik: {employee.GetFullName()}");
            Console.WriteLine($"Obecne stanowisko: {oldTitle}");
            Console.WriteLine($"Nowe stanowisko: {newTitle}");
            Console.Write("\nPotwierdzasz awans? (T/N): ");
            var confirm = Console.ReadKey(true);
            Console.WriteLine();

            if (confirm.Key == ConsoleKey.T)
            {
                await _employeeService.RankupEmployee(id);
            }
            else
            {
                Console.WriteLine("\n[INFO] Anulowanie...");
            }

            Pause();
        }

        private async Task FireEmployee()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║            ZWALNIANIE PRACOWNIKA               ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");

            var employees = await _employeeService.GetAllEmployees();
            if (!employees.Any())
            {
                Console.WriteLine("[BŁĄD] Brak pracowników w systemie!");
                Pause();
                return;
            }

            Console.WriteLine("AKTUALNI PRACOWNICY:\n");
            foreach (var e in employees)
            {
                Console.WriteLine($"    [{e.Id}] {e.GetFullName()} {e.Title}");
            }

            Console.Write("\nPodaj ID pracownika do zwolnienia: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("\n[BŁĄD] Nieprawidłowe ID!");
                Pause();
                return;
            }

            var employee = employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                Console.WriteLine("\n[BŁĄD] Nie znaleziono pracownika!");
                Pause();
                return;
            }

            Console.WriteLine($"\nPracownik: {employee.GetFullNameWithTitle()}");
            Console.WriteLine($"Placówka: {employee.Department?.Name ?? "Nie przypisano"}");
            Console.Write("\nCzy na pewno zwolnić tego pracownika? (T/N)");
            var confirm = Console.ReadKey(true);
            Console.WriteLine();

            if (confirm.Key == ConsoleKey.T)
            {
                await _employeeService.FireEmployee(id);
            }
            else
            {
                Console.WriteLine("\n[INFO] Anulowanie...");
            }

            Pause();
        }

        private async Task EmployeeDetails()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║              SZCZEGÓŁY PRACOWNIKA              ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");

            var employees = await _employeeService.GetAllEmployees();
            if (!employees.Any())
            {
                Console.WriteLine("[BŁĄD] Brak pracowników w systemie!");
                Pause();
                return;
            }

            Console.WriteLine("AKTUALNI PRACOWNICY:\n");
            foreach (var e in employees)
            {
                Console.WriteLine($"    [{e.Id}] {e.GetFullName()} {e.Title}");
            }

            Console.Write("\nPodaj ID pracownika: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("\n[BŁĄD] Nieprawidłowe ID!");
                Pause();
                return;
            }

            var employee = await _employeeService.GetEmployee(id);
            if (employee == null)
            {
                Console.WriteLine("\n[BŁĄD] Nie znaleziono pracownika!");
                Pause();
                return;
            }

            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║              SZCZEGÓŁY PRACOWNIKA              ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");
            Console.WriteLine($" [INFORMACJE ID: {employee.Id}]");
            Console.WriteLine($"    Imię i nazwisko: {employee.GetFullName()}");
            Console.WriteLine($"    Stanowisko: {employee.Title}");
            Console.WriteLine($"    Placówka: {employee.Department?.Name ?? "Nie przypisano"}, {employee.Department?.GetFullAddress() ?? ""}");
            Console.WriteLine();
            Console.WriteLine($"    Kadra zarządzająca: {(employee.IsInManagement() ? "TAK" : "NIE")}");
            if (employee.CanBePromoted())
            {
                Console.Write($"    Możliwość awansu na {employee.GetNextTitle()}");
            }
            Console.WriteLine();

            Pause();
        }

        private async Task ReportsMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║              RAPORTY I STATYSTYKI              ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");

            var vehicleCount = await _vehicleService.GetVehicleCount();
            var rentedCount = await _vehicleService.GetRentedVehicleCount();
            var availableCount = await _vehicleService.GetAvailableVehicleCount();

            Console.WriteLine("=== FLOTA POJAZDÓW ===");
            Console.WriteLine($"Łączna liczba pojazdów: {vehicleCount}");
            Console.WriteLine($"Pojazdy wypożyczone: {rentedCount}");
            Console.WriteLine($"Pojazdy dostępne: {availableCount}\n");

            var customerCount = await _customerService.GetCustomerCount();
            var activeCustomers = await _customerService.GetActiveCustomerCount();

            Console.WriteLine("=== KLIENCI ===");
            Console.WriteLine($"Łączna liczba klientów: {customerCount}");
            Console.WriteLine($"Klienci z aktywnymi rezerwacjami: {activeCustomers}\n");

            var departmentCount = await _departmentService.GetDepartmentCount();
            var employeesCount = await _employeeService.GetEmployeeCount();

            Console.WriteLine("=== PLACÓWKI ===");
            Console.WriteLine($"Łączna liczba placówek: {departmentCount}");
            Console.WriteLine($"Łączna liczba pracowników: {employeesCount}\n");

            var reservationsCount = await _reservationService.GetReservationCount();
            var activeReservations = await _reservationService.GetActiveReservationCount();
            var completedReservations = await _reservationService.GetCompletedReservationCount();
            var canceledReservations = await _reservationService.GetCanceledReservationCount();

            Console.WriteLine("=== REZERWACJE ===");
            Console.WriteLine($"Łączna liczba rezerwacji: {reservationsCount}");
            Console.WriteLine($"Aktywne rezerwacje: {activeReservations}");
            Console.WriteLine($"Anulowane rezerwacje: {canceledReservations}");
            Console.WriteLine($"Zakończone rezerwacje: {completedReservations}\n");

            var managementCount = await _employeeService.GetManagementCount();
            Console.WriteLine("=== KADRA ===");
            Console.WriteLine($"Pracownicy w kadrze zarządzającej: {managementCount}");

            Pause();
        }

        private void Pause()
        {
            Console.WriteLine("\nNaciśnij dowolny klawisz aby cofnąć się do menu...");
            Console.ReadKey(true);
        }
    }
}