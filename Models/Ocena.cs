namespace Korepetycje.Models;

public class Ocena
{
    public double OcenaNumeryczna { get; private set; }
    public string OpisOceny { get; private set; }

    public Ocena(double ocena, string opis)
    {
        if(ocena <= 0 || ocena > 5)
        {
            Console.WriteLine("Ocena musi być w zakresie od 1-5!");
        } else if(opis == "")
        {
            Console.WriteLine("Opis oceny nie może być pusty!");
        }
        OcenaNumeryczna = ocena;
        OpisOceny = opis;
    }
}