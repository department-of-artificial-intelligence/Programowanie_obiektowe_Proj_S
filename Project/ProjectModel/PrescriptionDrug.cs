using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class PrescriptionDrug : Drug
    {
        public PrescriptionDrug(string nazwa, string typ, string cena, string opis, Pharmacy pharmacy) : base(nazwa, typ, cena, opis, pharmacy) { }
        public PrescriptionDrug() : base() { }
        public override string ToString()
        {
            return base.ToString() + "| Lek Na Recepte |";
        }
    }
}
