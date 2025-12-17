using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Project.DAL;
using Project.Model;
#nullable disable

class Program
{
    static void Main(string[] args)
    {
        // Read connection string
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        IConfiguration config = builder.Build();
        string connectionString = config.GetConnectionString("DefaultConnection");

        // Create Options
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        // Run with Database Context
        using (var context = new ApplicationDbContext(optionsBuilder.Options))
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("   PIZZERIA NETWORK MANAGEMENT SYSTEM   ");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Create New Pizzeria");
                Console.WriteLine("2. Enter Pizzeria Management");
                Console.WriteLine("3. View All Networks");
                Console.WriteLine("4. Remove Pizzeria");
                Console.WriteLine("0. Exit");
                Console.Write("\nSelect an option: ");

                switch (Console.ReadLine())
                {
                    case "1": CreatePizzeria(context); break;
                    case "2": SelectPizzeria(context); break;
                    case "3": ViewAll(context); break;
                    case "4": RemovePizzeria(context); break;
                    case "0": running = false; break;
                }
            }
        }
    }

    // NETWORK MANAGEMENT

    static void CreatePizzeria(ApplicationDbContext context)
    {
        Console.Clear();
        Console.WriteLine("--- CREATE NEW PIZZERIA ---");
        Console.Write("Enter Pizzeria Name: ");
        string name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name)) return;

        Console.Write("Enter Address: ");
        string address = Console.ReadLine();

        Pizzeria p = new Pizzeria(name, address);

        context.Pizzerias.Add(p);
        context.SaveChanges();

        Console.WriteLine("Pizzeria saved to Database!");
        Pause();
    }

    static void ViewAll(ApplicationDbContext context)
    {
        Console.Clear();
        Console.WriteLine("--- NETWORK OVERVIEW ---");

        var pizzerias = context.Pizzerias
            .Include(p => p.Workers)
            .Include(p => p.Orders)
            .Include(p => p.Menu).ThenInclude(m => m.AvailableItems)
            .ToList();

        if (pizzerias.Count == 0) Console.WriteLine("Database is empty.");
        else
            foreach (var p in pizzerias) Console.WriteLine(p.GetInfo());

        Pause();
    }

    static void RemovePizzeria(ApplicationDbContext context)
    {
        Console.Clear();
        var list = context.Pizzerias.ToList();

        if (list.Count == 0) { Console.WriteLine("Database is empty."); Pause(); return; }

        Console.WriteLine("--- REMOVE PIZZERIA ---");
        foreach (var p in list) Console.WriteLine($" - {p.Name}");

        Console.Write("\nEnter name of Pizzeria to remove: ");
        string name = Console.ReadLine();

        var toRemove = context.Pizzerias.FirstOrDefault(p => p.Name == name);
        if (toRemove != null)
        {
            context.Pizzerias.Remove(toRemove);
            context.SaveChanges();
            Console.WriteLine("Deleted.");
        }
        else Console.WriteLine("Not found.");

        Pause();
    }

    static void SelectPizzeria(ApplicationDbContext context)
    {
        Console.Clear();
        Console.WriteLine("--- SELECT PIZZERIA ---");
        var list = context.Pizzerias.ToList();
        foreach (var pizzeria in list) Console.WriteLine($" - {pizzeria.Name}");

        Console.Write("\nEnter Pizzeria Name to manage: ");
        string name = Console.ReadLine();

        // Load everything for the selected Pizzeria
        var p = context.Pizzerias
            .Include(x => x.Menu).ThenInclude(m => m.AvailableItems)
            .Include(x => x.Workers)
            .Include(x => x.Orders).ThenInclude(o => o.Items)
            .Include(x => x.Orders).ThenInclude(o => o.Client)
            .FirstOrDefault(x => x.Name == name);

        if (p == null) { Console.WriteLine("Pizzeria not found!"); Pause(); }
        else ManageSpecificPizzeria(p, context);
    }

    // SPECIFIC PIZZERIA MANAGEMENT

    static void ManageSpecificPizzeria(Pizzeria p, ApplicationDbContext context)
    {
        bool inMenu = true;
        while (inMenu)
        {
            Console.Clear();
            Console.WriteLine($"=== MANAGING: {p.Name.ToUpper()} ===");
            Console.WriteLine("1. Staff Management");
            Console.WriteLine("2. Menu Management");
            Console.WriteLine("3. Order System");
            Console.WriteLine("4. View Stats");
            Console.WriteLine("0. Back");
            Console.Write("\nOption: ");

            switch (Console.ReadLine())
            {
                case "1": ManageStaff(p, context); break;
                case "2": ManageMenu(p, context); break;
                case "3": ManageOrders(p, context); break;
                case "4": Console.Clear(); Console.WriteLine(p.GetInfo()); Pause(); break;
                case "0": inMenu = false; break;
            }
        }
    }

    static void ManageStaff(Pizzeria p, ApplicationDbContext context)
    {
        Console.Clear();
        Console.WriteLine("1. Hire Kitchen Worker");
        Console.WriteLine("2. Hire Hall Worker");
        Console.WriteLine("3. List Staff");
        Console.WriteLine("0. Back");

        string choice = Console.ReadLine();
        if (choice == "0") return;
        if (choice == "3")
        {
            foreach (var w in p.Workers) Console.WriteLine(w.GetInfo());
            Pause(); return;
        }

        Console.Write("First Name: "); string fn = Console.ReadLine();
        Console.Write("Last Name: "); string ln = Console.ReadLine();
        Console.Write("Salary: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal salary)) return;

        if (choice == "1")
        {
            Console.Write("Station: ");
            p.AddWorker(new KitchenWorker(fn, ln, salary, Console.ReadLine()));
        }
        else if (choice == "2")
        {
            p.AddWorker(new HallWorker(fn, ln, salary));
        }

        context.SaveChanges();
        Console.WriteLine("Staff hired and saved.");
        Pause();
    }

    static void ManageMenu(Pizzeria p, ApplicationDbContext context)
    {
        Console.Clear();
        Console.WriteLine("1. Add Item");
        Console.WriteLine("2. Remove Item");
        Console.WriteLine("3. View Menu");
        Console.WriteLine("0. Back");

        string choice = Console.ReadLine();
        if (choice == "0") return;
        if (choice == "3") { Console.WriteLine(p.Menu.GetInfo()); Pause(); return; }

        if (choice == "1")
        {
            Console.Write("Name: "); string name = Console.ReadLine();
            Console.Write("Price: "); decimal.TryParse(Console.ReadLine(), out decimal price);
            Console.Write("Desc: "); string desc = Console.ReadLine();
            p.Menu.AddItem(new MenuItem(0, name, price, desc));
            context.SaveChanges();
            Console.WriteLine("Saved.");
        }
        else if (choice == "2")
        {
            Console.Write("Name to remove: ");
            if (p.Menu.RemoveItem(Console.ReadLine())) { context.SaveChanges(); Console.WriteLine("Removed."); }
            else Console.WriteLine("Not found.");
        }
        Pause();
    }

    static void ManageOrders(Pizzeria p, ApplicationDbContext context)
    {
        Console.Clear();
        Console.WriteLine("1. New Order");
        Console.WriteLine("2. View Orders");
        Console.WriteLine("0. Back");

        string choice = Console.ReadLine();
        if (choice == "2")
        {
            foreach (var o in p.Orders) Console.WriteLine(o.GetInfo());
            Pause(); return;
        }

        if (choice == "1")
        {
            if (p.Menu.AvailableItems.Count == 0) { Console.WriteLine("Menu empty."); Pause(); return; }

            Console.Write("Client Name: "); string cName = Console.ReadLine();
            Console.Write("Phone: "); string cPhone = Console.ReadLine();
            Client tempClient = new Client(0, cName, "", cPhone);

            Console.WriteLine("\n" + p.Menu.GetInfo());
            List<string> itemNames = new List<string>();

            Console.WriteLine("Enter item names (type 'done' to finish):");
            while (true)
            {
                Console.Write("- ");
                string item = Console.ReadLine();
                if (item == "done") break;
                if (p.Menu.FindItem(item) != null) itemNames.Add(item);
                else Console.WriteLine("Not in menu.");
            }

            if (itemNames.Count > 0)
            {
                p.PlaceOrder(tempClient, itemNames);
                context.SaveChanges();
                Console.WriteLine("Order saved!");
            }
        }
        Pause();
    }

    static void Pause() { Console.WriteLine("\nPress any key..."); Console.ReadKey(); }
}