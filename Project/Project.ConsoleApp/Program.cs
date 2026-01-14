using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.Dal.Repositories;
using Microsoft.Extensions.Logging;
using Project.DAL;
using Project.Logic.Extensions;
using Project.Logic.PaymentService;
using Project.Logic.StoreManagement;
using Project.Model;
using Project.Model.Interfaces;
using Project.Model.Orders;
using Project.Model.People;
using Project.Model.Stores;
using System.Globalization;
using System.Text.RegularExpressions;



var culture = new CultureInfo("pl-PL"); 
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;




IHost _host = Host.CreateDefaultBuilder()
    .ConfigureLogging(logging =>
    {
        
        logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);

        logging.AddFilter("Microsoft.EntityFrameworkCore.Query", LogLevel.Error);
    })
    .ConfigureServices((context, services) =>
    {

        var cns = "Server=(localdb)\\mssqllocaldb;Database=ElectroHub_Final_Dbv605;Trusted_Connection=True;MultipleActiveResultSets=true";
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(cns));
    })
    .Build();



var context = _host.Services.GetService<ApplicationDbContext>();
if (context != null)
{

    context.Database.Migrate();
    


    if (!context.Stores.Any())
    {
        Console.WriteLine("--- GENEROWANIE DANYCH TESTOWYCH (SEEDING) ---");


        var addressWaw = new Address { City = "Warszawa", Street = "ul. Marszałkowska 100", ZipCode = "00-100", Country = "Polska" };
        var addressKrk = new Address { City = "Kraków", Street = "ul. Floriańska 15", ZipCode = "31-000", Country = "Polska" };

        var storeWaw = new Store
        {
            Name = "ElectroHub Warszawa Central",
            Address = addressWaw,
            PhoneNumber = "+48221112233",
            Inventory = new List<Product>()
        };

        var storeKrk = new Store
        {
            Name = "ElectroHub Kraków Rynek",
            Address = addressKrk,
            PhoneNumber = "+48124445566",
            Inventory = new List<Product>()
        };


        storeWaw.Inventory.Add(new Product("Laptop Gamingowy MSI", 5500m, 10, ProductCategory.Computer));
        storeWaw.Inventory.Add(new Product("MacBook Air M2", 6200m, 5, ProductCategory.Computer));
        storeWaw.Inventory.Add(new Product("Samsung Galaxy S24", 4500m, 20, ProductCategory.Smartphone));
        storeWaw.Inventory.Add(new Product("iPhone 15 Pro", 5999m, 15, ProductCategory.Smartphone));
        storeWaw.Inventory.Add(new Product("Słuchawki Sony WH-1000XM5", 1400m, 30, ProductCategory.Audio));
        storeWaw.Inventory.Add(new Product("Telewizor LG OLED 65 Cal", 8500m, 3, ProductCategory.TV));
        storeWaw.Inventory.Add(new Product("Lodówka Samsung NoFrost", 3200m, 4, ProductCategory.LargeAppliance));


        storeKrk.Inventory.Add(new Product("Laptop Dell XPS 15", 9000m, 2, ProductCategory.Computer));
        storeKrk.Inventory.Add(new Product("iPhone 13 Mini", 2999m, 8, ProductCategory.Smartphone));
        storeKrk.Inventory.Add(new Product("Głośnik JBL Charge 5", 600m, 50, ProductCategory.Audio));
        storeKrk.Inventory.Add(new Product("Kabel HDMI 2.1", 99m, 100, ProductCategory.Accessory));
        storeKrk.Inventory.Add(new Product("Myszka Logitech MX Master", 450m, 15, ProductCategory.Accessory));
        storeKrk.Inventory.Add(new Product("Ekspres do kawy DeLonghi", 2500m, 6, ProductCategory.SmallAppliance));


        context.Stores.AddRange(storeWaw, storeKrk);


        var emp1 = new Employee("Jan", "Kowalski", "+48700800900", "jan.kowalski@eh.pl", storeWaw, EmployeePosition.Manager, 8000m, DateTime.Now.AddMonths(-12));
        var emp2 = new Employee("Anna", "Nowak", "+48600500400", "anna.nowak@eh.pl", storeWaw, EmployeePosition.Cashier, 4500m, DateTime.Now.AddMonths(-5));
        var emp3 = new Employee("Piotr", "Wiśniewski", "+48500400300", "piotr.wis@eh.pl", storeWaw, EmployeePosition.WarehouseWorker, 4800m, DateTime.Now.AddMonths(-2));

        var emp4 = new Employee("Katarzyna", "Wójcik", "+48666777888", "kasia.w@eh.pl", storeKrk, EmployeePosition.Supervisor, 6500m, DateTime.Now.AddMonths(-8));
        var emp5 = new Employee("Tomek", "Zieliński", "+48999888777", "tomek.z@eh.pl", storeKrk, EmployeePosition.Technician, 5500m, DateTime.Now.AddMonths(-1));

        context.Employees.AddRange(emp1, emp2, emp3, emp4, emp5);


        var c1 = new Customer
        {
            FirstName = "Adam",
            LastName = "Testowy",
            Email = "user@test.pl",
            PhoneNumber = "+48111222333",
            WalletBalance = 10000m,
            RegistrationDate = DateTime.Now
        };

        
        var c2 = new Customer
        {
            FirstName = "Michał",
            LastName = "Student",
            Email = "michal@student.pl",
            PhoneNumber = "+48999888777",
            WalletBalance = 50.00m, 
            RegistrationDate = DateTime.Now.AddDays(-5)
        };

       
        var c3 = new Customer
        {
            FirstName = "Robert",
            LastName = "Bogaty",
            Email = "robert@vip.pl",
            PhoneNumber = "+48600700800",
            WalletBalance = 50000.00m, 
            RegistrationDate = DateTime.Now.AddMonths(-2)
        };

       
        var c4 = new Customer
        {
            FirstName = "Ewa",
            LastName = "Zakupowa",
            Email = "ewa@zakupy.pl",
            PhoneNumber = "+48555444333",
            WalletBalance = 2500.00m,
            RegistrationDate = DateTime.Now.AddDays(-10)
        };

        context.Customers.AddRange(c1, c2, c3, c4);

       
        context.SaveChanges(); 

        Console.WriteLine("----------------------------------------------------------");
        Console.WriteLine(" [SUKCES] Wygenerowano dane testowe!");
        Console.WriteLine(" 2 Sklepy, 13 Produktów, 5 Pracowników, 1 Klient.");
        Console.WriteLine($" Konto testowe klienta -> Email: {c1.Email}");
        Console.WriteLine("----------------------------------------------------------");


    }

}





