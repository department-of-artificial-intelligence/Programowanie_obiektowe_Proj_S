namespace Project.Model;

public class Play
{
    // Pola prywatne
    private string _title = string.Empty;

    // Właściwości
    public int PlayId { get; private set; } // PK
    public string Title
    {
        get => _title;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Tytuł nie może być null lub pusty", nameof(Title));
            _title = value;
        }
    }
    public Author? Author { get; set; } 
    public Director? Director { get; set; } 
    public List<Actor> Actors { get; } = new List<Actor>();

    // Konstruktory
    public Play() { }

    public Play(string title, Author? author = null, Director? director = null, List<Actor>? actors = null)
    {
        Title = title;
        Author = author;
        Author?.AddPlay(this);
        Director = director;
        Director?.AddPlay(this);
        if (actors is null) return;
        foreach (var actor in actors)
        {
            actor.AddPlay(this);
        }
    }

    // Metody string
    public string GetActorsString()
    {
        return Actors.ListToString("Nikt nie gra w tej sztuce", '-');
    }

    public override string ToString()
    {
        return $"{PlayId}/\"{Title}\"/" +
            $"autor: {Author?.FirstName ?? "Nieznany"} {Author?.LastName}/" +
            $"reżyser: {Director?.FirstName ?? "Nieznany"} {Director?.LastName}";
    }
}
