using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Group: IReportable
    {
        public required int GroupId {  get; set; }
        public required string GroupName {  get; set; }
        public required int CourseId {  get; set; }
        public required int TeacherId {  get; set; }
        public required int MaxStudents {  get; set; }
        public required string Schedule {  get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        public List<Enrollment> Enrollments { get; set; }
        public List<Attendance> Attendances { get; set; } = new List<Attendance>();

        public Group()
        {
            Enrollments = new List<Enrollment>();
        }

        public virtual Teacher? Teacher { get; set; }
        public virtual Course? Course { get; set; }


        public string GetInfo()
        {
            return $"Grupa {GroupName} ({Course?.Language}), Uczniów: {Enrollments?.Count ?? 0}/{MaxStudents}";
        }
    }
}
