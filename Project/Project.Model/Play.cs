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
    private Play() { }

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
            AddActor(actor);
        }
    }

    // Metody dodawania i usuwania elementów listy Actor
    public bool AddActor(Actor actor)
    {
        if (actor is null || Actors.Contains(actor)) return false;
        if (!actor.Plays.Contains(this))
        {
            actor.Plays.Add(this);
        }
        Actors.Add(actor);
        return true;
    }
    public bool RemoveActor(Actor actor)
    {
        if (!Actors.Contains(actor)) return false;
        actor.Plays.Remove(this);
        return Actors.Remove(actor);
    }
    public void RemoveAllActors()
    {
        foreach (var actor in Actors.ToList())
        {
            actor.Plays.Remove(this);
        }
        Actors.Clear();
    }

    // Metody string
    public string GetActorsString()
    {
        return Actors.ListToString("Nikt nie gra w tej sztuce", '-');
    }

    public override string ToString()
    {
        return $"\"{Title}\"/" +
            $"autor: {Author?.FirstName} {Author?.LastName ?? "Nieznany"}/" +
            $"reżyser: {Director?.FirstName} {Director?.LastName ?? "Nieznany"}";
    }
}
