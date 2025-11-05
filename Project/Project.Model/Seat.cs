using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Seat
    {
        public int SeatId { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
        public Ticket Ticket { get; set; }

        public Seat() : this(default, default, default, new Ticket()) { }
        public Seat(int seatId, int rowNumber, int seatNumber, Ticket ticket)
        {
            SeatId = seatId;
            RowNumber = rowNumber;
            SeatNumber = seatNumber;
            Ticket = ticket;
        }
    }
}
