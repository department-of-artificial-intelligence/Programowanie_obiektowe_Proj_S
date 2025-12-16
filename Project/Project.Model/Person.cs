public abstract class Person
{ 
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Person() : this(string.Empty, string.Empty) { }
    public Person(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public string GetFullName()
    {
        return $"{FirstName} {LastName}";
    }
}
