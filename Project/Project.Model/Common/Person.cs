namespace Project.Models.Common
{
    public abstract class Person : Base
    {
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Nationality { get; protected set; }
        public DateTime BirthDate { get; protected set; }
        public string ProfileImageUrl { get; protected set; }

        public string FullName => $"{FirstName} {LastName}";
        public int Age => CalculateAge();


        protected Person()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Nationality = string.Empty;
            BirthDate = DateTime.Now;
            ProfileImageUrl = string.Empty;
        }

        protected Person(string firstName, string lastName, string nationality, DateTime birthDate, string profileImageUrl) : base()
        {
            ValidatePersonData(firstName, lastName, nationality, birthDate);

            FirstName = firstName;
            LastName = lastName;
            Nationality = nationality;
            BirthDate = birthDate;
            ProfileImageUrl = profileImageUrl;
        }

        private static void ValidatePersonData(string firstName, string lastName, string nationality, DateTime birthDate)
        {
            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name is required", nameof(firstName));
            if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("Last name is required", nameof(lastName));
            if (string.IsNullOrWhiteSpace(nationality)) throw new ArgumentException("Nationality is required", nameof(nationality));
            if (birthDate > DateTime.Now) throw new ArgumentException("Invalid birth date", nameof(birthDate));
        }

        private int CalculateAge()
        {
            var today = DateTime.Today;
            var age = today.Year - BirthDate.Year;

            if (BirthDate.Date > today.AddYears(-age)) age--;
            return age;
        }

        public void UpdatePersonalInfo(string firstName, string lastName, string nationality, DateTime birthDate, string profileImageUrl)
        {
            ValidatePersonData(firstName, lastName, nationality, birthDate);

            FirstName = firstName;
            LastName = lastName;
            Nationality = nationality;
            BirthDate = birthDate;
            ProfileImageUrl = profileImageUrl;

            MarkAsUpdated();
        }
    }
}