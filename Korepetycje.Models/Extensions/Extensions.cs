using Korepetycje.Models.Models;
namespace Korepetycje.Models.Extensions;

public static class Extensions
{
    public static double ObliczSrednia(this List<Ocena> list)
    {
        double sum = 0;
        foreach (Ocena ocena in list)
        {
            sum += ocena.OcenaNumeryczna;
        }
        return sum / list.Count;
    }
}
