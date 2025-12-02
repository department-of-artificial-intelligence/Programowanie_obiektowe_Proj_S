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
        RowNumber = rowNumber;
        SeatNumber = seatNumber;
        Hall = hall;
    }

    public (int Row,int Seat) SeatLocation() // ??? testowe
    {
        return (RowNumber, SeatNumber);
    }

    public string SeatLocationString()
    {
        return $"rząd:{RowNumber},miejsce:{SeatNumber}";
    }

    public override string ToString()
    {
        return $"{Hall}/{SeatLocationString()}";
    }
}
