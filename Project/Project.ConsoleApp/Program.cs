using System;
using Project.Model;
using Project.Model.Orders;
using Project.Model.People;
using Project.Model.Stores;

namespace Project.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var adresSklepu = new Address("Warszawa", "Złota 44", "00-120", "Polska");
                Store sklep = new Store("Szybki Sklep", adresSklepu, "+48111222333");

                Employee pracownik = new Employee("Jan", "Kowalski", "500-123-456", "jan@sklep.pl", sklep, EmployeePosition.Manager, 4000m, new DateTime(2023, 1, 1));
                sklep.Staff.Add(pracownik);

                Console.WriteLine($"Utworzono: {sklep}");
                Console.WriteLine($"Pracownik: {pracownik.GetInfo()}\n");

                Product mleko = new Product("Mleko", 5.00m, "Nabiał");
                Product chleb = new Product("Chleb", 4.00m, "Pieczywo");

                Customer klient = new Customer("Anna", "Testowa", "999-888-777", "anna@test.pl");

                Order zamowienie = new Order(klient, new Address("Gdańsk", "Długa", "80-001", "Polska"));
                zamowienie.AddProduct(mleko, 2);
                zamowienie.AddProduct(chleb, 1);

                zamowienie.ShipOrder();
                klient.Orders.Add(zamowienie);

                Console.WriteLine("--- INFO O KLIENCIE ---");
                Console.WriteLine(klient.GetInfo());

                Console.WriteLine($"\nSprawdzenie metody TotalAmount(): {zamowienie.GetTotalAmount()} PLN");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[BŁĄD]: {ex.Message}");
            }

            Console.ReadKey();
        }
    }
}