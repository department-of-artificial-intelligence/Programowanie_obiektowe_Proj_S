using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Student : Person
    {
        public required int StudentId { get; set; }
        public required string LanguageOfLearning { get; set; }
        public required decimal Balance { get; set; }
        public List<Enrollment> Enrollments { get; set; }

        public Student()
        {
            Enrollments = new List<Enrollment>();
        }

        public void AddGroup(Group group)
        {
            if (group != null)
            {
            }
        }
    }
}
