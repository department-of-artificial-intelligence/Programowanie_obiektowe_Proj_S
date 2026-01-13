using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.Dal.Repositories;
using Project.DAL;
using Project.Logic.StoreManagement;
using Project.Model;
using Project.Model.Orders;
using Project.Model.People;
using Project.Model.Stores;
using Project.Logic.Extensions;


IHost _host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
{
    var cns = context.Configuration.GetConnectionString("DefaultConnection");
    services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(cns));
}).Build();



var context = _host.Services.GetService<ApplicationDbContext>();
if (context != null)
{
    context.Database.Migrate();
    context.Database.EnsureCreated();


    if (!context.Stores.Any())
    {


        var adres1 = new Address
        {
            City = "Warszawa",
            Street = "ul. Marszałkowska 1",
            ZipCode = "12-690",
            Country = "Polska"
        };


        var newStore = new Store
        {
            Name = "Nowe MediaExpert",
            Address = adres1,
            PhoneNumber = "+48863972431"

        };


        var newEmployee = new Employee
        {
            FirstName = "Jan",
            LastName = "Kowalski",
            PhoneNumber = "+48732456823",
            Email = "jan.kowalski@firma.pl",
            WorkPlace = newStore,
            Position = EmployeePosition.Cashier,
            Salary = 4500m,


        };


        context.Stores.Add(newStore);
        context.Employees.Add(newEmployee);


        context.SaveChanges();

        Console.WriteLine("Dodano przykładowe dane (Sklep + Pracownik).");
    }
}




var storeRepo = new StoreRepository(context);
var employeeRepo = new EmployeeRepository(context);
var orderRepo = new OrderRepository(context);



var employeeManager = new EmployeeManager(employeeRepo);
var orderService = new OrderService(orderRepo);
var productManager = new ProductManager(storeRepo); 
var statsService = new ServiceOfStatistics(orderRepo);


var menu = new MenuContainer();


var stores = context.Stores.Include(s => s.Address).ToList();

bool startProgram = true;

while (startProgram)
{
    Console.Clear();
    Console.WriteLine($"--- SYSTEM ELECTROHUB ---");
    Console.WriteLine($"Aktualnie obsługujemy {stores.Count} lokalizacji.");

    menu.ShowMainMenu();
    string mainChoice = Console.ReadLine();

    switch (mainChoice)
    {
        case "2": 
            HandleAdminSection(context, menu, stores, employeeManager, orderService);
            break;

        case "0":
            startProgram = false;
            break;
    }
}


void HandleAdminSection(ApplicationDbContext context, MenuContainer menu, List<Store> stores, EmployeeManager empMgr, OrderService orderSvc)
{
    Console.Clear();
    menu.ShowStoreSelectionHeader();

    
    for (int i = 0; i < stores.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {stores[i].Name} - {stores[i].Address.City} [ID: {stores[i].Id}]");
    }
    Console.WriteLine("0. Powrót");

    Console.Write("\nWybierz numer sklepu: ");
    if (!int.TryParse(Console.ReadLine(), out int index) || index == 0 || index > stores.Count) return;

    var selectedStore = stores[index - 1];

    
    bool inStore = true;
    while (inStore)
    {
        Console.Clear();
        Console.WriteLine($">>> ZARZĄDZANIE: {selectedStore.Name.ToUpper()} <<<");
        menu.ShowAdminMenu();

        string adminChoice = Console.ReadLine();
        if (adminChoice == "0") inStore = false;

        
    }
}
