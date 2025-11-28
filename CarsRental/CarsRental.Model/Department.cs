using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CarsRental.Model
{
    public class DepartmentManager : IManager<Department>
    {
        private List<Department> _departments = new List<Department>();
        private int _departmentCounter = 0;

        public void Add(Department department)
        {
            Console.WriteLine("Dodawanie wypożyczalni...");

            if (string.IsNullOrEmpty(department.Name) && string.IsNullOrEmpty(department.Address) && (string.IsNullOrEmpty(department.PhoneNumber) || (string.IsNullOrEmpty(department.EmailAddress))))
            {
                Console.WriteLine("Nie można dodać pustych danych!\n");
                return;
            }

            foreach (Department d in _departments)
            {
                if (d.Name.Equals(department.Name))
                {
                    Console.WriteLine("Podana wypożyczalnia znajduje się już w bazie danych!\n");
                    return;
                }
            }

            _departmentCounter++;
            department.Id = _departmentCounter;
            _departments.Add(department);

            Console.WriteLine($"Dodano '{department.Name}' {department.Address}\n");
        }

        public List<Department> GetAll()
        {
            return _departments;
        }

        public Department? GetById(int id)
        {
            var dep = _departments.Find(d => d.Id == id);
            if (dep == null)
            {
                Console.WriteLine($"Nie znaleziono wypożyczalni o ID({id})\n");
                return null;
            }
            return dep;
        }

        public void Remove(int id)
        {
            Console.WriteLine("Usuwanie wypożyczalni...");

            Department? depToRemove = GetById(id);
            if (depToRemove != null)
            {
                _departments.Remove(depToRemove);
                Console.WriteLine($"Usunięto '{depToRemove.Name}' {depToRemove.Address}\n");
            }
        }
    }

    public class Department : IIdentify
    {
        private int _id;
        public int Id { get { return _id; } set { _id = value; } }

        private string _name;
        public string Name { get { return _name; } set { _name = value; } }

        private string _address;
        public string Address { get { return _address; } set { _address = value; } }

        private string _phoneNumber;
        public string PhoneNumber { get { return _phoneNumber; } set { _phoneNumber = value; } }

        private string _emailAddress;
        public string EmailAddress { get { return _emailAddress; } set { _emailAddress = value; } }

        public Department() : this(0, string.Empty, string.Empty, string.Empty, string.Empty) { }
        public Department(int id, string name, string address, string phoneNumber, string emailAddress)
        {
            _id = id;
            _name = name;
            _address = address;
            _phoneNumber = phoneNumber;
            _emailAddress = emailAddress;
        }

        public override string ToString()
        {
            return $"'{Name}' {Address}\n{PhoneNumber}, {EmailAddress}\n";
        }
    }
}
