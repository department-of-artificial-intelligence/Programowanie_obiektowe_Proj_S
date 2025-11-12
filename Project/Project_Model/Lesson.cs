using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Model
{
    public class Lesson{
        public int Id {  get; set; }
        public DateTime Date {  get; set; }
        public int DurationMinutes { get; set; }
        public bool Paid { get; set; }
        public Lesson(){
            Id= 0;
            Date= DateTime.MinValue;
            DurationMinutes= 0;
            Paid= false;
        }
        public Lesson(int id, DateTime date, int durationMinutes, bool paid){
            Id= id;
            Date= date;
            DurationMinutes= durationMinutes;
            Paid= paid;
        }
    }
}
