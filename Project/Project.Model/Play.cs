using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model;

public class Play
{
    private static int MaxId = 0;
    private string _title;
    public int PlayId { get; }
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
    public List<Actor> Actors { get; }
    private static int GetNextId() => MaxId + 1;

    //public Play()
    //{
    //    PlayId = GetNextId();
    //    MaxId = PlayId;
    //    Title = string.Empty;
    //    Author = null;
    //    Director = null;
    //    Actors = new List<Actor>();
    //}

    public Play(string title, Author? author = null, Director? director = null, List<Actor>? actors = null) 
        : this(GetNextId(), title, author, director, actors) { }

    public Play(int playId, string title, Author? author = null, Director? director = null, List<Actor>? actors = null)
    {
        if (playId <= MaxId) throw new ArgumentOutOfRangeException(nameof(playId), $"ID sztuki {playId} jest mniejsze lub równe MaxId {MaxId}");
        PlayId = playId;
        MaxId = PlayId;
        Title = title;
        Author = author;
        Author?.AddPlay(this);
        Director = director;
        Director?.AddPlay(this);
        Actors = actors ?? new List<Actor>();
    }

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

    public string GetActorsString()
    {
        return Actors.ListToString("Nikt nie gra w tej sztuce", '-');
    }

    public override string ToString()
    {
        return $"{PlayId}/\"{Title}\"/" +
            $"autor: {Author?.FirstName} {Author?.LastName ?? "Nieznany"}/" +
            $"reżyser: {Director?.FirstName} {Director?.LastName ?? "Nieznany"}";
    }
}
