using Project.Interfaces;
using Project.Models.Common;
using Project.Utils;

namespace Project.Models
{
    public class Film : Base, IRatable, IListManageable<string>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public uint DurationMinutes { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public bool HasAgeRestriction { get; set; }
        public string PosterUrl { get; set; }
        public string TrailerUrl { get; set; }
        public double Rating { get; set; }
        public uint TotalRatings { get; set; }

        private readonly List<string> _actorIds;
        public IReadOnlyList<string> ActorIds => _actorIds.AsReadOnly();
        public IReadOnlyList<string> Items => ActorIds;

        public Film()
        {
            _actorIds = [];
            Title = string.Empty;
            Description = string.Empty;
            Director = string.Empty;
            Genre = string.Empty;
            PosterUrl = string.Empty;
            TrailerUrl = string.Empty;
            Rating = 0;
            TotalRatings = 0;
        }

        public Film(string title, string description, uint durationMinutes, string director, string genre, bool hasAgeRestriction, string posterUrl, string trailerUrl) 
               : base()
        {
            ValidateFilmData(title, description, durationMinutes, director, genre);

            Title = title;
            Description = description;
            DurationMinutes = durationMinutes;
            Director = director;
            Genre = genre;
            HasAgeRestriction = hasAgeRestriction;
            PosterUrl = posterUrl;
            TrailerUrl = trailerUrl;
            _actorIds = [];
        }
        private static void ValidateFilmData(string title, string description, uint durationMinutes, string director, string genre)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title is required", nameof(title));
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description is required", nameof(description));
            if (durationMinutes < 1) throw new ArgumentException("Duration must be at least 1 minute", nameof(durationMinutes));
            if (string.IsNullOrWhiteSpace(director)) throw new ArgumentException("Director is required", nameof(director));
            if (string.IsNullOrWhiteSpace(genre)) throw new ArgumentException("Genre is required", nameof(genre));
        }

        public void AddRating(uint rating)
        {
            if (rating < 1 || rating > 5) throw new ArgumentException("Rating must be between 1 and 5");

            Rating = RatingCalculator.CalculateNewRating(TotalRatings, Rating, rating);
            TotalRatings++;

            MarkAsUpdated();
        }

        public bool AddItem(string actorId)
        {
            if (CollectionHelper.AddUniqueItem(_actorIds, actorId, 10))
            {
                MarkAsUpdated();
                return true;
            }

            return false;
        }

        public bool RemoveItem(string actorId)
        {
            if (CollectionHelper.RemoveItem(_actorIds, actorId))
            {
                MarkAsUpdated();
                return true;
            }

            return false;
        }

        public string GetItemsAsString() => CollectionHelper.ToString(_actorIds);

        public void UpdateInfo(string title, string description, uint durationMinutes, string director, string genre, bool hasAgeRestriction, string posterUrl, string trailerUrl)
        {
            ValidateFilmData(title, description, durationMinutes, director, genre);

            Title = title;
            Description = description;
            DurationMinutes = durationMinutes;
            Director = director;
            Genre = genre;
            HasAgeRestriction = hasAgeRestriction;
            PosterUrl = posterUrl;
            TrailerUrl = trailerUrl;

            MarkAsUpdated();
        }

        public override string ToString()
        {
            return $"Film: {Title}\n" +
                   $"Director: {Director} | Genre: {Genre}\n" +
                   $"Duration: {DurationMinutes} minutes\n" +
                   $"Age Restriction: {(HasAgeRestriction ? "Yes" : "No")}\n" +
                   $"Rating: {Rating} ({TotalRatings} ratings)\n" +
                   $"Actors: {GetItemsAsString()}\n" +
                   $"ID: {Id}";
        }
    }
}