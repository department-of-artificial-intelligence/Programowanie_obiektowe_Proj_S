using Xunit;
using Project.Model;

namespace Project.Tests;

public class PerformanceTests
{
    [Fact]
    public void Constructor()
    {
        var play = new Play("Zemsta");

        var start = new DateTime(2025, 12, 27, 18, 0, 0);
        var end = new DateTime(2025, 12, 27, 20, 0, 0);

        var performance = new Performance(play, start, end);

        Assert.NotNull(performance);
        Assert.Equal(play, performance.Play);
        Assert.Equal(start, performance.StartTime);
        Assert.Equal(end, performance.EndTime);
        Assert.Equal(PerformanceStatus.Scheduled, performance.Status);
        Assert.True(performance.Tickets.Count == 0);
        Assert.Null(performance.Hall);

        Assert.Throws<ArgumentNullException>(() => new Performance(null!, start, end));
        Assert.Throws<ArgumentException>(() => new Performance(play, end, start));
    }

    [Fact]
    public void CreateTicket()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");
        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");
        var hall = theater!.CreateHall("Wielka Sala");
        var seat = hall!.CreateSeat(1, 1);

        var play = new Play("Zemsta");
        var performance = new Performance(play, new DateTime(2025, 12, 27, 18, 0, 0), new DateTime(2025, 12, 27, 20, 0, 0));
        performance.AddHall(hall);

        var ticket = performance.CreateTicket(100m, seat!);

        Assert.NotNull(ticket);
        Assert.True(performance.Tickets.Count == 1);
        Assert.Equal(100m, ticket!.Price);
        Assert.Equal(performance, ticket.Performance);
        Assert.Equal(seat, ticket.Seat);
        Assert.Equal(TicketStatus.Available, ticket.Status);

        ticket = performance.CreateTicket(-1m, seat!);
        Assert.Null(ticket);
        ticket = performance.CreateTicket(100m, null!);
        Assert.Null(ticket);

        // nie można stworzyć biletu dla siedzenia w innej sali niż jest przedstawienie
        var hall2 = theater.CreateHall("Sala Druga");
        var seatInHall2 = hall2!.CreateSeat(1, 1);
        ticket = performance.CreateTicket(100m, seatInHall2!);
        Assert.Null(ticket);

