using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;
namespace Project.Model
{
    public class PharmaciesInMemory : IPharmaciesSource
    {
        public List<Pharmacy> Pharmacies { get; private set; }

        public PharmaciesInMemory(List<Pharmacy> pharmacies)
        {
            Pharmacies = pharmacies;
        }
        public List<Pharmacy> AllPharmacies()
        {
            return Pharmacies;
        }
    }
}
