using Microsoft.EntityFrameworkCore;
using Hotel.Model;

namespace Hotel.Database
{
    public class Db : DbContext
    {
        // uzupe³niæ parametry po³¹czenia (dod. 4)
        private readonly string _connectionString = "";
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }

}
