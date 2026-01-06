using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Project.Model;

namespace Project
{
    class Program
    {
        static Store myStore;
        static Customer currentCustomer;

        static void Main(string[] args)
        {
            InitializeData();

            bool appRunning = true;

            while (appRunning)
            {
                Console.Clear();
                Console.WriteLine("==========================================");
                Console.WriteLine($"   WITAJ W {myStore.Name.ToUpper()}");
                Console.WriteLine("==========================================");
                Console.WriteLine($"Zalogowany: {currentCustomer.GetFullName()} | Portfel: {currentCustomer.WalletBalance:C}\n");

                Console.WriteLine("1. Katalog Produktów (Kupowanie)");
                Console.WriteLine("2. Moje Zamówienia");
                Console.WriteLine("3. Panel Admina (Pracownicy i Stan)");
                Console.WriteLine("0. Wyjście");
                Console.Write("\nWybierz opcję: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowShopMenu();
                        break;
                    case "2":
                        ShowCustomerOrders();
                        break;
                    case "3":
                        ShowAdminPanel();
                        break;
                    case "0":
                        appRunning = false;
                        break;
                    default:
                        Console.WriteLine("Niepoprawna opcja.");
                        Thread.Sleep(1000);
                        break;
                }
            }
        }

        static void ShowShopMenu()
        {
            Console.Clear();
            Console.WriteLine("--- KATALOG PRODUKTÓW ---");

            foreach (var item in myStore.Inventory)
            {
                Console.ForegroundColor = item.Quantity > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine($"ID: {item.Product.Id} | {item.Product.Name}");
                Console.ResetColor();
                Console.WriteLine($"Opis: {item.Product.GetDescription()}");
                Console.WriteLine($"Dostępne: {item.Quantity} szt. | Cena: {item.Product.Price:C}");
                Console.WriteLine("------------------------------------------");
            }

            Console.WriteLine("\nWpisz ID produktu, aby kupić (lub 0 aby wrócić):");
            if (int.TryParse(Console.ReadLine(), out int prodId) && prodId > 0)
            {
                BuyProductProcess(prodId);
            }
        }

        static void BuyProductProcess(int productId)
        {
            var inventoryItem = myStore.Inventory.FirstOrDefault(i => i.ProductId == productId);

            if (inventoryItem == null || inventoryItem.Quantity == 0)
            {
                Console.WriteLine("Produkt niedostępny lub błędne ID.");
                Thread.Sleep(1500);
                return;
            }

            Console.Write($"Podaj ilość (max {inventoryItem.Quantity}): ");
            if (int.TryParse(Console.ReadLine(), out int qty) && qty > 0)
            {
                if (qty > inventoryItem.Quantity)
                {
                    Console.WriteLine("Nie mamy tyle towaru na stanie.");
                    Thread.Sleep(1500);
                    return;
                }

                if (currentCustomer.WalletBalance < (inventoryItem.Product.Price * qty))
                {
                    Console.WriteLine("Brak wystarczających środków na koncie.");
                    Thread.Sleep(1500);
                    return;
                }

                Order newOrder = new Order
                {
                    Id = new Random().Next(1000, 9999),
                    DatePlaced = DateTime.Now,
                    Status = OrderStatus.New,
                    CustomerId = currentCustomer.CustomerId,
                    Customer = currentCustomer,
                    StoreId = myStore.Id,
                    FulfillingStore = myStore
                };

                newOrder.AddProduct(inventoryItem.Product, qty);

                currentCustomer.WalletBalance -= newOrder.TotalValue;
                myStore.UpdateStock(inventoryItem.Product, inventoryItem.Quantity - qty);
                myStore.Orders.Add(newOrder);

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n=== ZAMÓWIENIE PRZYJĘTE ===");
                Console.WriteLine(newOrder.ToString());
                Console.ResetColor();

                Console.WriteLine("\nNaciśnij dowolny klawisz...");
                Console.ReadKey();
            }
        }

