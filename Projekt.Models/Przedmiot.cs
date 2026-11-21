using Projekt.Models.Enums;

namespace Projekt.Models;


public class Przedmiot
{
    public int Id { get; set;}
    private static int NastepneId { get; set;} = 1;
    public string Name { get; private set;}
    public PoziomNauczania Level {get; private set;}

    public Przedmiot(string name, PoziomNauczania level)
    {
        Id = NastepneId++;
        Name = name;
        Level = level;
    }
}