using Microsoft.EntityFrameworkCore;
using Project.Model;
namespace Project.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceReservation> ServiceReservations { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().Property(r => r.CenaZaDobe).HasPrecision(10, 2);
            modelBuilder.Entity<Service>().Property(s => s.Cena).HasPrecision(10, 2);
            modelBuilder.Entity<Employees>().Property(e => e.Pensja).HasPrecision(12, 2);
            modelBuilder.Entity<ServiceReservation>().Property(sr => sr.CenaWChwiliZakupu).HasPrecision(10, 2);

            modelBuilder.Entity<Reservation>().Property(r => r.DataOd).HasColumnType("date");
            modelBuilder.Entity<Reservation>().Property(r => r.DataDo).HasColumnType("date");
            modelBuilder.Entity<Room>().Property(r => r.Od).HasColumnType("date");
            modelBuilder.Entity<Room>().Property(r => r.Do).HasColumnType("date");
            modelBuilder.Entity<Employees>().Property(e => e.DataZatrudnienia).HasColumnType("date");
            modelBuilder.Entity<ServiceReservation>().Property(sr => sr.Data).HasColumnType("date");

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Pokoj).WithMany().HasForeignKey(r => r.PokojId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Gosc).WithMany().HasForeignKey(r => r.GuestId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ServiceReservation>()
                .HasOne(sr => sr.Service).WithMany().HasForeignKey(sr => sr.ServiceId).OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
