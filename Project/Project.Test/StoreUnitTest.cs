using Xunit;
using System;
using System.Collections.Generic;
using Project.Model;
using Project.Model.Orders;
using Project.Model.People;
using Project.Model.Stores;

namespace Project.Test
{
    public class StoreTestUnit
    {
        [Fact]
        public void StoreTest() // Nazwa metody z Twojego logu błędów
        {
            // --- ARRANGE ---
            var addressWarsaw = new Address("Warszawa", "Marszałkowska", "00-001", "Polska");
            // Numer sklepu jest OK (+48...)
            var store1 = new Store("SuperMarkecik", addressWarsaw, "+48732304650");

            var addressCracow = new Address("Kraków", "Floriańska", "31-019", "Polska");
            var store2 = new Store("Delikatesy Centrum", addressCracow, "+48123456789");

            // POPRAWKA 1: Prawidłowy numer telefonu dla Managera
            var manager = new Employee(
                "Adam", "Nowak",
                "+48123456789", // <-- Było "123", musi być pełny format
                "a@a.pl",
                store1, EmployeePosition.Manager, 6000m, new DateTime(2022, 1, 10));
            store1.Staff.Add(manager);

            // POPRAWKA 2: Prawidłowy numer telefonu dla Senior Sprzedawcy
            var seniorSeller = new Employee(
                "Piotr", "Zielinski",
                "+48666777888", // <-- Było "555", musi być pełny format
                "p@p.pl",
                store2, EmployeePosition.Manager, 4800m, new DateTime(2020, 5, 15));
            store2.Staff.Add(seniorSeller);

            var p1 = new Product("Mleko 3.2%", 3.50m, "Nabiał");
            var p2 = new Product("Chleb Razowy", 4.20m, "Pieczywo");
            var p3 = new Product("Szynka Parmeńska", 89.90m, "Wędliny Premium");
            var p4 = new Product("Oliwa z Oliwek", 45.00m, "Import");

            store1.Inventory.AddRange(new[] { p1, p2 });
            store2.Inventory.AddRange(new[] { p3, p4, p2 });

            // POPRAWKA 3: Prawidłowy numer telefonu dla Klienta
            var customer = new Customer(
                "Ewa", "Kowalska",
                "+48987654321", // <-- Było z myślnikami, a Twój regex może ich nie lubić
                "ewa@klient.pl");

            var deliveryAddress = new Address("Warszawa", "ul. Polna 5", "00-123", "Polska");

            // --- ACT ---
            var order1 = new Order(customer, deliveryAddress);
            order1.AddProduct(p1, 10);
            order1.AddProduct(p2, 2);
            customer.Orders.Add(order1);

            var order2 = new Order(customer, deliveryAddress);
            order2.AddProduct(p3, 1);
            order2.AddProduct(p4, 2);
            customer.Orders.Add(order2);

            order1.ShipOrder();
            order2.ShipOrder();

            decimal totalSpent = 0;
            foreach (var o in customer.Orders)
            {
                if (o.Status == OrderStatus.Shipped)
                {
                    // Używamy metody z nawiasami ()
                    totalSpent += o.GetTotalAmount();
                }
            }

            
        }
    }
}