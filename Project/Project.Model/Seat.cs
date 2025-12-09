namespace Project.Model;

public class Seat
{
    // Właściwości
    public int SeatId { get; private set; } // PK
    public int RowNumber { get; }
    public int SeatNumber { get; }
    public Hall Hall { get; } = default!; // Navigation property

    // Konstruktory
    private Seat() { }

    internal Seat(int rowNumber, int seatNumber, Hall hall)
    {
        if (rowNumber < 0) throw new ArgumentOutOfRangeException(nameof(rowNumber), "Numer rzędu musi być dodatni");
        if (seatNumber < 0) throw new ArgumentOutOfRangeException(nameof(seatNumber), "Numer siedzenia musi być dodatni");
        if (hall.Seats.Any(s => s.RowNumber == rowNumber && s.SeatNumber == seatNumber)) throw new InvalidOperationException($"Sala {hall.HallName} posiada już miejsce ({rowNumber}, {seatNumber})");
        RowNumber = rowNumber;
        SeatNumber = seatNumber;
        Hall = hall;
    }

    // Metoda zwracająca pozycję siedzenia
    public (int Row, int Seat) SeatLocation()
    {
        return (RowNumber, SeatNumber);
    }

    // Metody string
    public override string ToString()
    {
        return $"rząd:{RowNumber},miejsce:{SeatNumber}";
    }
}
