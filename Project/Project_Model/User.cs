namespace Project.Model
{
    public abstract class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public User(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
        public User() { } 

        public override string ToString() => $"{Id}: {FirstName} {LastName} ({Email})";
    }
}