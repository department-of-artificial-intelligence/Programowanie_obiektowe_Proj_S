namespace Project.Model;

public abstract class Person 
{
    // Pola prywatne
    private string _firstName = string.Empty;
    private string _lastName = string.Empty;

    // Właściwości
    public int Id { get; set; } // PK
    public string FirstName
    {
        get => _firstName;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Imię nie może być null lub puste", nameof(FirstName));
            _firstName = value;
        }
    }
    public string LastName
    {
        get => _lastName;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Nazwisko nie może być null lub puste", nameof(LastName));
            _lastName = value;
        }
    }

    // Konstruktory
    protected Person() { }

    protected Person(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    // Metody string
    public override string ToString()
    {
        return $"{FirstName} {LastName}";
    }
}