var storeRepo = new StoreRepository(context);
var employeeRepo = new EmployeeRepository(context);
var orderRepo = new OrderRepository(context);
var statsService = new ServiceOfStatistics(context);


var employeeManager = new EmployeeManager(employeeRepo);
var orderService = new OrderService(orderRepo);
var productManager = new ProductManager(storeRepo); 



var menu = new MenuContainer();


var stores = context.Stores
    .Include(s => s.Address)
    .Include(s => s.Inventory)
    .Include(s => s.Staff)     
    .Include(s => s.Orders)    
    .ToList();

bool startProgram = true;
while (startProgram)
{
    
    menu.ShowMainMenu();

    string mainChoice = Console.ReadLine();

    switch (mainChoice)
    {
        case "1": 
            Console.Clear();
            menu.ShowStoreSelectionHeader(); 

            
            for (int i = 0; i < stores.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {stores[i].Name} - {stores[i].Address.City}");
            }
            Console.WriteLine("0. Powrót");
            Console.Write("\nWybierz sklep, w którym chcesz zrobić zakupy: ");

            if (int.TryParse(Console.ReadLine(), out int shopIdx) && shopIdx > 0 && shopIdx <= stores.Count)
            {
                var shopForClient = stores[shopIdx - 1];

                
                HandleClientSection(context, shopForClient, menu, orderService);
            }
            break;

        case "2": 
                 
            HandleAdminSection(context, menu, stores, employeeManager, productManager, statsService, orderService);
            break;

        case "3":
            HandleRegistration(context);
            break;

        case "4": 
            HandleAbout();
            break;

        case "0": 
            startProgram = false;
            Console.WriteLine("\nDziękujemy za skorzystanie z ElectroHub. Do widzenia!");
            break;

        default:
            Console.WriteLine("Nieznana opcja. Naciśnij dowolny klawisz...");
            Console.ReadKey();
            break;
    }
}









