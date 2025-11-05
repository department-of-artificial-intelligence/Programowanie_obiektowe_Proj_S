using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Group
    {
        public required int GroupId {  get; set; }
        public required string GroupName {  get; set; }
        public required int CourseId {  get; set; }
        public required int TeacherId {  get; set; }
        public required int MaxStudents {  get; set; }
        public required string Schedule {  get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        public List<Student> Students { get; set; }

        public Group()
        {
            Students = new List<Student>();
        }

        public required Teacher Teacher { get; set; }
        public required Course Course { get; set; }

    }
}
