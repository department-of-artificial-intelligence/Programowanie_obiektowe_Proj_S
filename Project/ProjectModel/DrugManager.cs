using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class DrugManager : IDrugManager
    {
        private readonly IDrugsSource _source;

        public DrugManager(IDrugsSource source)
        {
            _source = source;
        }
        public void DisplayDrugs()
        {
            var lista = _source.AllDrugs();
            foreach (var drug in lista)
            {
                Console.WriteLine(drug);
            }
        }
        public bool AddDrug(string nazwa, string typ, string cena, string opis)
        {
            var lista = _source.AllDrugs();
            int new_id = 1;
            while(lista.Any(x => x.DrugId == new_id))
            {
                new_id++;
            }
            Drug nowy = new Drug(new_id, nazwa, typ, cena, opis);
            if (nowy is null) return false;
            if(_source.AddNewDrug(nowy))
            {
                _source.SortDrugs();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool RemoveDrug(string nazwa)
        {
            if (string.IsNullOrWhiteSpace(nazwa)) return false;
            _source.RemoveDrug(nazwa);
            return true;
        }
        public void sortByFirstLetter()
        {
            var lista = _source.AllDrugs();
            var pogrupowane = lista.GroupBy(x => x.Name[0]).OrderBy(x => x.Key);
            foreach (var group in pogrupowane)
            {
                string polaczone = string.Join(", ", group.Select(x => x.Name));
                Console.WriteLine($"{group.Key}: {polaczone}");
            }
        }
        public void sortWhetherDrugIsOnPrescription()
        {
            var lista = _source.AllDrugs();
            var pogrupowane = lista.Where(x => x is PrescriptionDrug);
            string polaczone = string.Join(", ", pogrupowane.Select(x => x.Name));
            Console.WriteLine($"Leki Na Recepte: {polaczone}");
        }
    }
}