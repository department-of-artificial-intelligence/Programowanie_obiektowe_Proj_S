using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Model
{
    public class Student:Person{
        public required int Id { get; set; }
        public required string Year {  get; set; }
        public List<Lesson> Lessons { get; set; } = new List<Lesson>();

    }
}
