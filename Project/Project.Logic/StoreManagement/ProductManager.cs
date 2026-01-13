using Project.Model.Interfaces;
using Project.Model.Stores;
using System.Collections.Generic;
using System.Linq;
using System; 

namespace Project.Logic.StoreManagement
{
    public class ProductManager : IProductManager
    {
        private readonly IStoreRepository _storeRepository;

        public ProductManager(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public List<Store> AllStores()
        {
            return _storeRepository.GetAll();
        }

        public bool AddProduct(Store store, Product product, int quantity)
        {
            if (store == null || product == null || quantity <= 0)
            {
                return false;
            }

           
            var existingProduct = store.Inventory.FirstOrDefault(p => p.Name == product.Name);

            if (existingProduct != null)
            {
                
                existingProduct.Stock += quantity;
            }
            else
            {
               
                product.Stock = quantity;
                product.Store = store; 
                store.Inventory.Add(product);
            }

           
            _storeRepository.Update(store);
            return true;
        }

      
        public bool RemoveProduct(Store store, Product product, int quantity)
        {
            if (store == null || product == null || quantity <= 0)
                return false;

            var existingProduct = store.Inventory.FirstOrDefault(p => p.Name == product.Name);

            if (existingProduct == null || existingProduct.Stock < quantity)
                return false;

            existingProduct.Stock -= quantity;

            if (existingProduct.Stock == 0)
            {
                store.Inventory.Remove(existingProduct);
            }

            _storeRepository.Update(store);
            return true;
        }

        public bool IsProductAvailable(Store store, string productName, int quantity)
        {
            if (store == null || string.IsNullOrEmpty(productName)) return false;

            var product = store.Inventory.FirstOrDefault(p => p.Name == productName);

            if (product == null) return false;

            return product.Stock >= quantity;
        }

        public List<Product> GetLowStockProducts(Store store, int threshold)
        {
            if (store == null) return new List<Product>();

            return store.Inventory.Where(p => p.Stock < threshold).ToList();
        }

        public void SortProductsByFirstLetter(Store store)
        {
            if (store == null || store.Inventory == null) return;
            store.Inventory = store.Inventory.OrderBy(p => p.Name).ToList();
        }

        public void SortProductsByCategory(Store store)
        {
            if (store == null || store.Inventory == null) return;
            store.Inventory = store.Inventory.OrderBy(p => p.Category).ToList();
        }
    }
}