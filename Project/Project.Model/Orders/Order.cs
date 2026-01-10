using Project.Model.People;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Project.Model.Orders
{
    public class Order
    {
        public int OrderId { get; private set; }
        public DateTime OrderDate { get; private set; } = DateTime.Now;
        public OrderStatus Status { get; private set; } = OrderStatus.New;

        public required Customer Purchaser { get; set; }
        public required Address DeliveryAddress { get; set; }

        public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        private Order() { }

        [SetsRequiredMembers]
        public Order(Customer purchaser, Address deliveryAddress)
        {
            Purchaser = purchaser;
            DeliveryAddress = deliveryAddress;
        }

        

        public void AddProduct(Product product, int quantity)
        {
            if (Status != OrderStatus.New)
            {
                throw new InvalidOperationException("Nie można dodawać produktów do przetworzonego zamówienia.");
            }

            
            var existingItem = Items.FirstOrDefault(item => item.Product.ProductId == product.ProductId);

            if (existingItem != null)
            {
                
                existingItem.Quantity += quantity;
            }
            else
            {
                
                var newItem = new OrderItem(product, quantity);
                Items.Add(newItem);
            }
        }

        
        public void ShipOrder()
        {
            if (Status == OrderStatus.Cancelled)
                throw new InvalidOperationException("Nie można wysłać anulowanego zamówienia.");
            Status = OrderStatus.Shipped;
        }

        public void CancelOrder()
        {
            if (Status == OrderStatus.Shipped || Status == OrderStatus.Completed)
                throw new InvalidOperationException("Nie można anulować wysłanego zamówienia.");
            Status = OrderStatus.Cancelled;
        }

        

        public decimal GetTotalAmount()
        {
            if (Items == null || Items.Count == 0) return 0;

            
            return Items.Sum(item => item.GetLineTotal());
        }

        public override string ToString()
        {
            string idInfo = OrderId == 0 ? "NOWE" : OrderId.ToString();

            
            string itemsDescription = string.Join("\n   - ", Items);

            return $"[ZAMÓWIENIE #{idInfo}]\n" +
                   $"Status: {Status}\n" +
                   $"Klient: {Purchaser.GetInfo()}\n" +
                   $"Adres:  {DeliveryAddress}\n" +
                   $"Pozycje:\n   - {itemsDescription}\n" +
                   $"RAZEM DO ZAPŁATY: {GetTotalAmount():C}";
        }
    }
}