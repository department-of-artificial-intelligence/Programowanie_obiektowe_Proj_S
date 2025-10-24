using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Review : BaseEntity<int>
    {
        public User user { get; set; }
        public Movie movie { get; set; }
        public float rate { get; set; } // between 0 and 5 
        public string comment {get; set; }

        public Review() { }

        public Review(User user, Movie movie, float rate, string comment) 
        {
            this.user = user;
            this.movie = movie;
            this.rate = rate;
            this.comment = comment;
        }
    }
}
