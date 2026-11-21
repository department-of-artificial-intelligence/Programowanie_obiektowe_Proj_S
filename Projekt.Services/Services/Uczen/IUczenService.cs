namespace Projekt.Services.Uczen;
using Projekt.Models;

public interface IUczenService
{
    void AddGrade(Uczen uczen, Ocena ocena);
    string PrintInfo(Uczen uczen);
    void RemoveGrade(Uczen uczen, Ocena ocena);
    double ObliczSrednia(Uczen uczen);
}