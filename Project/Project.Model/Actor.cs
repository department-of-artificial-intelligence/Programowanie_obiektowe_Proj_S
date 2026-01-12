using Project.Models.Common;

namespace Project.Models
{
    public class Actor : Person
    {
        public string Biography { get; set; } = string.Empty;
        public double Popularity { get; set; } = 0;


        public Actor() { }

        public Actor(string firstName, string lastName, string nationality, DateTime birthDate, string profileImageUrl, string biography, double popularity)
               : base(firstName, lastName, nationality, birthDate, profileImageUrl)
        {
            SetBiography(biography);
            SetPopularity(popularity);
        }

        public void SetBiography(string biography)
        {
            Biography = biography ?? throw new ArgumentNullException(nameof(biography));
            MarkAsUpdated();
        }

        public void SetPopularity(double popularity)
        {
            if (popularity < 0 || popularity > 100) throw new ArgumentException("Popularity must be between 0 and 100");

            Popularity = popularity;
            MarkAsUpdated();
        }

        public override string ToString()
        {
            return $"Actor: {FullName} ({Age} years)\n" +
                   $"Nationality: {Nationality}\n" +
                   $"Biography: {Biography}...\n" +
                   $"Popularity: {Popularity}\n" +
                   $"ID: {Id}";
        }
    }
}