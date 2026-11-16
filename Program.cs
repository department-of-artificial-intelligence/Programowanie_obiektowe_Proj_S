using Korepetycje.Models.Models;

public class Program()
{
    public static void Main()
    {
        Uczen uczen = new Uczen("Kacper", "Ciszewski", "kacperciszewskimrm@gmail.com", "Liceum");

        uczen.DodajOcene(4, "Kartkówka z matematyki");
        uczen.DodajOcene(5, "Kartkówka z matematyki");
        uczen.DodajOcene(3, "Kartkówka z matematyki");
        uczen.DodajOcene(5, "Kartkówka z matematyki");
        uczen.DodajOcene(5, "Kartkówka z matematyki");
        
        double wynik = uczen.ObliczSrednia();

        Console.WriteLine($"wynik: {wynik}");
    }
}