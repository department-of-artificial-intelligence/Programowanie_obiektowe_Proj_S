using Project.DTO;

namespace Project.UI.ViewModel;

/// <summary>
/// Wrapper class for displaying AuthorDto in UI
/// </summary>
public class AuthorDisplayItem
{
    public AuthorDto Author { get; }
    public string DisplayName => Author.LastName; // Just last name

    public AuthorDisplayItem(AuthorDto author)
    {
        Author = author;
    }
}
