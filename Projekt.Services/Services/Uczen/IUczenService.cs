namespace Projekt.Services.Uczen;
using Projekt.Models;
using Projekt.Models.Enums;

public interface IUczenService
{
    void AddGrade(Uczen uczen, Ocena ocena);
    bool RemoveGrade(Uczen uczen, Ocena ocena);
    bool ChangeEducationLevel(Uczen uczen, PoziomNauczania poziom);
}