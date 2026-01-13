using Project.Model.Interfaces;
using Project.Model.Orders;
using Project.Model.People;
using Project.Model.Stores;
using Project.Logic.PaymentService; 
using System;
using System.Linq;

namespace Project.Logic.StoreManagement
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;

        
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        
        public Order CreateOrder(Customer customer, Address address, Store store)
        {
            if (store == null)
            {
                throw new ArgumentNullException(nameof(store), "Zamówienie musi być przypisane do sklepu!");
            }

            var order = new Order(customer, address, store);

           
            _orderRepository.Add(order);

            return order;
        }

       
        public void AddItemToOrder(Order order, Product product, int quantity)
        {
            if (order == null || product == null || quantity <= 0) return;

            try
            {
                

                Product realProduct = product;

                if (order.Store != null)
                {
                   
                    var storeProduct = order.Store.Inventory.FirstOrDefault(p => p.Name == product.Name);

                    if (storeProduct != null)
                    {
                       
                        realProduct = storeProduct;
                    }
                    else
                    {
                        Console.WriteLine($"[BŁĄD] Produkt '{product.Name}' nie jest dostępny w ofercie sklepu '{order.Store.Name}'!");
                        return;
                    }
                }

                
                if (realProduct.Stock < quantity)
                {
                    Console.WriteLine($"[BŁĄD] Za mało towaru '{realProduct.Name}'! Dostępne: {realProduct.Stock}, Chcesz: {quantity}");
                    return;
                }

               
                realProduct.Stock -= quantity;

               
                order.AddProduct(realProduct, quantity);

                
                _orderRepository.Update(order);

                Console.WriteLine($"[SERWIS] Dodano: {realProduct.Name} (x{quantity}). Pozostało w magazynie: {realProduct.Stock}");
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


            if (paymentMethod.Pay(amountToPay, order.Purchaser))
            {
                
                order.Status = OrderStatus.Paid;

                
                _orderRepository.Update(order);

                return true;
            }

            Console.WriteLine("[SERWIS] Płatność odrzucona.");
            return false;
        }
    }
}