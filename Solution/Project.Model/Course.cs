using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Course
    {
        public required int CourseId {  get; set; }
        public required string Language {  get; set; }
        public required string Level {  get; set; }
        public required decimal PricePerHour {  get; set; }
        public required int DurationInHours {  get; set; }
        public List<Group> Groups { get; set; }

        public Course()
        {
            Groups = new List<Group>();
        }
    }
}