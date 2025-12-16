using System;
using System.Collections.Generic;
using System.Numerics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Project.DAL;
using Project.Model;
#nullable disable

class Program
{
    static PizzeriasNetwork network = new PizzeriasNetwork();

    static void Main(string[] args)
    {

        /**IHost _host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
        {
            var cns = context.Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(cns));
        }).Build();


        var context = _host.Services.GetService<ApplicationDbContext>();
        if (context != null)
        {
            context.Database.Migrate();
            context.Database.EnsureCreated();
            var person = new Person() { FirstName = "Mykhailo", LastName = "Lytvyn" };
            context.People.Add(person);
            context.SaveChanges();
        }**/

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

            string input = Console.ReadLine();

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
                    if (network.PizzeriasList.Count == 0)
                    {
                        Console.WriteLine("Network is empty.");
                    }
                    else
                    {
                        network.DisplayAll();
                    }
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
        Console.Clear();
        Console.WriteLine("--- CREATE NEW PIZZERIA ---");
        Console.Write("Enter Pizzeria Name: ");
        string name = Console.ReadLine();

        // Basic validation so we don't add empty names
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Name cannot be empty.");
            Pause();
            return;
        }

        Console.Write("Enter Address: ");
        string address = Console.ReadLine();

        Pizzeria p = new Pizzeria(name, address);
        network.AddPizzeria(p);
        Pause();
    }

    static void RemovePizzeria()
    {
        Console.Clear();
        // 1. Check if empty
        if (network.PizzeriasList.Count == 0)
        {
            Console.WriteLine("Network is empty. Add a pizzeria first.");
            Pause();
            return;
        }

        // 2. Show options
        Console.WriteLine("--- REMOVE PIZZERIA ---");
        Console.WriteLine("Available Pizzerias:");
        foreach (var p in network.PizzeriasList)
        {
            Console.WriteLine($" - {p.Name}");
        }

        Console.Write("\nEnter name of Pizzeria to remove: ");
        string name = Console.ReadLine();
        network.RemovePizzeria(name);
        Pause();
    }

    static void SelectPizzeria()
    {
        Console.Clear();
        // 1. Check if empty
        if (network.PizzeriasList.Count == 0)
        {
            Console.WriteLine("Network is empty. Add a pizzeria first.");
            Pause();
            return;
        }

        // 2. Show options
        Console.WriteLine("--- SELECT PIZZERIA ---");
        Console.WriteLine("Available Pizzerias:");
        foreach (var pizzeria in network.PizzeriasList)
        {
            Console.WriteLine($" - {pizzeria.Name}");
        }

        Console.Write("\nEnter Pizzeria Name to manage: ");
        string name = Console.ReadLine();

        Pizzeria p = network.GetPizzeria(name);
        if (p == null)
        {
            Console.WriteLine("Pizzeria not found!");
            Pause();
        }
        else
        {
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
        Console.WriteLine($"--- STAFF MANAGEMENT ({p.Name}) ---");
        Console.WriteLine("1. Hire Kitchen Worker");
        Console.WriteLine("2. Hire Hall Worker");
        Console.WriteLine("3. List All Staff");
        Console.WriteLine("0. Back");
        Console.Write("Option: ");

        string choice = Console.ReadLine();
        if (choice == "0") return;

        if (choice == "3")
        {
            Console.WriteLine("\n--- Current Staff ---");
            if (p.Workers.Count == 0) Console.WriteLine("(No staff hired yet)");

            foreach (var w in p.Workers)
                Console.WriteLine(w.GetInfo());

            Pause();
            return;
        }

        // Common inputs for hiring
        Console.Write("First Name: "); string fn = Console.ReadLine();
        Console.Write("Last Name: "); string ln = Console.ReadLine();

        Console.Write("Salary: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal salary))
        {
            Console.WriteLine("Invalid salary.");
            Pause();
            return;
        }

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
        Console.WriteLine($"--- MENU MANAGEMENT ({p.Name}) ---");
        Console.WriteLine("1. Add Item");
        Console.WriteLine("2. Remove Item");
        Console.WriteLine("3. View Menu");
        Console.WriteLine("0. Back");
        Console.Write("Option: ");

        string choice = Console.ReadLine();
        if (choice == "0") return;

        if (choice == "3")
        {
            Console.WriteLine("\n" + p.Menu.GetInfo());
            Pause();
            return;
        }

        if (choice == "1")
        {
            Console.Write("Item ID (number): ");
            int.TryParse(Console.ReadLine(), out int id);

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Price: ");
            decimal.TryParse(Console.ReadLine(), out decimal price);

            Console.Write("Description: ");
            string desc = Console.ReadLine();

            p.Menu.AddItem(new MenuItem(id, name, price, desc));
            Console.WriteLine("Item added!");
        }
        else if (choice == "2")
        {
            // List items so user knows what to remove
            Console.WriteLine("\nAvailable Items:");
            foreach (var item in p.Menu.AvailableItems) Console.WriteLine($"- {item.Name}");

            Console.Write("\nName of item to remove: ");
            string name = Console.ReadLine();
            if (p.Menu.RemoveItem(name)) Console.WriteLine("Removed.");
            else Console.WriteLine("Item not found.");
        }
        Pause();
    }

    static void ManageOrders(Pizzeria p)
    {
        Console.Clear();
        Console.WriteLine($"--- ORDER SYSTEM ({p.Name}) ---");
        Console.WriteLine("1. New Order");
        Console.WriteLine("2. View All Orders");
        Console.WriteLine("0. Back");
        Console.Write("Option: ");

        string choice = Console.ReadLine();
        if (choice == "0") return;

        if (choice == "2")
        {
            Console.WriteLine("\n--- Order History ---");
            if (p.Orders.Count == 0) Console.WriteLine("(No orders yet)");

            foreach (var o in p.Orders) Console.WriteLine(o.GetInfo());
            Pause();
            return;
        }

        if (choice == "1")
        {
            if (p.Menu.AvailableItems.Count == 0)
            {
                Console.WriteLine("Menu is empty! Add items to menu before ordering.");
                Pause();
                return;
            }

            Console.WriteLine("\nClient Details:");
            Console.Write("Name: "); string cName = Console.ReadLine();
            Console.Write("Phone: "); string cPhone = Console.ReadLine();
            Client tempClient = new Client(0, cName, "", cPhone);

            // Display Menu so they know what to order
            Console.WriteLine("\n" + p.Menu.GetInfo());

            List<string> itemNames = new List<string>();
            Console.WriteLine("Type item names to add (type 'done' to finish):");

            while (true)
            {
                Console.Write("Item Name: ");
                string item = Console.ReadLine();
                if (item.ToLower() == "done") break;

                // Visual feedback if item exists
                if (p.Menu.FindItem(item) != null) itemNames.Add(item);
                else Console.WriteLine($"'{item}' is not on the menu.");
            }

            if (itemNames.Count > 0)
            {
                var order = p.PlaceOrder(tempClient, itemNames);
                Console.WriteLine("Order Placed Successfully!");
                Console.WriteLine($"Total: {order.Items.Sum(i => i.Price)} zł");
            }
            else
            {
                Console.WriteLine("Order cancelled (no valid items).");
            }
        }
        Pause();
    }

    static void Pause()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}