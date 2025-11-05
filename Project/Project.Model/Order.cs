using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Project.Model
{

    public class Order
    {
        public int Id { get; set; }
        public DateTime DatePlaced { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalValue { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int StoreId { get; set; }
        public Store FulfillingStore { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

       

        public decimal CalculateTotalValue()
        {
            
            decimal total = this.OrderItems.Sum(item => item.CalculateItemTotal());

            this.TotalValue = total;
            return total;
        }

        public void AddItem(Product product, int quantity)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Produkt nie może być pusty.");
            }
            if (quantity <= 0)
            {
                throw new ArgumentException("Ilość musi być dodatnia.", nameof(quantity));
            }

            
            var existingItem = this.OrderItems.FirstOrDefault(item => item.ProductId == product.Id);

            if (existingItem != null)
            {
                
                existingItem.Quantity += quantity;
            }
            else
            {
                
                var newItem = new OrderItem
                {
                    Order = this,
                    OrderId = this.Id,
                    Product = product,
                    ProductId = product.Id,
                    Quantity = quantity,
                    PriceAtTimeOfPurchase = product.Price 
                };
                this.OrderItems.Add(newItem);
            }

            
            this.CalculateTotalValue();
        }

        public void UpdateStatus(OrderStatus newStatus)
        {
            this.Status = newStatus;
        }

        

        public override string ToString()
        {
            return $"Zamówienie #{Id} [Status: {Status}] - {DatePlaced:yyyy-MM-dd}";
        }
    }





}
