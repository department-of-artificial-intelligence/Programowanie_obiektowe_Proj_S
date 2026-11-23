namespace Projekt.Services.Uczen;
using Projekt.Models;
public static class UczenMapper
{
    public static UczenDto ToDto(Uczen uczen)
    {
        ArgumentNullException.ThrowIfNull(uczen);

        return new UczenDto(
            uczen.Id,
            uczen.Name,
            uczen.Surname,
            uczen.Email,
            uczen.PoziomNauczania,
            uczen.Oceny.Select(ToDto).ToArray());
    }

    public static OcenaDto ToDto(Ocena ocena)
    {
        ArgumentNullException.ThrowIfNull(ocena);

        return new OcenaDto(ocena.NumericGrade, ocena.Descripiton);
    }

    public static Uczen ToEntity(CreateUczenDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        return new Uczen(
            dto.Name,
            dto.Surname,
            dto.Email,
            dto.PoziomNauczania,
            dto.Oceny.Select(ToEntity).ToList());
    }

    public static Ocena ToEntity(OcenaDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        return new Ocena(dto.NumericGrade, dto.Description);
    }
}
