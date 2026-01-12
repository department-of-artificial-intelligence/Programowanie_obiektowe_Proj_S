using System.Collections.Generic;
using Project.Model.Orders;

namespace Project.Model.Interfaces
{
    
    public interface IOrderRepository
    {
        

        List<Order> GetAll();         
        Order GetById(int id);        
        void Add(Order order);         
        void Update(Order order);      
        void Remove(Order order);      
    }
}