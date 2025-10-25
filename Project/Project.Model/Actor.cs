using Project.Entities;

namespace Project.Models
{
    public class Actor : PersonEntity
    {
        public string Biography { get; private set; }
        public double Popularity { get; private set; }

        public Actor()
        {
            throw new NotImplementedException("Cannot create an epty Actor obj");
        }

        public Actor
        (
            string firstName,
            string lastName,
            string nationality,
            DateTime birthDate,
            string previewImgUrl,
            string biography,
            double popularity
        ) : base(firstName, lastName, nationality, birthDate, previewImgUrl)
        {
           this.Biography = biography;
           this.Popularity = popularity;
        }

        public Actor
        (
            string id,
            string firstName,
            string lastName,
            string nationality,
            DateTime birthDate,
            string previewImgUrl,
            string biography,
            double popularity,
            DateTime updatedAt,
            DateTime createdAt
        ) : base(id, firstName, lastName, nationality, birthDate, previewImgUrl, updatedAt, createdAt)
        {
            this.Biography = biography;
            this.Popularity = popularity;
        }

        public override string ToString()
        {
            return $"Actor id: {this.Id} \n" +
                   $"Actor name/surname: {this.FirstName} / {this.LastName} \n" +
                   $"Actor nationality: {this.Nationality} \n" +
                   $"Actor birth date: {this.BirthDate} \n" +
                   $"Actor biography: {this.Biography} \n" +
                   $"Actor imgUrl: {this.PreviewImgUrl} \n" +
                   $"Actor popularity: {this.Popularity} \n" +
                   $"Actor updated_at {this.UpdatedAt} \n" +
                   $"Actor created_at: {this.CreatedAt} \n";
        }
    }
}
