using Wypozyczalnia.Logic;
using Wypozyczalnia.Logic.Interfaces;
using Wypozyczalnia.Model;
using Wypozyczalnia.UI.ConsoleApp;

// Composition root: składamy serwisy i wstrzykujemy zależności
IRowerService rowerService = new RowerService();
IWypozyczenieService wypozyczenieService = new WypozyczenieService(rowerService);

var klient = new Klient("Anna", "anna@example.com");
var rower = new Rower("NS Bikes");
rower.Print();

var wypozyczenie = wypozyczenieService.Wypozycz(klient, rower, DateTime.Now, DateTime.Now.AddHours(2));
rower.Print();

Console.WriteLine($"Koniec przed: {wypozyczenie.DataKonca}");
wypozyczenieService.Przedluz(wypozyczenie, 3);
Console.WriteLine($"Koniec po: {wypozyczenie.DataKonca}");

wypozyczenieService.Zakoncz(wypozyczenie);
rower.Print();

// test reguły biznesowej: próba wypożyczenia roweru w naprawie
rower.Status = StatusRoweru.WNaprawie;
try {
    wypozyczenieService.Wypozycz(klient, rower, DateTime.Now, DateTime.Now.AddHours(1));
}
catch (InvalidOperationException ex) {
    Console.WriteLine($"Blad: {ex.Message}");
}