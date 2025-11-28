using Pharmacy.Interfaces;
using Pharmacy.Models;
using Pharmacy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Services
{
    public class PharmacyQueries
    {
        private List<Product> products;
        private List<Transaction> transactions;

        public PharmacyQueries(List<Product> products, List<Transaction> transactions)
        {
            this.products = products;
            this.transactions = transactions;
        }

        public void ShowAllProducts()
        {
            Console.WriteLine("\n=== Wszystkie produkty ===");
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Id}. {product.Name} - {product.Price} zl., {product.Quantity} l.");
            }
        }

        public void ShowLowStockProducts()
        {
            Console.WriteLine("\n=== Produkty ktorych malo w magazynie ===");
            var lowStock = products.Where(p => p.IsLowStock);
            foreach (var product in lowStock)
            {
                Console.WriteLine($"{product.Name}:  {product.Quantity} l.");
            }
        }

        public void ShowTopSellingProducts()
        {
            Console.WriteLine("\n=== Najczeszczej sprzedane produktyЫ ===");
            var topProducts = transactions
                .GroupBy(t => t.ProductId)
                .Select(g => new
                {
                    Product = products.First(p => p.Id == g.Key),
                    TotalSold = g.Sum(t => t.Quantity)
                })
                .OrderByDescending(x => x.TotalSold)
                .Take(3);

            foreach (var item in topProducts)
            {
                Console.WriteLine($"{item.Product.Name}: {item.TotalSold} l.");
            }
        }
    }

    
}
