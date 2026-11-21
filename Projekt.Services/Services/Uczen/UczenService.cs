namespace Projekt.Services.Uczen;
using Projekt.Models;

public class UczenService : IUczenService
{
    public void AddGrade(Uczen uczen, Ocena ocena)
    {
        if(uczen == null)
        {
            throw new ArgumentNullException(nameof(uczen));
        }

        if(ocena == null)
        {
            throw new ArgumentNullException(nameof(ocena));
        }
        uczen.Oceny.Add(ocena);
    }

    public string PrintInfo(Uczen uczen)
    {
        return $"ID: {uczen.Id} Name: {uczen.Name} {uczen.Surname}, Email: {uczen.Email}, poziom nauczania: {uczen.PoziomNauczania}, oceny: {uczen.Oceny}";
    }

    public void RemoveGrade(Uczen uczen, Ocena ocena)
    {
        if(uczen == null)
        {
            throw new ArgumentNullException(nameof(uczen));
        }

        if(ocena == null)
        {
            throw new ArgumentNullException(nameof(ocena));
        }
        uczen.Oceny.Remove(ocena);
    }

    public double ObliczSrednia(Uczen uczen)
    {
        return uczen.Oceny.Average(o => o.NumericGrade);
    }
}