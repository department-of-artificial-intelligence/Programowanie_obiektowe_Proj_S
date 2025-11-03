using Project.Entities;

namespace Project.Models
{
    public class Actor : PersonEntity
    {
        public string Biography { get; private set; } = string.Empty;
        public double Popularity { get; private set; }

        public Actor(string firstName, string lastName, string nationality,
            DateTime birthDate, string profileImageUrl, string biography, double popularity)
            : base(firstName, lastName, nationality, birthDate, profileImageUrl)
        {
            SetBiography(biography);
            SetPopularity(popularity);
        }

        public Actor(string id, string firstName, string lastName, string nationality,
            DateTime birthDate, string profileImageUrl, string biography, double popularity,
            DateTime createdAt, DateTime updatedAt)
            : base(id, firstName, lastName, nationality, birthDate, profileImageUrl, createdAt, updatedAt)
        {
            SetBiography(biography, false);
            SetPopularity(popularity, false);
        }

        public void SetBiography(string biography, bool markAsUpdated = true)
        {
            Biography = biography ?? throw new ArgumentNullException(nameof(biography));
            if(markAsUpdated) MarkAsUpdated();
        }

        public void SetPopularity(double popularity, bool markAsUpdated = true)
        {
            if (popularity < 0 || popularity > 100)
                throw new ArgumentException("Popularity must be between 0 and 100");

            Popularity = popularity;
            if (markAsUpdated) MarkAsUpdated();
        }

        public override string ToString()
        {
            return $"Actor: {FullName} ({Age} years)\n" +
                   $"Nationality: {Nationality}\n" +
                   $"Biography: {Biography}...\n" +
                   $"Popularity: {Popularity:F1}\n" +
                   $"ID: {Id}";
        }
    }
}