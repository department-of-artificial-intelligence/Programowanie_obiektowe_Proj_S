using Xunit;
using Project.Model;

namespace Project.Tests;

public class TheaterNetworkTests
{
    [Fact]
    public void Constructor()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");

        Assert.Equal("Polskie Teatry", theaterNetwork.NetworkName);
        Assert.True(theaterNetwork.Theaters.Count == 0);
    }

    [Fact]
    public void NetworkNameStringException()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");

        Assert.Throws<ArgumentException>(() => theaterNetwork.NetworkName = null!);
        Assert.Throws<ArgumentException>(() => theaterNetwork.NetworkName = "");
        Assert.Throws<ArgumentException>(() => theaterNetwork.NetworkName = "    ");
    }

    [Fact]
    public void CreateTheater()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");

        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");

        Assert.NotNull(theater);
        Assert.True(theaterNetwork.Theaters.Count == 1);

        theater = theaterNetwork.CreateTheater(null!, "Polska", "Częstochowa", "Kościuszki 1225");
        Assert.Null(theater);
        theater = theaterNetwork.CreateTheater("Teatr Wielki", "", "Częstochowa", "Kościuszki 1225");
        Assert.Null(theater);
        theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "    ", "Kościuszki 1225");
        Assert.Null(theater);
        theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "    ");
        Assert.Null(theater);

        Assert.True(theaterNetwork.Theaters.Count == 1);
    }

    [Fact]
    public void GetTheatersString()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");

        var result = theaterNetwork.GetTheatersString();

        Assert.Equal("Brak teatrów", result);

        theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");
        theaterNetwork.CreateTheater("Teatr Duży", "Polska", "Katowice", "Mickiewicza 137");

        result = theaterNetwork.GetTheatersString();

        Assert.Contains("Teatr Wielki", result);
        Assert.Contains("Polska", result);
        Assert.Contains("Częstochowa", result);
        Assert.Contains("Kościuszki 1225", result);
        Assert.Contains("Teatr Duży", result);
    }

    [Fact]
    public void ToStringTest()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");

        var result = theaterNetwork.ToString();

        Assert.Contains("Polskie Teatry", result);
        Assert.Contains("Brak teatrów", result);

        theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Częstochowa", "Kościuszki 1225");
        theaterNetwork.CreateTheater("Teatr Duży", "Polska", "Katowice", "Mickiewicza 137");

        result = theaterNetwork.ToString();

        Assert.Contains("Polskie Teatry", result);
        Assert.Contains("Teatr Wielki", result);
        Assert.Contains("Polska", result);
        Assert.Contains("Częstochowa", result);
        Assert.Contains("Kościuszki 1225", result);
        Assert.Contains("Teatr Duży", result);
    }
}