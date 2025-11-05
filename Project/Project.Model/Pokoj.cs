namespace SiecHoteli
{
    public class Pokoj
    {
        public int Numer { get; set; }
        public int LiczbaMiejsc { get; set; }
        public bool Dostepny { get; set; } = true;

        public override string ToString() =>
            $"Pokój {Numer} (miejsc: {LiczbaMiejsc}) - {(Dostepny ? "wolny" : "zajęty")}";
    }
}
