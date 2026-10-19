using ConsoleApp;
using ConsoleApp.Testy;


RowerTesty.TestZmienStatus();


Klient klient = new Klient(1, "Anna", "anna@example.com");

Console.WriteLine(klient);

klient.Zarejestruj();
klient.Zaloguj();
