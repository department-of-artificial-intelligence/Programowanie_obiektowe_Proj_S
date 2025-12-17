using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RatingSystem.Domain;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace RatingSystem.DAL
{

    public class ApplicationDbContext : DbContext
    {
        private readonly string _connectionParams = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\klymr\\source\\repos\\Ranking_System\\Programowanie_obiektowe_Proj_S\\DAL\\Database1.mdf;Integrated Security=True";
        public DbSet<Service> Services { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\klymr\\source\\repos\\Ranking_System\\Programowanie_obiektowe_Proj_S\\DAL\\Database1.mdf;Integrated Security=True");

                return new ApplicationDbContext(optionsBuilder.Options);
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionParams);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.User)
                .WithMany(u => u.Ratings)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }


    }
}