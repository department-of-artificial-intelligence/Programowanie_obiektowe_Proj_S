using System;
using System.Collections.Generic;
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

        public DateTime HireDate { get; set; } = DateTime.Now;

        public int? StoreId { get; set; }

        public Store? WorkPlace { get; set; }




        protected Employee() { }

        
        
        public Employee(int id, string firstName, string lastName, string email, int phone, decimal salary, EmployeePosition position)
            : base(firstName, lastName, phone, email)
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
