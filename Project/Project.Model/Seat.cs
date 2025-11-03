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

        public Seat() : this(default, default, default) { }
        public Seat(int seatId, int rowNumber, int seatNumber)
        {
            SeatId = seatId;
            RowNumber = rowNumber;
            SeatNumber = seatNumber;
        }
    }
}
