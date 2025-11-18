using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Pharmacy
    {
        public IEmployeeManager Employees { get; private set; }
        public IDrugManager Drugs { get; private set; }
        public Address Address { get; private set; }
        public int Id { get; set; }
        public string Name { get; set; }

        public Pharmacy(int id, string nazwa, Address address, IEmployeeManager employeeManager, IDrugManager drugManager)
        {
            Id = id;
            Name = nazwa;
            Address = address;
            Employees = employeeManager;
            Drugs = drugManager;
        }
        public override string ToString()
        {
            return $"Apteka -- Id: {Id}, Nazwa: {Name}";
        }

    }
}
