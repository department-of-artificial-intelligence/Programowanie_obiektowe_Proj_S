using System;
using System.Linq;
using Project.Model;

namespace Project
{
    class Program
    {
        static Store? mainStore;
        static Customer? activeCustomer;

        static void Main(string[] args)
        {
            SetupData();

            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("==========================================");
                Console.WriteLine($"   WITAJ W {mainStore.Name.ToUpper()}");
                Console.WriteLine("==========================================");
                Console.WriteLine($"Klient: {activeCustomer.GetFullName()}");
                Console.WriteLine($"Portfel: {activeCustomer.WalletBalance:C}");
                Console.WriteLine("------------------------------------------");

                Console.WriteLine("1. Lista Produktów (Kupowanie)");
                Console.WriteLine("2. Twoja Historia Zamówień");
                Console.WriteLine("3. Informacje o Sklepie");
                Console.WriteLine("0. Wyjście");
                Console.Write("\nWybierz opcję: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        MenuBuying();
                        break;
                    case "2":
                        MenuHistory();
                        break;
                    case "3":
                        MenuStoreInfo();
                        break;
                    case "0":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Nieznana opcja.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void MenuBuying()
        {
            Console.Clear();
            Console.WriteLine("--- DOSTĘPNE PRODUKTY ---");

            foreach (var item in mainStore.Inventory)
            {
                if (item.Quantity > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"[ID: {item.Product.Id}] {item.Product.Name}");
                    Console.ResetColor();
                    Console.WriteLine($" - {item.Product.Price:C}");
                    Console.WriteLine($"   Opis: {item.Product.GetDescription()}");
                    Console.WriteLine($"   Magazyn: {item.Quantity} szt.");
                    Console.WriteLine("- - -");
                }
            }

            Console.WriteLine("\nWpisz ID produktu, aby kupić (lub ENTER aby wrócić):");
            string idInput = Console.ReadLine();

            if (int.TryParse(idInput, out int prodId))
            {
                ProcessTransaction(prodId);
            }
        }

        static void ProcessTransaction(int productId)
        {
            var stockItem = mainStore.Inventory.FirstOrDefault(x => x.ProductId == productId);

            if (stockItem == null || stockItem.Quantity <= 0)
            {
                Console.WriteLine("Błąd: Produkt niedostępny.");
                Console.ReadKey();
                return;
            }

            Console.Write("Podaj ilość: ");
            if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
            {
                if (quantity > stockItem.Quantity)
                {
                    Console.WriteLine("Za mało towaru w magazynie!");
                    Console.ReadKey();
                    return;
                }

                decimal totalPrice = stockItem.Product.Price * quantity;

                if (activeCustomer.WalletBalance < totalPrice)
                {
                    Console.WriteLine("Niestety, masz za mało środków na koncie.");
                    Console.ReadKey();
                    return;
                }

                Order newOrder = new Order
                {
                    Id = new Random().Next(10000, 99999),
                    DatePlaced = DateTime.Now,
                    Status = OrderStatus.New,
                    CustomerId = activeCustomer.CustomerId,
                    Customer = activeCustomer,
                    StoreId = mainStore.Id,
                    FulfillingStore = mainStore
                };

                newOrder.AddProduct(stockItem.Product, quantity);

                mainStore.Orders.Add(newOrder);
                mainStore.UpdateStock(stockItem.Product, stockItem.Quantity - quantity);
                activeCustomer.WalletBalance -= totalPrice;

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nSUKCES! Zamówienie złożone.");
                Console.WriteLine($"Pobrano z konta: {totalPrice:C}");
                Console.ResetColor();
                Console.ReadKey();
            }
        }

        static void MenuHistory()
        {
            Console.Clear();
            Console.WriteLine($"--- HISTORIA ZAMÓWIEŃ: {activeCustomer.FirstName} ---");

            var orders = mainStore.Orders.Where(o => o.CustomerId == activeCustomer.CustomerId).ToList();

            if (orders.Count == 0)
            {
                Console.WriteLine("Brak zamówień.");
            }
            else
            {
                foreach (var order in orders)
                {
                    Console.WriteLine(order.ToString());
                    foreach (var line in order.OrderItems)
                    {
                        Console.WriteLine($" * {line.Product.Name} (x{line.Quantity})");
                    }
                    Console.WriteLine("-----------------------------");
                }
            }
            Console.ReadKey();
        }

        static void MenuStoreInfo()
        {
            Console.Clear();
            Console.WriteLine(mainStore.ToString());
            Console.WriteLine($"Adres: {mainStore.Address}, {mainStore.City}");
            Console.WriteLine($"Pracowników: {mainStore.Employees.Count}");
            Console.WriteLine($"Wartość magazynu: {mainStore.Inventory.Sum(x => x.Quantity * x.Product.Price):C}");
            Console.ReadKey();
        }

        static void SetupData()
        {
            mainStore = new Store
            {
                Id = 1,
                Name = "ElectroHub",
                Address = "Szkolna 12",
                City = "Warszawa",
                Region = "Mazowieckie",
                PostalCode = "00-100",
                Country = "Polska",
                PhoneNumber = "111-222-333"
            };

            var p1 = new ElectronicDevice
            {
                Id = 101,
                Name = "Laptop Gamingowy",
                Manufacturer = "Lenovo",
                Price = 4500m,
                Category = ProductCategory.Computer,
                Processor = "Ryzen 7",
                RamSizeGB = 16,
                ScreenSize = "15.6 cala"
            };

            var p2 = new ElectronicDevice
            {
                Id = 102,
                Name = "Smartfon Pro",
                Manufacturer = "Xiaomi",
                Price = 2500m,
                Category = ProductCategory.Smartphone,
                Processor = "Snapdragon 8",
                RamSizeGB = 8,
                ScreenSize = "6.7 cala"
            };

            var p3 = new HomeAppliance
            {
                Id = 201,
                Name = "Pralka Automatyczna",
                Manufacturer = "Bosch",
                Price = 1800m,
                Category = ProductCategory.LargeAppliance,
                EnergyClass = "A+++",
                Capacity = 8.0,
                PowerConsumptionWatts = 1200
            };

            mainStore.UpdateStock(p1, 5);
            mainStore.UpdateStock(p2, 10);
            mainStore.UpdateStock(p3, 3);

            activeCustomer = new Customer(1, "Jan", "Kowalski", "jan@test.pl", "500-123", "Kraków", "Małopolskie", "30-001")
            {
                WalletBalance = 10000m
            };
        }
    }
}