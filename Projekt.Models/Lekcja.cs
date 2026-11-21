namespace Projekt.Models;

using System.Dynamic;
using Projekt.Models.Enums;

public class Lekcja
{
    public List<Uczen> Students { get; set;} = new();

    private static int NextId {get; set;} = 1;
    public int Id{ get; set;}
    public DateTime StartDate {get; private set;}
    public int TimeInMinutes { get; set;}
    public StatusLekcji Status {get; set;}
    public decimal Cost {get; set;}
    public Korepetytor Tutor { get; private set;}
    public Przedmiot Subject {get; private set;}

    public Lekcja(DateTime date, int time, StatusLekcji status, decimal cost, Korepetytor tutor
                    ,Przedmiot subject, List<Uczen> students)
    {
        if(students.Count <= 0)
        {
            throw new ArgumentException("Liczba uczniow musi byc wieksza od 0!");
        }
        Id = NextId++;
        StartDate = date;
        TimeInMinutes = time;
        Status = status;
        Cost = cost;
        Tutor = tutor;
        Students = students;
        Subject = subject;
    }
}