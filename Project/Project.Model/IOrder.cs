using System;

namespace Project.Model
{
    public interface IOrder
    {
        decimal CalculateTotalValue();
        void AddProduct(Product product, int quantity);
        void UpdateStatus(OrderStatus newStatus);
    }
}