using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model;

public class Play
{
    public int PlayId { get; set; }
    public string Title { get; set; }
    public Director? Director { get; set; }
    public List<Actor> Actors { get; set; }

    public Play(int playId, string title, Director? director = null, List<Actor>? actors = null)
    {
        PlayId = playId;
        Title = title;
        Director = director;
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
}
