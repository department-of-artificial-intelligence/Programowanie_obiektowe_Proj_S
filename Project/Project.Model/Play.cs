using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Play
    {
        public string Title { get; set; }
        public Director Director { get; set; }
        public List<Actor> Actors { get; set; }

        public Play() : this(string.Empty, new Director(), new List<Actor>()) { }
        public Play(string title, Director director, List<Actor> actors)
        {
            Title = title;
            Director = director;
            Actors = actors;
        }
    }
}
