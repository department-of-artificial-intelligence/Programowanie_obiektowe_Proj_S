using Xunit;
using Project.Model;

namespace Project.Tests;

public class DirectorTests
{
    [Fact]
    public void Constructor()
    {
        var director = new Director("Jan", "Kowalski", 10, 15000m);

        Assert.Equal("Jan", director.FirstName);
        Assert.Equal("Kowalski", director.LastName);
        Assert.Equal(10, director.YearsOfExperience);
        Assert.Equal(15000m, director.Salary);
        Assert.True(director.Plays.Count == 0);
    }

    [Fact]
    public void DirectorStringException()
    {
        var director = new Director("Jan", "Kowalski", 10, 15000m);

        Assert.Throws<ArgumentException>(() => director.FirstName = null!);
        Assert.Throws<ArgumentException>(() => director.FirstName = "");
        Assert.Throws<ArgumentException>(() => director.LastName = "     ");
    }

    [Fact]
    public void SalaryException()
    {
        var director = new Director("Jan", "Kowalski", 10, 15000m);

        Assert.Throws<ArgumentOutOfRangeException>(() => director.Salary = -1);
    }

    [Fact]
    public void AddPlay()
    {
        var director = new Director("Jan", "Kowalski", 10, 15000m);
        var play = new Play("Zemsta");

        var result = director.AddPlay(play);

        Assert.True(result);
        Assert.True(director.Plays.Count == 1);
        Assert.Equal(director, play.Director);

        result = director.AddPlay(null!);

        Assert.False(result);
        Assert.True(director.Plays.Count == 1);

        result = director.AddPlay(play);

        Assert.False(result);
        Assert.True(director.Plays.Count == 1);
    }

    [Fact]
    public void RemovePlay()
    {
        var director = new Director("Jan", "Kowalski", 10, 15000m);
        var play = new Play("Zemsta");
        director.AddPlay(play);

        var result = director.RemovePlay(play);

        Assert.True(result);
        Assert.True(director.Plays.Count == 0);
        Assert.True(play.Director is null);

        result = director.RemovePlay(play);

        Assert.False(result);
    }

    [Fact]
    public void RemoveAllPlays()
    {
        var director = new Director("Jan", "Kowalski", 10, 15000m);
        var play1 = new Play("Zemsta");
        var play2 = new Play("Makbet");
        director.AddPlay(play1);
        director.AddPlay(play2);

        var result = director.RemoveAllPlays();

        Assert.True(result);
        Assert.True(director.Plays.Count == 0);
        Assert.True(play1.Director is null);
        Assert.True(play2.Director is null);

        result = director.RemoveAllPlays();

        Assert.False(result);
    }

    [Fact]
    public void GetPlaysString()
    {
        var director = new Director("Jan", "Kowalski", 10, 15000m);

        var result = director.GetPlaysString();

        Assert.Equal("Nie reżyserował żadnych sztuk", result);

        director.AddPlay(new Play("Zemsta"));

        result = director.GetPlaysString();

        Assert.Contains("Zemsta", result);
    }

    [Fact]
    public void ToStringTest()
    {
        var director = new Director("Jan", "Kowalski", 10, 15000m);
        var play = new Play("Zemsta");
        director.AddPlay(play);

        var result = director.ToString();

        Assert.Contains("Jan", result);
        Assert.Contains("Kowalski", result);
        Assert.Contains("10", result);
        Assert.Contains("15000PLN", result);
        Assert.Contains("l.sztuk:1", result);
    }

}