using System;
using Project.Model; 

namespace Project.Model
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ROZPOCZYNAMY SYMULACJĘ SYSTEMU SKLEPOWEGO ===\n");

            
            Store myStore = new Store
            {
                Id = 1,
                Name = "Mega Electro",
                Address = "Marszałkowska 100",
                City = "Warszawa",
                Region = "Mazowieckie",
                PostalCode = "00-001",
                Country = "Polska",
                PhoneNumber = "22 123 45 67"
            };
            Console.WriteLine($"1. Utworzono sklep: {myStore}");

            
            Employee manager = new Employee(
                id: 101,
                firstName: "Jan",
                lastName: "Kowalski",
                email: "jan.k@megaelectro.pl",
                phone: "500-100-100",
                salary: 8500m,
                position: EmployeePosition.Manager
            );

            Employee seller = new Employee(
                id: 102,
                firstName: "Anna",
                lastName: "Nowak",
                email: "anna.n@megaelectro.pl",
                phone: "500-200-200",
                salary: 4500m,
                position: EmployeePosition.Salesperson
            );

           
            myStore.AddEmployee(manager);
            myStore.AddEmployee(seller);
            Console.WriteLine($"2. Zatrudniono {myStore.Employees.Count} pracowników.");

            
            Hardware laptop = new Hardware
            {
                Id = 1,
                Name = "Dell XPS 15",
                Manufacturer = "Dell",
                Price = 8000m,
                Category = ProductCategory.Computer,
                Specifications = "i7, 32GB RAM, 1TB SSD",
                WarrantyMonths = 24
            };

            Hardware phone = new Hardware
            {
                Id = 2,
                Name = "iPhone 15",
                Manufacturer = "Apple",
                Price = 5000m,
                Category = ProductCategory.Smartphone,
                Specifications = "128GB, Black",
                WarrantyMonths = 12
            };

           
            myStore.UpdateStock(laptop, 10);
            myStore.UpdateStock(phone, 20);

            
            int laptopCount = myStore.GetStockLevel(laptop);
            Console.WriteLine($"3. Magazyn zaktualizowany. Mamy {laptopCount} sztuk {laptop.Name}.");

            
            Customer customer = new Customer(
                id: 501,
                firstName: "Piotr",
                lastName: "Klientowski",
                email: "piotr@gmail.com",
                phone: "600-900-900",
                city: "Kraków",
                region: "Małopolskie",
                postalCode: "30-001"
            );
            Console.WriteLine($"4. Klient wchodzi do sklepu: {customer.GetInfo()}");

            
            Order newOrder = new Order
            {
                Id = 1001,
                DatePlaced = DateTime.Now,
                Status = OrderStatus.New,
                CustomerId = customer.CustomerId,
                Customer = customer,
                StoreId = myStore.Id,
                FulfillingStore = myStore
            };

            
            newOrder.AddProduct(laptop, 1);
            newOrder.AddProduct(phone, 2);

            Console.WriteLine("\n=== PODSUMOWANIE ZAMÓWIENIA ===");
            Console.WriteLine(newOrder.ToString());
            Console.WriteLine("Pozycje na paragonie:");
            foreach (var item in newOrder.OrderItems)
            {
                Console.WriteLine($" - {item.Product.Name} (x{item.Quantity}) - Cena jedn.: {item.UnitPrice:C} | Suma: {item.CalculateLineTotal():C}");
            }

            Console.WriteLine($"\nŁĄCZNA WARTOŚĆ: {newOrder.TotalValue:C}");

            
            Console.ReadKey();
        }
    }
}