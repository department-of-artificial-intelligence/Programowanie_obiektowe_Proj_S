using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Enrollment
    {
        public required int EnrollmentId {  get; set; }
        public required int StudentId {  get; set; }
        public required int GroupId {  get; set; }
        public required  DateTime EnrollmentDate {  get; set; }
        public required EnrollmentStatus Status {  get; set; }
        public required decimal AmountPaid {  get; set; }

        public required Student Student { get; set; }
        public required Group Group { get; set; }
    }
}
