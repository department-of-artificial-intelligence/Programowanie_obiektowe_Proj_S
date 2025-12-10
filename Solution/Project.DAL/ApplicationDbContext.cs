using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Model;

namespace Project.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; } 
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
               : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Course>()
                .Property(c => c.PricePerHour)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Enrollment>()
                .Property(e => e.AmountPaid)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Student>()
                .Property(s => s.Balance)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Teacher>()
                .Property(t => t.Salary)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Teacher>()
                .Property(t => t.HoursWorked)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Student)
                .WithMany() 
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
                                  