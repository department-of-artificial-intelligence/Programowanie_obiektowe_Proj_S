using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Teacher: Person
    {
        public required int TeacherId { get; set; }
        public required string LanquageOfTeaching { get; set; }
        public required decimal Salary { get; set; }
        public required decimal HoursWorked { get; set; }
        public List<Group> AssignedGroups {  get; set; }

        public Teacher()
        {
            AssignedGroups = new List<Group> ();
        }
    }
}
