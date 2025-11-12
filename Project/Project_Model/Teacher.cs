using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Model
{
    public class Teacher:Person{
        public required string Subject {  get; set; }
        public decimal PricePerHour { get; set; }
        public List<Lesson> Lessons { get; set; }=new List<Lesson>();

        public Teacher() {
            Subject = string.Empty;
            PricePerHour = 0m;
            Lessons = new List<Lesson>();
        }
        public Teacher(int personId, string firstName, string lastName, long phoneNumber, string email, string subject, decimal pricePerHour, List<Lesson> lessons)
            : base(personId, firstName, lastName, phoneNumber, email){
            Subject = subject;
            PricePerHour = pricePerHour;
            Lessons = lessons ?? new List<Lesson>();
        }

    }
    
}
