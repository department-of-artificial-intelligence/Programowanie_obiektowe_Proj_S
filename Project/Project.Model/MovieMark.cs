using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{

    // Enum for MovieList.cs to identify Movie type for user as Favorite, Watched, WatchLater 
    public class MovieMark
    {
        public int _id { get; set; }
        public User User { get; set; }

        public Movie movie { get; set; }

        public MovieMarkType type { get; set; }

        public MovieMark(int id, User user, Movie movie, MovieMarkType type)
        {
            _id = id;
            User = user;
            this.movie = movie;
            this.type = type;
        }

        //public override string ToString()
        //{
        //    return String.Format("""Movie: {1} marked as {2} for user: {3}""", movie.Title, type.ToString, User.Username);
        //}
    }
}