void HandleClientSection(ApplicationDbContext context, Store store, MenuContainer menu, OrderService orderService)
{
    Console.Clear();
    Console.WriteLine("--- LOGOWANIE DO SKLEPU ---");


    
    Console.Write("Podaj swój adres e-mail użyty przy rejestracji: ");
    string emailInput = Console.ReadLine();

    var currentCustomer = context.Customers
    .FirstOrDefault(c => c.Email.ToLower() == emailInput.ToLower());

    if (currentCustomer == null)
    {
        Console.WriteLine("\n[BŁĄD] Nie znaleziono użytkownika o takim adresie e-mail.");
        Console.WriteLine("Upewnij się, że wpisałeś go poprawnie lub zarejestruj się w Menu Głównym.");
        Console.ReadKey();
        return;
    }

    Console.WriteLine($"\nWitaj, {currentCustomer.FirstName}! Twój portfel: {currentCustomer.WalletBalance:C}");
    Console.ReadKey();

    

    var currentBasket = new Dictionary<Product, int>();

    bool loggedIn = true;
    while (loggedIn)
    {
        menu.ShowClientMenu();
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1": 
                Console.Clear();
                Console.WriteLine($"--- OFERTA SKLEPU: {store.Name} ---");
                var availableProducts = store.Inventory.Where(p => p.Stock > 0).ToList();

                if (availableProducts.Any())
                {
                    foreach (var p in availableProducts)
                    {
                        Console.WriteLine($"| ID: {p.ProductId} | [{p.Name}] Cena: {p.Price:C} | Dostępne: {p.Stock} szt. | Kat: {p.Category}");
                    }
                }
                else Console.WriteLine("Aktualnie brak towaru w magazynie.");

                Console.WriteLine("\nNaciśnij dowolny klawisz...");
                Console.ReadKey();
                break;

            case "2": 
                Console.Clear();
                Console.Write("Wpisz nazwę produktu: ");
                string search = Console.ReadLine().ToLower();

                var found = store.Inventory.Where(p => p.Name.ToLower().Contains(search) && p.Stock > 0).ToList();

                if (found.Any())
                {
                    foreach (var p in found)
                        Console.WriteLine($"ZNALEZIONO: {p.Name} | {p.Price:C} | Stan: {p.Stock}");
                }
                else Console.WriteLine("Nic nie znaleziono.");
                Console.ReadKey();
                break;

            case "3": 
                Console.Clear();
                Console.WriteLine("--- DODAWANIE DO KOSZYKA ---");
                Console.Write("Podaj dokładną nazwę produktu: ");
                string prodName = Console.ReadLine();

                var productToAdd = store.Inventory.FirstOrDefault(p => p.Name.Equals(prodName, StringComparison.OrdinalIgnoreCase));

                if (productToAdd != null)
                {
                    Console.Write($"Dostępne {productToAdd.Stock} szt. Ile chcesz kupić? ");
                    if (int.TryParse(Console.ReadLine(), out int qty) && qty > 0)
                    {
                        int alreadyInBasket = currentBasket.ContainsKey(productToAdd) ? currentBasket[productToAdd] : 0;

                        if (productToAdd.Stock >= (qty + alreadyInBasket))
                        {
                            if (currentBasket.ContainsKey(productToAdd)) currentBasket[productToAdd] += qty;
                            else currentBasket.Add(productToAdd, qty);
                            Console.WriteLine($"Dodałeś {qty} szt. produktu '{productToAdd.Name}' do koszyka.");
                        }
                        else Console.WriteLine("Błąd: Niewystarczająca ilość towaru.");
                    }
                    else Console.WriteLine("Błąd: Niepoprawna ilość.");
                }
                else Console.WriteLine("Nie znaleziono produktu.");
                Console.ReadKey();
                break;

            case "4": 
                Console.Clear();
                Console.WriteLine("--- TWÓJ KOSZYK ---");
                if (currentBasket.Any())
                {
                    decimal basketTotal = 0;
                    foreach (var item in currentBasket)
                    {
                        decimal lineTotal = item.Key.Price * item.Value;
                        basketTotal += lineTotal;
                        Console.WriteLine($"{item.Key.Name} x{item.Value} = {lineTotal:C}");
                    }
                    Console.WriteLine("---------------------------");
                    Console.WriteLine($"RAZEM DO ZAPŁATY: {basketTotal:C}");
                }
                else Console.WriteLine("Twój koszyk jest pusty.");
                Console.ReadKey();
                break;

            case "5": 
                Console.Clear();
                Console.WriteLine("--- FINALIZACJA ZAMÓWIENIA ---");
                if (!currentBasket.Any())
                {
                    Console.WriteLine("Koszyk jest pusty.");
                    Console.ReadKey();
                    break;
                }


                decimal totalAmount = currentBasket.Sum(x => x.Key.Price * x.Value);
                Console.WriteLine($"Kwota do zapłaty: {totalAmount:C}");
                Console.WriteLine("\nCzy chcesz kontynuować? (t/n)");
                if (Console.ReadLine().ToLower() != "t") break;




                Console.WriteLine("\n--- ADRES DOSTAWY ---");
                Console.Write("Miasto: "); string city = Console.ReadLine();
                Console.Write("Ulica: "); string street = Console.ReadLine();
                Console.Write("Kod: "); string zip = Console.ReadLine();
                var address = new Address { City = city, Street = street, ZipCode = zip, Country = "Polska" };





                Console.WriteLine("\n--- METODA PŁATNOŚCI ---");
                Console.WriteLine("1. BLIK\n2. Karta\n3. Gotówka");
                Console.Write("Wybór: ");
                string payChoice = Console.ReadLine();
                IPayment paymentStrategy = null;

                try
                {
                    switch (payChoice)
                    {
                        case "1":
                            Console.Write("Kod BLIK: ");
                            paymentStrategy = new BlikPayment(Console.ReadLine());

                            
                            System.Console.WriteLine("\n\nŁączenie z bankiem...");
                            for (int i = 0; i < 15; i++)
                            {
                                System.Console.Write("█");
                                Thread.Sleep(300); 
                            }
                            System.Console.WriteLine(); 
                                                        
                            break;

                        case "2":
                            Console.Write("Nr karty: "); string cNum = Console.ReadLine();
                            Console.Write("Właściciel: "); string cOwn = Console.ReadLine();
                            paymentStrategy = new CreditCardPayment(cNum, cOwn);

                            
                            System.Console.WriteLine("\n\nŁączenie z bankiem...");
                            for (int i = 0; i < 20; i++)
                            {
                                System.Console.Write("█");
                                Thread.Sleep(300);
                            }
                            
                                                        
                            break;

                        case "3":
                            paymentStrategy = new CashPayment();
                            break;

                        default:
                            Console.WriteLine("Zła opcja.");
                            break;
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"\n[BŁĄD DANYCH] {ex.Message}");
                    Console.ReadKey();
                    break;
                }

                if (paymentStrategy == null) { Console.ReadKey(); break; }

                var newOrder = orderService.CreateOrder(currentCustomer, address, store);
                foreach (var item in currentBasket) orderService.AddItemToOrder(newOrder, item.Key, item.Value);

                if (orderService.ProcessOrderPayment(newOrder, paymentStrategy))
                {
                    context.SaveChanges();

                    currentCustomer.Orders.Add(newOrder);


                    currentBasket.Clear();
                    Console.WriteLine("\n[SUKCES] Zamówienie zrealizowane!");
                }
                else
                {
                    Console.WriteLine("\n[BŁĄD] Płatność odrzucona.");


                    context.Entry(newOrder).State = EntityState.Detached;

                    
                    if (newOrder.DeliveryAddress != null)
                    {
                        context.Entry(newOrder.DeliveryAddress).State = EntityState.Detached;
                    }

                }

                Console.ReadKey();
                break;

            case "6": 
                Console.Clear();
                Console.WriteLine("--- HISTORIA ---");
                if (currentCustomer.Orders != null && currentCustomer.Orders.Any())
                {
                    foreach (var o in currentCustomer.Orders)
                        Console.WriteLine($"Data: {o.OrderDate:yyyy-MM-dd} | Status: {o.Status} | {o.GetTotalAmount():C}");
                }
                else Console.WriteLine("Brak zamówień.");
                Console.ReadKey();
            break;

            case "7": 
                Console.Clear();
                Console.WriteLine("--- DOŁADOWANIE PORTFELA ---");
                Console.WriteLine($"Twój aktualny stan konta: {currentCustomer.WalletBalance:C}");

                Console.Write("\nPodaj kwotę, jaką chcesz wpłacić: ");
                string amountInput = Console.ReadLine();

                if (decimal.TryParse(amountInput, out decimal topUpAmount) && topUpAmount > 0)
                {
                   
                    currentCustomer.WalletBalance += topUpAmount;

                   
                    context.SaveChanges();

                    Console.WriteLine("\n=========================================");
                    Console.WriteLine(" [SUKCES] Środki zostały dodane do konta.");
                    Console.WriteLine($" Nowy stan konta: {currentCustomer.WalletBalance:C}");
                    Console.WriteLine("=========================================");
                }
                else
                {
                    Console.WriteLine("\n[BŁĄD] Podano nieprawidłową kwotę (musi być większa od 0).");
                }

                Console.WriteLine("\nNaciśnij dowolny klawisz...");
                Console.ReadKey();
                break;

            case "0":
                loggedIn = false;
                break;
        }
    }
}















