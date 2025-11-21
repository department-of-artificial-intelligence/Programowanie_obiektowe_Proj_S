using Project.Model;
using Xunit.Sdk;

namespace Project.Tests;
public class PersonModelTests
{
    private List<Author> Authors { get; set; }

    public PersonModelTests()
    {
        Authors = new List<Author>();
        Authors.Add(new Author(0, "Alex", "7Z", new DateTime(2000, 5, 12)));
        Authors.Add(new Author(1, "Lisa", "7Z", new DateTime(2008, 1, 12)));
        Authors.Add(new Author(2, "Alice", "Stayson", new DateTime(2001, 4, 12)));
        Authors.Add(new Author(3, "Mark", "Marker", new DateTime(1987, 9, 12)));
        Authors.Add(new Author(4, "Adam", "Nine", new DateTime(1999, 12, 20)));
    }


    [Fact]
    public void TestAuthorsInitialization()
    {
        Assert.NotEmpty(Authors);
        Assert.NotNull(Authors[0]);
        Assert.Equal(5, Authors.Count());
    }

    [Fact]
    public void TestFindAuthorsWithMore18YearsOld()
    {
        List<Author> filteredAuthors = Authors.Where(a => DateTime.Today.Year - a.BirthDay.Year >= 18).ToList();

        Assert.NotEmpty(filteredAuthors);
        Assert.Equal(4, filteredAuthors.Count);
    }
}