using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Interfaces
{
    public interface IAssortmentOperations
    {
        
        void AddProduct(object product) { }
        void RemoveProduct(int productId) { }
        void UpdateProductQuantity(int productId, int newQuantity) { }
        void SellProduct(int productId, int quantity,  int workerId) {  }
    }
}
