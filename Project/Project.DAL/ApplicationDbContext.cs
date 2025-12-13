using Microsoft.EntityFrameworkCore; // <-- BRAKOWAŁO TEGO DLA DbContext/DbSet
using Project.Model;
using Project.DAL; // Czasem pomaga, jeśli klasa jest w tej przestrzeni nazw
//...

namespace Project.DAL
{
    public class ApplicationDbContext : DbContext
    {
        // 1. Zbiory (DbSet) dla encji w bazie
        public DbSet<Person> Persons { get; set; } // Obejmuje Client i Trainer
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Set> Sets { get; set; }

        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<TrainerSlot> TrainerSlots { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // W pliku ApplicationDbContext.cs
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 2. Konfiguracja dziedziczenia TPH (Table Per Hierarchy)
            modelBuilder.Entity<Person>()
                .HasDiscriminator<string>("Type")
                .HasValue<Client>("Client")
                .HasValue<Trainer>("Trainer");

            // ***************************************************************
            // ✅ POPRAWKA: Konfiguracja typu dla decimal w encji Trainer
            modelBuilder.Entity<Trainer>()
                .Property(t => t.HourlyRate)
                .HasColumnType("decimal(6, 2)"); // np. do 9999.99

            // 3. Konfiguracja relacji 1:W (Workout:Client)
            modelBuilder.Entity<Workout>()
                .HasOne(w => w.Client)
                .WithMany(c => c.PlannedWorkouts)
                .IsRequired();

            // Konfiguracja NOWEJ/ZMIENIONEJ encji Reservation
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Client)
                .WithMany(c => c.ScheduledReservations) // W Client.cs
                .HasForeignKey(r => r.ClientId)
                .IsRequired();

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Trainer)
                .WithMany(t => t.ScheduledReservations) // W Trainer.cs
                .HasForeignKey(r => r.TrainerId)
                .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(modelBuilder);
        }
    }
}
