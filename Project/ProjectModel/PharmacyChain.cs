using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class PharmacyChain
    {
        private readonly IPharmaciesSource _source;

        public PharmacyChain(IPharmaciesSource source)
        {
            _source = source;
        }
        public void displayAllPharmacies()
        {
            var pharmacies = _source.AllPharmacies();
            foreach(Pharmacy a in pharmacies)
            {
                Console.WriteLine($"{a},\n");
            }
        }
    }
}
