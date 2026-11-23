namespace Projekt.Services.Uczen;

using System.Runtime.InteropServices;
using Projekt.Models;
using Projekt.Models.Enums;

public class UczenService : IUczenService
{
    public void AddGrade(Uczen uczen, Ocena ocena)
    {
        ArgumentNullException.ThrowIfNull(uczen);
        ArgumentNullException.ThrowIfNull(ocena);
        uczen.Oceny.Add(ocena);
    }


    public bool RemoveGrade(Uczen uczen, Ocena ocena)
    {
        ArgumentNullException.ThrowIfNull(uczen);
        ArgumentNullException.ThrowIfNull(ocena);
        return uczen.Oceny.Remove(ocena);
    }

    public bool ChangeEducationLevel(Uczen uczen, PoziomNauczania poziom)
    {
        ArgumentNullException.ThrowIfNull(uczen);

        if(uczen.PoziomNauczania != poziom)
        {
            uczen.PoziomNauczania = poziom;
            return true;
        }
        return false;
    }

}