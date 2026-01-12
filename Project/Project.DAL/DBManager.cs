using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.DAL
{
    public static class DBManager
    {
        private static ApplicationDBContext? Context;
        private static readonly string ConnectionString = @"Server=(localdb)\mssqllocaldb;Database=CinemaManagementDB;Trusted_Connection=True;MultipleActiveResultSets=true;";

        public static ApplicationDBContext BuildDB()
        {
            if (Context == null)
            {
                try
                {
                    var optionsBuilder = new DbContextOptionsBuilder<ApplicationDBContext>();
                    optionsBuilder.UseSqlServer(ConnectionString);
                    
                    Context = new ApplicationDBContext(optionsBuilder.Options);
                    
                    Context.Database.EnsureCreated();
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error connecting to database: {ex.Message}");
                    throw;
                }
            }

            return Context;
        }

        public static void ClearDB()
        {
            if (Context is null) return;

            try
            {
                using var transaction = Context.Database.BeginTransaction();
                
                Context.Tickets.RemoveRange(Context.Tickets);
                Context.Reservations.RemoveRange(Context.Reservations);
                Context.Seances.RemoveRange(Context.Seances);
                Context.Auditoriums.RemoveRange(Context.Auditoriums);
                Context.Cinemas.RemoveRange(Context.Cinemas);
                Context.Films.RemoveRange(Context.Films);
                Context.Actors.RemoveRange(Context.Actors);
                
                Context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clearing database: {ex.Message}");
                throw;
            }
        }
    }
}