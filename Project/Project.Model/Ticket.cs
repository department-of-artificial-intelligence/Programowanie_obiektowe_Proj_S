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
        public double Price { get; set; }

      
        public Concert Concert { get; set; }
        public enum TicketType
        {
            Floor = 0,
            Seated = 1,
        };

        public TicketType Type { get; set; }

        public string SeatNumber {  get; set; }

        public Ticket() 
        {
            Concert = new Concert();
            Concert.TicketsSold += 1;
            SeatNumber = string.Empty;
        }
        



        public override string ToString()
        {
            string _type= string.Empty;
            if (Type == TicketType.Seated)
            {
                _type = "Miejsce siedzące";
                return Concert.ToString() + $", {_type}, Miejsce {SeatNumber}, {Price} zł";
            }
            else
            {
                _type = "Miejsce stojące";
                return Concert.ToString() + $", {_type}, {Price} zł";
            }
        }
    }
}
