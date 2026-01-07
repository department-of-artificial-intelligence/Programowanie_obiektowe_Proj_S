using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{

    public class Employee : Person
    {
        private int _employeeId;
        private decimal _salary;

        
        public required int EmployeeId
        {
            get { return _employeeId; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(EmployeeId), "ID cannot be negative!");
                _employeeId = value;
            }
        }

        
        public required decimal Salary
        {
            get { return _salary; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Salary), "Salary cannot be negative!");
                _salary = value;
            }
        }


        public required EmployeePosition Position { get; set; }

        public required DateTime HireDate { get; set; } = DateTime.Now;

        public required int? StoreId { get; set; }

        public required Store? WorkPlace { get; set; }

        


        protected Employee() { }


        [SetsRequiredMembers]
        public Employee(int id, string firstName, string lastName, string email, string address, string phone, decimal salary, EmployeePosition position)
            : base(firstName, lastName, phone, email, address)
        {
            EmployeeId = id;
            Salary = salary;
            Position = position;
        }
        
        


        public override string GetInfo()
        {
            return $"[EMPLOYEE #{EmployeeId}] {base.GetInfo()} | Stanowisko: {Position}";
        }
    }






    
}
