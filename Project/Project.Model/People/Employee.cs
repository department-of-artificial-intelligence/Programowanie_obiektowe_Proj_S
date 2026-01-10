using Project.Model.Stores;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.People
{

    public class Employee : Person
    {
        public int EmployeeId {  get; private set; }

        public required Store WorkPlace { get; set; }
        public required EmployeePosition Position { get; set; }



        private decimal _salary;
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


        public required DateTime HireDate { get; set; } = DateTime.Now;

        [SetsRequiredMembers]
        public Employee(): base(string.Empty, string.Empty, string.Empty, string.Empty) { }


        [SetsRequiredMembers]
        public Employee(string firstName, string lastName, string phone, string email, Store workPlace, EmployeePosition position, decimal salary, DateTime hireDate)
            : base(firstName, lastName, phone, email)
        {
            WorkPlace = workPlace;
            Position = position;
            Salary = salary;
            HireDate = hireDate;
        }
        
        


        public override string GetInfo()
        {
            return $"[EMPLOYEE #{EmployeeId}] {base.GetInfo()} | Stanowisko: {Position}";
        }
    }






    
}
