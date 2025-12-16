using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project.Model
{
    public class DrugManager
    {
        private readonly IDrugsSource _source;

        public DrugManager(IDrugsSource source)
        {
            _source = source;
        }
        public bool AddDrug(string nazwa, string typ, string cena, string opis, Pharmacy phar)
        {
            if (phar is null) return false;
            Drug nowy = new Drug(nazwa, typ, cena, opis, phar);
            if (nowy is null) return false;
            if (!_source.AddNewDrug(nowy)) return false;
            return true;
        }
        public bool AddPrescriptionDrug(string nazwa, string typ, string cena, string opis, Pharmacy phar)
        {
            if (phar is null) return false;
            Drug drug = new PrescriptionDrug(nazwa, typ, cena, opis, phar);
            if (!_source.AddNewDrug(drug)) return false;
            return true;
        }
        public bool RemoveDrug(int id, Pharmacy phar)
        {
            if (phar is null) return false;
            var lista = _source.AllDrugs().Where(x => x.PharmacyId == phar.Id).ToList();
            Drug? doUsuniecia = lista.FirstOrDefault(x => x.DrugId == id);
            if (doUsuniecia is null) return false;
            if (!_source.RemoveDrug(doUsuniecia)) return false;
            return true;
        }
        public void sortByFirstLetter(Pharmacy phar)
        {
            var lista = _source.AllDrugs();
            var pogrupowane = lista.Where(x => x.PharmacyId == phar.Id).GroupBy(x => x.Name[0]).OrderBy(x => x.Key);
            foreach (var group in pogrupowane)
            {
                string polaczone = string.Join(", ", group.Select(x => x.Name));
                Console.WriteLine($"{group.Key}: {polaczone}");
            }
        }
        public void sortByTypeOfDrug(Pharmacy phar)
        {
            if (phar is null) return;
            var lista = _source.AllDrugs().Where(x => x.PharmacyId == phar.Id);
            var pogrupowane = lista.GroupBy(x => x.TypeOfMedicine).OrderBy(x => x.Key);
            foreach(var grupa in pogrupowane)
            {
                string polaczone = string.Join(", ", grupa.Select(x => x.Name));
                Console.WriteLine($"{grupa.Key} : {polaczone}");
            }
        }
    }
}