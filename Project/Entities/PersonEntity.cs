using Project.Utils;


namespace Project.Entities
{
    public abstract class PersonEntity : BaseEntity
    {
        public string Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Nationality { get; private set; }
        public DateTime BirthDate { get; private set; }

        public string PreviewImgUrl { get; private set; }

        protected PersonEntity()
        {
            throw new NotImplementedException("Cannot create an epty Person obj");
        }

        protected PersonEntity
        (
            string firstName, 
            string lastName, 
            string nationality, 
            DateTime birthDate,
            string previewImgUrl
        )
        {
            this.Id = IdHandler.CreateId();
    
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Nationality = nationality;
            this.BirthDate = birthDate;
            this.PreviewImgUrl = previewImgUrl;
        }

        protected PersonEntity
        (
            string id, 
            string firstName, 
            string lastName, 
            string nationality, 
            DateTime birthDate, 
            string previewImgUrl,
            
            DateTime updatedAt,
            DateTime createdAt
        )
        {
            this.Id = id;

            this.FirstName = firstName;
            this.LastName = lastName;
            this.Nationality = nationality;
            this.BirthDate = birthDate;
            this.PreviewImgUrl = previewImgUrl;

            this.UpdatedAt = updatedAt;
            this.CreatedAt = createdAt;
        }
    }
}
