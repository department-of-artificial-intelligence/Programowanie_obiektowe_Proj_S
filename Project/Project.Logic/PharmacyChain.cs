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
        public override string ToString()
        {
            var pharmacies = _source.AllPharmacies();
            string s = string.Empty;
            foreach(var phar in pharmacies)
            {
                s += phar + "\n" + "\n";
            }
            return "\n" + s;
        }
    }
}
