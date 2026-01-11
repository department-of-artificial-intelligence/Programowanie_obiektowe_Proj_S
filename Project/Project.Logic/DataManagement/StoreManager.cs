using System;
using System.Collections.Generic;
using System.Linq;
using Project.Model;
using Project.Model.Interfaces; 
using Project.Model.Stores;

namespace Project.Logic.Managers
{
    public class StoreManager : IStoreManager
    {
        
        private readonly List<Store> _fakeStoreDatabase;

        public StoreManager()
        {
            _fakeStoreDatabase = new List<Store>();
        }

       
        public List<Store> AllStores()
        {
            return _fakeStoreDatabase;
        }

       
        public void RegisterNewStore(Store store)
        {
            if (!_fakeStoreDatabase.Contains(store))
            {
                _fakeStoreDatabase.Add(store);
            }
        }

        public bool AddProduct(Store store, Product product, int quantity)
        {
            if (store == null || product == null || quantity <= 0) return false;

           

            for (int i = 0; i < quantity; i++)
            {
                store.Inventory.Add(product);
            }

            Console.WriteLine($"[LOGIKA] Dodano {quantity} szt. '{product.Name}' do sklepu '{store.Name}'.");
            return true;
        }

        public bool RemoveProduct(Store store, Product product, int quantity)
        {
            if (store == null || product == null || quantity <= 0) return false;

          
            int currentCount = store.Inventory.Count(p => p.Name == product.Name);
            if (currentCount < quantity)
            {
                Console.WriteLine($"[BŁĄD] Nie można usunąć {quantity} szt. '{product.Name}'. W magazynie jest tylko {currentCount}.");
                return false;
            }

          
            for (int i = 0; i < quantity; i++)
            {
               
                var itemToRemove = store.Inventory.FirstOrDefault(p => p.Name == product.Name);
                if (itemToRemove != null)
                {
                    store.Inventory.Remove(itemToRemove);
                }
            }
            return true;
        }

        public bool IsProductAvailable(Store store, string productName, int quantity)
        {
            if (store == null) return false;

            
            int availableCount = store.Inventory.Count(p => p.Name == productName);

            return availableCount >= quantity;
        }

        public List<Product> GetLowStockProducts(Store store, int threshold)
        {
            if (store == null) return new List<Product>();

            var lowStock = store.Inventory
                .GroupBy(p => p.Name)
                .Where(grupa => grupa.Count() < threshold)
                .Select(grupa => grupa.First()) 
                .ToList();

            return lowStock;
        }
    }
}