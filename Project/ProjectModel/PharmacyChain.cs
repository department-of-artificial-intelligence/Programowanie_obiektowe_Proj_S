using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class PharmacyChain
    {
        public List<Pharmacy> _apteki { get; set; }

        public PharmacyChain(List<Pharmacy> apteki)
        {
            _apteki = new List<Pharmacy>();
            if(apteki != null)
            {
                foreach(Pharmacy apteka in apteki)
                {
                    _apteki.Add(apteka);
                }
            }
        }
        public void wyswietlSiecAptek()
        {
            foreach(Pharmacy a in _apteki)
            {
                Console.WriteLine($"{a},\n");
            }
        }
    }
}
