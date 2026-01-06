using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.Model
{
    public class Order : IOrder
    {
        public required int Id { get; set; }
        public required DateTime DatePlaced { get; set; }
        public required OrderStatus Status { get; set; }

        public decimal TotalValue { get; private set; }

        public required int CustomerId { get; set; }
        public required Customer Customer { get; set; }

        public required int StoreId { get; set; }
        public required Store FulfillingStore { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public Order() { }

        public decimal CalculateTotalValue()
        {
            decimal total = OrderItems.Sum(item => item.CalculateLineTotal());
            TotalValue = total;
            return total;
        }

        public void AddProduct(Product product, int quantity)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Produkt nie może być pusty.");
            }
            if (quantity <= 0)
            {
                throw new ArgumentException("Ilość musi być dodatnia.", nameof(quantity));
            }

            var existingItem = OrderItems.FirstOrDefault(item => item.ProductId == product.Id);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                // TUTAJ BYŁ BŁĄD - Poprawiona inicjalizacja
                var newItem = new OrderItem
                {
                    Order = this,              // <--- TA LINIA JEST KLUCZOWA (CS9035)
                    OrderId = this.Id,
                    Product = product,
                    ProductId = product.Id,
                    Quantity = quantity,
                    UnitPrice = product.Price
                };

                OrderItems.Add(newItem);
            }

            CalculateTotalValue();
        }

        public void UpdateStatus(OrderStatus newStatus)
        {
            Status = newStatus;
        }

        public override string ToString()
        {
            return $"Zamówienie #{Id} [Status: {Status}] - {DatePlaced:yyyy-MM-dd} | Total: {TotalValue:C}";
        }
    }
}