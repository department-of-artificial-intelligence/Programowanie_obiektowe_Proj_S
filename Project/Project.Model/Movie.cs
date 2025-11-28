using System.Diagnostics.CodeAnalysis;

namespace Project.Model;

public class Movie : BaseEntity<int>
{
    public required string Title { get; set; }
    public string? Description { get; set; }

    public string? TagLine { get; set; }

    public Genre Genre { get; set; }

    public Author Author { get; set; }

    [SetsRequiredMembers]
    public Movie(int id, string title, string desc, string tagLine, Genre gen, Author author) : base(id)
    {
        Title = title;
        Description = desc;
        TagLine = tagLine;
        Genre = gen;
        Author = author;
    }

    [SetsRequiredMembers]
    public Movie() : base(0) { }

    public override string ToString()
    {
        return $"Move:{Title}/{Genre.ToString()}/{TagLine}";
    }
}
