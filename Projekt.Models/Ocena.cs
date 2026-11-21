namespace Projekt.Models;

public class Ocena
{
    public double NumericGrade {get; private set;}
    public string Descripiton {get; private set;}

    public Ocena(double grade, string description)
    {
        if(grade <= 0 || grade > 5)
        {
            Console.WriteLine("Ocena musi być w zakresie od 1-5!");
            grade = 2;
        } else if(description == "" || description == null)
        {
            Console.WriteLine("Opis oceny nie może być pusty!");
            description = string.Empty;
        }
        NumericGrade = grade;
        Descripiton = description;
    }
}