using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Project.Domain;
public static class DatabaseExtension
{
    public static void SeedDatabase( this ApplicationDbContext context)
    {
        Console.WriteLine("Invoked SeedDatabase method");

        // Here we can add mock data to DB
    }


}
