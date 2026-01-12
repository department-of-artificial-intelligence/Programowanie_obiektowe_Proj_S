using Project.Model.People;
using Project.Model.Stores;
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


        public int StoreId { get; set; } 
        public virtual Store Store { get; set; } 


        public List<OrderItem> Items { get; set; } = new List<OrderItem>();



        private Order() { }

        [SetsRequiredMembers]
        public Order(Customer purchaser, Address deliveryAddress, Store store)
        {
            OrderDate = DateTime.Now;
            Status = OrderStatus.New;
            Purchaser = purchaser;
            DeliveryAddress = deliveryAddress;

           
            Store = store ?? throw new ArgumentNullException(nameof(store));
        }


        public void AddProduct(Product product, int quantity)
        {
            if (Status != OrderStatus.New)
            {
                throw new InvalidOperationException("Nie można dodawać produktów do przetworzonego zamówienia.");
            }

            var existingItem = Items.FirstOrDefault(item =>
                (item.Product.ProductId != 0 && item.Product.ProductId == product.ProductId) ||
                (item.Product.Name == product.Name));

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


        public void MarkAsPaid()
        {
            if (Status != OrderStatus.New)
                throw new InvalidOperationException("Tylko nowe zamówienie można opłacić.");

            Status = OrderStatus.Paid;
        }


        public void ShipOrder()
        {
            if (Status == OrderStatus.Cancelled)
                throw new InvalidOperationException("Nie można wysłać anulowanego zamówienia.");
            Status = OrderStatus.Shipped;
        }


        public void CancelOrder()
        {
            if (Status == OrderStatus.Completed)
                throw new InvalidOperationException("Nie można anulować zakończonego zamówienia.");
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