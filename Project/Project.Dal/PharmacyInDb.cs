using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Model;
namespace Project.Dal
{
    public class PharmaciesInDataBase : IPharmaciesSource
    {
        private readonly ApplicationDbContext _db;

        public PharmaciesInDataBase(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<Pharmacy> AllPharmacies()
        {
            return _db.Pharmacies.Include(p => p.Address).Include(p => p.Employees).Include(p => p.Drugs).OrderBy(x => x.Id).ToList();
        }
    }
}
