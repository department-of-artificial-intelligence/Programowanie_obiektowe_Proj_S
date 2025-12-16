using System.Reflection.Metadata;

public abstract class Person : IPerson
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public Person(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
   public Person() {
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
    }
}