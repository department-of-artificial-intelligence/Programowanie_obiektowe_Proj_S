using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model;

public class Seat
{
    public int RowNumber { get; }
    public int SeatNumber { get; }
    public Hall Hall { get; }

    public Seat(int rowNumber, int seatNumber, Hall hall)
    {
        if (hall is null) throw new ArgumentNullException(nameof(hall), "Sala nie może być null");
        if (rowNumber < 0) throw new ArgumentOutOfRangeException(nameof(rowNumber), "Numer rzędu musi być dodatni");
        if (seatNumber < 0) throw new ArgumentOutOfRangeException(nameof(seatNumber), "Numer siedzenia musi być dodatni");
        if (hall.Seats.Any(s => s.RowNumber == rowNumber && s.SeatNumber == seatNumber)) throw new InvalidOperationException($"Sala {hall.HallId} posiada już miejsce ({rowNumber}, {seatNumber})");
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
