using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Model
{ 
    public class Reservation
    {
        public int Id { get; set; }
        public Lesson Lesson { get; set; }
        public DateTime CreationDate {  get; set; }
        public string Status {  get; set; }
        public Reservation()
        {
            Id = 0;
            Lesson = null;
            CreationDate = DateTime.MinValue;
            Status = string.Empty;
        }
        public Reservation(int id, Lesson lesson)
        {
            Id = id;
            Lesson = lesson;
            CreationDate = DateTime.Now;
            //Status = ReservationStatuses.Pending;
        }
    }
}
