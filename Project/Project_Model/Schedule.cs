using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Model
{
    public class Schedule
    {
        public Teacher Teacher { get; set; }
        public List<Lesson> Lessons { get; set; }= new List<Lesson>();


    }
}
