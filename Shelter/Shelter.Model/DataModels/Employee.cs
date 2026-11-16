using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelter.Model.DataModels
{
    public class Employee
    {
        public int Id { get; set; }
        public int ShelterId { get; set; }
        public int SectorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Pesel { get; set; }
        public string Position { get; set; }
        public int Salary { get; set; }
        public Employee(int id, int shelterId, int sectorId, string firstName, string lastName, int pesel, string position, int salary)
        {
            Id = id;
            ShelterId = shelterId;
            SectorId = sectorId;
            FirstName = firstName;
            LastName = lastName;
            Pesel = pesel;
            Position = position;
            Salary = salary;
        }
    }
}
