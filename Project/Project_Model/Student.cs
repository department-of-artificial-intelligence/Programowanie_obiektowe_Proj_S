using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Model
{
    public class Student:Person{
        public int Id { get; set; }
        public string Year {  get; set; }
        public List<Lesson> Lekcje { get; set; } =new List<Lesson>();

    }
}