        static void ShowCustomerOrders()
        {
            Console.Clear();
            Console.WriteLine($"--- HISTORIA ZAMÓWIEŃ: {currentCustomer.FirstName} ---");

            var myOrders = myStore.Orders.Where(o => o.CustomerId == currentCustomer.CustomerId).ToList();

            if (myOrders.Count == 0)
            {
                Console.WriteLine("Brak zamówień.");
            }
            else
            {
                foreach (var order in myOrders)
                {
                    Console.WriteLine(order.ToString());
                    foreach (var item in order.OrderItems)
                    {
                        Console.WriteLine($"  -> {item.Product.Name} x{item.Quantity} ({item.CalculateLineTotal():C})");
                    }
                    Console.WriteLine("- - - - - - - - -");
                }
            }
            Console.WriteLine("\nNaciśnij dowolny klawisz...");
            Console.ReadKey();
        }

        static void ShowAdminPanel()
        {
            Console.Clear();
            Console.WriteLine($"--- PANEL SKLEPU: {myStore.Name} ---");
            Console.WriteLine("\n[PRACOWNICY]");

            foreach (var emp in myStore.Employees)
            {
                Console.WriteLine(emp.GetInfo());
            }

            Console.WriteLine("\n[STATYSTYKI]");
            Console.WriteLine($"Liczba produktów (rodzajów): {myStore.Inventory.Count}");
            Console.WriteLine($"Całkowita wartość magazynu: {CalculateInventoryValue():C}");
            Console.WriteLine($"Liczba zamówień: {myStore.Orders.Count}");

            Console.WriteLine("\nNaciśnij dowolny klawisz...");
            Console.ReadKey();
        }

        static decimal CalculateInventoryValue()
        {
            return myStore.Inventory.Sum(item => item.Quantity * item.Product.Price);
        }

        static void InitializeData()
        {
            myStore = new Store
            {
                Id = 1,
                Name = "ElectroCenter",
                Address = "Aleje Jerozolimskie 1",
                City = "Warszawa",
                Region = "Mazowieckie",
                PostalCode = "00-001",
                Country = "Polska",
                PhoneNumber = "22 111 22 33"
            };

            var manager = new Employee(1, "Jan", "Kowalski", "jan@sklep.pl", "123", 8000m, EmployeePosition.Manager);
            var seller = new Employee(2, "Anna", "Nowak", "anna@sklep.pl", "456", 4000m, EmployeePosition.Salesperson);

            myStore.AddEmployee(manager);
            myStore.AddEmployee(seller);

            var laptop = new ElectronicDevice
            {
                Id = 101,
                Name = "Asus ROG",
                Manufacturer = "Asus",
                Price = 6500m,
                Category = ProductCategory.Computer,
                Processor = "Intel i9",
                RamSizeGB = 32,
                ScreenSize = "17 cali"
            };

            var phone = new ElectronicDevice
            {
                Id = 102,
                Name = "Samsung S24",
                Manufacturer = "Samsung",
                Price = 4500m,
                Category = ProductCategory.Smartphone,
                Processor = "Snapdragon 8 Gen 3",
                RamSizeGB = 12,
                ScreenSize = "6.2 cala"
            };

            var washingMachine = new HomeAppliance
            {
                Id = 201,
                Name = "UltraWash 5000",
                Manufacturer = "Bosch",
                Price = 2200m,
                Category = ProductCategory.LargeAppliance,
                EnergyClass = "A",
                Capacity = 9.0,
                PowerConsumptionWatts = 1500
            };

            myStore.UpdateStock(laptop, 5);
            myStore.UpdateStock(phone, 10);
            myStore.UpdateStock(washingMachine, 4);

            currentCustomer = new Customer(99, "Marek", "Klient", "marek@dom.pl", "999-999", "Poznań", "Wielkopolskie", "60-100")
            {
                WalletBalance = 15000m
            };
        }
    }
}