void HandleAdminSection(ApplicationDbContext context, MenuContainer menu, List<Store> stores, EmployeeManager empMgr, ProductManager prodMgr, ServiceOfStatistics stats, OrderService orderService)
{
    Console.Clear();
    menu.ShowStoreSelectionHeader();

    Console.WriteLine("--- STREFA CHRONIONA ---");
    Console.Write("Podaj hasło administratora: ");
    string password = Console.ReadLine();

    if (password != "zaq1@WSX")
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n[BŁĄD] Nieprawidłowe hasło! Dostęp odmówiony.");
        Console.ResetColor();
        Console.WriteLine("Naciśnij dowolny klawisz, aby wrócić...");
        Console.ReadKey();
        return; 
    }
    

    
    Console.Clear();
    menu.ShowStoreSelectionHeader();


    for (int i = 0; i < stores.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {stores[i].Name} | {stores[i].Address.Street} | {stores[i].Address.ZipCode} | {stores[i].Address.Country} ");
    }
    Console.WriteLine("0. Powrót");



    Console.Write("\nWybierz sklep: ");
    if (!int.TryParse(Console.ReadLine(), out int index) || index == 0 || index > stores.Count) return;

    var selectedStore = stores[index - 1];
    bool inStore = true;




    while (inStore)
    {
        Console.Clear();
        Console.WriteLine($">>> ZARZĄDZANIE: {selectedStore.Name.ToUpper()} <<<");
        menu.ShowAdminMenu();

        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1": HandleInventory(context, selectedStore, prodMgr, menu); break;
            case "2": HandleStaff(context, selectedStore, empMgr, menu); break;
            case "3": HandleCustomers(context, menu); break;
            case "4": HandleReports(selectedStore, stats, menu); break;
            case "5":
                Console.Clear();
                Console.WriteLine("--- SYMULACJA LOGISTYKI I DOSTAW ---");
                Console.WriteLine("Sprawdzanie statusów zamówień...");

               
                int updates = orderService.SimulateLogistics();

                
                context.SaveChanges();

                if (updates > 0)
                    Console.WriteLine($"\n[SUKCES] Zaktualizowano statusy {updates} zamówień.");
                else
                    Console.WriteLine("\n[INFO] Brak zamówień wymagających aktualizacji (za krótki czas oczekiwania).");

                Console.WriteLine("Naciśnij dowolny klawisz...");
                Console.ReadKey();
                break;
            case "0": inStore = false; break;
        }
    }
}






