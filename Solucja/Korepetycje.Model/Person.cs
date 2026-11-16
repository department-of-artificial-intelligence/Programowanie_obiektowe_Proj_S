using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korepetycje.Model
{
    public abstract class Person
    {
        public string Name;
        public string Surname;
        public string Phone_Number;
        public string Address;
        public Plan Schedule;
        public enum PrefferedPlaceOfLesson   //At who's house is the lesson to take place at
        {
            Tutor,
            Student
        }
    }
}
