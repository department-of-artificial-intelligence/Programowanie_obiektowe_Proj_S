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
        public bool AddNewDrug(Drug drug)
        {
            if (drug is null) return false;
            foreach (var l in Drugs)
            {
                if (l.Name == drug.Name)
                {
                    return false;
                }
            }
            Drugs.Add(drug);
            return true;
        }
        public bool SortDrugs()
        {
            Drugs = Drugs.OrderBy(x => x.DrugId).ToList();
            return true;
        }
        public bool RemoveDrug(string name)
        {
            var doUsuniecia = Drugs.FirstOrDefault(x => x.Name == name);
            if (doUsuniecia is null) return false;
            Drugs.Remove(doUsuniecia);
            return true;
        }
    }
}