void HandleInventory(ApplicationDbContext context, Store store, ProductManager prodMgr, MenuContainer menu)
{
    bool back = false;
    while (!back)
    {
        menu.ShowInventoryMenu(store.Name);
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Clear();
                Console.WriteLine("--- DODAWANIE NOWEGO PRODUKTU ---");

                Console.Write("Podaj nazwę produktu: ");
                string name = Console.ReadLine();

                decimal price = 0;
                while (true)
                {
                    Console.Write("Podaj cenę: ");
                    if (decimal.TryParse(Console.ReadLine(), out price) && price >= 0) break;
                    Console.WriteLine("Błąd: Podaj poprawną wartość liczbową dla ceny.");
                }

                int quant = 0;
                while (true)
                {
                    Console.Write("Podaj ilość początkową: ");
                    if (int.TryParse(Console.ReadLine(), out quant) && quant >= 0) break;
                    Console.WriteLine("Błąd: Ilość musi być liczbą całkowitą nieujemną.");
                }

                ProductCategory category = ProductCategory.Accessory;
                bool validCat = false;
                while (!validCat)
                {
                    Console.WriteLine("\nWybierz kategorię produktu:");
                    Console.WriteLine("1. Computer");
                    Console.WriteLine("2. Smartphone");
                    Console.WriteLine("3. Audio");
                    Console.WriteLine("4. TV");
                    Console.WriteLine("5. LargeAppliance");
                    Console.WriteLine("6. SmallAppliance");
                    Console.WriteLine("7. Accessory");
                    Console.Write("Twój wybór (1-7): ");

                    string catInput = Console.ReadLine();
                    switch (catInput)
                    {
                        case "1": category = ProductCategory.Computer; validCat = true; break;
                        case "2": category = ProductCategory.Smartphone; validCat = true; break;
                        case "3": category = ProductCategory.Audio; validCat = true; break;
                        case "4": category = ProductCategory.TV; validCat = true; break;
                        case "5": category = ProductCategory.LargeAppliance; validCat = true; break;
                        case "6": category = ProductCategory.SmallAppliance; validCat = true; break;
                        case "7": category = ProductCategory.Accessory; validCat = true; break;
                        default:
                            Console.WriteLine("Błąd: Wybierz cyfrę z zakresu 1-7.");
                            break;
                    }
                }

                
                var newProd = new Product(name, price, 0, category);

                
                prodMgr.AddProduct(store, newProd, quant);


                context.SaveChanges();



                Console.WriteLine($"\n[SYSTEM] Pomyślnie dodano produkt: {name} ({category}) w ilości {quant} szt.");
                Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
                Console.ReadKey();
                break;


            case "2":
                Console.Clear();
                Console.WriteLine("--- EDYCJA PRODUKTU ---");
                Console.Write("Podaj nazwę produktu, który chcesz edytować: ");
                string editName = Console.ReadLine();

                var prodToEdit = store.Inventory.FirstOrDefault(p => p.Name == editName);

                if (prodToEdit != null)
                {
                    Console.WriteLine($"\nEdytujesz: {prodToEdit.Name}");
                    Console.WriteLine("Co chcesz zmienić?");
                    Console.WriteLine("1. Nazwę");
                    Console.WriteLine("2. Cenę");
                    Console.WriteLine("3. Ilość (Stock)");
                    Console.WriteLine("4. Kategorię");
                    Console.WriteLine("0. Anuluj");
                    Console.Write("Wybór: ");

                    string editChoice = Console.ReadLine();
                    switch (editChoice)
                    {
                        case "1":
                            Console.Write($"Stara nazwa: {prodToEdit.Name}. Podaj nową nazwę: ");
                            prodToEdit.Name = Console.ReadLine();
                            Console.WriteLine("Nazwa została zmieniona.");
                            break;

                        case "2":
                            Console.Write($"Stara cena: {prodToEdit.Price:C}. Podaj nową cenę: ");
                            if (decimal.TryParse(Console.ReadLine(), out decimal newPrice))
                            {
                                prodToEdit.Price = newPrice;
                                Console.WriteLine("Cena została zmieniona.");
                            }
                            break;

                        case "3":
                            Console.Write($"Aktualny stan: {prodToEdit.Stock}. Podaj nową ilość: ");
                            if (int.TryParse(Console.ReadLine(), out int newStock))
                            {
                                prodToEdit.Stock = newStock;
                                Console.WriteLine("Stan magazynowy został zmieniony.");
                            }
                            break;

                        case "4":
                            ProductCategory newCategory = prodToEdit.Category;
                            bool validCategory = false;

                            while (!validCategory)
                            {
                                Console.WriteLine("\nWybierz nową kategorię:");
                                Console.WriteLine("1. Computer");
                                Console.WriteLine("2. Smartphone");
                                Console.WriteLine("3. Audio");
                                Console.WriteLine("4. TV");
                                Console.WriteLine("5. LargeAppliance");
                                Console.WriteLine("6. SmallAppliance");
                                Console.WriteLine("7. Accessory");
                                Console.Write("Wybór (1-7): ");

                                string cat1Input = Console.ReadLine();

                                switch (cat1Input)
                                {
                                    case "1": newCategory = ProductCategory.Computer; validCategory = true; break;
                                    case "2": newCategory = ProductCategory.Smartphone; validCategory = true; break;
                                    case "3": newCategory = ProductCategory.Audio; validCategory = true; break;
                                    case "4": newCategory = ProductCategory.TV; validCategory = true; break;
                                    case "5": newCategory = ProductCategory.LargeAppliance; validCategory = true; break;
                                    case "6": newCategory = ProductCategory.SmallAppliance; validCategory = true; break;
                                    case "7": newCategory = ProductCategory.Accessory; validCategory = true; break;
                                    default:
                                        Console.WriteLine("Błąd: Nieprawidłowy wybór. Wybierz cyfrę od 1 do 7.");
                                        break;
                                }
                            }

                            prodToEdit.Category = newCategory;
                            Console.WriteLine($"Kategoria została zmieniona na: {newCategory}");
                            break;

                        case "0":
                            Console.WriteLine("Anulowano edycję.");
                            break;
                    }

                   
                    if (editChoice != "0")
                    {
                        context.SaveChanges();
                        Console.WriteLine("Zmiany zostały zapisane w bazie danych.");
                    }
                }
                else
                {
                    Console.WriteLine("Nie znaleziono produktu o takiej nazwie.");
                }

                Console.WriteLine("\nNaciśnij dowolny klawisz, aby kontynuować...");
                Console.ReadKey();
                break;




            case "3":
                Console.Clear();
                Console.WriteLine("--- USUWANIE PRODUKTU Z OFERTY ---");
                Console.Write("Podaj nazwę produktu do usunięcia: ");
                string removeName = Console.ReadLine();

                var prodToRemove = store.Inventory.FirstOrDefault(p => p.Name == removeName);

                if (prodToRemove != null)
                {
                    Console.WriteLine($"\nZNALEZIONO PRODUKT:");
                    Console.WriteLine($"Nazwa: {prodToRemove.Name}");
                    Console.WriteLine($"Cena: {prodToRemove.Price:C}");
                    Console.WriteLine($"Kategoria: {prodToRemove.Category}");
                    Console.WriteLine($"Stan: {prodToRemove.Stock} szt.");

                    Console.WriteLine("\nCzy na pewno chcesz usunąć ten produkt?");
                    Console.WriteLine("1. Tak, usuń");
                    Console.WriteLine("0. Nie, anuluj");
                    Console.Write("Wybór: ");

                    string confirm = Console.ReadLine();
                    switch (confirm)
                    {
                        case "1":
                            
                            prodMgr.RemoveProduct(store, prodToRemove, prodToRemove.Stock);


                            context.SaveChanges();



                            Console.WriteLine("\n[SYSTEM] Produkt został całkowicie usunięty z oferty sklepu.");
                            break;

                        case "0":
                        default:
                            Console.WriteLine("\n[SYSTEM] Operacja usuwania przerwana.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\n[SYSTEM] Błąd: Nie znaleziono produktu o takiej nazwie.");
                }

                Console.WriteLine("Naciśnij dowolny klawisz, aby wrócić...");
                Console.ReadKey();
                break;





            case "4": 
                Console.WriteLine("\n--- AKTUALIZACJA STANU (DOSTAWA) ---");
                Console.Write("Podaj nazwę produktu: ");
                string stockName = Console.ReadLine();

                var prodToUpdate = store.Inventory.FirstOrDefault(p => p.Name == stockName);

                if (prodToUpdate != null)
                {
                    Console.Write("Ile sztuk dostarczono: ");
                    if (int.TryParse(Console.ReadLine(), out int addQty))
                    {
                       
                        prodMgr.AddProduct(store, prodToUpdate, addQty);

                        context.SaveChanges();

                        Console.WriteLine("Stan magazynowy zaktualizowany.");
                    }
                }
                else Console.WriteLine("Brak produktu w magazynie. Użyj opcji 'Dodaj', aby wprowadzić nowy asortyment.");
                Console.ReadKey();
                break;

            case "0":
                back = true;
                break;
        }
    }
}




