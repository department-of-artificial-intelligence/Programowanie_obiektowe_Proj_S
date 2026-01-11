using System;
using System.Collections.Generic;
using System.Linq;
using Project.Model;
using Project.Model.Interfaces;
using Project.Model.Orders;
using Project.Model.People;
using Project.Model.Stores;

namespace Project.Logic.Services
{
    public class OrderService : IOrderService
    {
        
        private readonly List<Order> _ordersRepository;

        public OrderService()
        {
            _ordersRepository = new List<Order>();
        }

        

        public List<Order> AllOrders()
        {
            return _ordersRepository;
        }

        public List<Order> GetCustomerHistory(Customer customer)
        {
            
            return _ordersRepository
                .Where(o => o.Purchaser == customer || o.Purchaser.Email == customer.Email)
                .ToList();
        }

        public Order CreateOrder(Customer customer, Address deliveryAddress)
        {
            if (customer == null || deliveryAddress == null)
                throw new ArgumentNullException("Klient i adres są wymagane.");

           
            var newOrder = new Order(customer, deliveryAddress);

            
            _ordersRepository.Add(newOrder);

            
            customer.Orders.Add(newOrder);

            Console.WriteLine($"[SERWIS] Utworzono nowe zamówienie #{newOrder.OrderId} dla {customer.FirstName}.");
            return newOrder;
        }

        public void AddItemToOrder(Order order, Product product, int quantity)
        {
            if (order == null || product == null) return;

            
            try
            {
                order.AddProduct(product, quantity);
                Console.WriteLine($"[SERWIS] Dodano {product.Name} (x{quantity}) do zamówienia.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[BŁĄD] Nie udało się dodać produktu: {ex.Message}");
            }
        }

        public bool ProcessOrderPayment(Order order, IPayment paymentMethod)
        {
            if (order == null || paymentMethod == null) return false;

            decimal amountToPay = order.GetTotalAmount();

            if (amountToPay <= 0)
            {
                Console.WriteLine("[SERWIS] Zamówienie jest puste lub darmowe. Płatność niepotrzebna.");
                return false;
            }

            Console.WriteLine($"[SERWIS] Rozpoczynam płatność za zamówienie #{order.OrderId} na kwotę {amountToPay:C}...");

            
            bool paymentSuccess = paymentMethod.Pay(amountToPay, order.Purchaser);

            if (paymentSuccess)
            {
                
                try
                {
                    order.MarkAsPaid();
                    Console.WriteLine($"[SERWIS] Zamówienie #{order.OrderId} zostało opłacone.");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[BŁĄD] Płatność przeszła, ale nie udało się zmienić statusu: {ex.Message}");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("[SERWIS] Płatność odrzucona.");
                return false;
            }
        }

        public void ShipOrder(Order order)
        {
            if (order == null) return;

            
            if (order.Status == OrderStatus.New)
            {
                Console.WriteLine("[SERWIS BŁĄD] Nie można wysłać nieopłaconego zamówienia!");
                return;
            }

            try
            {
                order.ShipOrder();
                Console.WriteLine($"[SERWIS] Zamówienie #{order.OrderId} zostało wysłane do klienta.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[BŁĄD] {ex.Message}");
            }
        }
    }
}