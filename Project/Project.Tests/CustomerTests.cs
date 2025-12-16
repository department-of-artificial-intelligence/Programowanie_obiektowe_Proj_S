using Xunit;
using Project.Model;

namespace Project.Tests;

public class CustomerTests
{
    [Fact]
    public void Constructor()
    {
        var customer = new Customer("Jan", "Kowalski");

        Assert.Equal("Jan", customer.FirstName);
        Assert.Equal("Kowalski", customer.LastName);
        Assert.True(customer.Tickets.Count == 0);
    }

    [Fact]
    public void CustomerStringException()
    {
        var customer = new Customer("Jan", "Kowalski");

        Assert.Throws<ArgumentException>(() => customer.FirstName = null!);
        Assert.Throws<ArgumentException>(() => customer.FirstName = "");
        Assert.Throws<ArgumentException>(() => customer.LastName = "     ");
    }

    [Fact]
    public void BuyTicket()
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

        var result = customer.BuyTicket(ticket!);

        Assert.True(result);
        Assert.True(customer.Tickets.Count == 1);
        Assert.Equal(TicketStatus.Sold, ticket!.Status);
        Assert.Equal(customer, ticket!.Customer);

        result = customer.BuyTicket(null!);
        Assert.False(result);
        Assert.True(customer.Tickets.Count == 1);
    }

    [Fact]
    public void ReserveTicket()
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

        var result = customer.ReserveTicket(ticket!);

        Assert.True(result);
        Assert.True(customer.Tickets.Count == 1);
        Assert.Equal(TicketStatus.Reserved, ticket!.Status);
        Assert.Equal(customer, ticket!.Customer);

        result = customer.ReserveTicket(null!);
        Assert.False(result);
        Assert.True(customer.Tickets.Count == 1);
    }

    [Fact]
    public void RefundTicket()
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
        customer.BuyTicket(ticket!);

        var result = customer.RefundTicket(ticket!);

        Assert.True(result);
        Assert.True(customer.Tickets.Count == 0);
        Assert.Equal(TicketStatus.Available, ticket!.Status);
        Assert.Null(ticket!.Customer);
    }

    [Fact]
    public void CancelReservation()
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
        customer.ReserveTicket(ticket!);

        var result = customer.CancelReservation(ticket!);

        Assert.True(result);
        Assert.True(customer.Tickets.Count == 0);
        Assert.Equal(TicketStatus.Available, ticket!.Status);
        Assert.Null(ticket!.Customer);
    }

    [Fact]
    public void BuyAllReserved()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");
        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");
        var hall = theater!.CreateHall("Wielka Sala");
        var seat1 = hall!.CreateSeat(1, 1);
        var seat2 = hall!.CreateSeat(1, 2);

        var play = new Play("Zemsta");
        var performance = new Performance(play, new DateTime(2025, 12, 27, 18, 0, 0), new DateTime(2025, 12, 27, 20, 0, 0));
        performance.AddHall(hall);

        var ticket1 = performance.CreateTicket(100m, seat1!);
        var ticket2 = performance.CreateTicket(120m, seat2!);
        var customer = new Customer("Jan", "Kowalski");
        
        customer.ReserveTicket(ticket1!);
        customer.ReserveTicket(ticket2!);

        var result = customer.BuyAllReserved();

        Assert.True(result);
        Assert.True(customer.Tickets.Count == 2);
        Assert.Equal(TicketStatus.Sold, ticket1!.Status);
        Assert.Equal(TicketStatus.Sold, ticket2!.Status);

        result = customer.BuyAllReserved();
        Assert.False(result);
    }

    [Fact]
    public void CancelAllReserved()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");
        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");
        var hall = theater!.CreateHall("Wielka Sala");
        var seat1 = hall!.CreateSeat(1, 1);
        var seat2 = hall!.CreateSeat(1, 2);

        var play = new Play("Zemsta");
        var performance = new Performance(play, new DateTime(2025, 12, 27, 18, 0, 0), new DateTime(2025, 12, 27, 20, 0, 0));
        performance.AddHall(hall);

        var ticket1 = performance.CreateTicket(100m, seat1!);
        var ticket2 = performance.CreateTicket(120m, seat2!);
        var customer = new Customer("Jan", "Kowalski");
        
        customer.ReserveTicket(ticket1!);
        customer.ReserveTicket(ticket2!);

        var result = customer.CancelAllReserved();

        Assert.True(result);
        Assert.True(customer.Tickets.Count == 0);
        Assert.Equal(TicketStatus.Available, ticket1!.Status);
        Assert.Equal(TicketStatus.Available, ticket2!.Status);

        result = customer.CancelAllReserved();
        Assert.False(result);
    }

    [Fact]
    public void RefundAllBought()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");
        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");
        var hall = theater!.CreateHall("Wielka Sala");
        var seat1 = hall!.CreateSeat(1, 1);
        var seat2 = hall!.CreateSeat(1, 2);

        var play = new Play("Zemsta");
        var performance = new Performance(play, new DateTime(2025, 12, 27, 18, 0, 0), new DateTime(2025, 12, 27, 20, 0, 0));
        performance.AddHall(hall);

        var ticket1 = performance.CreateTicket(100m, seat1!);
        var ticket2 = performance.CreateTicket(120m, seat2!);
        var customer = new Customer("Jan", "Kowalski");
        
        customer.BuyTicket(ticket1!);
        customer.BuyTicket(ticket2!);

        var result = customer.RefundAllBought();

        Assert.True(result);
        Assert.True(customer.Tickets.Count == 0);
        Assert.Equal(TicketStatus.Available, ticket1!.Status);
        Assert.Equal(TicketStatus.Available, ticket2!.Status);

        result = customer.RefundAllBought();
        Assert.False(result);
    }

    [Fact]
    public void GetTicketsString()
    {
        var customer = new Customer("Jan", "Kowalski");

        var result = customer.GetTicketsString();

        Assert.Equal("Brak biletów", result);

        var theaterNetwork = new TheaterNetwork("Polskie Teatry");
        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");
        var hall = theater!.CreateHall("Wielka Sala");
        var seat = hall!.CreateSeat(1, 1);

        var play = new Play("Zemsta");
        var performance = new Performance(play, new DateTime(2025, 12, 27, 18, 0, 0), new DateTime(2025, 12, 27, 20, 0, 0));
        performance.AddHall(hall);

        var ticket = performance.CreateTicket(100m, seat!);
        customer.BuyTicket(ticket!);

        result = customer.GetTicketsString();

        Assert.Contains("Zemsta", result);
    }

    [Fact]
    public void GetTicketsWithStatusString()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");
        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");
        var hall = theater!.CreateHall("Wielka Sala");
        var seat1 = hall!.CreateSeat(1, 1);
        var seat2 = hall!.CreateSeat(1, 2);

        var play = new Play("Zemsta");
        var performance = new Performance(play, new DateTime(2025, 12, 27, 18, 0, 0), new DateTime(2025, 12, 27, 20, 0, 0));
        performance.AddHall(hall);

        var ticket1 = performance.CreateTicket(100m, seat1!);
        var ticket2 = performance.CreateTicket(120m, seat2!);
        var customer = new Customer("Jan", "Kowalski");
        
        customer.BuyTicket(ticket1!);
        customer.ReserveTicket(ticket2!);

        var result = customer.GetTicketsWithStatusString(TicketStatus.Sold);
        Assert.Contains("Zemsta", result);

        result = customer.GetTicketsWithStatusString(TicketStatus.Reserved);
        Assert.Contains("Zemsta", result);

        result = customer.GetTicketsWithStatusString(TicketStatus.Available);
        Assert.Equal("Brak biletów", result);
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
        customer.BuyTicket(ticket!);

        var result = customer.ToString();

        Assert.Contains("Jan", result);
        Assert.Contains("Kowalski", result);
        Assert.Contains("l.biletów:1", result);
    }
}