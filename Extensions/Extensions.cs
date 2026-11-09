namespace Korepetycje.Extensions;

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

    public static double ObliczSredniaUcznia(this Uczen uczen)
    {
        double sum = 0;
        uczen.Oceny.ObliczSrednia();
    }
}
