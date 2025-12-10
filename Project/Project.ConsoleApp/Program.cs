using System;
using System.Collections.Generic;
using System.Numerics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Project.DAL;
using Project.Model;

class Program
{
    static PizzeriasNetwork network = new PizzeriasNetwork();

    static void Main(string[] args)
    {

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
            Person person = new Person() { FirstName = "Mykhailo", LastName = "Lytvyn" };
            context.People.Add(person);
            context.SaveChanges();
        }

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

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    CreatePizzeria();
                    break;
                case "2":
                    SelectPizzeria();
                    break;
                case "3":
                    Console.Clear();
                    network.DisplayAll();
                    Pause();
                    break;
                case "4":
                    RemovePizzeria();
                    break;
                case "0":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    Pause();
                    break;
            }
        }
    }

    // --- LEVEL 1: NETWORK MANAGEMENT ---

    static void CreatePizzeria()
    {
        Console.Write("\nEnter Pizzeria Name: ");
        string? name = Console.ReadLine();
        Console.Write("Enter Address: ");
        string? address = Console.ReadLine();

        Pizzeria p = new Pizzeria(name, address);
        network.AddPizzeria(p);
        Pause();
    }

    static void RemovePizzeria()
    {
        Console.Write("\nEnter name of Pizzeria to remove: ");
        string? name = Console.ReadLine();
        network.RemovePizzeria(name);
        Pause();
    }

    static void SelectPizzeria()
    {
        Console.Write("\nEnter Pizzeria Name to manage: ");
        string? name = Console.ReadLine();

        Pizzeria? p = network.GetPizzeria(name);
        if (p == null)
        {
            Console.WriteLine("Pizzeria not found!");
            Pause();
        }
        else
        {
            // Enter the sub-menu for this specific pizzeria
            ManageSpecificPizzeria(p);
        }
    }

    // --- LEVEL 2: SPECIFIC PIZZERIA MANAGEMENT ---

    static void ManageSpecificPizzeria(Pizzeria p)
    {
        bool inMenu = true;
        while (inMenu)
        {
            Console.Clear();
            Console.WriteLine($"=== MANAGING: {p.Name.ToUpper()} ===");
            Console.WriteLine($"Address: {p.Address}");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("1. Staff Management (Hire/Fire)");
            Console.WriteLine("2. Menu Management (Add/Remove items)");
            Console.WriteLine("3. Order System (Place/View Orders)");
            Console.WriteLine("4. View Pizzeria Statistics");
            Console.WriteLine("0. Back to Main Network");
            Console.Write("\nOption: ");

            switch (Console.ReadLine())
            {
                case "1":
                    ManageStaff(p);
                    break;
                case "2":
                    ManageMenu(p);
                    break;
                case "3":
                    ManageOrders(p);
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine(p.GetInfo());
                    Pause();
                    break;
                case "0":
                    inMenu = false;
                    break;
            }
        }
    }

    // --- SUB-MENUS ---

    static void ManageStaff(Pizzeria p)
    {
        Console.Clear();
        Console.WriteLine("--- STAFF MANAGEMENT ---");
        Console.WriteLine("1. Hire Kitchen Worker");
        Console.WriteLine("2. Hire Hall Worker");
        Console.WriteLine("3. List All Staff");
        Console.WriteLine("0. Back");
        Console.Write("Option: ");

        string choice = Console.ReadLine();
        if (choice == "0") return;

        if (choice == "3")
        {
            foreach (var w in p.Workers) Console.WriteLine(w.GetInfo());
            Pause();
            return;
        }

        // Common inputs
        Console.Write("First Name: "); string fn = Console.ReadLine();
        Console.Write("Last Name: "); string ln = Console.ReadLine();
        Console.Write("Salary: "); decimal salary = decimal.Parse(Console.ReadLine());

        if (choice == "1")
        {
            Console.Write("Kitchen Station (e.g., Oven, Prep): ");
            string station = Console.ReadLine();
            p.AddWorker(new KitchenWorker(fn, ln, salary, station));
            Console.WriteLine("Chef hired!");
        }
        else if (choice == "2")
        {
            p.AddWorker(new HallWorker(fn, ln, salary));
            Console.WriteLine("Waiter hired!");
        }
        Pause();
    }

    static void ManageMenu(Pizzeria p)
    {
        Console.Clear();
        Console.WriteLine("--- MENU MANAGEMENT ---");
        Console.WriteLine("1. Add Item");
        Console.WriteLine("2. Remove Item");
        Console.WriteLine("3. View Menu");
        Console.WriteLine("0. Back");
        Console.Write("Option: ");

        string choice = Console.ReadLine();
        if (choice == "0") return;

        if (choice == "3")
        {
            Console.WriteLine(p.Menu.GetInfo());
            Pause();
            return;
        }

        if (choice == "1")
        {
            Console.Write("Item ID: "); int id = int.Parse(Console.ReadLine());
            Console.Write("Name: "); string name = Console.ReadLine();
            Console.Write("Price: "); decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Desc: "); string desc = Console.ReadLine();

            p.Menu.AddItem(new MenuItem(id, name, price, desc));
            Console.WriteLine("Item added!");
        }
        else if (choice == "2")
        {
            Console.Write("Name of item to remove: ");
            string name = Console.ReadLine();
            if (p.Menu.RemoveItem(name)) Console.WriteLine("Removed.");
            else Console.WriteLine("Not found.");
        }
        Pause();
    }

    static void ManageOrders(Pizzeria p)
    {
        Console.Clear();
        Console.WriteLine("--- ORDER SYSTEM ---");
        Console.WriteLine("1. New Order");
        Console.WriteLine("2. View All Orders");
        Console.WriteLine("0. Back");
        Console.Write("Option: ");

        string choice = Console.ReadLine();
        if (choice == "0") return;

        if (choice == "2")
        {
            foreach (var o in p.Orders) Console.WriteLine(o.GetInfo());
            Pause();
            return;
        }

        if (choice == "1")
        {
            // Create Temporary Client
            Console.WriteLine("\nClient Details:");
            Console.Write("Name: "); string cName = Console.ReadLine();
            Console.Write("Phone: "); string cPhone = Console.ReadLine();
            Client tempClient = new Client(0, cName, "", cPhone);

            // Select Items
            List<string> itemNames = new List<string>();
            Console.WriteLine("\n" + p.Menu.GetInfo());
            Console.WriteLine("Type item names to add (type 'done' to finish):");

            while (true)
            {
                Console.Write("Item Name: ");
                string item = Console.ReadLine();
                if (item.ToLower() == "done") break;
                itemNames.Add(item);
            }

            var order = p.PlaceOrder(tempClient, itemNames);
            Console.WriteLine("Order Placed Successfully!");
            Console.WriteLine($"Total: {order.Items.Sum(i => i.Price)} zł");
        }
        Pause();
    }

    // --- UTILS ---
    static void Pause()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    static void PreloadData()
    {
        Pizzeria p = new Pizzeria("Best Pizza", "Main St");
        p.Menu.AddItem(new MenuItem(1, "Cheese", 20, "Basic"));
        p.Menu.AddItem(new MenuItem(2, "Salami", 25, "Meat"));
        p.AddWorker(new KitchenWorker("Mario", "Bros", 3000, "Oven"));
        network.AddPizzeria(p);
    }
}