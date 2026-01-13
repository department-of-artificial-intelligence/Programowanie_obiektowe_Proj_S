using Project.DAL;
using Project.Model.Interfaces;
using Project.Model.Stores;
using System.Collections.Generic;
using System.Linq;

namespace Project.Dal.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly ApplicationDbContext _db;

        public StoreRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Store> GetAll()
        {
            return _db.Stores.ToList();
        }

        public Store GetById(int id)
        {
            return _db.Stores.FirstOrDefault(s => s.Id == id);
        }

        public bool Add(Store store)
        {
            if (store == null) return false;

            _db.Stores.Add(store);
            _db.SaveChanges();
            return true;
        }

        public bool Remove(int id)
        {
            var store = _db.Stores.FirstOrDefault(s => s.Id == id);
            if (store == null) return false;

            _db.Stores.Remove(store);
            _db.SaveChanges();
            return true;
        }

        public bool Update(Store store)
        {
            if (store == null) return false;

            _db.Stores.Update(store);
            _db.SaveChanges();
            return true;
        }
    }
}