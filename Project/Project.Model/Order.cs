using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Project.Model
{


    public interface IOrders
    {
        decimal CalculateTotalValue();

        void AddItem(Item item, int quantity);

        void UpdateStatus(OrderStatus newStatus);


    }


    public class Order: IOrders
    {
        public required int Id { get; set; }
        public required DateTime DatePlaced { get; set; }
        public required OrderStatus Status { get; set; }
        public required decimal TotalValue { get; set; }

        public required int CustomerId { get; set; }
        public required Customer Customer { get; set; }

        public required int StoreId { get; set; }
        public required Store FulfillingStore { get; set; }

        public required List<Item> OrderItems { get; set; } = new List<Item>();

       

        public decimal CalculateTotalValue()
        {
            
            decimal total = this.OrderItems.Sum(item => item.CalculateItemTotal());

            this.TotalValue = total;
            return total;
        }

        public void AddItem(Item item, int quantity)
        {
            if(item) == null)
            {
                throw new ArgumentNullException(nameof(item), "Produkt nie może być pusty.");
            }
            if (quantity <= 0)
            {
                throw new ArgumentException("Ilość musi być dodatnia.", nameof(quantity));
            }

            
            var existingItem = this.OrderItems.FirstOrDefault(item => item.Id == item.Id);

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