void HandleStaff(ApplicationDbContext context, Store store, EmployeeManager empMgr, MenuContainer menu)
{
    bool back = false;
    while (!back)
    {
        menu.ShowStaffMenu(store.Name);
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1": 
                Console.WriteLine($"\n--- LISTA KADRY: {store.Name} ---");
                
                if (store.Staff == null || !store.Staff.Any())
                {
                    Console.WriteLine("Brak zatrudnionych pracowników w tym sklepie.");
                }
                else
                {
                    foreach (var e in store.Staff)
                    {
                        Console.WriteLine($"ID: {e.Id} | {e.FirstName} {e.LastName} | {e.Position} | {e.PhoneNumber} | Pensja: {e.Salary:C}");
                    }
                }
                Console.WriteLine("\nNaciśnij dowolny klawisz...");
                Console.ReadKey();
            break;




            case "2":
                Console.Clear();
                Console.WriteLine("\n--- NOWE ZATRUDNIENIE PRACOWNIKA ---");

                
                Console.Write("Imię: "); string firstName = Console.ReadLine();
                Console.Write("Nazwisko: "); string lastName = Console.ReadLine();


                string phone = "";
                string phonePattern = @"^\+\d{2}\d{9}$"; 
                while (true)
                {
                    Console.Write("Telefon (+48xxxxxxxxx): ");
                    phone = Console.ReadLine();

                    
                    if (Regex.IsMatch(phone, phonePattern)) break;

                    Console.WriteLine("Błąd: Numer musi być w formacie +48123456789 (bez spacji!).");
                }


                string email = "";   
                string emailPattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,6}$";

                while (true)
                {
                    Console.Write("Email: ");
                    email = Console.ReadLine();

                   
                    if (Regex.IsMatch(email, emailPattern)) break;

                    Console.WriteLine("Błąd: Niepoprawny format email (wymagany np. jan@domena.pl).");
                }


                EmployeePosition position = EmployeePosition.Cashier;
                bool validPos = false;

                while (!validPos)
                {
                    Console.WriteLine("\nWybierz stanowisko:");
                    Console.WriteLine("1. Cashier (Kasjer)");
                    Console.WriteLine("2. WarehouseWorker (Magazynier)");
                    Console.WriteLine("3. Technician (Serwisant)");
                    Console.WriteLine("4. Cleaner (Personel Sprzątający)");
                    Console.WriteLine("5. Supervisor (Nadzorca)");
                    Console.WriteLine("6. Manager (Kierownik)");
                    Console.Write("Twój wybór (1-6): ");

                    string posInput = Console.ReadLine();
                    switch (posInput)
                    {
                        case "1": position = EmployeePosition.Cashier; validPos = true; break;
                        case "2": position = EmployeePosition.WarehouseWorker; validPos = true; break;
                        case "3": position = EmployeePosition.Technician; validPos = true; break;
                        case "4": position = EmployeePosition.Cleaner; validPos = true; break;
                        case "5": position = EmployeePosition.Supervisor; validPos = true; break;
                        case "6": position = EmployeePosition.Manager; validPos = true; break;
                        default:
                            Console.WriteLine("Błąd: Wybierz cyfrę z zakresu 1-6.");
                            break;
                    }
                }

               
                decimal salary = 0;
                while (true)
                {
                    Console.Write("Pensja początkowa: ");
                    if (decimal.TryParse(Console.ReadLine(), out salary) && salary >= 0) break;
                    Console.WriteLine("Błąd: Podaj poprawną kwotę (nieujemną).");
                }

                
                var newEmployee = new Employee(
                    firstName, lastName, phone, email,
                    store, position, salary, DateTime.Now
                );

                
                empMgr.HireEmployee(store, newEmployee); 
                context.SaveChanges();

                Console.WriteLine($"\n[SUKCES] Zatrudniono pracownika: {firstName} {lastName} jako {position}.");
                Console.ReadKey();
                break;



            case "3":
                Console.Clear();
                Console.WriteLine("\n--- EDYCJA DANYCH PRACOWNIKA ---");

                Console.Write("Podaj ID pracownika: ");
                if (!int.TryParse(Console.ReadLine(), out int empId))
                {
                    Console.WriteLine("Błąd: ID musi być liczbą.");
                    Console.ReadKey();
                    break;
                }

                var employee = empMgr.FindById(empId); 

                
                if (employee != null && store.Staff.Any(s => s.Id == employee.Id))
                {
                    Console.WriteLine($"\nEdytujesz: {employee.FirstName} {employee.LastName}");
                    Console.WriteLine($"Obecna rola: {employee.Position}, Pensja: {employee.Salary:C}");

                   
                    EmployeePosition newPos = employee.Position;
                    bool validPos2 = false;

                    while (!validPos2)
                    {
                        Console.WriteLine("\nWybierz nową rolę:");
                        Console.WriteLine("1. Cashier (Kasjer)");
                        Console.WriteLine("2. WarehouseWorker (Magazynier)");
                        Console.WriteLine("3. Technician (Serwisant)");
                        Console.WriteLine("4. Cleaner (Personel Sprzątający)");
                        Console.WriteLine("5. Supervisor (Nadzorca)");
                        Console.WriteLine("6. Manager (Kierownik)");
                        Console.Write("Twój wybór (1-6): ");

                        string posChoice = Console.ReadLine();
                        switch (posChoice)
                        {
                            case "1": newPos = EmployeePosition.Cashier; validPos2 = true; break;
                            case "2": newPos = EmployeePosition.WarehouseWorker; validPos2 = true; break;
                            case "3": newPos = EmployeePosition.Technician; validPos2 = true; break;
                            case "4": newPos = EmployeePosition.Cleaner; validPos2 = true; break;
                            case "5": newPos = EmployeePosition.Supervisor; validPos2 = true; break;
                            case "6": newPos = EmployeePosition.Manager; validPos2 = true; break;
                            default:
                                Console.WriteLine("Błąd: Wybierz cyfrę z zakresu 1-6.");
                                break;
                        }
                    }

                    
                    decimal newSalary = 0;
                    while (true)
                    {
                        Console.Write("Podaj nową pensję: ");
                        if (decimal.TryParse(Console.ReadLine(), out newSalary) && newSalary >= 0) break;
                        Console.WriteLine("Błąd: Podaj poprawną kwotę.");
                    }

                    
                    empMgr.ChangePosition(employee, newPos, newSalary); //
                    context.SaveChanges();

                    Console.WriteLine("\n[SUKCES] Dane pracownika zostały zaktualizowane.");
                }
                else
                {
                    Console.WriteLine("\n[BŁĄD] Nie znaleziono pracownika o tym ID w bieżącym sklepie.");
                }

                Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
                Console.ReadKey();
            break;




            case "4": 
                Console.Clear();
                Console.WriteLine("\n--- ZWOLNIENIE PRACOWNIKA ---");

                Console.Write("Podaj ID pracownika do zwolnienia: ");

                
                if (!int.TryParse(Console.ReadLine(), out int fireId))
                {
                    Console.WriteLine("Błąd: ID musi być liczbą.");
                    Console.ReadKey();
                    break;
                }

                
                var empToFire = store.Staff.FirstOrDefault(e => e.Id == fireId);

                if (empToFire != null)
                {
                   
                    Console.WriteLine($"\n[OSTRZEŻENIE] Czy na pewno chcesz rozwiązać umowę z pracownikiem?");
                    Console.WriteLine($"Pracownik: {empToFire.FirstName} {empToFire.LastName}");
                    Console.WriteLine($"Stanowisko: {empToFire.Position}");
                    Console.WriteLine($"Pensja: {empToFire.Salary:C}");
                    Console.WriteLine($"Numer Telefonu: {empToFire.PhoneNumber}");
                    Console.WriteLine($"Email: {empToFire.Email}");

                    
                    Console.WriteLine("\n1. TAK, zwolnij pracownika (Operacja trwała)");
                    Console.WriteLine("0. NIE, anuluj");
                    Console.Write("Twój wybór: ");

                    string decision = Console.ReadLine();

                    if (decision == "1")
                    {
                       
                        empMgr.FireEmployeeByObject(store, empToFire);

                        
                        context.SaveChanges();

                        Console.WriteLine($"\n[SUKCES] Pracownik {empToFire.FirstName} {empToFire.LastName} został usunięty z systemu.");
                    }
                    else
                    {
                        Console.WriteLine("\n[INFO] Operacja zwolnienia została anulowana.");
                    }
                }
                else
                {
                    Console.WriteLine("\n[BŁĄD] Nie znaleziono pracownika o takim ID w bieżącym sklepie.");
                }

                Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
                Console.ReadKey();
                break;

            case "0":
                back = true;
                break;
        }
    }
}




