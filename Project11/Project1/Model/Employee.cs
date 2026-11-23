using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Model
{
    class Employee : Person
    {
        private int _employeeNumber;
        private string _position;
        private DateTime _hireDate;
        private decimal _salary;
        public int EmployeeNumber
        {
            get => _employeeNumber;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException("EmployeeNumberOutOfRange");
                _employeeNumber = value;
            }
        }
        public string Position
        {
            get => _position;
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentException("NoPosition");
                _position = value;
            }
        }
        public DateTime HireDate
        {
            get => _hireDate;
            set
            {
                HireDate = value;
            }
        }
        public decimal Salary
        {
            get => _salary;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException("SalaryOutOfRange");
                _salary = value;
            }
        }
        public Employee():base() {
            EmployeeNumber = 1;
            Position = "0";
            HireDate = DateTime.Now;
            Salary = 1;
        }
        public Employee(int id, string firstName, string lastName, string email, string phoneNumber, int employeeNumber, string position, DateTime hireDate, decimal salary):base(id, firstName, lastName, email, phoneNumber)
        {
            EmployeeNumber = employeeNumber;
            Position = position;
            HireDate = hireDate;
            Salary = salary;
        }
    }
}
