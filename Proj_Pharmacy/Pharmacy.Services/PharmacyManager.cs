using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Pharmacy.Interfaces;
using Pharmacy.Models;


namespace Pharmacy.Services
{
    public class PharmacyManager : IAssortmentOperations, IReportGenerator
    {
        private List<Product> products = new List<Product>();
        private List<Worker> workers = new List<Worker>();
        private List<Pharmacy.Models.Transaction> transactions = new List<Pharmacy.Models.Transaction>();

        public PharmacyManager()
        {
            
        }

        public void AddWorker(int id, string firstName, string lastName, string position) {
            Worker w = new Worker( id,  firstName,  lastName,  position);
            workers.Add(w);
        }

        public void AddProduct(object  productObj)
        {

            if (productObj is Product product)
            {
                product.Id = products.Count + 1;
                products.Add(product);
                Console.WriteLine($"Dodany produkt: {product.Name}");
            }
            else
            {
                Console.WriteLine("Ошибка: передан объект неправильного типа");
            }
        }
        public void AddProduct(string name, decimal price, int quantity, string manufacturer, DateTime bestBeforeDate, bool needPrescription)
        {
            Product p = new Product( name,  price,  quantity,  manufacturer,  bestBeforeDate,  needPrescription);
            p.Id = products.Count + 1;
            products.Add(p);
            Console.WriteLine($"Dodany produkt: {p.Name}");
        }

        public void RemoveProduct(int productId)
        {
            var product = products.FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                products.Remove(product);
                Console.WriteLine($"Usuniety produkt: {product.Name}");
            }
        }

        public void UpdateProductQuantity(int productId, int newQuantity)
        {
            var product = products.FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                product.Quantity = newQuantity;
                Console.WriteLine($"Обновлено количество {product.Name}: {newQuantity} шт.");
            }
        }

        public void SellProduct(int productId, int quantity, int workerId)
        {
            var product = products.FirstOrDefault(p => p.Id == productId);
            var worker = workers.FirstOrDefault(w => w.Id == workerId);

            if (product == null ||  worker == null)
            {
                Console.WriteLine("Blad: produkt lub pracownik nie znajdziony");
                return;
            }

            if (product.Quantity < quantity)
            {
                Console.WriteLine($"Nie wystarcza produktu {product.Name}. In stock: {product.Quantity}");
                return;
            }

            var transaction = new Pharmacy.Models.Transaction(transactions.Count + 1, workerId, productId, DateTime.Now, quantity, product.Price * quantity);
            transactions.Add(transaction);
            product.Quantity -= quantity;

            Console.WriteLine($"Sprzedany: {product.Name}  X{quantity}. Suma: {transaction.TotalPrice} zl.");
        }

        public void GenerateSalesReport()
        {
            Console.WriteLine("\n=== Raport o sprzedazy ===");
            var salesByProduct = transactions
                .GroupBy(t => t.ProductId)
                .Select(g => new
                {
                    Product = products.First(p => p.Id == g.Key),
                    TotalSold = g.Sum(t => t.Quantity),
                    TotalRevenue = g.Sum(t => t.TotalPrice)
                });

            foreach (var item in salesByProduct)
            {
                Console.WriteLine($"{item.Product.Name}: {item.TotalSold} шт., Выручка: {item.TotalRevenue} руб.");
            }
        }

        public void GenerateInventoryReport()
        {
            Console.WriteLine("\n=== Raport o magazynie ===");
            foreach (var product in products)
            {
                string status = product.Quantity <10 ? "Malo" : "Normalnie";
                string expired = product.IsExpired ? "\r\nprodukt przeterminowany" : "zdatny do spożycia";
                Console.WriteLine($"{product.Name}: {product.Quantity} l. ({status}), {expired}");
            }
        }

        public void GenerateExpiryReport()
        {
            Console.WriteLine("\n=== Raport o datach waznosci ===");
            var expiringSoon = products
                .Where(p => p.BestBeforeDate <= DateTime.Now.AddMonths(3))
                .OrderBy(p => p.BestBeforeDate);

            foreach (var product in expiringSoon)
            {
                int daysLeft = (product.BestBeforeDate - DateTime.Now).Days;
                Console.WriteLine($"{product.Name}: {daysLeft} dni do przeterminowania, {product.Quantity} l.");
            }
        }

        public List<Product> GetProducts() => products;
        public List<Pharmacy.Models.Transaction> GetTransactions() => transactions;

        private void AddWorker(Worker worker)
        {
            worker.Id = workers.Count + 1;
            workers.Add(worker);
        }

  

        
    }
    }
