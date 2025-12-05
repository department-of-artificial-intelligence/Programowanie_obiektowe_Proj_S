using Microsoft.EntityFrameworkCore;
using Project.Model;

namespace Project.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<HotelRoom> HotelRooms { get; set; }

        public DbSet<Manager> Managers { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<Resident> Residents { get; set; }

        public DbSet<RoomHistoricResident> RoomHistoricResidents { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
