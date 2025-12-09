using System.Numerics;

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
    public Author? Author { get; set; } // Navigation property
    public Director? Director { get; set; } // Navigation property
    public List<Actor> Actors { get; } = new List<Actor>(); // Navigation property

    // Konstruktory
    private Play() { }

    public Play(string title, Author? author = null, Director? director = null, List<Actor>? actors = null)
    {
        Title = title;
        Author = author;
        Author?.AddPlay(this);
        Director = director;
        Director?.AddPlay(this);
        Actors = actors ?? new List<Actor>();
        foreach (var actor in Actors)
        {
            if (!actor.Plays.Contains(this))
            {
                actor.Plays.Add(this);
            }
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
        if (Actors.Count == 0 || actor is null) return false;
        if (actor.Plays.Contains(this))
        {
            actor.Plays.Remove(this);
        }
        return Actors.Remove(actor);
    }
    public bool RemoveActor(int actorId)
    {
        if (Actors.Count == 0) return false;
        var actor = Actors.FirstOrDefault(p => p.ActorId == actorId);
        if (actor is null) return false;
        if (actor.Plays.Contains(this))
        {
            actor.Plays.Remove(this);
        }
        return Actors.Remove(actor);
    }
    public void RemoveAllActors()
    {
        foreach (Actor actor in Actors)
        {
            if (actor.Plays.Contains(this))
            {
                actor.Plays.Remove(this);
            }
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
