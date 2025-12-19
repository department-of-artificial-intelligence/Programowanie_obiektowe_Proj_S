using Microsoft.EntityFrameworkCore;
using Project.Model;
using System.Collections.Generic;

namespace Project.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Veterinarian> Veterinarians { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Treatment> Treatments { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Owner -> Animals 
            modelBuilder.Entity<Owner>()
                .HasMany(o => o.Animals)
                .WithOne(a => a.Owner)
                .HasForeignKey(a => a.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Clinic -> Veterinarians 
            modelBuilder.Entity<Clinic>()
                .HasMany(c => c.Veterinarians)
                .WithOne(v => v.Clinic)
                .HasForeignKey(v => v.ClinicId)
                .OnDelete(DeleteBehavior.Restrict);

            // Appointment -> Animal 
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Animal)
                .WithMany()
                .HasForeignKey(a => a.AnimalId)
                .OnDelete(DeleteBehavior.Cascade);

            // Appointment -> Veterinarian
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Veterinarian)
                .WithMany()
                .HasForeignKey(a => a.VeterinarianId)
                .OnDelete(DeleteBehavior.Restrict);

            // Appointment <-> Treatment
            modelBuilder.Entity<Appointment>()
                .HasMany<Treatment>()
                .WithMany()
                .UsingEntity(j => j.ToTable("AppointmentTreatments"));

            modelBuilder.Entity<Treatment>()
                .Property(t => t.Cost)
                .HasPrecision(10, 2);
        }
    }
}