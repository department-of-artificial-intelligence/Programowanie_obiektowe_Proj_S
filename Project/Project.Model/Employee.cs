using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Employee: Person
    {

        public required int Id { get; set; }


        public required EmployeePosition Position { get; set; }

        public required decimal Salary { get; set; }

        public required int StoreId { get; set; }

        public required string WorkPlace { get; set; }





        private Employee() : base() { }


        public Employee(int id, string firstName, string lastName, EmployeePosition position, decimal salary, int storeId, string workPlace) : base(firstName, lastName)
        {
            if (id < 0) throw new ArgumentException("Id nie może być wartością ujemną", nameof(id));
            FirstName = firstName;
            LastName = lastName;
            Position = position;
            Salary = salary;
            StoreId = storeId;
            WorkPlace = workPlace;
        }



        public void ChangePosition(int id, EmployeePosition position)
        {
            if (id < 0) throw new ArgumentException("Id nie może być wartością ujemną", nameof(id));

            Position = position;


        }



    
        public void ChangeStore(int id,  int storeId)
        {
            if (id >= 0)
            {
                this.StoreId = storeId;
            }
            else
            {
                this.StoreId = 0;
            }
        }






    }
}
