using Wypozyczalnia.Model;

var klient = new Klient("Anna", "anna@example.com");
var rower = new Rower("NS Bikes");
Console.WriteLine(rower);

var wypozyczenie = new Wypozyczenie(klient, rower, DateTime.Now, DateTime.Now.AddHours(2));
Console.WriteLine(rower);

Console.WriteLine($"Koniec przed: {wypozyczenie.DataKonca}");
wypozyczenie.Przedluz(3);
Console.WriteLine($"Koniec po:    {wypozyczenie.DataKonca}");

wypozyczenie.Zakoncz();
Console.WriteLine(rower);