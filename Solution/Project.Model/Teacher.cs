using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Teacher: Person
    {
        public required string LanguageOfTeaching { get; set; }
        public required decimal Salary { get; set; }
        public required decimal HoursWorked { get; set; }
        public List<Group> AssignedGroups {  get; set; }

        public Teacher()
        {
            AssignedGroups = new List<Group> ();
        }

        public override string GetInfo()
        {
            return $"[Teacher] {FirstName} {LastName} (ID: {Id})";
        }
    }
}
