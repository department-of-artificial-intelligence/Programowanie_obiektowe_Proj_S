namespace Projekt.Models;

public abstract class Osoba
{
    public int Id { get; protected set;}
    public string Name {get; protected set;}
    public string Surname { get; protected set;}
    public string Email { get; protected set;}

    protected Osoba(int id, string name, string surname, string email)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Email = email;
    }
}