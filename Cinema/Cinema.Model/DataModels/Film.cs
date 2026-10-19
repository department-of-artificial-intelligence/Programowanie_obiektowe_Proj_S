using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Model.DataModels
{
    internal class Film
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PremiereDate { get; set; }
        public int Film_Time { get; set; }
        public int Screening_Time {get; set;}
        public string Genre { get; set; }
        public string Language { get; set; }
        public int PG_Rating { get; set; }
        public int Critic_Score { get; set; }
        public string URL_Trailer { get; set; }
        public string URL_Poster { get; set; }
        public string Booking { get; set; }
        public string Ticket { get; set; }


        public List<Actor> Cast { get; set; } = new List<Actor>();
        public List<Director> Director { get; set; } = new List<Director>();
        public List<Tags> Tag { get; set; } = new List<Tags>();


    }
}
