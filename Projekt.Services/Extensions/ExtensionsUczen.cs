namespace Projekt.Services.Extensions;
using Projekt.Models;

public static class ExtensionsUczen
{
    public static string PrintInfo(this Uczen uczen)
    {
        return $"[ID]: {uczen.Id} Imie: {uczen.Name} Nazwisko: {uczen.Surname} - Email: {uczen.Email} - Poziom Nauczania: {uczen.PoziomNauczania}";
    }

    public static List<Ocena> PrintGrades(this Uczen uczen)
    {
        return uczen.Oceny;
    }

    public static double CalculateAverage(this Uczen uczen)
    {
        double sum = 0;
        foreach(Ocena grade in uczen.Oceny)
        {
            sum += grade.NumericGrade;
        }
        return sum / uczen.Oceny.Count;
    }

    public static double CalculateAllStudentsAverage(this List<Uczen> students)
    {
        double sum = 0;
        foreach(Uczen student in students)
        {
            sum += student.CalculateAverage();
        }
        return sum / students.Count;
    }
}