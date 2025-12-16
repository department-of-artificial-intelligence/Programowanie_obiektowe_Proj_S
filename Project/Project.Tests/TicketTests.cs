using Xunit;
using Project.Model;

namespace Project.Tests;

public class TicketTests
{
    [Fact]
    public void Composition()
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
        Assert.Equal(100m, ticket!.Price);
        Assert.Equal(performance, ticket.Performance);
        Assert.Equal(seat, ticket.Seat);
        Assert.Equal(TicketStatus.Available, ticket.Status);
        Assert.Null(ticket.Customer);
    }

    [Fact]
    public void CanBeReserved()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");
        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");
        var hall = theater!.CreateHall("Wielka Sala");
        var seat = hall!.CreateSeat(1, 1);

        var play = new Play("Zemsta");
        var performance = new Performance(play, new DateTime(2025, 12, 27, 18, 0, 0), new DateTime(2025, 12, 27, 20, 0, 0));
        performance.AddHall(hall);

        var ticket = performance.CreateTicket(100m, seat!);
        var customer = new Customer("Jan", "Kowalski");
        var customer2 = new Customer("Anna", "Nowak");

        Assert.True(ticket!.CanBeReserved(customer));
        Assert.False(ticket.CanBeReserved(null!));

        ticket.Status = TicketStatus.Reserved;
        Assert.False(ticket.CanBeReserved(customer));
        Assert.False(ticket!.CanBeReserved(customer2));

        ticket.Status = TicketStatus.Sold;
        Assert.False(ticket.CanBeReserved(customer));
    }

    [Fact]
    public void CanBeCanceled()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");
        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");
        var hall = theater!.CreateHall("Wielka Sala");
        var seat = hall!.CreateSeat(1, 1);

        var play = new Play("Zemsta");
        var performance = new Performance(play, new DateTime(2025, 12, 27, 18, 0, 0), new DateTime(2025, 12, 27, 20, 0, 0));
        performance.AddHall(hall);

        var ticket = performance.CreateTicket(100m, seat!);
        var customer = new Customer("Jan", "Kowalski");

        Assert.False(ticket!.CanBeCanceled(customer));
        Assert.False(ticket.CanBeCanceled(null!));

        ticket.Status = TicketStatus.Reserved;
        ticket.Customer = customer;

        Assert.True(ticket.CanBeCanceled(customer));
    }

    [Fact]
    public void CanBeBought()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");
        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");
        var hall = theater!.CreateHall("Wielka Sala");
        var seat = hall!.CreateSeat(1, 1);

        var play = new Play("Zemsta");
        var performance = new Performance(play, new DateTime(2025, 12, 27, 18, 0, 0), new DateTime(2025, 12, 27, 20, 0, 0));
        performance.AddHall(hall);

        var ticket = performance.CreateTicket(100m, seat!);
        var customer = new Customer("Jan", "Kowalski");
        var customer2 = new Customer("Anna", "Nowak");

        Assert.True(ticket!.CanBeBought(customer));
        Assert.False(ticket.CanBeBought(null!));

        ticket.Status = TicketStatus.Reserved;
        ticket.Customer = customer;

        Assert.True(ticket.CanBeBought(customer));
        Assert.False(ticket.CanBeBought(customer2));
        Assert.False(ticket.CanBeBought(null!));

        ticket.Status = TicketStatus.Sold;
        Assert.False(ticket.CanBeBought(customer));
    }

    [Fact]
    public void CanBeRefunded()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");
        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");
        var hall = theater!.CreateHall("Wielka Sala");
        var seat = hall!.CreateSeat(1, 1);

        var play = new Play("Zemsta");
        var performance = new Performance(play, new DateTime(2025, 12, 27, 18, 0, 0), new DateTime(2025, 12, 27, 20, 0, 0));
        performance.AddHall(hall);

        var ticket = performance.CreateTicket(100m, seat!);
        var customer = new Customer("Jan", "Kowalski");

        Assert.False(ticket!.CanBeRefunded(customer));
        Assert.False(ticket.CanBeRefunded(null!));

        ticket.Status = TicketStatus.Sold;
        ticket.Customer = customer;

        Assert.True(ticket.CanBeRefunded(customer));
        Assert.False(ticket.CanBeRefunded(null!));

        performance.Status = PerformanceStatus.Canceled;
        Assert.True(ticket.CanBeRefunded(customer));

        performance.Status = PerformanceStatus.Finished;
        Assert.False(ticket.CanBeRefunded(customer));
    }

    [Fact]
    public void ToStringTest()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");
        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");
        var hall = theater!.CreateHall("Wielka Sala");
        var seat = hall!.CreateSeat(1, 1);

        var play = new Play("Zemsta");
        var performance = new Performance(play, new DateTime(2025, 12, 27, 18, 0, 0), new DateTime(2025, 12, 27, 20, 0, 0));
        performance.AddHall(hall);

        var ticket = performance.CreateTicket(100m, seat!);
        var customer = new Customer("Jan", "Kowalski");

        var result = ticket!.ToString();

        Assert.Contains("Sztuka:Zemsta", result);
        Assert.Contains("100PLN", result);
        Assert.Contains("Available", result);
        Assert.Contains("rząd:1,miejsce:1", result);
        Assert.Contains("Klient:nieznany", result);

        ticket.Customer = customer;
        result = ticket.ToString();

        Assert.Contains("Klient:Jan Kowalski", result);
    }
}