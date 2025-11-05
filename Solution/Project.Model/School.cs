using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class School
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Country { get; set; }
        public required string City { get; set; }
        public required string Address { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<Student> Students { get; set; }
        public List<Course> Courses { get; set; }
        public List<Group> Groups { get; set; }

        public School()
        {
            Teachers = new List<Teacher>();
            Students = new List<Student>();
            Courses = new List<Course>();
            Groups = new List<Group>();
        }

    }
}
