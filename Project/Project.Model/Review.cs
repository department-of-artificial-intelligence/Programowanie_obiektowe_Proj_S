using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Review
    {
        public int _id;
        public User user { get; set; }
        public Movie movie { get; set; }
        public float rate { get; set; } // between 0 and 5 
        public string comment {get; set; }

        public Review() { }

        public Review(int id, User user, Movie movie, float rate, string comment) 
        {
            _id = id;
            this.user = user;
            this.movie = movie;
            this.rate = rate;
            this.comment = comment;
        }

        public override string ToString()
        {
            return String.Format("Review {0} by {1} about {2} with rate {3} and comment '{4}'", _id, user.Username, movie.Title, rate, comment);
        }
    }
}
