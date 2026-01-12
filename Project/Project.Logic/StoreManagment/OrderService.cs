using System;
using System.Collections.Generic;
using System.Linq;
using Project.Model;
using Project.Model.Interfaces;
using Project.Model.Orders;
using Project.Model.People;
using Project.Model.Stores;

namespace Project.Logic.StoreManagment
{
   
    public class OrderService : IOrderService
    {
        
        private readonly IOrderRepository _repository;

      
        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public List<Order> AllOrders()
        {
           
            return _repository.GetAll();
        }


        public List<Order> GetCustomerHistory(Customer customer)
        {
            if (customer == null) return new List<Order>();

            
            return _repository.GetAll()
                .Where(o => o.Purchaser != null && o.Purchaser.Id == customer.Id)
                .ToList();
        }


        public Order CreateOrder(Customer customer, Address deliveryAddress, Store store)
        {
            if (customer == null || deliveryAddress == null)
                throw new ArgumentNullException("Dane są wymagane.");

            var newOrder = new Order(customer, deliveryAddress, store);

          
            _repository.Add(newOrder);

            
            customer.Orders.Add(newOrder);

            Console.WriteLine($"[SERWIS] Utworzono zamówienie #{newOrder.OrderId} w bazie.");
            return newOrder;
        }


        public void AddItemToOrder(Order order, Product product, int quantity)
        {
            if (order == null || product == null) return;

            try
            {
                order.AddProduct(product, quantity);

                
                _repository.Update(order);

                Console.WriteLine($"[SERWIS] Zapisano w bazie: {product.Name} (x{quantity}).");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[BŁĄD] {ex.Message}");
            }
        }


        public bool ProcessOrderPayment(Order order, IPayment paymentMethod)
        {
            if (order == null || paymentMethod == null) return false;

            decimal amountToPay = order.GetTotalAmount();

            if (amountToPay <= 0)
            {
                Console.WriteLine("[SERWIS] Kwota 0. Płatność zbędna.");
                return false;
            }

            bool paymentSuccess = paymentMethod.Pay(amountToPay, order.Purchaser);

            if (paymentSuccess)
            {
                try
                {
                    order.MarkAsPaid();

                   
                    _repository.Update(order);

                    Console.WriteLine($"[SERWIS] Status 'Opłacone' zapisano w bazie.");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[BŁĄD ZAPISU] {ex.Message}");
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

                
                _repository.Update(order);

                Console.WriteLine($"[SERWIS] Status 'Wysłane' zapisano w bazie.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[BŁĄD] {ex.Message}");
            }
        }
    }
}