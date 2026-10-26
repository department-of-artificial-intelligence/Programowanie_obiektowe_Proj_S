using ConsoleApp;
using ConsoleApp.Testy;

class Program {
    static void Main() {
        /*RowerTesty.TestZmienStatus();
        Klient klient = new Klient(1, "Anna", "anna@example.com");
        Console.WriteLine(klient);
        klient.Zarejestruj();
        klient.Zaloguj();*/
        Platnosc nowaPlatnosc = new Platnosc();
        Console.WriteLine(nowaPlatnosc);
        List<Platnosc> WszystkiePlatnosci = new();
        WszystkiePlatnosci.Add(new Platnosc(1, 100.23m, "Processing"));
        WszystkiePlatnosci.Add(new Platnosc(1, 100.23m, "Processing"));
        WszystkiePlatnosci.Add(new Platnosc(1, 100.23m, "Processing"));
        foreach(var p in WszystkiePlatnosci) {
            Console.WriteLine(p);
        }

        WszystkiePlatnosci[2].ZaplacOnline(100.23m);
    }
}

