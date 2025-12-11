namespace Project.Model;

public class Seat
{
    // Właściwości
    public int SeatId { get; private set; } // PK
    public int RowNumber { get; }
    public int SeatNumber { get; }

    // Konstruktory
    private Seat() { }

    internal Seat(int rowNumber, int seatNumber)
    {
        if (rowNumber <= 0) throw new ArgumentOutOfRangeException(nameof(rowNumber), "Numer rzędu musi być dodatni");
        if (seatNumber <= 0) throw new ArgumentOutOfRangeException(nameof(seatNumber), "Numer siedzenia musi być dodatni");
        RowNumber = rowNumber;
        SeatNumber = seatNumber;
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
