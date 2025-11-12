using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Play
    {
        public string Title { get; set; }
        public Director Director { get; set; }
        public List<Actor> Actors { get; set; }

        public Play(string title, Director director, List<Actor> actors)
        {
            Title = title;
            Director = director;
            Actors = actors;
        }

        public bool AssignDirector(Director director)
        {
            if (director == null) return false;
            Director = director;
            if (!director.Plays.Contains(this)) director.AddPlay(this);
            return true;
        }

        public bool AddActor(Actor actor)
        {
            if (actor is null || Actors.Contains(actor)) return false;
            Actors.Add(actor);
            if (!actor.Plays.Contains(this)) actor.AddPlay(this);
            return true;
        }
        public bool RemoveActor(Actor actor)
        {
            if (Actors.Count == 0 || actor is null) return false;
            if (actor.Plays.Contains(this)) actor.RemovePlay(this);
            return Actors.Remove(actor);
        }
        public void RemoveAllActors()
        {
            foreach (Actor actor in Actors) if (actor.Plays.Contains(this)) actor.RemovePlay(this);
            Actors.Clear();
        }
    }
}
