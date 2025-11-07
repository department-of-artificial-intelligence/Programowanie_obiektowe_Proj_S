using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Ticket
    {
        public int Id { get; set; }
        public float Price { get; set; }

      
        public Concert Concert { get; set; }
        public enum Ticket_type
        {
            Floor = 0,
            Seated = 1,
        };

        public Ticket_type Type { get; set; }

        public char Sector {  get; set; }
        public int Seat_Number { get; set; }

        public Ticket() 
        {
            Concert = new Concert();
            Concert.TicketsSold += 1;
        }
        public Ticket() { }



        public override string ToString()
        {
            string _type= string.Empty;
            if (Type == Ticket_type.Seated)
            {
                _type = "Miejsce siedzące";
                return Concert.ToString() + $", {_type}, Miejsce {Sector}{Seat_Number}, {Price} zł";
            }
            else
            {
                _type = "Miejsce stojące";
                return Concert.ToString() + $", {_type}, {Price} zł";
            }
        }
    }
}
