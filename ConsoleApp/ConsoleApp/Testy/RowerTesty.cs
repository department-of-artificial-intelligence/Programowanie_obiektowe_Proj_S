namespace ConsoleApp.Testy;

public static class RowerTesty {
    public static void TestZmienStatus() {
        Rower rower = new Rower(1, "nsbikes", "wolny");

        rower.ZmienStatus("zajety");

        Console.WriteLine(rower.ToString());

        if (rower.ToString().Contains("zajety")) {
            Console.WriteLine("TEST 1 ZALICZONY. ZMIANA STATUSU z 'wolny' na 'zajety'\n");
        }
    }
}
