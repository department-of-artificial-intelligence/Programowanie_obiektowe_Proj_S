using System.Diagnostics.CodeAnalysis;

namespace Project.Model;

public class Author : BaseEntity<int>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }

    public DateTime BirthDay { get; set; }

    [SetsRequiredMembers]
    public Author(int id, string firstName, string lastName, DateTime birthDay) : base(id) { 
        FirstName = firstName;
        LastName = lastName;
        BirthDay = birthDay;
    }

    [SetsRequiredMembers]
    public Author() : base(0) { }

    public override string ToString()
    {
        return $"Author: {FirstName}/{LastName}/{BirthDay}";
    }
}
