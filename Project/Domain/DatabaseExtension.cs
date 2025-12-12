using Microsoft.EntityFrameworkCore;
using Project.Model;
using System.Runtime.CompilerServices;

namespace Project.Domain;
public static class DatabaseExtension
{
    public static void SeedDatabase( this ApplicationDbContext context)
    {
        Console.WriteLine("Invoked SeedDatabase method");

        // Here we can add mock data to DB
        context.Add<Author>(new Author(0, "Jan", "Kowalski", new DateTime(2006, 12, 27)));
        context.SaveChanges();
    }


}
