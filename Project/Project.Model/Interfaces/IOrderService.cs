using Project.Model.Orders;
using Project.Model.People;
using Project.Model.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.Interfaces
{
    public interface IOrderService
    {
        List<Order> AllOrders();


        Order CreateOrder(Customer customer, Address deliveryAddress, Store store);
        void AddItemToOrder(Order order, Product product, int quantity);
        bool ProcessOrderPayment(Order order, IPayment paymentMethod);
        void ShipOrder(Order order);
        List<Order> GetCustomerHistory(Customer customer);


    }
}
