using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Model
{
    public class TutoringProgram{
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Teacher> Teachers { get; set; }= new List<Teacher>();
        public List<Lesson> Lessons { get; set; }= new List<Lesson>();

    }
}
