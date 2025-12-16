using Xunit;
using Project.Model;

namespace Project.Tests;

public class ActorTests
{
    [Fact]
    public void Constructor()
    {
        var actor = new Actor("Jan", "Kowalski", 12000m);

        Assert.Equal("Jan", actor.FirstName);
        Assert.Equal("Kowalski", actor.LastName);
        Assert.Equal(12000m, actor.Salary);
        Assert.True(actor.Plays.Count == 0);
    }

    [Fact]
    public void ActorStringException()
    {
        var actor = new Actor("Jan", "Kowalski", 12000m);

        Assert.Throws<ArgumentException>(() => actor.FirstName = null!);
        Assert.Throws<ArgumentException>(() => actor.FirstName = "");
        Assert.Throws<ArgumentException>(() => actor.LastName = "     ");
    }

    [Fact]
    public void SalaryException()
    {
        var actor = new Actor("Jan", "Kowalski", 12000m);

        Assert.Throws<ArgumentOutOfRangeException>(() => actor.Salary = -1);
    }

    [Fact]
    public void AddPlay()
    {
        var actor = new Actor("Jan", "Kowalski", 12000m);
        var play = new Play("Zemsta");

        var result = actor.AddPlay(play); 

        Assert.True(result);
        Assert.True(actor.Plays.Count == 1);
        Assert.True(play.Actors.Count == 1);

        result = actor.AddPlay(null!);

        Assert.False(result);
        Assert.True(actor.Plays.Count == 1);

        result = actor.AddPlay(play);

        Assert.False(result);
        Assert.True(actor.Plays.Count == 1);
    }

    [Fact]
    public void RemovePlay()
    {
        var actor = new Actor("Jan", "Kowalski", 12000m);
        var play = new Play("Zemsta");
        actor.AddPlay(play);

        var result = actor.RemovePlay(play);

        Assert.True(result);
        Assert.True(actor.Plays.Count == 0);
        Assert.True(play.Actors.Count == 0);

        result = actor.RemovePlay(play);

        Assert.False(result);
    }

    [Fact]
    public void RemoveAllPlays()
    {
        var actor = new Actor("Jan", "Kowalski", 12000m);
        var play1 = new Play("Zemsta");
        var play2 = new Play("Makbet");
        actor.AddPlay(play1);
        actor.AddPlay(play2);

        var result = actor.RemoveAllPlays();

        Assert.True(result);
        Assert.True(actor.Plays.Count == 0);
        Assert.True(play1.Actors.Count == 0);
        Assert.True(play2.Actors.Count == 0);

        result = actor.RemoveAllPlays();

        Assert.False(result);
    }

    [Fact]
    public void GetPlaysString()
    {
        var actor = new Actor("Jan", "Kowalski", 12000m);

        var result = actor.GetPlaysString();

        Assert.Equal("Nie gra w żadnych sztukach", result);

        actor.AddPlay(new Play("Zemsta"));

        result = actor.GetPlaysString();

        Assert.Contains("Zemsta", result);
    }

    [Fact]
    public void ToStringTest()
    {
        var actor = new Actor("Jan", "Kowalski", 12000m);
        var play = new Play("Zemsta");
        actor.AddPlay(play);

        var result = actor.ToString();

        Assert.Contains("Jan", result);
        Assert.Contains("Kowalski", result);
        Assert.Contains("12000PLN", result);
        Assert.Contains("l.sztuk:1", result);
    }
}