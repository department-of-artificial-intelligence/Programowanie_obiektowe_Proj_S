using Xunit;
using Project.Model;

namespace Project.Tests;

public class SeatTests
{
    [Fact]
    public void Composition()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");
        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");
        var hall = theater!.CreateHall("Wielka Sala");

        var seat = hall!.CreateSeat(1, 1);
        Assert.NotNull(seat);
        Assert.Equal(1, seat.RowNumber);
        Assert.Equal(1, seat.SeatNumber);

        seat = hall.CreateSeat(-1, 1);
        Assert.Null(seat);
        seat = hall.CreateSeat(1, -1);
        Assert.Null(seat);

        List<Seat> seats = hall.CreateSeats(1, 3);

        Assert.NotNull(seats[0]);
        Assert.NotNull(seats[1]);
        Assert.Equal(1, seats[0].RowNumber);
        Assert.Equal(2, seats[0].SeatNumber);
        Assert.Equal(1, seats[1].RowNumber);
        Assert.Equal(3, seats[1].SeatNumber);
    }

    [Fact]
    public void SeatLocation()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");
        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");
        var hall = theater!.CreateHall("Wielka Sala");

        var seat = hall!.CreateSeat(2, 3);
        Assert.NotNull(seat);

        var location = seat!.SeatLocation();
        Assert.Equal(2, location.Row);
        Assert.Equal(3, location.Seat);
    }

    [Fact]
    public void ToStringTest()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");
        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");
        var hall = theater!.CreateHall("Wielka Sala");

        var seat = hall!.CreateSeat(2, 3);

        var result = seat!.ToString();

        Assert.Contains("rząd:2", result);
        Assert.Contains("miejsce:3", result);
    }
}