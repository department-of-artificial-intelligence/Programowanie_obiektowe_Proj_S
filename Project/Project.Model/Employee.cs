using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Employee
    {

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public EmployeePosition Position { get; set; }

        public decimal Salary { get; set; }

        public int StoreId { get; set; }

        public string WorkPlace { get; set; }





        private Employee() :base() { }


        public Employee(int id, string firstName, string lastName, EmployeePosition position, decimal salary, int storeId, string workPlace)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Position = position;
            Salary = salary;
            StoreId = storeId;
            WorkPlace = workPlace;
        }



        public void ChangePosition(int id, EmployeePosition position)
        {
            if(id >= 0 )
            {
                this.Position = position;
            }
            else
            {
                this.Position = 0;
            }


        }



    
        public void ChangeStore(int id,  int storeId)
        {
            if (id > 0 && id != null)
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
