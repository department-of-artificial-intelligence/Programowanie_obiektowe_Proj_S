using System;
using System.Collections.Generic;
using System.Linq;
using Project.Model;
using Project.Model.Orders;
using Project.Model.People;

namespace Project
{
    class Program
    {
        private static List<Customer> _customers = new List<Customer>();
        private static List<Employee> _employees = new List<Employee>();
        private static List<Product> _products = new List<Product>();
        private static Store _mainStore;

        static void Main(string[] args)
        {
            InitializeData();
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== SYSTEM ZARZĄDZANIA SKLEPEM TECH ===");
                Console.WriteLine("1. Lista Pracowników");
                Console.WriteLine("2. Lista Klientów");
                Console.WriteLine("3. Magazyn Produktów");
                Console.WriteLine("4. Złóż nowe Zamówienie (Symulacja)");
                Console.WriteLine("5. Dodaj nowego Klienta (Test Regex)");
                Console.WriteLine("0. Wyjście");
                Console.Write("\nWybierz opcję: ");

                switch (Console.ReadLine())
                {
                    case "1": ShowEmployees(); break;
                    case "2": ShowCustomers(); break;
                    case "3": ShowProducts(); break;
                    case "4": CreateOrderSimulation(); break;
                    case "5": AddNewCustomer(); break;
                    case "0": running = false; break;
                    default: Console.WriteLine("Niepoprawna opcja!"); break;
                }

                if (running)
                {
                    Console.WriteLine("\nNaciśnij dowolny klawisz, aby kontynuować...");
                    Console.ReadKey();
                }
            }
        }

        private static void InitializeData()
        {
            _mainStore = new Store { Id = 1, Name = "Główny Magazyn", City = "Częstochowa", PhoneNumber = "+48340000000" };

            _employees.Add(new Employee(1, "Marek", "Kowalski", "m.k@tech.pl", "Jasna 1", "+48500100200", 5000m, EmployeePosition.Manager));
            _employees.Add(new Employee(2, "Adam", "Nowak", "a.n@tech.pl", "Ciemna 2", "+48500200300", 3500m, EmployeePosition.Seller));

            _customers.Add(new Customer(1, "Anna", "Zaradna", "anna@poczta.pl", "Polna 5", "+48600111222", "Częstochowa", "Śląskie", "42-200") { WalletBalance = 1000m });

            _products.Add(new Product { Id = 101, Name = "Słuchawki BT", Price = 299.99m, Manufacturer = "AudioTech" });
            _products.Add(new Product { Id = 102, Name = "Mysz Bezprzewodowa", Price = 150.00m, Manufacturer = "Logi" });
        }

        private static void ShowEmployees()
        {
            Console.WriteLine("\n--- LISTA PRACOWNIKÓW ---");
            foreach (var emp in _employees) Console.WriteLine(emp.GetInfo());
        }

        private static void ShowCustomers()
        {
            Console.WriteLine("\n--- LISTA KLIENTÓW ---");
            foreach (var cust in _customers) Console.WriteLine(cust.GetInfo());
        }

        private static void ShowProducts()
        {
            Console.WriteLine("\n--- MAGAZYN ---");
            foreach (var prod in _products) Console.WriteLine($"ID: {prod.Id} | {prod.Name} - {prod.Price:C} (Producent: {prod.Manufacturer})");
        }

        private static void AddNewCustomer()
        {
            try
            {
                Console.Write("Imię: "); string fn = Console.ReadLine();
                Console.Write("Nazwisko: "); string ln = Console.ReadLine();
                Console.Write("Telefon (+48XXXXXXXXX): "); string ph = Console.ReadLine();

                Customer nc = new Customer(_customers.Count + 1, fn, ln, "test@mail.pl", "Adres", ph, "Miasto", "Region", "00-000");
                _customers.Add(nc);
                Console.WriteLine("Sukces! Klient dodany.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"BŁĄD: {ex.Message}");
            }
        }

        private static void CreateOrderSimulation()
        {
            Console.WriteLine("\n--- SYMULACJA ZAMÓWIENIA ---");
            var customer = _customers.First();
            var product = _products.First();

            Order order = new Order
            {
                Id = 5001,
                Customer = customer,
                FulfillingStore = _mainStore,
                Status = OrderStatus.New
            };

            order.AddProduct(product, 1);
            decimal total = order.CalculateTotalValue();

            Console.WriteLine($"Klient: {customer.FirstName} kupuje {product.Name}");
            Console.WriteLine($"Do zapłaty: {total:C} | Stan portfela: {customer.WalletBalance:C}");

            Console.WriteLine("Wybierz płatność: 1. BLIK, 2. Gotówka");
            string choice = Console.ReadLine();

            if (choice == "1") order.SetPaymentMethod(new BlikPayment("123456"));
            else order.SetPaymentMethod(new CashPayment());

            order.FinalizeOrder();
            Console.WriteLine($"Status zamówienia: {order.Status}");
            Console.WriteLine($"Nowy stan portfela: {customer.WalletBalance:C}");
        }
    }
}