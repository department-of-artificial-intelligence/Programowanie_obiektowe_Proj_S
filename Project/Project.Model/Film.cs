using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Film
    {
        public readonly string name;
        public readonly int filmDurationInMinutes;
        public readonly double tiketPrice;

        public string description = "No description added.";
        public string category = "No category.";
        public string previewImgUrl = "DefaultImgUrl";
        
        public string? trailerUrl;

        public string[]? previewFilmImgUrls;
        public string[]? actors;

        private double rating = 0;
        
        Film()
        {
            throw new NotImplementedException();
        }
        public Film(string _name, int _filmDurationInMinutes, int _tiketPrice)
        {
            name = _name;
            filmDurationInMinutes = Math.Abs(_filmDurationInMinutes);
            tiketPrice = Math.Abs(_tiketPrice);
        }


    }
}
