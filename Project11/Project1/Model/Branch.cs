using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Model
{
   class Branch
    {
        private int _id;
        private string _name;
        private string _address;
        private string _city;
        private string _phoneNumber;
        private string _openingHours;
        private List<Employee> _employees = [];
        private List<Car> _cars = [];
        public int Id
        {
            get => _id;
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException("IdOutOfRange");
                _id = value;
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException("NameIsEmpty");
                _name = value;
            }
        }
        public string Address
        {
            get => _address;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException("AddressIsEmpty");
                _address = value;
            }
        }
        public string City
        {
            get => _city;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException("CityIsEmpty");
                _city = value;
            }
        }
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException("PhoneNumberIsEmpty");
                _phoneNumber = value;
            }
        }
        public string OpeningHours
        {
            get => _openingHours;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException("OpeningHoursIsEmpty");
                _openingHours = value;
            }
        }
        public List<Employee> Employees
        {
            get=> _employees;
            set
            {
                if (value == null) throw new ArgumentNullException("EmployeesIsNull");
                _employees = value;
            }
        }
        public List<Car> Cars
        {
            get => _cars;
            set
            {
                if (value == null) throw new ArgumentNullException("CarsIsNull");
                _cars = value;
            }
        }
        public Branch(int id, string name, string address, string city, string phoneNumber, string openingHours, List<Employee> employees, List<Car> cars)
        {
            Id = id;
            Name = name;
            Address = address;
            City = city;
            PhoneNumber = phoneNumber;
            OpeningHours = openingHours;
            Employees = employees;
            Cars = cars;
        }
        public void AddEmployee(Employee employee)
        {
            if (employee == null || Employees == null) throw new ArgumentNullException("EmployeeOrEmployeesIsNull");
            Employees.Add(employee);
        }
        public void RemoveEmployee(Employee employee)
        {
            if (employee == null || Employees == null) throw new ArgumentNullException("EmployeeOrEmployeesIsNull");
            Employees.Remove(employee);
        }
        public void AddCar(Car car)
        {
            if (car == null || Cars == null) throw new ArgumentNullException("CarOrCarsIsNull");
            Cars.Add(car);
        }
        public void RemoveCar(Car car)
        {
            if (car == null || Cars == null) throw new ArgumentNullException("CarOrCarsIsNull");
            Cars.Remove(car);
        }
    }
}
