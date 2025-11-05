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

        public Employeeposition Position { get; set; }

        public decimal Salary { get; set; }

        public int StoreId { get; set; }

        public string StorePlace { get; set; }





        public Employee() :base() { }


        public Employee(int id, string firstName, string lastName, EmployeePosition position, decimal salary, int storeId, string storePlace)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Position = position;
            Salary = salary;
            StoreId = storeId;
            StorePlace = storePlace;
        }



        public void ChangePosition(int id, EmployeePosition position)
        {
            if(id > 0 && id != null)
            {
                this.Position = position;
            }
            else
            {
                this.Position = null;
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
