// See https://aka.ms/new-console-template for more information

using Project.Model;

Console.WriteLine("--------------------------------------------");
Console.WriteLine("System zarządzania siecią teatrów");
Console.WriteLine("--------------------------------------------");

TheaterNetwork siecTeatrow = new TheaterNetwork("Globalna Sieć Teatralna");
Console.WriteLine(siecTeatrow);
Console.WriteLine();

siecTeatrow.CreateTheater(1, "Teatr Stary", "Polska", "Kraków", "Jagiellońska 1");
siecTeatrow.CreateTheater(2, "Teatr Wielki - Opera Narodowa", "Polska", "Warszawa", "Plac Teatralny 1");
siecTeatrow.CreateTheater(3, "Teatr Muzyczny Capitol", "Polska", "Wrocław", "Piłsudskiego 67");

Console.WriteLine(siecTeatrow);