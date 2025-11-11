using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Seat
    {
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
        public Ticket? Ticket { get; private set; }

        public Seat(int rowNumber, int seatNumber)
        {
            RowNumber = rowNumber;
            SeatNumber = seatNumber;
            Ticket = null;
        }

        public bool AssignTicket(Ticket ticket)
        {
            if (ticket == null) return false;
            Ticket = ticket;
            return true;
        }
    }
}
