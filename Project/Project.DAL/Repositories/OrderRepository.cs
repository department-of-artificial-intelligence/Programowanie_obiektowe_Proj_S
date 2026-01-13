using Project.DAL;
using Project.Model.Interfaces;
using Project.Model.Orders;
using Project.Model.Stores;
using System.Collections.Generic;
using System.Linq;

namespace Project.Dal.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Order> GetAll()
        {
            return _db.Orders.ToList();
        }

        public List<Order> GetByStore(int storeId)
        {
            return _db.Orders
                      .Where(o => o.StoreId == storeId)
                      .ToList();
        }

        public bool AddOrder(Order order)
        {
            if (order == null) return false;

            _db.Orders.Add(order);
            _db.SaveChanges();
            return true;
        }

        public Order GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(Order order)
        {
            throw new NotImplementedException();
        }

        public void Update(Order order)
        {
            throw new NotImplementedException();
        }

        public void Remove(Order order)
        {
            throw new NotImplementedException();
        }
    }
}