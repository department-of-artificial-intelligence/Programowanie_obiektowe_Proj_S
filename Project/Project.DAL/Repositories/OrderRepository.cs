using Project.DAL;
using Project.Model.Interfaces;
using Project.Model.Orders;
using System;
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

        public Order GetById(int id)
        {
            return _db.Orders.FirstOrDefault(o => o.OrderId == id);
        }

        public void Add(Order order)
        {
            if (order != null)
            {
                _db.Orders.Add(order);
                _db.SaveChanges();
            }
        }

        public bool AddOrder(Order order)
        {
            if (order == null) return false;

            try
            {
                _db.Orders.Add(order);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Update(Order order)
        {
            if (order != null)
            {
                _db.Orders.Update(order);
                _db.SaveChanges();
            }
        }

        public void Remove(Order order)
        {
            if (order != null)
            {
                _db.Orders.Remove(order);
                _db.SaveChanges();
            }
        }
    }
}