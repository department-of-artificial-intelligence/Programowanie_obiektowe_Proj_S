using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Model
{
    internal class Teacher:Person{
        public string Subject {  get; set; }
        public decimal PricePerHour { get; set; }
        public List<Lesson> Lessons { get; set; }=new List<Lesson>();
    }
}
