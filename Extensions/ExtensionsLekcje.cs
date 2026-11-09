using Korepetycje.Models;
using Korepetycje.Enums;

namespace Korepetycje.Extensions;

public static class ExtensionsLekcje
{
    public static IEnumerable<Lekcja> Zakonczone(this IEnumerable<Lekcja> lekcje)
    {
        return lekcje.Where(l => l.Status == StatusLekcji.Zakonczona);
    }

    public static decimal LacznyPrzychod(this IEnumerable<Lekcja> lekcje)
    {
        return lekcje.Sum(l => l.Cena);
    }

    public static IEnumerable<Lekcja> ZeStatusem(this IEnumerable<Lekcja> lekcje, StatusLekcji status)
    {
        return lekcje.Where(l => l.Status == status);
    }

    public static decimal SredniaCena(this IEnumerable<Lekcja> lekcje)
    {
        return lekcje.Select(l => l.Cena).DefaultIfEmpty(0m).Average();
    }
}