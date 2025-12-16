using Xunit;
using Project.Model;

namespace Project.Tests;

public class HallTests
{
    [Fact]
    public void Composition()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");
        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");

        var hall = theater!.CreateHall("Wielka Sala");

        Assert.NotNull(hall);
        Assert.Equal("Wielka Sala", hall.HallName);
        Assert.True(hall.Seats.Count == 0);
        Assert.True(hall.Performances.Count == 0);

        hall = theater!.CreateHall(null!);
        Assert.Null(hall);
        hall = theater!.CreateHall("");
        Assert.Null(hall);
        hall = theater!.CreateHall("    ");
        Assert.Null(hall);
    }

    [Fact]
    public void HallStringException()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");
        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");

        var hall = theater!.CreateHall("Wielka Sala");

        Assert.Throws<ArgumentException>(() => hall!.HallName = null!);
        Assert.Throws<ArgumentException>(() => hall!.HallName = "");
        Assert.Throws<ArgumentException>(() => hall!.HallName = "    ");
    }

    [Fact]
    public void CreateSeat()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");
        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");
        var hall = theater!.CreateHall("Wielka Sala");

        var seat = hall!.CreateSeat(1, 1);

        Assert.NotNull(seat);
        Assert.True(hall.Seats.Count == 1);

        seat = hall!.CreateSeat(-1, 1);
        Assert.Null(seat);
        seat = hall!.CreateSeat(1, -1);
        Assert.Null(seat);
        seat = hall!.CreateSeat(1, 1);
        Assert.Null(seat);

        Assert.True(theater.Halls.Count == 1);
    }

    [Fact]
    public void CreateSeats()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");
        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");
        var hall = theater!.CreateHall("Wielka Sala");

        List<Seat> seats = hall!.CreateSeats(1, 2);

        Assert.True(seats.Count == 2);
        Assert.True(hall.Seats.Count == 2);

        seats = hall!.CreateSeats(2, 2);

        Assert.True(seats.Count == 2);
        Assert.True(hall.Seats.Count == 4);

        seats = hall!.CreateSeats(2, 2);

        Assert.True(seats.Count == 0);
        Assert.True(hall.Seats.Count == 4);
    }

    [Fact]
    public void GetSeatByLocation()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");
        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");
        var hall = theater!.CreateHall("Wielka Sala");

        List<Seat> seats = hall!.CreateSeats(2, 2);

        var seat = hall.GetSeatByLocation(1, 2);
        Assert.NotNull(seat);
        Assert.Equal(1, seat.RowNumber);
        Assert.Equal(2, seat.SeatNumber);

        seat = hall.GetSeatByLocation(3, 2);
        Assert.Null(seat);
    }

    [Fact]
    public void AddPerformance()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");
        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");
        var hall = theater!.CreateHall("Wielka Sala");

        var play = new Play("Zemsta");
        var performance = new Performance(play, new DateTime(2025, 12, 27, 18, 0, 0), new DateTime(2025, 12, 27, 20, 0, 0));

        var result = hall!.AddPerformance(performance);

        Assert.True(result);
        Assert.True(hall.Performances.Count == 1);
        Assert.Equal(hall, performance.Hall);

        result = hall.AddPerformance(performance);
        Assert.False(result);
        Assert.True(hall.Performances.Count == 1);

        result = hall.AddPerformance(null!);
        Assert.False(result);
        Assert.True(hall.Performances.Count == 1);

        // performance2 zostało dodane do hall2 więc nie może zostać dodane do hall
        var hall2 = theater.CreateHall("Sala Druga");
        var performance2 = new Performance(play, new DateTime(2025, 1, 2, 18, 0, 0), new DateTime(2025, 1, 2, 20, 0, 0));
        hall2!.AddPerformance(performance2);

        result = hall.AddPerformance(performance2);
        Assert.False(result);
        Assert.True(hall.Performances.Count == 1);
    }

    [Fact]
    public void GetSeatsString()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");
        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");
        var hall = theater!.CreateHall("Wielka Sala");

        var result = hall!.GetSeatsString();

        Assert.Equal("Brak siedzeń", result);

        hall.CreateSeats(1, 2);

        result = hall.GetSeatsString();

        Assert.Contains("rząd:1,miejsce:1", result);
        Assert.Contains("rząd:1,miejsce:2", result);
    }

    [Fact]
    public void VisualizeSeatsString()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");
        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");
        var hall = theater!.CreateHall("Wielka Sala");

        var result = hall!.VisualizeSeatsString();

        Assert.Equal("Brak siedzeń w Sali", result);

        hall.CreateSeats(1, 2);
        hall.CreateSeat(2, 2);

        result = hall.VisualizeSeatsString();

        Assert.Contains("(1, 1) (1, 2)", result);
        Assert.Contains("(X, X) (2, 2)", result);
        Assert.Contains("\n", result);
    }

    [Fact]
    public void GetPerformancesString()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");
        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");
        var hall = theater!.CreateHall("Wielka Sala");

        var result = hall!.GetPerformancesString();

        Assert.Equal("Brak przedstawień", result);

        var play = new Play("Zemsta");
        var performance = new Performance(play, new DateTime(2025, 12, 27, 18, 0, 0), new DateTime(2025, 12, 27, 20, 0, 0));
        hall.AddPerformance(performance);

        result = hall.GetPerformancesString();

        Assert.Contains("Zemsta", result);
        Assert.Contains("Sala Wielka Sala", result);
    }
}