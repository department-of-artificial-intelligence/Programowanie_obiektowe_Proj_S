using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Dal;
using Project.Model;
namespace Project.Dal
{
    public class DrugsInDataBase : IDrugsSource
    {
        private readonly ApplicationDbContext _db;

        public DrugsInDataBase(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<Drug> AllDrugs()
        {
            return _db.Drugs.OrderBy(x => x.DrugId).ToList();
        }
        public bool AddNewDrug(Drug drug)
        {
            if (drug is null) return false;
            foreach (var l in _db.Drugs)
            {
                if (l.Name == drug.Name)
                {
                    return false;
                }
            }
            _db.Drugs.Add(drug);
            _db.SaveChanges();
            return true;
        }
        public bool RemoveDrug(Drug drug)
        {
            if (drug is null) return false;
            _db.Drugs.Remove(drug);
            _db.SaveChanges();
            return true;
        }
    }
}