        var performanceWithNoHall = new Performance(play, new DateTime(2025, 12, 28, 18, 0, 0), new DateTime(2025, 12, 28, 20, 0, 0));
        ticket = performanceWithNoHall.CreateTicket(100m, seat!);
        Assert.Null(ticket);
    }

    [Fact]
    public void CreateTicketForEverySeat()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");
        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");
        var hall = theater!.CreateHall("Wielka Sala");
        hall!.CreateSeats(1, 2);

        var play = new Play("Zemsta");
        var performance = new Performance(play, new DateTime(2025, 12, 27, 18, 0, 0), new DateTime(2025, 12, 27, 20, 0, 0));
        performance.AddHall(hall);

        var tickets = performance.CreateTicketForEverySeat(100m);

        Assert.True(tickets.Count == 2);
        Assert.True(performance.Tickets.Count == 2);
        Assert.NotNull(tickets[0].Seat);
        Assert.Equal(100m, tickets[0].Price);
        Assert.Equal(performance, tickets[0].Performance);
        Assert.Equal(TicketStatus.Available, tickets[0].Status);
        Assert.NotNull(tickets[1].Seat);
        Assert.Equal(100m, tickets[1].Price);
        Assert.Equal(performance, tickets[1].Performance);
        Assert.Equal(TicketStatus.Available, tickets[1].Status);

        var performanceWithNoHall = new Performance(play, new DateTime(2025, 12, 28, 18, 0, 0), new DateTime(2025, 12, 28, 20, 0, 0));
        tickets = performanceWithNoHall.CreateTicketForEverySeat(100m);
        Assert.True(tickets.Count == 0);
    }

    [Fact]
    public void AddHall()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");
        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");
        var hall = theater!.CreateHall("Wielka Sala");

        var play = new Play("Zemsta");
        var performance = new Performance(play, new DateTime(2025, 12, 27, 18, 0, 0), new DateTime(2025, 12, 27, 20, 0, 0));

        var result = performance.AddHall(hall!);

        Assert.True(result);
        Assert.Equal(hall, performance.Hall);
        Assert.Contains(performance, hall!.Performances);

        result = performance.AddHall(hall);
        Assert.False(result);

        result = performance.AddHall(null!);
        Assert.False(result);
    }

    [Fact]
    public void OrderTickets()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");
        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");
        var hall = theater!.CreateHall("Wielka Sala");

        var play = new Play("Zemsta");
        var performance = new Performance(play, new DateTime(2025, 12, 27, 18, 0, 0), new DateTime(2025, 12, 27, 20, 0, 0));
        performance.AddHall(hall!);

        var seat22 = hall!.CreateSeat(2, 2);
        var seat11 = hall!.CreateSeat(1, 1);
        var seat12 = hall!.CreateSeat(1, 2);
        var seat21 = hall!.CreateSeat(2, 1);

        performance.CreateTicket(100m, seat22!);
        performance.CreateTicket(100m, seat11!);
        performance.CreateTicket(100m, seat12!);
        performance.CreateTicket(100m, seat21!);

        var ordered = performance.OrderTickets();

        Assert.True(ordered.Count == 4);
        Assert.Equal(1, ordered[0].Seat.RowNumber);
        Assert.Equal(1, ordered[0].Seat.SeatNumber);
        Assert.Equal(1, ordered[1].Seat.RowNumber);
        Assert.Equal(2, ordered[1].Seat.SeatNumber);
        Assert.Equal(2, ordered[2].Seat.RowNumber);
        Assert.Equal(1, ordered[2].Seat.SeatNumber);
        Assert.Equal(2, ordered[3].Seat.RowNumber);
        Assert.Equal(2, ordered[3].Seat.SeatNumber);
    }

    [Fact]
    public void GetTicketBySeatLocation()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");
        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");
        var hall = theater!.CreateHall("Wielka Sala");

        var play = new Play("Zemsta");
        var performance = new Performance(play, new DateTime(2025, 12, 27, 18, 0, 0), new DateTime(2025, 12, 27, 20, 0, 0));
        performance.AddHall(hall!);

        var ticket11 = performance.CreateTicket(100m, hall!.CreateSeat(1, 1)!);
        var ticket12 = performance.CreateTicket(120m, hall!.CreateSeat(1, 2)!);
        hall.CreateSeat(2, 1);

        var foundTicket = performance.GetTicketBySeatLocation(1, 1);
        Assert.NotNull(foundTicket);
        Assert.Equal(ticket11, foundTicket);

        foundTicket = performance.GetTicketBySeatLocation(1, 2);
        Assert.NotNull(foundTicket);
        Assert.Equal(ticket12, foundTicket);

        foundTicket = performance.GetTicketBySeatLocation(2, 1);
        Assert.Null(foundTicket);
    }

    [Fact]
    public void GetTicketsString()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");
        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");
        var hall = theater!.CreateHall("Wielka Sala");

        var play = new Play("Zemsta");
        var performance = new Performance(play, new DateTime(2025, 12, 27, 18, 0, 0), new DateTime(2025, 12, 27, 20, 0, 0));
        performance.AddHall(hall!);

        var result = performance.GetTicketsString();
        Assert.Equal("Brak biletów", result);

        performance.CreateTicket(120m, hall!.CreateSeat(1, 1)!);
        performance.CreateTicket(80m, hall!.CreateSeat(1, 2)!);

        result = performance.GetTicketsString();

        Assert.Contains("rząd:1,miejsce:1", result);
        Assert.Contains("rząd:1,miejsce:2", result);
        Assert.Contains("120PLN", result);
        Assert.Contains("80PLN", result);
    }

    [Fact]
    public void VisualizeTicketsString()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");
        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");
        var hall = theater!.CreateHall("Wielka Sala");

        var play = new Play("Zemsta");
        var performance = new Performance(play, new DateTime(2025, 12, 27, 18, 0, 0), new DateTime(2025, 12, 27, 20, 0, 0));

        var result = performance.VisualizeTicketsString();
        Assert.Equal("Brak przypisanej Sali", result);

        performance.AddHall(hall!);

        result = performance.VisualizeTicketsString();
        Assert.Equal("Brak siedzeń w Sali", result);

        hall!.CreateSeat(1, 1);
        hall!.CreateSeat(1, 2);
        hall!.CreateSeat(2, 1);

        performance.CreateTicket(100m, hall!.CreateSeat(1, 1)!);
        performance.CreateTicket(120m, hall!.CreateSeat(1, 2)!);

        var visualization = performance.VisualizeTicketsString();

        Assert.Contains("Legenda: D - Dostępny, Z - Zarezerwowany, S - Sprzedany, B - Brak biletu, X - Brak Siedzenia", visualization);
        Assert.Contains("Rząd 01: 1B 2B", visualization);
        Assert.Contains("Rząd 02: 1B 2X", visualization);
    }

    [Fact]
    public void ToStringTest()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");
        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");
        var hall = theater!.CreateHall("Wielka Sala");

        var play = new Play("Zemsta");
        var performance = new Performance(play, new DateTime(2025, 12, 27, 18, 0, 0), new DateTime(2025, 12, 27, 20, 0, 0));

        var result = performance.ToString();

        Assert.Contains("Zemsta", result);
        Assert.Contains("Scheduled", result);
        Assert.Contains("nieznana", result);
        Assert.Contains("27.12.2025 18:00:00", result);
        Assert.Contains("27.12.2025 20:00:00", result);

        performance.AddHall(hall!);

        result = performance.ToString();

        Assert.Contains("Wielka Sala", result);
    }
}