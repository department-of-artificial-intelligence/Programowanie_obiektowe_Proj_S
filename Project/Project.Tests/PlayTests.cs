using Project.Model;
using System.IO;
using Xunit;

namespace Project.Tests;

public class PlayTests
{
    [Fact]
    public void Constructor()
    {
        var author = new Author("Jan", "Kowalski");
        var director = new Director("Anna", "Nowak", 5, 10000m);
        
        var play = new Play("Zemsta", author, director);

        Assert.Equal("Zemsta", play.Title);
        Assert.Equal(author, play.Author);
        Assert.True(author.Plays.Count == 1);
        Assert.Equal(director, play.Director);
        Assert.True(director.Plays.Count == 1);
        Assert.True(play.Actors.Count == 0);
    }

    [Fact]
    public void TilteStringException()
    {
        var play = new Play("Zemsta");

        Assert.Throws<ArgumentException>(() => play.Title = null!);
        Assert.Throws<ArgumentException>(() => play.Title = "");
        Assert.Throws<ArgumentException>(() => play.Title = "    ");
    }

    [Fact]
    public void GetActorsString()
    {
        var play = new Play("Zemsta");
        
        var result = play.GetActorsString();

        Assert.Equal("Nikt nie gra w tej sztuce", result);

        var actor = new Actor("Jan", "Kowalski", 12000m);
        actor.AddPlay(play);

        result = play.GetActorsString();

        Assert.Contains("Jan", result);
        Assert.Contains("Kowalski", result);
        Assert.Contains("12000", result);
    }

    [Fact]
    public void ToStringTest()
    {
        var author = new Author("Jan", "Kowalski");
        var director = new Director("Anna", "Nowak", 5, 10000m);

        var play = new Play("Zemsta", author, director);

        var result = play.ToString();

        Assert.Contains("Zemsta", result);
        Assert.Contains("Jan", result);
        Assert.Contains("Kowalski", result);
        Assert.Contains("Anna", result);
        Assert.Contains("Nowak", result);
    }
}