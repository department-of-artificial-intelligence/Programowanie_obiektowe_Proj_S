using Xunit;
using Project.Model;

namespace Project.Tests;

public class AddressTests
{
    [Fact]
    public void Constructor()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");
        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Czêstochowa", "Koœciuszki 1225");

        Assert.Equal("Polska", theater!.Address.Country);
        Assert.Equal("Czêstochowa", theater!.Address.City);
        Assert.Equal("Koœciuszki 1225", theater!.Address.Street);
    }

    [Fact]
    public void ToStringTest()
    {
        var theaterNetwork = new TheaterNetwork("Polskie Teatry");
        var theater = theaterNetwork.CreateTheater("Teatr Wielki", "Polska", "Czêstochowa", "Koœciuszki 1225");

        var result = theater!.Address.ToString();

        Assert.Contains("Polska, Czêstochowa, Koœciuszki 1225", result);
    }
}