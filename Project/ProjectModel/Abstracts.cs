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
    }
    public interface IPharmaciesSource
    {
        List<Pharmacy> AllPharmacies();
    }
    public interface IDrugsSource
    {
        List<Drug> AllDrugs();
        bool AddNewDrug(Drug drug);
        bool RemoveDrug(Drug drug);
    }
}
