using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Student : Person
    {
        public required string LanguageOfLearning { get; set; }
        public required decimal Balance { get; set; }
        public List<Enrollment> Enrollments { get; set; }
        public List<Payment> Payments { get; set; } = new List<Payment>();
        public Student()
        {
            Enrollments = new List<Enrollment>();
        }

        public void AddGroup(Group group)
        {
            if (group == null)
            {
                throw new ArgumentNullException(nameof(group));
            }
            var enrollment = new Enrollment
            {
                EnrollmentId = 0,
                StudentId = this.Id,
                Student = this,
                GroupId = group.GroupId,
                Group = group,
                EnrollmentDate = DateTime.Now,
                Status = EnrollmentStatus.Active,
                AmountPaid = 0
            };

            Enrollments.Add(enrollment);
        }
    }
}
