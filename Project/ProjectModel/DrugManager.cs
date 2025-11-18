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
            foreach(var drug in lista)
            {
                Console.WriteLine(drug);
            }
            
        }
        public bool AddDrug()
        {
            /*
            var drugs = _source.AllDrugs();
            
            int new_id = 0;
            while (drugs.Any(x => x.Id == new_id))   gdy dodamy Id do klasy Drug < --
            {
                new_id++;
            }
            */
            Console.Write("Podaj nazwe dodawanego leku: ");
            string? nazwa = Console.ReadLine();
            Console.Write("Podaj typ dodawanego leku: ");                              
            string? typ = Console.ReadLine();
            Console.Write("Podaj cene leku - musi to byc liczba: ");
            string? cena = Console.ReadLine();
            Console.Write("Podaj opis leku: ");
            string? opis = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(nazwa) || string.IsNullOrWhiteSpace(typ) || string.IsNullOrWhiteSpace(cena) || string.IsNullOrWhiteSpace(opis))
            {
                Console.WriteLine("Nie wpisales ktorejs z danych leku");
                return false;
            }
            Drug nowy = new Drug(nazwa, typ, cena, opis);
            if (nowy is null) return false;
            _source.AddDrug(nowy);
            return true;
        }
        public bool DeleteDrug()
        {
            Console.WriteLine("Podaj nazwe leku do usuniecia: ");
            string? nazwa = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(nazwa))
            {
                Console.WriteLine("Nie wpisales nic!");
                return false;
            }
            _source.DeleteDrug(nazwa);
            return true;
        }
        public void sortByFirstLetter()
        {
            var lista = _source.AllDrugs();
            var pogrupowane = lista.GroupBy(x => x.Name[0]).OrderBy(x => x.Key);
            foreach(var group in pogrupowane)
            {
                string polaczone = string.Join(", ", group.Select(x => x.Name));
                Console.WriteLine($"{group.Key}: {polaczone}");
            }
        }
        public void sortByTypeOfDrug()
        {
            var lista = _source.AllDrugs();
            var pogrupowane = lista.GroupBy(x => x.TypeOfMedicine);
            foreach (var group in pogrupowane)
            {
                string polaczone = string.Join(", ", group.Select(x => x.Name));
                Console.WriteLine($"{group.Key}: {polaczone}");
            }
        }
    }
}
