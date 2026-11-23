using Projekt.Models.Enums;

namespace Projekt.Services.Uczen;

public sealed record OcenaDto(double NumericGrade, string Description);

public sealed record UczenDto(
    int Id,
    string Name,
    string Surname,
    string Email,
    PoziomNauczania PoziomNauczania,
    IReadOnlyList<OcenaDto> Oceny);

public sealed record CreateUczenDto(
    string Name,
    string Surname,
    string Email,
    PoziomNauczania PoziomNauczania,
    IReadOnlyList<OcenaDto> Oceny);
