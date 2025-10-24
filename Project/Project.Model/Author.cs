namespace Project.Model;

public class Author : BaseEntity<int>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }

    public DateTime BirthDay { get; set; }

    public Author(string firstName, string lastName, DateTime birthDay)
    {
        FirstName = firstName;
        LastName = lastName;
        BirthDay = birthDay;
    }
}
