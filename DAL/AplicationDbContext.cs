using Microsoft.EntityFrameworkCore;
using RatingSystem.Domain;
namespace DataAccess
{

    public class ApplicationDbContext : DbContext
    {
        private readonly string _connectionParams = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\klymr\\source\\repos\\Ranking_System\\Programowanie_obiektowe_Proj_S\\DAL\\Database1.mdf;Integrated Security=True";
        public DbSet<Service> Services { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<User> Users { get; set; }

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionParams);
            }
        }


    }
}