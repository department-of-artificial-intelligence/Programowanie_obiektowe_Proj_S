using Project.Model.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.Interfaces
{
    public interface IProductManager
    {
        List<Store> AllStores();
        bool AddProduct(Store store, Product product, int quantity);
        bool RemoveProduct(Store store, Product product, int quantity);
        bool IsProductAvailable(Store store, string productName, int quantity);
        List<Product> GetLowStockProducts(Store store, int threshold);
        void SortProductsByFirstLetter(Store store);
        void SortProductsByCategory(Store store);
    }
}
