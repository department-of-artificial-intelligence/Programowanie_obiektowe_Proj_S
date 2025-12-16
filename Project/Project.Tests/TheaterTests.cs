using Xunit;
using Project.Model;

namespace Project.Tests;

public class TheaterTests
{
    [Fact]
    public void Composition()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");

        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");
        Assert.NotNull(theater);
        Assert.Equal("Teatr Wielki", theater.TheaterName);
        Assert.NotNull(theater.Address);
        Assert.Equal("Polska", theater.Address.Country);
        Assert.Equal("Częstochowa", theater.Address.City);
        Assert.Equal("Kościuszki 1225", theater.Address.Street);
        Assert.True(theater.Halls.Count == 0);

        theater = theaterNetwork.CreateTheater(null!, "Polska", "Częstochowa", "Kościuszki 1225");
        Assert.Null(theater);
        theater = theaterNetwork.CreateTheater("Teatr Wielki", "", "Częstochowa", "Kościuszki 1225");
        Assert.Null(theater);
        theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "    ", "Kościuszki 1225");
        Assert.Null(theater);
        theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "    ");
        Assert.Null(theater);
    }

    [Fact]
    public void TheaterStringException()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");

        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");

        Assert.Throws<ArgumentException>(() => theater!.TheaterName = null!);
        Assert.Throws<ArgumentException>(() => theater!.TheaterName = "");
        Assert.Throws<ArgumentException>(() => theater!.TheaterName = "    ");
    }

    [Fact]
    public void CreateHall()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");

        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");

        var hall = theater!.CreateHall("Wielka Sala");

        Assert.NotNull(hall);
        Assert.True(theater.Halls.Count == 1);

        hall = theater!.CreateHall(null!);
        Assert.Null(hall);
        hall = theater!.CreateHall("");
        Assert.Null(hall);
        hall = theater!.CreateHall("    ");
        Assert.Null(hall);

        Assert.True(theater.Halls.Count == 1);
    }

    [Fact]
    public void GetHallsString()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");

        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");

        var result = theater!.GetHallsString();

        Assert.Equal("Brak sal teatralnych", result);

        theater.CreateHall("Wielka Sala");
        theater.CreateHall("Sala Duża");

        result = theater!.GetHallsString();

        Assert.Contains("Wielka Sala", result);
        Assert.Contains("Sala Duża", result);
    }

    [Fact]
    public void ToStringTest()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");

        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");

        var result = theater!.ToString();

        Assert.Contains("Teatr Wielki", result);
        Assert.Contains("Polska, Częstochowa, Kościuszki 1225", result);
    }
}