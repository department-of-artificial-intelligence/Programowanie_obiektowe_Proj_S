using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Project.Domain;
public static class DatabaseExtension
{
    public static void SeedDatabse( this ApplicationDbContext context)
    {
        Console.WriteLine("Invoked SeedDatabase method");
    }


}
