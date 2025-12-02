using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;
namespace Project.Model
{
    public interface ISourceEmployee
    {
        List<Employee> AllEmployees();
        bool AddEmployee(Employee employee);
        bool RemoveEmployee(Employee employee);

        bool SortEmployees();
    }
    public interface IEmployeeManager
    {
        void DisplayEmployees();
        bool AddEmployee(string firstName, string lastName, string position);
        bool RemoveEmployee(int id);
    }
    //------------------------------Interfejsy do klasy EmployeeManager
    public interface IPharmaciesSource
    {
        List<Pharmacy> AllPharmacies();
    }
    //------------------------------Interfejsy do klasy PharmacyChain
    public interface IDrugsSource
    {
        List<Drug> AllDrugs();
        bool AddNewDrug(Drug drug);
        bool RemoveDrug(string name);
        bool SortDrugs();
    }
    public interface IDrugManager
    {
        void DisplayDrugs();
        bool AddDrug(string nazwa, string typ, string cena, string opis);
        bool RemoveDrug(string doUsuniecia);
        void sortByFirstLetter();
        void sortWhetherDrugIsOnPrescription();
    }
}
