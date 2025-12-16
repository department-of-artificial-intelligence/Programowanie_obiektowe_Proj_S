using Microsoft.EntityFrameworkCore; 
using Project.Model;
using Project.DAL; 

// definicja tabel i relacji
namespace Project.DAL
{
    public class ApplicationDbContext : DbContext
    {
        
        public DbSet<Person> Persons { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Set> Sets { get; set; }

        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<TrainerSlot> TrainerSlots { get; set; }

        //przekazuje opcje połączenia
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Instrukcje do tabel
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Person>()
                .HasDiscriminator<string>("Type")
                .HasValue<Client>("Client")
                .HasValue<Trainer>("Trainer");

          //konfiguracja typu decimal
            modelBuilder.Entity<Trainer>()
                .Property(t => t.HourlyRate)
                .HasColumnType("decimal(6, 2)"); 

            // relacja 1:W Workout:Client
            modelBuilder.Entity<Workout>()
                .HasOne(w => w.Client)
                .WithMany(c => c.PlannedWorkouts)
                .IsRequired();

            // Konfiguracja relacji rezerwaci, i trenera
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Client)
                .WithMany(c => c.ScheduledReservations) 
                .HasForeignKey(r => r.ClientId)
                .IsRequired();

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Trainer)
                .WithMany(t => t.ScheduledReservations) //kazda rezerwacja moze mieć jednego trenera, a jeden trener wiele rezerwacji
                .HasForeignKey(r => r.TrainerId)
                .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);//blokuje operacje usunięcia  trenera 
            base.OnModelCreating(modelBuilder); //jesli ma jakieś rezerwacje
        }
    }
}
