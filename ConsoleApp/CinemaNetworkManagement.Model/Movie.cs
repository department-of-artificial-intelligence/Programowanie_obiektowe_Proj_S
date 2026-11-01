using System.Collections.Generic;

namespace CinemaNetworkManagement.Model
{
    public class Movie
    {
        private readonly List<MovieLicense> _licenses;

        public int Id { get; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int DurationMinutes { get; set; }
        public int MinimumAge { get; set; }

        public List<MovieLicense> Licenses
        {
            get { return new List<MovieLicense>(_licenses); }
        }

        public Movie(int id, string title, string genre, int durationMinutes, int minimumAge)
        {
            Id = id;
            Title = title;
            Genre = genre;
            DurationMinutes = durationMinutes;
            MinimumAge = minimumAge;
            _licenses = new List<MovieLicense>();
        }

        
    }
}
