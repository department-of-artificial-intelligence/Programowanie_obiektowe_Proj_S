using Project.Entities;
using Project.Interfaces;
using Project.Utils;

namespace Project.Models
{
    public class Film : BaseEntity, IRating
    {
        public string Id { get; private set; }
        public List<string> ActorsId { get; private set; } = [];

        public string Title { get; private set; }
        public string Description { get; private set; }
        public uint DurationInMinutes { get; private set; } = 0;
        public string Director { get; private set; }
        public string Genre { get; private set; }
        public bool AgeRestriction { get; private set; } = false;
        public string PreviewImgUrl { get; private set; }
        public string TrailerUrl { get; private set; }

        public double Rating { get; private set; } = 0;
        public uint CustomersRated { get; private set; } = 0;

        public void UpdateRating(uint mark)
        {
            Rating = RatingHandler.CalculateRating(CustomersRated, Rating, mark);
            CustomersRated++;

            this.MarkAsUpdated();
        }

        public Film()
        {
            throw new NotImplementedException("Cannot create an epty Film obj");
        }

        public Film
        (
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
            this.Id = IdHandler.CreateId();

            this.Title = title;
            this.Description = description;
            this.DurationInMinutes = durationInMinutes;
            this.Director = director;
            this.Genre = genre;
            this.AgeRestriction = ageRestriction;
            this.PreviewImgUrl = previewImgUrl;
            this.TrailerUrl = trailerUrl;
        }

        public Film
        (
            string id,
            List<string> actorsId,
            string title,
            string description,
            uint durationInMinutes,
            string director,
            string genre,
            bool ageRestriction,
            string previewImgUrl,
            string trailerUrl,
            double rating,
            uint customersRated,
            DateTime updatedAt,
            DateTime createdAt
        )
        {
            this.Id = id;
            this.ActorsId = actorsId;
            this.Title = title;
            this.Description = description;
            this.DurationInMinutes = durationInMinutes;
            this.Director = director;
            this.Genre = genre;
            this.AgeRestriction = ageRestriction;
            this.PreviewImgUrl = previewImgUrl;
            this.TrailerUrl = trailerUrl;
            this.Rating = rating;
            this.CustomersRated = customersRated;
            this.UpdatedAt = updatedAt;
            this.CreatedAt = createdAt;
        }

        public bool AddActorId(string actorId)
        {
            if (!ArrayHandler.AddUniqueStringToMax5NlementsArray(this.ActorsId, actorId, 5))
            {
                return false;
            }

            this.MarkAsUpdated();
            return true;
        }

        public string GetAllActorsId()
        {
            return ArrayHandler.StringArrayToString(this.ActorsId);
        }

        public bool DeleteActorId(string actorId)
        {
            if (!ArrayHandler.DeleteElFromStringArray(this.ActorsId, actorId))
            {
                return false;
            }

            this.MarkAsUpdated();
            return true;
        }

        public override string ToString()
        {
            return $"Film id: {this.Id} \n" +
                   $"Film actors: {this.GetAllActorsId()} \n" +
                   $"Film title: {this.Title} \n" +
                   $"Film description: {this.Description} \n" +
                   $"Film duration: {this.DurationInMinutes}m \n" +
                   $"Film director: {this.Director} \n" +
                   $"Film genre: {this.Genre} \n" +
                   $"Film age restriction: {this.AgeRestriction} \n" +
                   $"Film img url: {this.PreviewImgUrl} \n" +
                   $"Film trailer url: {this.TrailerUrl} \n" +
                   $"Film rating: {this.Rating} \n" +
                   $"Film customers rated: {this.CustomersRated} \n" +
                   $"Film updated_at: {this.UpdatedAt} \n" +
                   $"Film created_at: {this.CreatedAt} \n";
        }

        public void UpdateGlobalInfo
        (
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
            this.Title = title;
            this.Description = description;
            this.DurationInMinutes = durationInMinutes;
            this.Director = director;
            this.Genre = genre;
            this.AgeRestriction = ageRestriction;
            this.PreviewImgUrl = previewImgUrl;
            this.TrailerUrl = trailerUrl;

            this.MarkAsUpdated();
        }
    }
}
