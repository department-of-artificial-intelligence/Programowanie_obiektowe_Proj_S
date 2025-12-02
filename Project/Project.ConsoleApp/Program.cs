// See https://aka.ms/new-console-template for more information

using Project.Model;

Console.WriteLine("--------------------------------------------");
Console.WriteLine("System zarządzania siecią teatrów");
Console.WriteLine("--------------------------------------------");

TheaterNetwork siecTeatrow = new TheaterNetwork("Globalna Sieć Teatralna");
//Console.WriteLine(siecTeatrow);
//Console.WriteLine();

siecTeatrow.CreateTheater(1, "Teatr Stary", "Polska", "Kraków", "Jagiellońska 1");
siecTeatrow.CreateTheater(2, "Teatr Wielki - Opera Narodowa", "Polska", "Warszawa", "Plac Teatralny 1");
siecTeatrow.CreateTheater(3, "Teatr Muzyczny Capitol", "Polska", "Wrocław", "Piłsudskiego 67");

//Console.WriteLine(siecTeatrow);
//Console.WriteLine();

var theater1 = siecTeatrow.Theaters[0];
var theater2 = siecTeatrow.Theaters[1];
var theater3 = siecTeatrow.Theaters[2];

theater1.CreateHall(1);
theater1.CreateHall(1);
theater2.CreateHall(2);
theater2.CreateHall(3);
theater3.CreateHall(1);
theater3.CreateHall(2);

Console.WriteLine(siecTeatrow);
Console.WriteLine();

var hall1 = siecTeatrow.Theaters[0].Halls[0];
hall1.CreateSeat(2, 2);
hall1.CreateSeat(1, 2);
hall1.CreateSeat(1, 1);
hall1.CreateSeat(1, 1); // powtórzenie siedzenia
hall1.CreateSeat(2, 1);

Console.WriteLine("Siedzenia w Sali 1:");
Console.WriteLine(hall1.GetSeats());