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

        public IPayment? PaymentMethod { get; set; }

        public IDeliveryMethod? Delivery { get; set; }

        public Order() { }

        public decimal CalculateTotalValue()
        {
            decimal productTotal = OrderItems.Sum(item => item.CalculateLineTotal());
            decimal deliveryCost = Delivery?.Cost ?? 0;

            TotalValue = productTotal + deliveryCost;
            return TotalValue;
        }

        public void SetPaymentMethod(IPayment method)
        {
            PaymentMethod = method;
        }

        public void FinalizeOrder()
        {
            CalculateTotalValue();

            if (TotalValue <= 0)
            {
                Console.WriteLine("Zamówienie jest puste lub darmowe.");
                Status = OrderStatus.Completed;
                return;
            }

            if (PaymentMethod == null)
            {
                Console.WriteLine("Błąd: Nie wybrano metody płatności!");
                return;
            }

            bool success = PaymentMethod.Pay(TotalValue, Customer);

            if (success)
            {
                Status = OrderStatus.Paid;
                Console.WriteLine($"Zamówienie #{Id} zostało zatwierdzone.");
            }
            else
            {
                Console.WriteLine("Płatność nie powiodła się. Zamówienie wstrzymane.");
            }
        }

        public void AddProduct(Product product, int quantity)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (quantity <= 0) throw new ArgumentException("Ilość musi być dodatnia.");

            var existingItem = OrderItems.FirstOrDefault(item => item.ProductId == product.Id);

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
            string paymentName = PaymentMethod != null ? PaymentMethod.GetType().Name : "Brak";
            string deliveryName = Delivery != null ? Delivery.Name : "Brak";

            return $"Zamówienie #{Id} [{Status}] | Data: {DatePlaced:yyyy-MM-dd} | Suma: {TotalValue:C} | Płatność: {paymentName} | Dostawa: {deliveryName}";
        }
    }
}