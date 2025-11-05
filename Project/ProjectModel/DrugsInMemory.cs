using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;
namespace Project.Model
{
    public class DrugsInMemory : IDrugsSource
    {
        public List<Drug> Drugs { get; private set; }

        public DrugsInMemory(List<Drug> drugs)
        {
            Drugs = drugs;
        }
        public List<Drug> AllDrugs()
        {
            return Drugs;
        }
        public bool AddDrug(Drug drug) 
        {
            if (drug is null) return false;
            Drugs.Add(drug);
            return true;
        }
        public bool DeleteDrug(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                return false;
            }
            var usuniety = Drugs.FirstOrDefault(x => x.Name == name);
            if (usuniety is null) return false;
            Drugs.Remove(usuniety);
            return true;
        }
    }
}
