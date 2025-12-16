using Xunit;
using Project.Model;

namespace Project.Tests;

public class AuthorTests
{
    [Fact]
    public void Constructor()
    {
        var author = new Author("Jan", "Kowalski");

        Assert.Equal("Jan", author.FirstName);
        Assert.Equal("Kowalski", author.LastName);
        Assert.True(author.Plays.Count == 0);
    }

    [Fact]
    public void AuthorStringException()
    {
        var author = new Author("Jan", "Kowalski");

        Assert.Throws<ArgumentException>(() => author.FirstName = null!);
        Assert.Throws<ArgumentException>(() => author.FirstName = "");
        Assert.Throws<ArgumentException>(() => author.LastName = "     ");
    }

    [Fact]
    public void AddPlay()
    {
        var author = new Author("Jan", "Kowalski");
        var play = new Play("Zemsta");

        var result = author.AddPlay(play);

        Assert.True(result);
        Assert.True(author.Plays.Count == 1);
        Assert.Equal(author, play.Author);

        result = author.AddPlay(null!);

        Assert.False(result);
        Assert.True(author.Plays.Count == 1);

        result = author.AddPlay(play);

        Assert.False(result);
        Assert.True(author.Plays.Count == 1);
    }

    [Fact]
    public void RemovePlay()
    {
        var author = new Author("Jan", "Kowalski");
        var play = new Play("Zemsta");
        author.AddPlay(play);

        var result = author.RemovePlay(play);

        Assert.True(result);
        Assert.True(author.Plays.Count == 0);
        Assert.True(play.Author is null);

        result = author.RemovePlay(play);

        Assert.False(result);
    }

    [Fact]
    public void RemoveAllPlays()
    {
        var author = new Author("Jan", "Kowalski");
        var play1 = new Play("Zemsta");
        var play2 = new Play("Makbet");
        author.AddPlay(play1);
        author.AddPlay(play2);

        var result = author.RemoveAllPlays();

        Assert.True(result);
        Assert.True(author.Plays.Count == 0);
        Assert.True(play1.Author is null);
        Assert.True(play2.Author is null);

        result = author.RemoveAllPlays();

        Assert.False(result);
    }

    [Fact]
    public void GetPlaysString()
    {
        var author = new Author("Jan", "Kowalski");

        var result = author.GetPlaysString();

        Assert.Equal("Nie napisał żadnych sztuk", result);

        author.AddPlay(new Play("Zemsta"));

        result = author.GetPlaysString();

        Assert.Contains("Zemsta", result);
    }

    [Fact]
    public void ToStringTest()
    {
        var author = new Author("Jan", "Kowalski");
        var play = new Play("Zemsta");
        author.AddPlay(play);

        var result = author.ToString();

        Assert.Contains("Jan", result);
        Assert.Contains("Kowalski", result);
        Assert.Contains("l.sztuk:1", result);
    }
}