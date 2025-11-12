using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Model
{
    public class Student:User{
        public string EducationalLevel {  get; set; }
        public List<Subject> Interests { get; set; } = new List<Subject>();

        public Student()
           : base(0, string.Empty, string.Empty, string.Empty)
        {
            EducationalLevel = string.Empty;
            Interests = new List<Subject>();
        }
        public Student(int id, string firstName, string lastName, string email, string educationalLevel)
            :base(id, firstName, lastName, email) {  
            EducationalLevel = educationalLevel;
        }
        public override string ToString()
            => $"{base.ToString()}, Poziom: {EducationalLevel}";
    }
}
