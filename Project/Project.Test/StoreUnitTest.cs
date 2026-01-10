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
        public void StoreTest() 
        {
            
            var addressWarsaw = new Address("Warszawa", "Marszałkowska", "00-001", "Polska");
            
            var store1 = new Store("SuperMarkecik", addressWarsaw, "+48732304650");

            var addressCracow = new Address("Kraków", "Floriańska", "31-019", "Polska");
            var store2 = new Store("Delikatesy Centrum", addressCracow, "+48123456789");

            
            var manager = new Employee(
                "Adam", "Nowak",
                "+48123456789", 
                "a@a.pl",
                store1, EmployeePosition.Manager, 6000m, new DateTime(2022, 1, 10));
            store1.Staff.Add(manager);

            
            var seniorSeller = new Employee(
                "Piotr", "Zielinski",
                "+48666777888",
                "p@p.pl",
                store2, EmployeePosition.Manager, 4800m, new DateTime(2020, 5, 15));
            store2.Staff.Add(seniorSeller);

            var p1 = new Product("Mleko 3.2%", 3.50m, "Nabiał");
            var p2 = new Product("Chleb Razowy", 4.20m, "Pieczywo");
            var p3 = new Product("Szynka Parmeńska", 89.90m, "Wędliny Premium");
            var p4 = new Product("Oliwa z Oliwek", 45.00m, "Import");

            store1.Inventory.Add(p1);
            store2.Inventory.AddRange(new[] { p3, p4, p2 });

            
            var customer = new Customer(
                "Ewa", "Kowalska",
                "+48987654321",
                "ewa@klient.pl");

            var deliveryAddress = new Address("Warszawa", "ul. Polna 5", "00-123", "Polska");

            
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
                    
                    totalSpent += o.GetTotalAmount();
                }
            }

            


        }
    }
}