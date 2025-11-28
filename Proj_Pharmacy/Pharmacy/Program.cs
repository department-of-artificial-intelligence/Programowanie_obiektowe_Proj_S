using System;
using Pharmacy.Interfaces;
using Pharmacy.Models;
using Pharmacy.Services;

namespace Pharmacy
{
    class Program
    {
        static void Main()
        {

            var pharmacy = new PharmacyManager();
            var products = pharmacy.GetProducts();
            var transactions = pharmacy.GetTransactions();
            var queries = new PharmacyQueries(products, transactions);
            pharmacy.AddWorker(1, "Taras", "Tananan", "Kasjer");

            Console.WriteLine("=== Sysytem zarzadzania aptekaЙ ===\n");

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1 - Pokaz wszystkie produkty");
                Console.WriteLine("2 - Sprzedaj produkt");
                Console.WriteLine("3 - Dodaj produkt");
                Console.WriteLine("4 - Pokaz raporty");
                Console.WriteLine("5 - Analityka");
                Console.WriteLine("0 - Wyjscie");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        queries.ShowAllProducts();
                        break;

                    case "2":
                        SellProductMenu(pharmacy);
                        break;

                    case "3":
                        AddProductMenu(pharmacy);
                        break;

                    case "4":
                        ShowReportsMenu(pharmacy);
                        break;

                    case "5":
                        ShowAnalyticsMenu(queries);
                        break;

                    case "0":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Zlu wybor");
                        break;
                }
            }

            Console.WriteLine("Program wylaczony.");
        }

        static void SellProductMenu(PharmacyManager pharmacy)
        {
            Console.WriteLine("\n=== Sprzedaz produktu ===");
            Console.Write("Wprowadz id produktu: ");
            int productId = int.Parse(Console.ReadLine());

            Console.Write("Ilosc produktu: ");
            int quantity = int.Parse(Console.ReadLine());

            pharmacy.SellProduct(productId, quantity,  1);
        }

        static void AddProductMenu(PharmacyManager pharmacy)
        {
            Console.WriteLine("\n=== Dodanie produktu ===");

            var product = new Product();

            Console.Write("Nazwa: ");
            product.Name = Console.ReadLine();

            Console.Write("Cena: ");
            product.Price = decimal.Parse(Console.ReadLine());

            Console.Write("Ilosc: ");
            product.Quantity = int.Parse(Console.ReadLine());

            Console.Write("Producent: ");
            product.Manufacturer = Console.ReadLine();

            pharmacy.AddProduct(product);
        }

        static void ShowReportsMenu(PharmacyManager pharmacy)
        {
            Console.WriteLine("\n=== Raporty ===");
            Console.WriteLine("1 - Raport sprzedazy");
            Console.WriteLine("2 - Raport o magazynie");
            Console.WriteLine("3 - Raport o datach waznosci");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    pharmacy.GenerateSalesReport();
                    break;
                case "2":
                    pharmacy.GenerateInventoryReport();
                    break;
                case "3":
                    pharmacy.GenerateExpiryReport();
                    break;
                default:
                    Console.WriteLine("Zly wybor!");
                    break;
            }
        }

        static void ShowAnalyticsMenu(PharmacyQueries queries)
        {
            Console.WriteLine("\n=== Analityka ===");
            Console.WriteLine("1 - Produkty ktorych malo w magazynie");
            Console.WriteLine("2 - Najczeszczej sprzedane produkty");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    queries.ShowLowStockProducts();
                    break;
                case "2":
                    queries.ShowTopSellingProducts();
                    break;
                default:
                    Console.WriteLine("Zly wybor!");
                    break;
            }
        }
    }
}