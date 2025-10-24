namespace Project.Model;

public class Movie : BaseEntity<int>
{
    public required string Title { get; set; }
    public string? Description { get; set; }

    public string? TagLine { get; set; }

    public Genre Genre { get; set; }

    public Author Author { get; set; }

    public Movie(string title, string desc, string tagLine, Genre gen, Author author)
    {
        Title = title;
        Description = desc;
        TagLine = tagLine;
        Genre = gen;
        Author = author;
    }
}
