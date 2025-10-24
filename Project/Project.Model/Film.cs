using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Film
    {
        public string Id { get; private set; }
        public List<string> ActorsId { get; private set; } = [];

        public string Title { get; private set; } = "New film";
        public string Description { get; private set; } = "No info provided.";
        public uint DurationInMinutes { get; private set; } = 0;
        public string Director { get; private set; }
        public string Genre { get; private set; }
        public bool AgeRestriction { get; private set; } = false;
        public string PreviewImgUrl { get; private set; } = "/DefaultImgUrl";
        public string TrailerUrl { get; private set; } = "/DefaultTrailerUrl";

        public double Rating { get; private set; } = 0;
        public uint CustomersRated { get; private set; } = 0;

        // created_at updated_at



        Film()
        {
            throw new NotImplementedException();
        }

        public Film(
            string title,
            string description, 
            uint durationInMinutes, 
            string director, 
            string genre, 
            bool ageRestriction, 
            string previewImgUrl, 
            string trailerUrl
            )
        {
            //Id = 
        }

        //public Film(
        //    string title,
        //    string description,
        //    uint durationInMinutes,
        //    string director,
        //    string genre,
        //    bool ageRestriction,
        //    string previewImgUrl,
        //    string trailerUrl
        //)
        //{

        //}


    }
}
