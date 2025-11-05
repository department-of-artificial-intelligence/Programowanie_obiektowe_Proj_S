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
        bool DeleteEmployee(int id);
    }
    public interface IEmployeeManager
    {
        void DisplayEmployees();
        bool AddEmployee();
        bool DeleteEmployee();
    }
    //------------------------------Interfejsy do klasy EmployeeManager
    public interface IPharmaciesSource
    {
        List<Pharmacy> AllPharmacies();
    }
    public interface IPharmacyChain
    {
        void displayAllPharmacies();
    }
    //------------------------------Interfejsy do klasy PharmacyChain
    public interface IDrugsSource
    {
        List<Drug> AllDrugs();
        bool AddDrug(Drug drug);
        bool DeleteDrug(string name);
    }
    public interface IDrugManager
    {
        void DisplayDrugs();
        bool AddDrug();
        bool DeleteDrug();
    }
}
