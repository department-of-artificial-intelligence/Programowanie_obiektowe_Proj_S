namespace ConsoleApp;
using System;

class Platnosc : IPlatnoscOnline {
    static int IloscPlatnosciWSystemie { get; set; } = 0;
    public int Id { get; set; }
    private decimal Kwota { get; set; }
    private string Status { get; set; }

    public void ZaplacOnline(decimal kwota) {
        Console.WriteLine("Podaj nr. karty: ");
        string karta = Console.ReadLine() ?? string.Empty;
        Kwota = kwota;
        Status = "Oplacona";
        Console.WriteLine("Platnosc zrealizowana.");
    }
    public Platnosc() : this(IloscPlatnosciWSystemie, 99.99m, "Processing") { }
    public Platnosc(int id, decimal kwota, string status) {
        if (id <= IloscPlatnosciWSystemie) {
            id = IloscPlatnosciWSystemie;
        }
        Id = id;
        Kwota = kwota;
        Status = status;
        IloscPlatnosciWSystemie++;
    }
    public override string ToString() {
        return $"ID PLATNOSCI: {Id} \n KWOTA PŁATNOŚCI: {Kwota} \n STATUS: {Status} \n ILOŚĆ PŁATNOŚCI W SYSTEMIE {IloscPlatnosciWSystemie}";
    }
}