void HandleCustomers(ApplicationDbContext context, MenuContainer menu)
{
    bool back = false;
    while (!back)
    {
        
        menu.ShowUsersMenu();
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1": 
                Console.Clear();
                Console.WriteLine("--- LISTA KLIENTÓW ---");

                
                var customers = context.Customers
                                       .Include(c => c.Orders)
                                       .ToList();

                if (customers.Any())
                {
                    foreach (var c in customers)
                    {
                        Console.WriteLine("------------------------------------------------");
                        Console.WriteLine(c.GetInfo());
                        Console.WriteLine($"Saldo portfela: {c.WalletBalance:C}");
                        Console.WriteLine($"Data rejestracji: {c.RegistrationDate:yyyy-MM-dd}");
                    }
                }
                else
                {
                    Console.WriteLine("Brak zarejestrowanych klientów w bazie.");
                }

                Console.WriteLine("\nNaciśnij dowolny klawisz, aby wrócić...");
                Console.ReadKey();
                break;

            case "2": 
                Console.Clear();
                Console.WriteLine("--- USUWANIE KONTA UŻYTKOWNIKA ---");
                Console.Write("Podaj ID klienta do usunięcia: ");

                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    var customerToRemove = context.Customers
                                                  .Include(c => c.Orders) 
                                                  .FirstOrDefault(c => c.Id == id); 

                    if (customerToRemove != null)
                    {
                        Console.WriteLine("\nZNALEZIONO KLIENTA:");  
                        Console.WriteLine(customerToRemove.GetInfo());
                        Console.WriteLine($"Saldo: {customerToRemove.WalletBalance:C}");

                        Console.WriteLine("\n[OSTRZEŻENIE] Czy na pewno chcesz usunąć to konto? (t/n)");
                        string confirm = Console.ReadLine();

                        switch (confirm.ToLower())
                        {
                            case "t":
                                context.Customers.Remove(customerToRemove);
                                context.SaveChanges();
                                Console.WriteLine("\n[SUKCES] Konto klienta zostało usunięte.");
                                break;
                            case "n":
                                Console.WriteLine("\n[INFO] Anulowano usuwanie.");
                                break;
                            default:
                                Console.WriteLine("\n[BŁĄD] Nieznana komenda. Anulowano.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n[BŁĄD] Nie znaleziono klienta o podanym ID.");
                    }
                }
                else
                {
                    Console.WriteLine("\n[BŁĄD] ID musi być liczbą.");
                }

                Console.ReadKey();
                break;

            case "0":
                back = true;
                break;

            default:
                
                break;
        }
    }
}


void HandleReports(Store store, ServiceOfStatistics stats, MenuContainer menu)
{
    bool back = false;
    while (!back)
    {
        
        menu.ShowStatisticsMenu(store.Name);
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1": 
                Console.Clear();
                Console.WriteLine("--- OGÓLNE STATYSTYKI SPRZEDAŻY ---");

                
                decimal avgValue = stats.GetAverageOrderValue();
                Console.WriteLine($"Średnia wartość zamówienia: {avgValue:C}");

               
                var expensiveOrder = stats.GetMostExpensiveOrder();
                if (expensiveOrder != null)
                {
                    Console.WriteLine("\n[NAJDROŻSZE ZAMÓWIENIE]");
                    Console.WriteLine($"ID: {expensiveOrder.OrderId}");
                    Console.WriteLine($"Kwota: {expensiveOrder.GetTotalAmount():C}");
                    Console.WriteLine($"Klient: {expensiveOrder.Purchaser.FirstName} {expensiveOrder.Purchaser.LastName}");
                    Console.WriteLine($"Data: {expensiveOrder.OrderDate}");
                }
                else
                {
                    Console.WriteLine("\nBrak danych o najdroższym zamówieniu.");
                }

                Console.WriteLine("\nNaciśnij dowolny klawisz...");
                Console.ReadKey();
                break;

            case "2": 
                Console.Clear();
                Console.WriteLine("--- PRZYCHÓD WG KATEGORII PRODUKTÓW ---");

                
                var categoryRevenue = stats.GetRevenueByCategory();

                if (categoryRevenue.Any())
                {
                    
                    foreach (var entry in categoryRevenue.OrderByDescending(x => x.Value))
                    {
                        
                        Console.WriteLine($"{entry.Key,-20} : {entry.Value:C}");
                    }
                }
                else Console.WriteLine("Brak danych sprzedażowych.");

                Console.ReadKey();
                break;

            case "3":
            
                Console.Clear();
                Console.WriteLine("--- TOP KLIENCI (WG WYDATKÓW) ---");

                // Wywołujesz metodę z serwisu
                var customerSpending = stats.GetTotalSpentByCustomer();

                if (customerSpending.Any())
                {
                    int rank = 1;
                    foreach (var entry in customerSpending)
                    {
                        Console.WriteLine($"{rank}. {entry.Key.FirstName} {entry.Key.LastName} (ID: {entry.Key.Id})");
                        Console.WriteLine($"   Suma wydatków: {entry.Value:C}");
                        Console.WriteLine("-----------------------------------");
                        rank++;
                    }
                }
                else Console.WriteLine("Brak danych o wydatkach.");

                Console.ReadKey();
                break;

            case "4": 
                Console.Clear();
                Console.WriteLine("--- ROZKŁAD STATUSÓW ZAMÓWIEŃ ---");

                
                var statusDist = stats.GetStatusDistribution();

                if (statusDist.Any())
                {
                    foreach (var entry in statusDist)
                    {
                        Console.WriteLine($"Status {entry.Key}: {entry.Value} zamówień");
                    }
                }
                else Console.WriteLine("Brak zamówień.");

                Console.ReadKey();
                break;

            case "5":
                Console.Clear();
                Console.WriteLine($"--- HISTORIA ZAMÓWIEŃ: {store.Name} ---");
                if (store.Orders != null && store.Orders.Any())
                {
                    foreach (var o in store.Orders)
                    {
                        Console.WriteLine($"#{o.OrderId} | {o.OrderDate:yyyy-MM-dd} | {o.Status} | {o.GetTotalAmount():C}");
                    }
                }
                else Console.WriteLine("Brak zamówień.");

                Console.ReadKey();
                break;

            case "0":
                back = true;
                break;
        }
    }
}



























