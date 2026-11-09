using Korepetycje.Models;
using Korepetycje.Extensions;


public class Program()
{
    public static void Main()
    {
        Uczen uczen = new Uczen(0, "Kacper", "Ciszewski", "kacperciszewskimrm@gmail.com", "Liceum");

        uczen.DodajOcene(4, "Kartkówka z matematyki");
        uczen.DodajOcene(5, "Kartkówka z matematyki");
        uczen.DodajOcene(3, "Kartkówka z matematyki");
        uczen.DodajOcene(5, "Kartkówka z matematyki");
        uczen.DodajOcene(6, "Kartkówka z matematyki");
        uczen.ObliczSredniaUcznia();
    }
}