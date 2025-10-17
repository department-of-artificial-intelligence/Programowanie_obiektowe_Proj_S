using Project.Models;

Film myTetsFilm = new("Test", 120, 30)
{
    description = "TestDescription"
};

Console.WriteLine(myTetsFilm.trailerUrl);