void HandleRegistration(ApplicationDbContext context)







{
    Console.Clear();
    Console.WriteLine("-------- Zarejestruj się (Nowy Klient) ---------");
    Console.WriteLine("Wypełnij formularz. Pamiętaj o poprawnych formatach!");

   
    string firstName = "";
    while (true)
    {
        Console.Write("\nPodaj Imię: ");
        firstName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(firstName)) break;
        Console.WriteLine("Błąd: Imię nie może być puste.");
    }

    
    string lastName = "";
    while (true)
    {
        Console.Write("Podaj Nazwisko: ");
        lastName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(lastName)) break;
        Console.WriteLine("Błąd: Nazwisko nie może być puste.");
    }

    
    string email = "";
    
    string emailPattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,6}$";

    while (true)
    {
        Console.Write("Email: ");
        email = Console.ReadLine();

        if (Regex.IsMatch(email, emailPattern)) break;
        Console.WriteLine("Błąd: Niepoprawny format email (wymagany np. jan@domena.pl).");
    }

    
    string phone = "";
   
    string phonePattern = @"^\+\d{2}\d{9}$";

    while (true)
    {
        Console.WriteLine("Format telefonu: +48xxxxxxxxx (razem z numerem kierunkowym)");
        Console.Write("Numer Telefonu: ");
        phone = Console.ReadLine();

        if (Regex.IsMatch(phone, phonePattern)) break;
        Console.WriteLine("Błąd: Numer musi zaczynać się od '+' i mieć kod kraju (np. +48123456789).");
    }

   
    decimal initialBalance = 0;


    Console.Write("Kwota początkowa w portfelu (np. 1000): ");
    if (!decimal.TryParse(Console.ReadLine(), out initialBalance))
    {
        initialBalance = 0;
    }

    
    try
    {
        var newCustomer = new Customer
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,        
            PhoneNumber = phone,  
            WalletBalance = initialBalance,
            RegistrationDate = DateTime.Now
        };

        context.Customers.Add(newCustomer);
        context.SaveChanges();

        Console.WriteLine("\n============================================");
        Console.WriteLine(" [SUKCES] KONTO ZOSTAŁO UTWORZONE!");
        Console.WriteLine("============================================");
        Console.WriteLine($" Witaj, {firstName}!");
        Console.WriteLine($" Twoje ID logowania to: >> {newCustomer.Id} <<");
        Console.WriteLine("============================================");
    }
    catch (ArgumentException argEx)
    {
        
        Console.WriteLine($"\n[BŁĄD DANYCH] Klasa odrzuciła dane: {argEx.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\n[BŁĄD BAZY] {ex.Message}");
    }

    Console.WriteLine("\nNaciśnij dowolny klawisz...");
    Console.ReadKey();
}














void HandleAbout()
{
    Console.Clear();
    Console.WriteLine("=============================================");
    Console.WriteLine("         O ELECTROHUB (KONTAKT)              ");
    Console.WriteLine("=============================================");

    Console.WriteLine("\n[O NAS]");
    Console.WriteLine("ElectroHub to nowoczesna sieć sklepów z elektroniką.");
    Console.WriteLine("Specjalizujemy się w sprzedaży sprzętu IT, RTV i AGD.");
    Console.WriteLine("Działamy na rynku od 2024 roku.");

    Console.WriteLine("\n[DANE KONTAKTOWE]");
    Console.WriteLine("Infolinia: +48 22 123 45 67");
    Console.WriteLine("E-mail:    kontakt@electrohub.pl");
    Console.WriteLine("Siedziba:  ul. Informatyczna 1, 00-001 Warszawa");

    Console.WriteLine("\n[GODZINY OTWARCIA]");
    Console.WriteLine("Poniedziałek - Piątek: 08:00 - 20:00");
    Console.WriteLine("Sobota: 10:00 - 18:00");
    Console.WriteLine("Niedziela: Nieczynne");

    Console.WriteLine("\n---------------------------------------------");
    Console.WriteLine("Wersja aplikacji: 1.0.0 (Beta)");
    Console.WriteLine("Autor systemu: Aleksander Nafalski, Student Politechniki Częstochowskiej ");
    Console.WriteLine("---------------------------------------------");

    Console.WriteLine("\nNaciśnij dowolny klawisz, aby wrócić...");
    Console.ReadKey();
}