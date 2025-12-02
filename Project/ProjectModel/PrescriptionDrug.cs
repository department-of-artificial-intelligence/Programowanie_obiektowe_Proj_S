using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class PrescriptionDrug : Drug
    {
        public PrescriptionDrug(int id, string nazwa, string typ, string cena, string opis) : base(id, nazwa, typ, cena, opis) { }
        public PrescriptionDrug() : base() { }
        public override string ToString()
        {
            return base.ToString() + "| Lek Na Recepte |";
        }
    }
}
