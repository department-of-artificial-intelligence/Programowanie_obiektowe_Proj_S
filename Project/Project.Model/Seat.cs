using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model;

public class Seat
{
    public int RowNumber { get; set; }
    public int SeatNumber { get; set; }
    public Hall Hall { get; }

    public Seat(int rowNumber, int seatNumber, Hall hall)
    {
        if (hall.Seats.Any(s => s.RowNumber == rowNumber && s.SeatNumber == seatNumber)) throw new Exception($"Hall {hall.HallId} posiada już miejsce ({rowNumber},{seatNumber})");
        RowNumber = rowNumber;
        SeatNumber = seatNumber;
        Hall = hall;
    }

    public (int Row, int Seat) SeatLocation()
    {
        return (RowNumber, SeatNumber);
    }

    public override string ToString()
    {
        return $"rząd:{RowNumber},miejsce:{SeatNumber}";
    }
}
