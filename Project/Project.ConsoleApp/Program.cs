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

siecTeatrow.Theaters[0].CreateHall(1);
siecTeatrow.Theaters[1].CreateHall(1);
siecTeatrow.Theaters[1].CreateHall(2);
siecTeatrow.Theaters[1].CreateHall(3);
siecTeatrow.Theaters[2].CreateHall(1);
siecTeatrow.Theaters[2].CreateHall(2);

Console.WriteLine(siecTeatrow);
Console.WriteLine();

siecTeatrow.Theaters[0].Halls[0].CreateSeat(1, 1);
siecTeatrow.Theaters[0].Halls[0].CreateSeat(1, 1); // powtórzenie sziedzenia
siecTeatrow.Theaters[0].Halls[0].CreateSeat(1, 2);
siecTeatrow.Theaters[0].Halls[0].CreateSeat(2, 1);
siecTeatrow.Theaters[0].Halls[0].CreateSeat(2, 2);

Console.WriteLine(siecTeatrow.Theaters[0].Halls[0].GetSeats());