using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shelter.Model.DataModules
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname {  get; set; }  = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public int positionId { get; set; }
        public int Salary { get; set; }

    }
}
