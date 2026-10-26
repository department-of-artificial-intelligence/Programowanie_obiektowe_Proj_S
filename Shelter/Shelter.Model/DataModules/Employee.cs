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
        public int ShelterId { get; set; }
        public int SectorId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName {  get; set; }  = string.Empty;
        public int Pesel { get; set; }
        public string Position { get; set; } = string.Empty;
        public int Salary { get; set; }

    }
}
